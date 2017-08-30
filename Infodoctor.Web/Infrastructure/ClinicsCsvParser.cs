using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infodoctor.Web.Infrastructure
{
    public class ClinicsCsvParser
    {
        private readonly IClinicTypeService _clinicTypeService;
        private readonly ICitiesService _citiesService;

        public ClinicsCsvParser(IClinicTypeService clinicTypeService, ICitiesService citiesService)
        {
            if (clinicTypeService == null) throw new ArgumentNullException(nameof(clinicTypeService));
            if (citiesService == null) throw new ArgumentNullException(nameof(citiesService));
            _clinicTypeService = clinicTypeService;
            _citiesService = citiesService;
        }

        public List<DtoClinicMultiLang> Parse(string path, string pathToSourceImagesFolder, string pathToImagesFolder)
        {
            var csv = ParseFile(path);
            var clinics = ParseToDto(csv, pathToSourceImagesFolder, pathToImagesFolder);
            return clinics.ToList();
        }

        private IEnumerable<DtoClinicMultiLang> ParseToDto(IEnumerable<ClinicCsvModel> csv, string pathToSourceImagesFolder, string pathToImagesFolder)
        {
            var clinics = new List<DtoClinicMultiLang>();

            //локальная функция для проверки имени сайта
            string CheckSiteName(string site)
            {
                return !site.ToLower().Contains("infodoctor.by") || !site.ToLower().Contains("sanatorii.by") ? site : string.Empty;
            }

            foreach (var csvModel in csv)
            {
                var phones = ParseLocalizedDtoPhones(csvModel.Phone);
                if (phones == null)
                    continue; //пропуск хода если парсер не вернул список телефонов

                var typeId = ParseType(csvModel.Type);
                if (typeId == 0)
                    continue;//пропусх хода если не удалось распознать тип учреждения

                var type = _clinicTypeService.GetType(typeId, "ru");
                var city = _citiesService.GetCity(csvModel.City, "ru");
                var images = ParseImages(pathToSourceImagesFolder, pathToImagesFolder, csvModel.Id, csvModel.Name);

                clinics.Add(new DtoClinicMultiLang()
                {
                    Id = csvModel.Id,
                    Email = csvModel.Email,
                    Site = CheckSiteName(csvModel.Site),
                    Type = type.Id,
                    Images = images?.Select(i => i.Name).ToList() ?? new List<string>(),
                    LocalizedClinic = new List<LocalizedDtoClinic>()
                    {
                        new LocalizedDtoClinic()
                        {
                            LangCode = "ru",
                            Name = csvModel.Name,
                            Specializations = csvModel.Specialisations.Length > 1? csvModel.Specialisations.Split('_').ToList(): new List<string>()
                        }
                    },
                    ClinicAddress = new List<DtoAddressMultiLang>()
                    {
                        new DtoAddressMultiLang()
                        {
                            CountryId = city?.CountryId ?? 0,
                            CityId = city?.Id ?? 0,
                            LocalizedAddress = new List<LocalizedDtoAddress>()
                            {
                                new LocalizedDtoAddress()
                                {
                                    City = csvModel.City,
                                    Street = csvModel.Address,
                                    Country = "Беларусь",
                                    LangCode = "ru"
                                }
                            },
                            Phones = new List<DtoPhoneMultiLang>()
                            {
                                new DtoPhoneMultiLang()
                                {
                                    LocalizedPhone = phones
                                }
                            }
                        }
                    }
                });
            }

            return clinics;
        }

        private static IEnumerable<ClinicCsvModel> ParseFile(string path)
        {
            var file = File.ReadAllLines(path, Encoding.GetEncoding("windows-1251"));
            return file.Select(s => s.Split(';'))
                .Select(rows => new ClinicCsvModel()
                {
                    Id = int.Parse(rows[0]),
                    Name = rows[1],
                    City = rows[2],
                    Address = rows[3],
                    Phone = rows[4],
                    Site = rows[5],
                    Email = rows[6],
                    Specialisations = rows[7],
                    Type = rows[8]
                })
                .ToList();
        }

        private static List<LocalizedDtoPhone> ParseLocalizedDtoPhones(string phones)
        {
            //проверка на не правильный формат списка
            // 20 - максимальная возвожная длина номера
            if (string.IsNullOrEmpty(phones))
                return null;
            if (phones.Length > 20 && !phones.Contains('|'))
                return null;
            if (phones[0] == '#' && phones[phones.Length] == '#')
                return null;

            var phonesList = phones.Split('|').ToList();
            var localizedDtoPhones = new List<LocalizedDtoPhone>();

            foreach (var phone in phonesList)
            {
                if (phone.Contains('"'))
                {
                    var firstMark = phone.IndexOf('"');
                    var secondMark = phone.LastIndexOf('"');

                    var desc = phone.Substring(firstMark, secondMark - firstMark);
                    var number = phone;

                    if (!string.IsNullOrEmpty(desc))
                        number = phone.Replace("\"", " ").Replace(desc, " ");

                    localizedDtoPhones.Add(new LocalizedDtoPhone()
                    {
                        LangCode = "ru",
                        Desc = desc,
                        Number = number
                    });
                }
                else
                {
                    localizedDtoPhones.Add(new LocalizedDtoPhone()
                    {
                        LangCode = "ru",
                        Desc = string.Empty,
                        Number = phone
                    });
                }
            }

            return localizedDtoPhones;
        }

        private static int ParseType(string word)
        {
            word = word.ToLower();
            if (word.Contains("стоматологи"))
                return 4;
            if (word.Contains("центр") && word.Contains("медицинский") ||
                word.Contains("центр") && word.Contains("мед"))
                return 1;
            if (word.Contains("поликлиник"))
                return 2;
            if (word.Contains("клиник"))
                return 3;
            return 0;
        }

        private static IEnumerable<DtoImage> ParseImages(string pathToSourceImagesFolder, string pathToImagesFolder, int clinicId, string clinicName)
        {
            var path = string.Empty;
            var dir = new DirectoryInfo(pathToSourceImagesFolder);
            var filesInOrigFolder = dir.GetDirectories().ToList();
            foreach (var directoryInfo in filesInOrigFolder)
                if (directoryInfo.Name.ToLower().Contains(clinicName.ToLower()) || directoryInfo.Name == clinicId.ToString())
                    path = pathToSourceImagesFolder + directoryInfo.Name;

            if (string.IsNullOrEmpty(path))
                return null;

            var types = new List<string>() { "jpg", "png" };
            var imageDir = new DirectoryInfo(path);

            var origFiles = new List<string>();
            var files = new List<string>();
            foreach (var type in types)
            {
                var filesInfolder = imageDir.GetFiles($"*.{type}").ToList();
                foreach (var fileInfo in filesInfolder)
                {
                    origFiles.Add(fileInfo.Name);
                    files.Add($"from-csv-{Guid.NewGuid()}{fileInfo.Extension}");
                }
            }

            for (var i = 0; i < origFiles.Count; i++)
                File.Copy((path + @"\" + origFiles[i]).Replace("/", @"\"), (pathToImagesFolder + files[i]).Replace("/", @"\"), true);

            var images = files.Select(file =>
                new DtoImage()
                {
                    Name = file
                }
            ).ToList();



            return images;
        }

        private class ClinicCsvModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Site { get; set; }
            public string Email { get; set; }
            public string Specialisations { get; set; }
            public string Type { get; set; }
        }
    }
}