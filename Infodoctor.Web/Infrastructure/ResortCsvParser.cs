using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;

namespace Infodoctor.Web.Infrastructure
{
    public class ResortsCsvParser
    {
        private readonly IResortTypeService _resortTypeService;
        private readonly ICitiesService _citiesService;

        public ResortsCsvParser(IResortTypeService resortTypeService, ICitiesService citiesService)
        {
            if (resortTypeService == null) throw new ArgumentNullException(nameof(resortTypeService));
            if (citiesService == null) throw new ArgumentNullException(nameof(citiesService));
            _resortTypeService = resortTypeService;
            _citiesService = citiesService;
        }

        public List<DtoResortMultiLang> Parse(string path, string pathToSourceImagesFolder,
            string pathToImagesFolder)
        {
            var csv = ParseFile(path);
            var resorts = ParseToDto(csv, pathToSourceImagesFolder, pathToImagesFolder);

            return resorts.ToList();
        }

        private IEnumerable<DtoResortMultiLang> ParseToDto(IEnumerable<ResortCsvModel> csv, string pathToSourceImagesFolder, string pathToImagesFolder)
        {
            var clinics = new List<DtoResortMultiLang>();

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

                var typeId = ParseType(csvModel.Name);
                if (typeId == 0)
                    continue;//пропусх хода если не удалось распознать тип учреждения

                var type = _resortTypeService.GetType(typeId, "ru");
                var city = _citiesService.GetCity(csvModel.City, "ru");
                var images = ParseImages(pathToSourceImagesFolder, pathToImagesFolder, csvModel.Id);
                var localizedAddress = ParseLocalizedDtoAddress(csvModel.Address);

                clinics.Add(new DtoResortMultiLang()
                {
                    Id = csvModel.Id,
                    Email = csvModel.Email,
                    Site = CheckSiteName(csvModel.Site),
                    Type = type.Id,
                    //Image = images?.Select(i => i.Name).ToList() ?? new List<string>(),
                    LocalizedResort = new List<LocalizedDtoResort>()
                    {
                        new LocalizedDtoResort()
                        {
                            LangCode = "ru",
                            Name = csvModel.Name,
                            Manipulations = csvModel.Specialisations.Length > 1? csvModel.Specialisations.Split('_').ToList(): new List<string>(),
                        }
                    },
                    Address = new DtoAddressMultiLang()
                    {
                        CountryId = city?.CountryId ?? 0,
                        CityId = city?.Id ?? 0,
                        LocalizedAddress = new List<LocalizedDtoAddress>()
                        {
                            new LocalizedDtoAddress()
                            {
                                Country = localizedAddress.Country,
                                City = localizedAddress.City,
                                Street = localizedAddress.Street,
                                LangCode = localizedAddress.LangCode
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
                });
            }

            return clinics;
        }

        private static LocalizedDtoAddress ParseLocalizedDtoAddress(string address)
        {
            //map - страна,область,город,адресс
            if (string.IsNullOrEmpty(address))
                return null;
            if (!address.Contains(','))
                return null;

            var splitValue = address.Split(',');

            if (splitValue.Length != 4)
                return null;

            var dtoAddress = new LocalizedDtoAddress()
            {
                Country = "Беларусь",
                LangCode = "ru",
                City = splitValue[3],
                Street = splitValue[4]
            };

            return null;
        }

        private static List<LocalizedDtoPhone> ParseLocalizedDtoPhones(string phones)
        {
            //проверка на не правильный формат списка
            // 20 - максимальная возвожная длина номера
            if (string.IsNullOrEmpty(phones))
                return null;
            if (phones.Length > 20 && !phones.Contains(';'))
                return null;
            if (phones[0] == '#' && phones[phones.Length] == '#')
                return null;

            var phonesList = phones.Split(';').ToList();
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

        private static int ParseType(string name)
        {
            name = name.ToLower();

            if (name.Contains("санатории"))
                return 1;

            if (name.Contains("баз") && name.Contains("отдых"))
                return 2;

            if (name.Contains("профилакторий"))
                return 3;

            if (name.Contains("центр") && name.Contains("реабилитаци"))
                return 4;

            return 0;
        }

        private static IEnumerable<DtoImage> ParseImages(string pathToSourceImagesFolder, string pathToImagesFolder, int clinicId)
        {
            var path = pathToSourceImagesFolder + clinicId;
            if (!Directory.Exists(path))
                throw new ApplicationException($"Directory {path} not found");

            var types = new List<string>() { "jpg", "png" };
            var dir = new DirectoryInfo(path);

            var origFiles = new List<string>();
            var files = new List<string>();
            foreach (var type in types)
            {
                var filesInfolder = dir.GetFiles($"*.{type}").ToList();
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

        private static IEnumerable<ResortCsvModel> ParseFile(string path)
        {
            var file = File.ReadAllLines(path, Encoding.GetEncoding("windows-1251"));
            return file.Select(s => s.Split(';'))
                .Select(rows => new ResortCsvModel()
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

        private class ResortCsvModel
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