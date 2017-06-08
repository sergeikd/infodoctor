using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.Web
{
    public class DbInitializerExtention
    {
        internal void PrepareLists(out List<Language> langs, out List<ClinicReview> clinicReviewList,
            out List<DoctorReview> doctorReviewList, out List<Clinic> clinicList, out List<Doctor> doctors,
            out List<ClinicPhone> phonesList, out List<ClinicAddress> clinicAddressList, out List<City> citiesList,
            out List<DoctorCategory> categoriesList, out List<ImageFile> imagesList)
        {
            imagesList = new List<ImageFile>()
            {
                new ImageFile() {Name = "7cf505b425af4f4ab7293b3a74a3aa3d.jpg"},
                new ImageFile() {Name = "973b8fff8ba24b6c88c24808b97bd13d.jpg"},
                new ImageFile() {Name = "de0c4e3b3d6f479da03f0f337b65c417.jpg"},
                new ImageFile() {Name = "df41387ab46b4241964b5d2663e3d843.jpg"},
                new ImageFile() {Name = "242c4ff4f8454af5bca78b631209902a.jpg"},
            };
            clinicReviewList = new List<ClinicReview>();
            var rnd = new Random();
            var ticks = DateTime.Now.Ticks - 100000000000000;
            for (var i = 0; i < 25; i++)
            {
                clinicReviewList.Add(new ClinicReview
                {
                    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                           "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                           "when an unknown printer took a galley of type and scrambled it to make a type specimen book. " +
                           "It has survived not only five centuries, but also the leap into electronic typesetting, " +
                           "remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                           "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing " +
                           "software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PublishTime = new DateTime(ticks + i * 1000000000000),
                    //ClinicId = i % 5 + 1,
                    UserId = "7d374085-71e4-4819-8d09-91cfc8239463",
                    UserName = "user0",
                    RatePoliteness = rnd.Next(3) + 3,
                    RatePrice = rnd.Next(3) + 3,
                    RateQuality = rnd.Next(3) + 3,
                    IsApproved = true
                });
            }

            langs = new List<Language>()
            {
                new Language() {Name = "Russian", Code = "ru"},
                new Language() {Name = "English", Code = "en"},
                new Language() {Name = "Deutsch", Code = "de"}
            };

            List<ClinicReview> clinicReviewSubList;
            clinicReviewSubList = clinicReviewList.Take(5).ToList();

            var clinic1 = new Clinic()
            {
                Email = "info@nordin.by",
                LocalizedClinics = new List<LocalizedClinic>()
                {
                    new LocalizedClinic()
                    {
                        Name = "Медицинский центр Нордин",
                        Language = langs.First(l => l.Code == "ru")
                    },
                    new LocalizedClinic()
                    {
                        Name = "Nordin Medical Center",
                        Language = langs.First(l => l.Code == "en")
                    }
                },
                RatePrice = clinicReviewSubList.Select(x => x.RatePrice).Average(),
                RateQuality = clinicReviewSubList.Select(x => x.RateQuality).Average(),
                RatePoliteness = clinicReviewSubList.Select(x => x.RatePoliteness).Average(),
                ClinicReviews = clinicReviewSubList,
                Favorite = true,
                ImageName = new List<ImageFile>() { imagesList[0] }
            };
            clinic1.RateAverage = (clinic1.RatePrice + clinic1.RateQuality + clinic1.RatePoliteness) / 3;

            clinicReviewSubList = clinicReviewList.Skip(5).Take(5).ToList();

            var clinic2 = new Clinic()
            {
                Email = string.Empty,
                LocalizedClinics = new List<LocalizedClinic>()
                {
                    new LocalizedClinic()
                    {
                        Name = "Стоматологический центр Дентко",
                        Language = langs.First(l => l.Code == "ru")
                    },
                    new LocalizedClinic()
                    {
                        Name = "Dentko Dental Center",
                        Language = langs.First(l => l.Code == "en")
                    }
                },
                RatePrice = clinicReviewSubList.Select(x => x.RatePrice).Average(),
                RateQuality = clinicReviewSubList.Select(x => x.RateQuality).Average(),
                RatePoliteness = clinicReviewSubList.Select(x => x.RatePoliteness).Average(),
                ClinicReviews = clinicReviewSubList,
                Favorite = false,
                ImageName = new List<ImageFile>() { imagesList[1] }
            };
            clinic2.RateAverage = (clinic2.RatePrice + clinic2.RateQuality + clinic2.RatePoliteness) / 3;

            clinicReviewSubList = clinicReviewList.Skip(10).Take(5).ToList();
            var clinic3 = new Clinic()
            {
                Email = "kravira@kravira.by",
                Site = "https://kravira.by/",
                LocalizedClinics = new List<LocalizedClinic>()
                {
                    new LocalizedClinic()
                    {
                        Name = "Медицинский центр Кравира",
                        Language = langs.First(l => l.Code == "ru")
                    },
                    new LocalizedClinic()
                    {
                        Name = "Kravira Medical Center",
                        Language = langs.First(l => l.Code == "en")
                    }
                },
                RatePrice = clinicReviewSubList.Select(x => x.RatePrice).Average(),
                RateQuality = clinicReviewSubList.Select(x => x.RateQuality).Average(),
                RatePoliteness = clinicReviewSubList.Select(x => x.RatePoliteness).Average(),
                ClinicReviews = clinicReviewSubList,
                Favorite = false,
                ImageName = new List<ImageFile>() { imagesList[2] }
            };
            clinic3.RateAverage = (clinic3.RatePrice + clinic3.RateQuality + clinic3.RatePoliteness) / 3;

            clinicReviewSubList = clinicReviewList.Skip(15).Take(5).ToList();
            var clinic4 = new Clinic()
            {
                Email = "medic4@tut.by",
                Site = "http://www.4gp.by/",
                LocalizedClinics = new List<LocalizedClinic>()
                {
                    new LocalizedClinic()
                    {
                        Name = "4-я городская поликлиника г.Минска",
                        Language = langs.First(l => l.Code == "ru")
                    },
                    new LocalizedClinic()
                    {
                        Name = "The 4th city polyclinic in Minsk",
                        Language = langs.First(l => l.Code == "en")
                    }
                },
                RatePrice = clinicReviewSubList.Select(x => x.RatePrice).Average(),
                RateQuality = clinicReviewSubList.Select(x => x.RateQuality).Average(),
                RatePoliteness = clinicReviewSubList.Select(x => x.RatePoliteness).Average(),
                ClinicReviews = clinicReviewSubList,
                Favorite = false,
                ImageName = new List<ImageFile>() { imagesList[3] }
            };
            clinic4.RateAverage = (clinic4.RatePrice + clinic4.RateQuality + clinic4.RatePoliteness) / 3;

            clinicReviewSubList = clinicReviewList.Skip(20).Take(5).ToList();
            var clinic5 = new Clinic()
            {
                Email = string.Empty,
                LocalizedClinics = new List<LocalizedClinic>()
                {
                    new LocalizedClinic()
                    {
                        Name = "2-я городская детская клиническая больница г. Минска",
                        Language = langs.First(l => l.Code == "ru")
                    },
                    new LocalizedClinic()
                    {
                        Name = "2-nd city children's clinical hospital in Minsk",
                        Language = langs.First(l => l.Code == "en")
                    }
                },
                RatePrice = clinicReviewSubList.Select(x => x.RatePrice).Average(),
                RateQuality = clinicReviewSubList.Select(x => x.RateQuality).Average(),
                RatePoliteness = clinicReviewSubList.Select(x => x.RatePoliteness).Average(),
                ClinicReviews = clinicReviewSubList,
                Favorite = false,
                ImageName = new List<ImageFile>() { imagesList[4] }

            };
            clinic5.RateAverage = (clinic5.RatePrice + clinic5.RateQuality + clinic5.RatePoliteness) / 3;
            clinicList = new List<Clinic> { clinic1, clinic2, clinic3, clinic4, clinic5 };

            doctorReviewList = new List<DoctorReview>();
            for (var i = 0; i < 25; i++)
            {
                doctorReviewList.Add(new DoctorReview
                {
                    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                           "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                           "when an unknown printer took a galley of type and scrambled it to make a type specimen book. " +
                           "It has survived not only five centuries, but also the leap into electronic typesetting, " +
                           "remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                           "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing " +
                           "software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PublishTime = new DateTime(ticks + i * 1000000000000),
                    //DoctorId = i % 5 + 1,
                    UserId = "7d374085-71e4-4819-8d09-91cfc8239463",
                    UserName = "user0",
                    RatePoliteness = rnd.Next(3) + 3,
                    RateProfessionalism = rnd.Next(3) + 3,
                    RateWaitingTime = rnd.Next(3) + 3,
                    IsApproved = true
                });
            }


            #region Список врачей

            List<DoctorReview> doctorReviewSubList;
            doctorReviewSubList = doctorReviewList.Take(5).ToList();
            var doc001 = new Doctor()
            {
                Name = "Степанов Степан Степанович",
                Email = "infosuperstepa1999@gmail.com",
                Experience = 14,
                Manipulation = "Может что-то хорошо.",
                RateProfessionalism = doctorReviewSubList.Select(x => x.RateProfessionalism).Average(),
                RateWaitingTime = doctorReviewSubList.Select(x => x.RateWaitingTime).Average(),
                RatePoliteness = doctorReviewSubList.Select(x => x.RatePoliteness).Average(),
                Favorite = false,
                Clinics = new List<Clinic> { clinic1 },
                DoctorReviews = doctorReviewSubList,
                ImageName = "98344f63b5854a7badd654353f341790.jpg"
            };
            doc001.RateAverage = (doc001.RateProfessionalism + doc001.RateWaitingTime + doc001.RatePoliteness) / 3;

            doctorReviewSubList = doctorReviewList.Skip(5).Take(5).ToList();
            var doc002 = new Doctor()
            {
                Name = "Степанов Иван Степанович",
                Email = "giperivan2@gmail.com",
                Experience = 20,
                Manipulation = "Может что-то отлично.",
                RateProfessionalism = doctorReviewSubList.Select(x => x.RateProfessionalism).Average(),
                RateWaitingTime = doctorReviewSubList.Select(x => x.RateWaitingTime).Average(),
                RatePoliteness = doctorReviewSubList.Select(x => x.RatePoliteness).Average(),
                Favorite = false,
                Clinics = new List<Clinic> { clinic1, clinic2 },
                DoctorReviews = doctorReviewSubList,
                ImageName = "e314bb55fdfe46ceb369ce3da3a6adae.jpg"
            };
            doc002.RateAverage = (doc002.RateProfessionalism + doc002.RateWaitingTime + doc002.RatePoliteness) / 3;

            doctorReviewSubList = doctorReviewList.Skip(10).Take(5).ToList();
            var doc003 = new Doctor()
            {
                Name = "Степанов Степан Иванович",
                Email = "darmaed19@gmail.com",
                Experience = 2,
                Manipulation = "Может что-то нормально.",
                RateProfessionalism = doctorReviewSubList.Select(x => x.RateProfessionalism).Average(),
                RateWaitingTime = doctorReviewSubList.Select(x => x.RateWaitingTime).Average(),
                RatePoliteness = doctorReviewSubList.Select(x => x.RatePoliteness).Average(),
                Favorite = false,
                Clinics = new List<Clinic> { clinic1 },
                DoctorReviews = doctorReviewSubList,
                ImageName = "5161c9cab8a4bee923a30e6a8c1b326.jpg"
            };
            doc003.RateAverage = (doc003.RateProfessionalism + doc003.RateWaitingTime + doc003.RatePoliteness) / 3;

            doctorReviewSubList = doctorReviewList.Skip(15).Take(5).ToList();
            var doc004 = new Doctor()
            {
                Name = "Иванов Степан Степанович",
                Email = "tainiidoctor2@gmail.com",
                Experience = 14,
                Manipulation = "Может что-то хорошо.",
                RateProfessionalism = doctorReviewSubList.Select(x => x.RateProfessionalism).Average(),
                RateWaitingTime = doctorReviewSubList.Select(x => x.RateWaitingTime).Average(),
                RatePoliteness = doctorReviewSubList.Select(x => x.RatePoliteness).Average(),
                Favorite = false,
                Clinics = new List<Clinic> { clinic2 },
                DoctorReviews = doctorReviewSubList,
                ImageName = "0e76cf893e9a4b27bcaeeb3450f02e9c.jpg"
            };
            doc004.RateAverage = (doc004.RateProfessionalism + doc004.RateWaitingTime + doc004.RatePoliteness) / 3;

            doctorReviewSubList = doctorReviewList.Skip(20).Take(5).ToList();
            var doc005 = new Doctor()
            {
                Name = "Степанов Сергей Степанович",
                Email = "123456789@gmail.com",
                Experience = 29,
                Manipulation = "Может что-то отлично.",
                RateProfessionalism = doctorReviewSubList.Select(x => x.RateProfessionalism).Average(),
                RateWaitingTime = doctorReviewSubList.Select(x => x.RateWaitingTime).Average(),
                RatePoliteness = doctorReviewSubList.Select(x => x.RatePoliteness).Average(),
                Favorite = true,
                Clinics = new List<Clinic> { clinic2 },
                DoctorReviews = doctorReviewSubList,
                ImageName = "03206dbe56b04c1682ab90422923867c.jpg"
            };
            doc005.RateAverage = (doc005.RateProfessionalism + doc005.RateWaitingTime + doc005.RatePoliteness) / 3;

            #endregion

            doctors = new List<Doctor> { doc001, doc002, doc003, doc004, doc005 };

            #region Категории врачей

            var cat001 = new DoctorCategory() { Name = "Без категории", Doctors = new List<Doctor> { doc003 } };
            var cat002 = new DoctorCategory() { Name = "Первая категория", Doctors = new List<Doctor> { doc002 } };
            var cat003 = new DoctorCategory() { Name = "Вторая категории", Doctors = new List<Doctor> { doc001 } };
            var cat004 = new DoctorCategory() { Name = "Высшая категории", Doctors = new List<Doctor> { doc005 } };
            var cat005 = new DoctorCategory()
            {
                Name = "Кандидат в доктора медицинских наук",
                Doctors = new List<Doctor> { doc004 }
            };
            var cat006 = new DoctorCategory() { Name = "Доктор медицинских наук" };

            #endregion

            categoriesList = new List<DoctorCategory>() { cat001, cat002, cat003, cat004, cat005, cat006 };

            var phone1 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "159",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone2 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "(017) 296 62 72",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var phone3 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 110 12 12",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone4 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 211 28 61",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone5 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 611 28 61",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone6 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 314 94 94",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone7 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 664 44 44",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone8 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 253 33 33",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone9 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 103 43 43",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone10 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "взрослые",
                        Number = "+375 17 369 64 59",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone11 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "взрослые",
                        Number = "+375 17 369 69 16",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone12 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "дети",
                        Number = "+375 17 369 64 57",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone13 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "дети",
                        Number = "+375 17 369 65 56",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone14 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "стоматология",
                        Number = "+375 17 369 67 65",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone15 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "платные услуги",
                        Number = "+375 17 369 52 04",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone16 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = "платные услуги",
                        Number = "+375 44 580 90 33",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone17 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 290 81 11",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone18 = new ClinicPhone()
            {
                LocalizedPhones = new List<LocalizedClinicPhone>()
                {
                    new LocalizedClinicPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 44 575 08 89",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };


            phonesList = new List<ClinicPhone>
            {
                phone1,
                phone2,
                phone3,
                phone4,
                phone5,
                phone6,
                phone7,
                phone8,
                phone9,
                phone10,
                phone11,
                phone12,
                phone13,
                phone14,
                phone15,
                phone16,
                phone17,
                phone18
            };

            var ca1 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "ул.Сурганова 47Б",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic1,
                Doctors = new List<Doctor> { doc001, doc002, doc003 },
                Phones = new List<ClinicPhone>() { phone1, phone2 }
            };
            var ca2 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "пр-т. Независимости 58",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic2,
                Doctors = new List<Doctor> { doc004, doc002, doc005 },
                Phones = new List<ClinicPhone>() { phone3 }
            };
            var ca3 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "пр-т. Победителей 75,",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic3,
                Phones = new List<ClinicPhone>() { phone4, phone5 }
            };
            var ca4 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "ул.Скрипникова 11Б,",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic3,
                Phones = new List<ClinicPhone>() { phone6, phone7 }
            };
            var ca5 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "ул.Захарова 50Д",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic3,
                Phones = new List<ClinicPhone>() { phone8, phone9 }
            };
            var ca6 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "ул.Победителей 93",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic4,
                Phones = new List<ClinicPhone>() { phone10, phone11, phone12, phone13, phone14, phone15, phone16 }
            };
            var ca7 = new ClinicAddress()
            {
                LocalizedAddresses = new List<LocalizedClinicAddress>()
                {
                    new LocalizedClinicAddress()
                    {
                        Country = "Беларусь",
                        Street = "ул. Нарочанская 17",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic5,
                Phones = new List<ClinicPhone>() { phone17, phone18 }
            };

            //var ca1 = new ClinicAddress() { Country = "Беларусь", Street = "ул.Сурганова 47Б", Clinic = clinic1, Doctors = new List<Doctor> { doc001, doc002, doc003 }, Phones = new List<ClinicPhone>() { phone1, phone2 } };
            //var ca2 = new ClinicAddress() { Country = "Беларусь", Street = "пр-т. Независимости 58", Clinic = clinic2, Doctors = new List<Doctor> { doc004, doc002, doc005 }, Phones = new List<ClinicPhone>() { phone3 } };
            //var ca3 = new ClinicAddress() { Country = "Беларусь", Street = "пр-т. Победителей 75,", Clinic = clinic3, Phones = new List<ClinicPhone>() { phone4, phone5 } };
            //var ca4 = new ClinicAddress() { Country = "Беларусь", Street = "ул.Скрипникова 11Б,", Clinic = clinic3, Phones = new List<ClinicPhone>() { phone6, phone7 } };
            //var ca5 = new ClinicAddress() { Country = "Беларусь", Street = "ул.Захарова 50Д", Clinic = clinic3, Phones = new List<ClinicPhone>() { phone8, phone9 } };
            //var ca6 = new ClinicAddress() { Country = "Беларусь", Street = "ул.Победителей 93", Clinic = clinic4, Phones = new List<ClinicPhone>() { phone10, phone11, phone12, phone13, phone14, phone15, phone16 } };
            //var ca7 = new ClinicAddress() { Country = "Беларусь", Street = "ул. Нарочанская 17", Clinic = clinic5, Phones = new List<ClinicPhone>() { phone17, phone18 } };

            clinicAddressList = new List<ClinicAddress> { ca1, ca2, ca3, ca4, ca5, ca6, ca7 };

            #region Очень длинный список городов РБ

            //Adresses = new List<ClinicAddress>() {ca1, ca2, ca3, ca4, ca5, ca6, ca7} минск

            var city1 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Бобруйск", Language = langs.First(l => l.Code == "ru") } } };
            var city2 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Барановичи", Language = langs.First(l => l.Code == "ru") } } };
            var city3 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Борисов", Language = langs.First(l => l.Code == "ru") } } };
            var city4 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Барань", Language = langs.First(l => l.Code == "ru") } } };
            var city5 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Белоозерск", Language = langs.First(l => l.Code == "ru") } } };
            var city6 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Береза", Language = langs.First(l => l.Code == "ru") } } };
            var city7 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Березино", Language = langs.First(l => l.Code == "ru") } } };
            var city8 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Березовка", Language = langs.First(l => l.Code == "ru") } } };
            var city9 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Браслав", Language = langs.First(l => l.Code == "ru") } } };
            var city10 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Брест", Language = langs.First(l => l.Code == "ru") } } };
            var city11 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Буда-Кошелево", Language = langs.First(l => l.Code == "ru") } } };
            var city12 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Быхов", Language = langs.First(l => l.Code == "ru") } } };
            var city13 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Василевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city14 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Верхнедвинск", Language = langs.First(l => l.Code == "ru") } } };
            var city15 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ветка", Language = langs.First(l => l.Code == "ru") } } };
            var city16 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Вилейка", Language = langs.First(l => l.Code == "ru") } } };
            var city17 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Витебск", Language = langs.First(l => l.Code == "ru") } } };
            var city18 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Волковыск", Language = langs.First(l => l.Code == "ru") } } };
            var city19 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Воложин", Language = langs.First(l => l.Code == "ru") } } };
            var city20 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Высокое", Language = langs.First(l => l.Code == "ru") } } };
            var city21 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ганцевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city22 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Глубокое", Language = langs.First(l => l.Code == "ru") } } };
            var city23 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Гомель", Language = langs.First(l => l.Code == "ru") } } };
            var city24 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Горки", Language = langs.First(l => l.Code == "ru") } } };
            var city25 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Городок", Language = langs.First(l => l.Code == "ru") } } };
            var city26 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Гродно", Language = langs.First(l => l.Code == "ru") } } };
            var city27 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Давид-Городок", Language = langs.First(l => l.Code == "ru") } } };
            var city28 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Дзержинск", Language = langs.First(l => l.Code == "ru") } } };
            var city29 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Дисна", Language = langs.First(l => l.Code == "ru") } } };
            var city30 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Добруш", Language = langs.First(l => l.Code == "ru") } } };
            var city31 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Докшицы", Language = langs.First(l => l.Code == "ru") } } };
            var city32 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Дрогичин", Language = langs.First(l => l.Code == "ru") } } };
            var city33 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Дубровно", Language = langs.First(l => l.Code == "ru") } } };
            var city34 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Дятлово", Language = langs.First(l => l.Code == "ru") } } };
            var city35 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ельск", Language = langs.First(l => l.Code == "ru") } } };
            var city36 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Жодино", Language = langs.First(l => l.Code == "ru") } } };
            var city37 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Жабинка", Language = langs.First(l => l.Code == "ru") } } };
            var city38 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Житковичи", Language = langs.First(l => l.Code == "ru") } } };
            var city39 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Жлобин", Language = langs.First(l => l.Code == "ru") } } };
            var city40 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Заславль", Language = langs.First(l => l.Code == "ru") } } };
            var city41 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Иваново", Language = langs.First(l => l.Code == "ru") } } };
            var city42 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ивацевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city43 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ивье", Language = langs.First(l => l.Code == "ru") } } };
            var city44 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Калинковичи", Language = langs.First(l => l.Code == "ru") } } };
            var city45 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Каменец", Language = langs.First(l => l.Code == "ru") } } };
            var city46 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Кировск", Language = langs.First(l => l.Code == "ru") } } };
            var city47 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Клецк", Language = langs.First(l => l.Code == "ru") } } };
            var city48 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Климовичи", Language = langs.First(l => l.Code == "ru") } } };
            var city49 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Кличев", Language = langs.First(l => l.Code == "ru") } } };
            var city50 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Кобрин", Language = langs.First(l => l.Code == "ru") } } };
            var city51 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Копыль", Language = langs.First(l => l.Code == "ru") } } };
            var city52 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Коссово", Language = langs.First(l => l.Code == "ru") } } };
            var city53 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Костюковичи", Language = langs.First(l => l.Code == "ru") } } };
            var city54 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Кричев", Language = langs.First(l => l.Code == "ru") } } };
            var city55 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Крупки", Language = langs.First(l => l.Code == "ru") } } };
            var city56 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Лепель", Language = langs.First(l => l.Code == "ru") } } };
            var city57 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Лида", Language = langs.First(l => l.Code == "ru") } } };
            var city58 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Логойск", Language = langs.First(l => l.Code == "ru") } } };
            var city59 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Лунинец", Language = langs.First(l => l.Code == "ru") } } };
            var city60 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Любань", Language = langs.First(l => l.Code == "ru") } } };
            var city61 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ляховичи", Language = langs.First(l => l.Code == "ru") } } };
            var city62 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Мозырь", Language = langs.First(l => l.Code == "ru") } } };
            var city63 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Малорита", Language = langs.First(l => l.Code == "ru") } } };
            var city64 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Марьина Горка", Language = langs.First(l => l.Code == "ru") } } };
            var city65 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Микашевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city66 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Минск", Language = langs.First(l => l.Code == "ru") } }, Adresses = new List<ClinicAddress>() { ca1, ca2, ca3, ca4, ca5, ca6, ca7 } };
            var city67 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Миоры", Language = langs.First(l => l.Code == "ru") } } };
            var city68 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Могилев", Language = langs.First(l => l.Code == "ru") } } };
            var city69 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Молодечно", Language = langs.First(l => l.Code == "ru") } } };
            var city70 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Мосты", Language = langs.First(l => l.Code == "ru") } } };
            var city71 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Мстиславль", Language = langs.First(l => l.Code == "ru") } } };
            var city72 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Мядель", Language = langs.First(l => l.Code == "ru") } } };
            var city73 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Новополоцк", Language = langs.First(l => l.Code == "ru") } } };
            var city74 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Наровля", Language = langs.First(l => l.Code == "ru") } } };
            var city75 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Несвиж", Language = langs.First(l => l.Code == "ru") } } };
            var city76 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Новогрудок", Language = langs.First(l => l.Code == "ru") } } };
            var city77 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Новолукомль", Language = langs.First(l => l.Code == "ru") } } };
            var city78 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Орша", Language = langs.First(l => l.Code == "ru") } } };
            var city79 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Осиповичи", Language = langs.First(l => l.Code == "ru") } } };
            var city80 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Ошмяны", Language = langs.First(l => l.Code == "ru") } } };
            var city81 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Пинск", Language = langs.First(l => l.Code == "ru") } } };
            var city82 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Полоцк", Language = langs.First(l => l.Code == "ru") } } };
            var city83 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Петриков", Language = langs.First(l => l.Code == "ru") } } };
            var city84 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Поставы", Language = langs.First(l => l.Code == "ru") } } };
            var city85 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Пружаны", Language = langs.First(l => l.Code == "ru") } } };
            var city86 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Речица", Language = langs.First(l => l.Code == "ru") } } };
            var city87 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Рогачев", Language = langs.First(l => l.Code == "ru") } } };
            var city88 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Светлогорск", Language = langs.First(l => l.Code == "ru") } } };
            var city89 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Свислочь", Language = langs.First(l => l.Code == "ru") } } };
            var city90 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Сенно", Language = langs.First(l => l.Code == "ru") } } };
            var city91 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Скидель", Language = langs.First(l => l.Code == "ru") } } };
            var city92 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Славгород", Language = langs.First(l => l.Code == "ru") } } };
            var city93 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Слоним", Language = langs.First(l => l.Code == "ru") } } };
            var city94 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Слуцк", Language = langs.First(l => l.Code == "ru") } } };
            var city95 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Смолевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city96 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Сморгонь", Language = langs.First(l => l.Code == "ru") } } };
            var city97 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Солигорск", Language = langs.First(l => l.Code == "ru") } } };
            var city98 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Старые Дороги", Language = langs.First(l => l.Code == "ru") } } };
            var city99 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Столбцы", Language = langs.First(l => l.Code == "ru") } } };
            var city100 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Столин", Language = langs.First(l => l.Code == "ru") } } };
            var city101 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Толочин", Language = langs.First(l => l.Code == "ru") } } };
            var city102 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Туров", Language = langs.First(l => l.Code == "ru") } } };
            var city103 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Узда", Language = langs.First(l => l.Code == "ru") } } };
            var city104 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Фаниполь", Language = langs.First(l => l.Code == "ru") } } };
            var city105 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Хойники", Language = langs.First(l => l.Code == "ru") } } };
            var city106 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Чаусы", Language = langs.First(l => l.Code == "ru") } } };
            var city107 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Чашники", Language = langs.First(l => l.Code == "ru") } } };
            var city108 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Червень", Language = langs.First(l => l.Code == "ru") } } };
            var city109 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Чериков", Language = langs.First(l => l.Code == "ru") } } };
            var city110 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Чечерск", Language = langs.First(l => l.Code == "ru") } } };
            var city111 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Шклов", Language = langs.First(l => l.Code == "ru") } } };
            var city112 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Щучин", Language = langs.First(l => l.Code == "ru") } } };
            var city113 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Брестская область", Language = langs.First(l => l.Code == "ru") } } };
            var city114 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Витебская область", Language = langs.First(l => l.Code == "ru") } } };
            var city115 = new City() { LocalisedCities = new List<LocalisedCity>() { new LocalisedCity() { Name = "Минская область", Language = langs.First(l => l.Code == "ru") } } };


            #endregion

            citiesList = new List<City>
            {
                city1,
                city2,
                city3,
                city4,
                city5,
                city6,
                city7,
                city8,
                city9,
                city10,
                city11,
                city12,
                city13,
                city14,
                city15,
                city16,
                city17,
                city18,
                city19,
                city20,
                city21,
                city22,
                city23,
                city24,
                city25,
                city26,
                city27,
                city28,
                city29,
                city30,
                city31,
                city32,
                city33,
                city34,
                city35,
                city36,
                city37,
                city38,
                city39,
                city40,
                city41,
                city42,
                city43,
                city44,
                city45,
                city46,
                city47,
                city48,
                city49,
                city50,
                city51,
                city52,
                city53,
                city54,
                city55,
                city56,
                city57,
                city58,
                city59,
                city60,
                city61,
                city62,
                city63,
                city64,
                city65,
                city66,
                city67,
                city68,
                city69,
                city70,
                city71,
                city72,
                city73,
                city74,
                city75,
                city76,
                city77,
                city78,
                city79,
                city80,
                city81,
                city82,
                city83,
                city84,
                city85,
                city86,
                city87,
                city88,
                city89,
                city90,
                city91,
                city92,
                city93,
                city94,
                city95,
                city96,
                city97,
                city98,
                city99,
                city100,
                city101,
                city102,
                city103,
                city104,
                city105,
                city106,
                city107,
                city108,
                city109,
                city110,
                city111,
                city112,
                city113,
                city114,
                city115
            };
        }
    }
}
