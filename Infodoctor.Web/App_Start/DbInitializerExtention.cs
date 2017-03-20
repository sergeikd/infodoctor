using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.Web
{
    public class DbInitializerExtention
    {
        internal void PrepareLists(out List<ClinicReview> clinicReviewList, out List<DoctorReview> doctorsReviewList, out List<Clinic> clinicList, out List<Doctor> doctors,
            out List<ClinicPhone> phonesList, out List<CityAddress> clinicAddressList, out List<City> citiesList, out List<DoctorCategory> categoriesList)
        {
            clinicReviewList = new List<ClinicReview>();
            var rnd = new Random();
            var ticks = DateTime.Now.Ticks - 100000000000000;
            for (var i = 0; i < 30; i++)
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
                    ClinicId = i % 5 + 1,
                    UserId = "7d374085-71e4-4819-8d09-91cfc8239463",
                    UserName = "user0",
                    RatePoliteness = rnd.Next(3) + 3,
                    RatePrice = rnd.Next(3) + 3,
                    RateQuality = rnd.Next(3) + 3
                });
            }

            doctorsReviewList = new List<DoctorReview>();
            for (var i = 0; i < 30; i++)
            {
                doctorsReviewList.Add(new DoctorReview
                {
                    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                           "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                           "when an unknown printer took a galley of type and scrambled it to make a type specimen book. " +
                           "It has survived not only five centuries, but also the leap into electronic typesetting, " +
                           "remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                           "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing " +
                           "software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PublishTime = new DateTime(ticks + i * 1000000000000),
                    DoctorId = i % 5 + 1,
                    UserId = "7d374085-71e4-4819-8d09-91cfc8239463",
                    UserName = "user0",
                    RatePoliteness = rnd.Next(3) + 3,
                    RateProfessionalism = rnd.Next(3) + 3,
                    RateWaitingTime = rnd.Next(3) + 3
                });
            }
            var clinic1 = new Clinic()
            {
                Email = "info@nordin.by",
                Name = "Медицинский центр Нордин",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 1).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 1).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 1).Average(y => y.RatePoliteness),
                Favorite = true,
                ImageName = "7cf505b425af4f4ab7293b3a74a3aa3d.jpg"
            };
            clinic1.RateAverage = (clinic1.RatePrice + clinic1.RateQuality + clinic1.RatePoliteness) / 3;
            var clinic2 = new Clinic()
            {
                Email = string.Empty,
                Name = "Стоматологический центр Дентко",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 2).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 2).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 2).Average(y => y.RatePoliteness),
                Favorite = false,
                ImageName = "973b8fff8ba24b6c88c24808b97bd13d.jpg"
            };
            clinic2.RateAverage = (clinic2.RatePrice + clinic2.RateQuality + clinic2.RatePoliteness) / 3;
            var clinic3 = new Clinic()
            {
                Email = "kravira@kravira.by",
                Name = "Медицинский центр Кравира",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 3).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 3).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 3).Average(y => y.RatePoliteness),
                Favorite = false,
                ImageName = "de0c4e3b3d6f479da03f0f337b65c417.jpg"
            };
            clinic3.RateAverage = (clinic3.RatePrice + clinic3.RateQuality + clinic3.RatePoliteness) / 3;
            var clinic4 = new Clinic()
            {
                Email = "medic4@tut.by",
                Name = "4-я городская поликлиника г.Минска",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 4).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 4).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 4).Average(y => y.RatePoliteness),
                Favorite = false,
                ImageName = "df41387ab46b4241964b5d2663e3d843.jpg"
            };
            clinic4.RateAverage = (clinic4.RatePrice + clinic4.RateQuality + clinic4.RatePoliteness) / 3;
            var clinic5 = new Clinic()
            {
                Email = string.Empty,
                Name = "2-я городская детская клиническая больница» г. Минска",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 5).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 5).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 5).Average(y => y.RatePoliteness),
                Favorite = false,
                ImageName = "242c4ff4f8454af5bca78b631209902a.jpg"
            };
            clinic5.RateAverage = (clinic5.RatePrice + clinic5.RateQuality + clinic5.RatePoliteness) / 3;
            clinicList = new List<Clinic> { clinic1, clinic2, clinic3, clinic4, clinic5 };

            #region Список врачей

            var doc001 = new Doctor()
            {
                Name = "Степанов Степан Степанович",
                Email = "infosuperstepa1999@gmail.com",
                Experience = 14,
                Manipulation = "Может что-то хорошо.",
                RateProfessionalism = doctorsReviewList.Where(x => x.DoctorId == 1).Average(y => y.RateProfessionalism),
                RateWaitingTime = doctorsReviewList.Where(x => x.DoctorId == 1).Average(y => y.RateWaitingTime),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 1).Average(y => y.RatePoliteness),
                Favorite = false,
                Clinics = new List<Clinic> { clinic1 },
                ImageName = "98344f63b5854a7badd654353f341790.jpg"
            };
            doc001.RateAverage = (doc001.RateProfessionalism + doc001.RateWaitingTime + doc001.RatePoliteness) / 3;
            var doc002 = new Doctor()
            {
                Name = "Степанов Иван Степанович",
                Email = "giperivan2@gmail.com",
                Experience = 20,
                Manipulation = "Может что-то отлично.",
                RateProfessionalism = doctorsReviewList.Where(x => x.DoctorId == 2).Average(y => y.RateProfessionalism),
                RateWaitingTime = doctorsReviewList.Where(x => x.DoctorId == 2).Average(y => y.RateWaitingTime),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 2).Average(y => y.RatePoliteness),
                Favorite = false,
                Clinics = new List<Clinic> { clinic1, clinic2 },
                ImageName = "e314bb55fdfe46ceb369ce3da3a6adae.jpg"
            };
            doc002.RateAverage = (doc002.RateProfessionalism + doc002.RateWaitingTime + doc002.RatePoliteness) / 3;
            var doc003 = new Doctor()
            {
                Name = "Степанов Степан Иванович",
                Email = "darmaed19@gmail.com",
                Experience = 2,
                Manipulation = "Может что-то нормально.",
                RateProfessionalism = doctorsReviewList.Where(x => x.DoctorId == 3).Average(y => y.RateProfessionalism),
                RateWaitingTime = doctorsReviewList.Where(x => x.DoctorId == 3).Average(y => y.RateWaitingTime),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 3).Average(y => y.RatePoliteness),
                Favorite = false,
                Clinics = new List<Clinic> { clinic1 },
                ImageName = "5161c9cab8a4bee923a30e6a8c1b326.jpg"
            };
            doc003.RateAverage = (doc003.RateProfessionalism + doc003.RateWaitingTime + doc003.RatePoliteness) / 3;
            var doc004 = new Doctor()
            {
                Name = "Иванов Степан Степанович",
                Email = "tainiidoctor2@gmail.com",
                Experience = 14,
                Manipulation = "Может что-то хорошо.",
                RateProfessionalism = doctorsReviewList.Where(x => x.DoctorId == 4).Average(y => y.RateProfessionalism),
                RateWaitingTime = doctorsReviewList.Where(x => x.DoctorId == 4).Average(y => y.RateWaitingTime),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 4).Average(y => y.RatePoliteness),
                Favorite = false,
                Clinics = new List<Clinic> { clinic2 },
                ImageName = "0e76cf893e9a4b27bcaeeb3450f02e9c.jpg"
            };
            doc004.RateAverage = (doc004.RateProfessionalism + doc004.RateWaitingTime + doc004.RatePoliteness) / 3;
            var doc005 = new Doctor()
            {
                Name = "Степанов Сергей Степанович",
                Email = "123456789@gmail.com",
                Experience = 29,
                Manipulation = "Может что-то отлично.",
                RateProfessionalism = doctorsReviewList.Where(x => x.DoctorId == 5).Average(y => y.RateProfessionalism),
                RateWaitingTime = doctorsReviewList.Where(x => x.DoctorId == 5).Average(y => y.RateWaitingTime),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 5).Average(y => y.RatePoliteness),
                Favorite = true,
                Clinics = new List<Clinic> { clinic2 },
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
            var cat005 = new DoctorCategory() { Name = "Кандидат в доктора медицинских наук", Doctors = new List<Doctor> { doc004 } };
            var cat006 = new DoctorCategory() { Name = "Доктор медицинских наук" };
            #endregion
            categoriesList = new List<DoctorCategory>(){cat001,cat002,cat003,cat004,cat005,cat006};

            var phone1 = new ClinicPhone() { Description = string.Empty, Number = "159" };
            var phone2 = new ClinicPhone() { Description = string.Empty, Number = "(017) 296 62 72" };
            var phone3 = new ClinicPhone() { Description = string.Empty, Number = "+375 29 110 12 12" };
            var phone4 = new ClinicPhone() { Description = string.Empty, Number = "+375 17 211 28 61" };
            var phone5 = new ClinicPhone() { Description = string.Empty, Number = "+375 29 611 28 61" };
            var phone6 = new ClinicPhone() { Description = string.Empty, Number = "+375 17 314 94 94" };
            var phone7 = new ClinicPhone() { Description = string.Empty, Number = "+375 29 664 44 44" };
            var phone8 = new ClinicPhone() { Description = string.Empty, Number = "+375 17 253 33 33" };
            var phone9 = new ClinicPhone() { Description = string.Empty, Number = "+375 29 103 43 43" };
            var phone10 = new ClinicPhone() { Description = "взрослые", Number = "+375 17 369 64 59" };
            var phone11 = new ClinicPhone() { Description = "взрослые", Number = "+375 17 369 69 16" };
            var phone12 = new ClinicPhone() { Description = "дети", Number = "+375 17 369 64 57" };
            var phone13 = new ClinicPhone() { Description = "дети", Number = "+375 17 369 65 56" };
            var phone14 = new ClinicPhone() { Description = "стоматология", Number = "+375 17 369 67 65" };
            var phone15 = new ClinicPhone() { Description = "платные услуги", Number = "+375 17 369 52 04" };
            var phone16 = new ClinicPhone() { Description = "платные услуги", Number = "+375 44 580 90 33" };
            var phone17 = new ClinicPhone() { Description = string.Empty, Number = "+375 17 290 81 11" };
            var phone18 = new ClinicPhone() { Description = string.Empty, Number = "+375 44 575 08 89" };

            phonesList = new List<ClinicPhone>{
                phone1, phone2, phone3, phone4, phone5, phone6, phone7, phone8, phone9,
                phone10, phone11, phone12, phone13, phone14, phone15, phone16, phone17, phone18};

            var ca1 = new CityAddress() { Street = "ул.Сурганова 47Б", Clinic = clinic1, Doctors = new List<Doctor> { doc001, doc002, doc003 }, ClinicPhones = new List<ClinicPhone>() { phone1, phone2 } };
            var ca2 = new CityAddress() { Street = "пр-т. Независимости 58", Clinic = clinic2, Doctors = new List<Doctor> { doc004, doc002, doc005 }, ClinicPhones = new List<ClinicPhone>() { phone3 } };
            var ca3 = new CityAddress() { Street = "пр-т. Победителей 75,", Clinic = clinic3, ClinicPhones = new List<ClinicPhone>() { phone4, phone5 } };
            var ca4 = new CityAddress() { Street = "ул.Скрипникова 11Б,", Clinic = clinic3, ClinicPhones = new List<ClinicPhone>() { phone6, phone7 } };
            var ca5 = new CityAddress() { Street = "ул.Захарова 50Д", Clinic = clinic3, ClinicPhones = new List<ClinicPhone>() { phone8, phone9 } };
            var ca6 = new CityAddress() { Street = "ул.Победителей 93", Clinic = clinic4, ClinicPhones = new List<ClinicPhone>() { phone10, phone11, phone12, phone13, phone14, phone15, phone16 } };
            var ca7 = new CityAddress() { Street = "ул. Нарочанская 17", Clinic = clinic5, ClinicPhones = new List<ClinicPhone>() { phone17, phone18 } };

            clinicAddressList = new List<CityAddress> { ca1, ca2, ca3, ca4, ca5, ca6, ca7 };


            #region Очень длинный список городов РБ
            var city1 = new City() { Name = "Бобруйск" }; ;
            var city2 = new City() { Name = "Барановичи" };
            var city3 = new City() { Name = "Борисов" };
            var city4 = new City() { Name = "Барань" };
            var city5 = new City() { Name = "Белоозерск" };
            var city6 = new City() { Name = "Береза" };
            var city7 = new City() { Name = "Березино" };
            var city8 = new City() { Name = "Березовка" };
            var city9 = new City() { Name = "Браслав" };
            var city10 = new City() { Name = "Брест" };
            var city11 = new City() { Name = "Буда-Кошелево" };
            var city12 = new City() { Name = "Быхов" };
            var city13 = new City() { Name = "Василевичи" };
            var city14 = new City() { Name = "Верхнедвинск" };
            var city15 = new City() { Name = "Ветка" };
            var city16 = new City() { Name = "Вилейка" };
            var city17 = new City() { Name = "Витебск" };
            var city18 = new City() { Name = "Волковыск" };
            var city19 = new City() { Name = "Воложин" };
            var city20 = new City() { Name = "Высокое" };
            var city21 = new City() { Name = "Ганцевичи" };
            var city22 = new City() { Name = "Глубокое" };
            var city23 = new City() { Name = "Гомель" };
            var city24 = new City() { Name = "Горки" };
            var city25 = new City() { Name = "Городок" };
            var city26 = new City() { Name = "Гродно" };
            var city27 = new City() { Name = "Давид-Городок" };
            var city28 = new City() { Name = "Дзержинск" };
            var city29 = new City() { Name = "Дисна" };
            var city30 = new City() { Name = "Добруш" };
            var city31 = new City() { Name = "Докшицы" };
            var city32 = new City() { Name = "Дрогичин" };
            var city33 = new City() { Name = "Дубровно" };
            var city34 = new City() { Name = "Дятлово" };
            var city35 = new City() { Name = "Ельск" };
            var city36 = new City() { Name = "Жодино" };
            var city37 = new City() { Name = "Жабинка" };
            var city38 = new City() { Name = "Житковичи" };
            var city39 = new City() { Name = "Жлобин" };
            var city40 = new City() { Name = "Заславль" };
            var city41 = new City() { Name = "Иваново" };
            var city42 = new City() { Name = "Ивацевичи" };
            var city43 = new City() { Name = "Ивье" };
            var city44 = new City() { Name = "Калинковичи" };
            var city45 = new City() { Name = "Каменец" };
            var city46 = new City() { Name = "Кировск" };
            var city47 = new City() { Name = "Клецк" };
            var city48 = new City() { Name = "Климовичи" };
            var city49 = new City() { Name = "Кличев" };
            var city50 = new City() { Name = "Кобрин" };
            var city51 = new City() { Name = "Копыль" };
            var city52 = new City() { Name = "Коссово" };
            var city53 = new City() { Name = "Костюковичи" };
            var city54 = new City() { Name = "Кричев" };
            var city55 = new City() { Name = "Крупки" };
            var city56 = new City() { Name = "Лепель" };
            var city57 = new City() { Name = "Лида" };
            var city58 = new City() { Name = "Логойск" };
            var city59 = new City() { Name = "Лунинец" };
            var city60 = new City() { Name = "Любань" };
            var city61 = new City() { Name = "Ляховичи" };
            var city62 = new City() { Name = "Мозырь" };
            var city63 = new City() { Name = "Малорита" };
            var city64 = new City() { Name = "Марьина Горка" };
            var city65 = new City() { Name = "Микашевичи" };
            var city66 = new City() { Name = "Минск", Adresses = new List<CityAddress>() { ca1, ca2, ca3, ca4, ca5, ca6, ca7 } };
            var city67 = new City() { Name = "Миоры" };
            var city68 = new City() { Name = "Могилев" };
            var city69 = new City() { Name = "Молодечно" };
            var city70 = new City() { Name = "Мосты" };
            var city71 = new City() { Name = "Мстиславль" };
            var city72 = new City() { Name = "Мядель" };
            var city73 = new City() { Name = "Новополоцк" };
            var city74 = new City() { Name = "Наровля" };
            var city75 = new City() { Name = "Несвиж" };
            var city76 = new City() { Name = "Новогрудок" };
            var city77 = new City() { Name = "Новолукомль" };
            var city78 = new City() { Name = "Орша" };
            var city79 = new City() { Name = "Осиповичи" };
            var city80 = new City() { Name = "Ошмяны" };
            var city81 = new City() { Name = "Пинск" };
            var city82 = new City() { Name = "Полоцк" };
            var city83 = new City() { Name = "Петриков" };
            var city84 = new City() { Name = "Поставы" };
            var city85 = new City() { Name = "Пружаны" };
            var city86 = new City() { Name = "Речица" };
            var city87 = new City() { Name = "Рогачев" };
            var city88 = new City() { Name = "Светлогорск" };
            var city89 = new City() { Name = "Свислочь" };
            var city90 = new City() { Name = "Сенно" };
            var city91 = new City() { Name = "Скидель" };
            var city92 = new City() { Name = "Славгород" };
            var city93 = new City() { Name = "Слоним" };
            var city94 = new City() { Name = "Слуцк" };
            var city95 = new City() { Name = "Смолевичи" };
            var city96 = new City() { Name = "Сморгонь" };
            var city97 = new City() { Name = "Солигорск" };
            var city98 = new City() { Name = "Старые Дороги" };
            var city99 = new City() { Name = "Столбцы" };
            var city100 = new City() { Name = "Столин" };
            var city101 = new City() { Name = "Толочин" };
            var city102 = new City() { Name = "Туров" };
            var city103 = new City() { Name = "Узда" };
            var city104 = new City() { Name = "Фаниполь" };
            var city105 = new City() { Name = "Хойники" };
            var city106 = new City() { Name = "Чаусы" };
            var city107 = new City() { Name = "Чашники" };
            var city108 = new City() { Name = "Червень" };
            var city109 = new City() { Name = "Чериков" };
            var city110 = new City() { Name = "Чечерск" };
            var city111 = new City() { Name = "Шклов" };
            var city112 = new City() { Name = "Щучин" };
            #endregion
            citiesList = new List<City>{
                city1, city2, city3, city4, city5, city6, city7, city8, city9, city10,
                city11, city12, city13, city14, city15, city16, city17, city18, city19, city20,
                city21, city22, city23, city24, city25, city26, city27, city28, city29, city30,
                city31, city32, city33, city34, city35, city36, city37, city38, city39, city40,
                city41, city42, city43, city44, city45, city46, city47, city48, city49, city50,
                city51, city52, city53, city54, city55, city56, city57, city58, city59, city60,
                city61, city62, city63, city64, city65, city66, city67, city68, city69, city70,
                city71, city72, city73, city74, city75, city76, city77, city78, city79, city80,
                city81, city82, city83, city84, city85, city86, city87, city88, city89, city90,
                city91, city92, city93, city94, city95, city96, city97, city98, city99, city100,
                city101, city102, city103, city104, city105, city106, city107, city108, city109, city110,
                city111, city112};
        }
    }
}