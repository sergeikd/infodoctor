using Infodoctor.BL.DtoModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infodoctor.Web.Infrastructure
{
    public class ClinicsCsvParser
    {
        public IEnumerable<DtoClinicMultiLang> Parse(string path)
        {
            var csv = ParseFile(path);
            var clinics = ParseToDto(csv);
            return clinics;
        }

        private IEnumerable<DtoClinicMultiLang> ParseToDto(IEnumerable<ClinicCsvModel> csv)
        {
            var clinics = new List<DtoClinicMultiLang>();

            foreach (var csvModel in csv)
            {
                var phones = ParseLocalizedDtoPhones(csvModel.Phone);
                if (phones == null)
                    continue; //пропуск хода если парсер не вернул список телефонов

                var type = ParseType(csvModel.Name);
                if (string.IsNullOrEmpty(type))
                    continue;//пропусх хода если не удалось распознать тип учреждения

                clinics.Add(new DtoClinicMultiLang()
                {
                    Id = csvModel.Id,
                    Email = csvModel.Email,
                    Site = csvModel.Site,
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
                    Specialisations = rows[7]
                })
                .ToList();
        }

        private List<LocalizedDtoPhone> ParseLocalizedDtoPhones(string phones)
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

        private static string ParseType(string name)
        {
            name = name.ToLower();
            var types = new List<string>() { "медицинский центр", "клиник", "поликлиник", "стоматологи" };

            foreach (var type in types)
                if (name.Contains(type))
                    return type;
            return string.Empty;
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
        }
    }
}