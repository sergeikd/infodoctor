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
            out List<Phone> phonesList, out List<Address> clinicAddressList, out List<City> citiesList,
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

            #region Очень длинный список городов РБ

            //Adresses = new List<Address>() {ca1, ca2, ca3, ca4, ca5, ca6, ca7} минск

            var city1 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Бобруйск", Language = langs.First(l => l.Code == "ru") } } };
            var city2 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Барановичи", Language = langs.First(l => l.Code == "ru") } } };
            var city3 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Борисов", Language = langs.First(l => l.Code == "ru") } } };
            var city4 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Барань", Language = langs.First(l => l.Code == "ru") } } };
            var city5 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Белоозерск", Language = langs.First(l => l.Code == "ru") } } };
            var city6 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Береза", Language = langs.First(l => l.Code == "ru") } } };
            var city7 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Березино", Language = langs.First(l => l.Code == "ru") } } };
            var city8 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Березовка", Language = langs.First(l => l.Code == "ru") } } };
            var city9 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Браслав", Language = langs.First(l => l.Code == "ru") } } };
            var city10 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Брест", Language = langs.First(l => l.Code == "ru") } } };
            var city11 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Буда-Кошелево", Language = langs.First(l => l.Code == "ru") } } };
            var city12 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Быхов", Language = langs.First(l => l.Code == "ru") } } };
            var city13 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Василевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city14 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Верхнедвинск", Language = langs.First(l => l.Code == "ru") } } };
            var city15 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ветка", Language = langs.First(l => l.Code == "ru") } } };
            var city16 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Вилейка", Language = langs.First(l => l.Code == "ru") } } };
            var city17 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Витебск", Language = langs.First(l => l.Code == "ru") } } };
            var city18 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Волковыск", Language = langs.First(l => l.Code == "ru") } } };
            var city19 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Воложин", Language = langs.First(l => l.Code == "ru") } } };
            var city20 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Высокое", Language = langs.First(l => l.Code == "ru") } } };
            var city21 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ганцевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city22 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Глубокое", Language = langs.First(l => l.Code == "ru") } } };
            var city23 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Гомель", Language = langs.First(l => l.Code == "ru") } } };
            var city24 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Горки", Language = langs.First(l => l.Code == "ru") } } };
            var city25 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Городок", Language = langs.First(l => l.Code == "ru") } } };
            var city26 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Гродно", Language = langs.First(l => l.Code == "ru") } } };
            var city27 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Давид-Городок", Language = langs.First(l => l.Code == "ru") } } };
            var city28 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Дзержинск", Language = langs.First(l => l.Code == "ru") } } };
            var city29 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Дисна", Language = langs.First(l => l.Code == "ru") } } };
            var city30 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Добруш", Language = langs.First(l => l.Code == "ru") } } };
            var city31 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Докшицы", Language = langs.First(l => l.Code == "ru") } } };
            var city32 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Дрогичин", Language = langs.First(l => l.Code == "ru") } } };
            var city33 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Дубровно", Language = langs.First(l => l.Code == "ru") } } };
            var city34 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Дятлово", Language = langs.First(l => l.Code == "ru") } } };
            var city35 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ельск", Language = langs.First(l => l.Code == "ru") } } };
            var city36 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Жодино", Language = langs.First(l => l.Code == "ru") } } };
            var city37 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Жабинка", Language = langs.First(l => l.Code == "ru") } } };
            var city38 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Житковичи", Language = langs.First(l => l.Code == "ru") } } };
            var city39 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Жлобин", Language = langs.First(l => l.Code == "ru") } } };
            var city40 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Заславль", Language = langs.First(l => l.Code == "ru") } } };
            var city41 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Иваново", Language = langs.First(l => l.Code == "ru") } } };
            var city42 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ивацевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city43 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ивье", Language = langs.First(l => l.Code == "ru") } } };
            var city44 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Калинковичи", Language = langs.First(l => l.Code == "ru") } } };
            var city45 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Каменец", Language = langs.First(l => l.Code == "ru") } } };
            var city46 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Кировск", Language = langs.First(l => l.Code == "ru") } } };
            var city47 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Клецк", Language = langs.First(l => l.Code == "ru") } } };
            var city48 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Климовичи", Language = langs.First(l => l.Code == "ru") } } };
            var city49 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Кличев", Language = langs.First(l => l.Code == "ru") } } };
            var city50 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Кобрин", Language = langs.First(l => l.Code == "ru") } } };
            var city51 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Копыль", Language = langs.First(l => l.Code == "ru") } } };
            var city52 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Коссово", Language = langs.First(l => l.Code == "ru") } } };
            var city53 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Костюковичи", Language = langs.First(l => l.Code == "ru") } } };
            var city54 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Кричев", Language = langs.First(l => l.Code == "ru") } } };
            var city55 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Крупки", Language = langs.First(l => l.Code == "ru") } } };
            var city56 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Лепель", Language = langs.First(l => l.Code == "ru") } } };
            var city57 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Лида", Language = langs.First(l => l.Code == "ru") } } };
            var city58 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Логойск", Language = langs.First(l => l.Code == "ru") } } };
            var city59 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Лунинец", Language = langs.First(l => l.Code == "ru") } } };
            var city60 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Любань", Language = langs.First(l => l.Code == "ru") } } };
            var city61 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ляховичи", Language = langs.First(l => l.Code == "ru") } } };
            var city62 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Мозырь", Language = langs.First(l => l.Code == "ru") } } };
            var city63 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Малорита", Language = langs.First(l => l.Code == "ru") } } };
            var city64 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Марьина Горка", Language = langs.First(l => l.Code == "ru") } } };
            var city65 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Микашевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city66 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Минск", Language = langs.First(l => l.Code == "ru") } }/*, Adresses = new List<Address>() { ca1, ca2, ca3, ca4, ca5, ca6, ca7 } */};
            var city67 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Миоры", Language = langs.First(l => l.Code == "ru") } } };
            var city68 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Могилев", Language = langs.First(l => l.Code == "ru") } } };
            var city69 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Молодечно", Language = langs.First(l => l.Code == "ru") } } };
            var city70 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Мосты", Language = langs.First(l => l.Code == "ru") } } };
            var city71 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Мстиславль", Language = langs.First(l => l.Code == "ru") } } };
            var city72 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Мядель", Language = langs.First(l => l.Code == "ru") } } };
            var city73 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Новополоцк", Language = langs.First(l => l.Code == "ru") } } };
            var city74 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Наровля", Language = langs.First(l => l.Code == "ru") } } };
            var city75 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Несвиж", Language = langs.First(l => l.Code == "ru") } } };
            var city76 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Новогрудок", Language = langs.First(l => l.Code == "ru") } } };
            var city77 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Новолукомль", Language = langs.First(l => l.Code == "ru") } } };
            var city78 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Орша", Language = langs.First(l => l.Code == "ru") } } };
            var city79 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Осиповичи", Language = langs.First(l => l.Code == "ru") } } };
            var city80 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Ошмяны", Language = langs.First(l => l.Code == "ru") } } };
            var city81 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Пинск", Language = langs.First(l => l.Code == "ru") } } };
            var city82 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Полоцк", Language = langs.First(l => l.Code == "ru") } } };
            var city83 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Петриков", Language = langs.First(l => l.Code == "ru") } } };
            var city84 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Поставы", Language = langs.First(l => l.Code == "ru") } } };
            var city85 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Пружаны", Language = langs.First(l => l.Code == "ru") } } };
            var city86 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Речица", Language = langs.First(l => l.Code == "ru") } } };
            var city87 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Рогачев", Language = langs.First(l => l.Code == "ru") } } };
            var city88 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Светлогорск", Language = langs.First(l => l.Code == "ru") } } };
            var city89 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Свислочь", Language = langs.First(l => l.Code == "ru") } } };
            var city90 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Сенно", Language = langs.First(l => l.Code == "ru") } } };
            var city91 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Скидель", Language = langs.First(l => l.Code == "ru") } } };
            var city92 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Славгород", Language = langs.First(l => l.Code == "ru") } } };
            var city93 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Слоним", Language = langs.First(l => l.Code == "ru") } } };
            var city94 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Слуцк", Language = langs.First(l => l.Code == "ru") } } };
            var city95 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Смолевичи", Language = langs.First(l => l.Code == "ru") } } };
            var city96 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Сморгонь", Language = langs.First(l => l.Code == "ru") } } };
            var city97 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Солигорск", Language = langs.First(l => l.Code == "ru") } } };
            var city98 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Старые Дороги", Language = langs.First(l => l.Code == "ru") } } };
            var city99 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Столбцы", Language = langs.First(l => l.Code == "ru") } } };
            var city100 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Столин", Language = langs.First(l => l.Code == "ru") } } };
            var city101 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Толочин", Language = langs.First(l => l.Code == "ru") } } };
            var city102 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Туров", Language = langs.First(l => l.Code == "ru") } } };
            var city103 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Узда", Language = langs.First(l => l.Code == "ru") } } };
            var city104 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Фаниполь", Language = langs.First(l => l.Code == "ru") } } };
            var city105 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Хойники", Language = langs.First(l => l.Code == "ru") } } };
            var city106 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Чаусы", Language = langs.First(l => l.Code == "ru") } } };
            var city107 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Чашники", Language = langs.First(l => l.Code == "ru") } } };
            var city108 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Червень", Language = langs.First(l => l.Code == "ru") } } };
            var city109 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Чериков", Language = langs.First(l => l.Code == "ru") } } };
            var city110 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Чечерск", Language = langs.First(l => l.Code == "ru") } } };
            var city111 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Шклов", Language = langs.First(l => l.Code == "ru") } } };
            var city112 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Щучин", Language = langs.First(l => l.Code == "ru") } } };
            var city113 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Брестская область", Language = langs.First(l => l.Code == "ru") } } };
            var city114 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Витебская область", Language = langs.First(l => l.Code == "ru") } } };
            var city115 = new City() { LocalizedCities = new List<LocalizedCity>() { new LocalizedCity() { Name = "Минская область", Language = langs.First(l => l.Code == "ru") } } };


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


            List<ClinicReview> clinicReviewSubList;
            clinicReviewSubList = clinicReviewList.Take(5).ToList();

            var clinic1 = new Clinic()
            {
                Email = "info@nordin.by",
                Localized = new List<LocalizedClinic>()
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
                Localized = new List<LocalizedClinic>()
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
                Localized = new List<LocalizedClinic>()
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
                Localized = new List<LocalizedClinic>()
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
                Localized = new List<LocalizedClinic>()
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
                Localized = new List<LocalizedDoctor>() { new LocalizedDoctor() { Name = "Степанов Степан Степанович", Manipulation = "Может что-то хорошо.", Language = langs.First(l => l.Code == "ru") } },
                Email = "infosuperstepa1999@gmail.com",
                Experience = 14,
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
                Localized = new List<LocalizedDoctor>() { new LocalizedDoctor() { Name = "Степанов Иван Степанович", Manipulation = "Может что-то отлично.", Language = langs.First(l => l.Code == "ru") } },
                Email = "giperivan2@gmail.com",
                Experience = 20,
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
                Localized = new List<LocalizedDoctor>() { new LocalizedDoctor() { Name = "Степанов Степан Иванович", Manipulation = "Может что-то нормально.", Language = langs.First(l => l.Code == "ru") } },
                Email = "darmaed19@gmail.com",
                Experience = 2,
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
                Localized = new List<LocalizedDoctor>() { new LocalizedDoctor() { Name = "Иванов Степан Степанович", Manipulation = "Может что-то хорошо.", Language = langs.First(l => l.Code == "ru") } },
                Email = "tainiidoctor2@gmail.com",
                Experience = 14,
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
                Localized = new List<LocalizedDoctor>() { new LocalizedDoctor() { Name = "Степанов Сергей Степанович", Manipulation = "Может что-то отлично.", Language = langs.First(l => l.Code == "ru") } },
                Email = "123456789@gmail.com",
                Experience = 29,
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

            var cat001 = new DoctorCategory()
            {
                Localized = new List<LocalizedDoctorCategory>() { new LocalizedDoctorCategory() { Name = "Без категории", Language = langs.First(l => l.Code == "ru") } },
                Doctors = new List<Doctor> { doc003 }
            };
            var cat002 = new DoctorCategory()
            {
                Localized = new List<LocalizedDoctorCategory>() { new LocalizedDoctorCategory() { Name = "Первая категория", Language = langs.First(l => l.Code == "ru") } },
                Doctors = new List<Doctor> { doc002 }
            };
            var cat003 = new DoctorCategory()
            {
                Localized = new List<LocalizedDoctorCategory>() { new LocalizedDoctorCategory() { Name = "Вторая категории", Language = langs.First(l => l.Code == "ru") } },
                Doctors = new List<Doctor> { doc001 }
            };
            var cat004 = new DoctorCategory()
            {
                Localized = new List<LocalizedDoctorCategory>() { new LocalizedDoctorCategory() { Name = "Высшая категории", Language = langs.First(l => l.Code == "ru") } },
                Doctors = new List<Doctor> { doc005 }
            };
            var cat005 = new DoctorCategory()
            {
                Localized = new List<LocalizedDoctorCategory>() { new LocalizedDoctorCategory() { Name = "Кандидат в доктора медицинских наук", Language = langs.First(l => l.Code == "ru") } },
                Doctors = new List<Doctor> { doc004 }
            };
            var cat006 = new DoctorCategory()
            {
                Localized = new List<LocalizedDoctorCategory>() { new LocalizedDoctorCategory() { Name = "Доктор медицинских наук", Language = langs.First(l => l.Code == "ru") } }

            };

            #endregion

            categoriesList = new List<DoctorCategory>() { cat001, cat002, cat003, cat004, cat005, cat006 };

            var phone1 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "159",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone2 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "(017) 296 62 72",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var phone3 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 110 12 12",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone4 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 211 28 61",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone5 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 611 28 61",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone6 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 314 94 94",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone7 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 664 44 44",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone8 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 253 33 33",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone9 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 29 103 43 43",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone10 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "взрослые",
                        Number = "+375 17 369 64 59",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone11 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "взрослые",
                        Number = "+375 17 369 69 16",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone12 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "дети",
                        Number = "+375 17 369 64 57",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone13 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "дети",
                        Number = "+375 17 369 65 56",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone14 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "стоматология",
                        Number = "+375 17 369 67 65",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone15 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "платные услуги",
                        Number = "+375 17 369 52 04",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone16 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = "платные услуги",
                        Number = "+375 44 580 90 33",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone17 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 17 290 81 11",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };
            var phone18 = new Phone()
            {
                LocalizedPhones = new List<LocalizedPhone>()
                {
                    new LocalizedPhone()
                    {
                        Description = string.Empty,
                        Number = "+375 44 575 08 89",
                        Language = langs.First(l => l.Code == "ru")
                    }
                }
            };


            phonesList = new List<Phone>
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

            var ca1 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "ул.Сурганова 47Б",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },

                Clinic = clinic1,
                Doctors = new List<Doctor> { doc001, doc002, doc003 },
                Phones = new List<Phone>() { phone1, phone2 }
            };
            var ca2 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "пр-т. Независимости 58",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic2,
                Doctors = new List<Doctor> { doc004, doc002, doc005 },
                Phones = new List<Phone>() { phone3 }
            };
            var ca3 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "пр-т. Победителей 75,",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic3,
                Phones = new List<Phone>() { phone4, phone5 }
            };
            var ca4 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "ул.Скрипникова 11Б,",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic3,
                Phones = new List<Phone>() { phone6, phone7 }
            };
            var ca5 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "ул.Захарова 50Д",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic3,
                Phones = new List<Phone>() { phone8, phone9 }
            };
            var ca6 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "ул.Победителей 93",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic4,
                Phones = new List<Phone>() { phone10, phone11, phone12, phone13, phone14, phone15, phone16 }
            };
            var ca7 = new Address()
            {
                LocalizedAddresses = new List<LocalizedAddress>()
                {
                    new LocalizedAddress()
                    {
                        Country = "Беларусь",
                        City =city66,
                        Street = "ул. Нарочанская 17",
                        Language = langs.First(l => l.Code == "ru")
                    }
                },
                Clinic = clinic5,
                Phones = new List<Phone>() { phone17, phone18 }
            };

            //var ca1 = new Address() { Country = "Беларусь", Street = "ул.Сурганова 47Б", Clinic = clinic1, Doctors = new List<Doctor> { doc001, doc002, doc003 }, Phones = new List<Number>() { phone1, phone2 } };
            //var ca2 = new Address() { Country = "Беларусь", Street = "пр-т. Независимости 58", Clinic = clinic2, Doctors = new List<Doctor> { doc004, doc002, doc005 }, Phones = new List<Number>() { phone3 } };
            //var ca3 = new Address() { Country = "Беларусь", Street = "пр-т. Победителей 75,", Clinic = clinic3, Phones = new List<Number>() { phone4, phone5 } };
            //var ca4 = new Address() { Country = "Беларусь", Street = "ул.Скрипникова 11Б,", Clinic = clinic3, Phones = new List<Number>() { phone6, phone7 } };
            //var ca5 = new Address() { Country = "Беларусь", Street = "ул.Захарова 50Д", Clinic = clinic3, Phones = new List<Number>() { phone8, phone9 } };
            //var ca6 = new Address() { Country = "Беларусь", Street = "ул.Победителей 93", Clinic = clinic4, Phones = new List<Number>() { phone10, phone11, phone12, phone13, phone14, phone15, phone16 } };
            //var ca7 = new Address() { Country = "Беларусь", Street = "ул. Нарочанская 17", Clinic = clinic5, Phones = new List<Number>() { phone17, phone18 } };

            clinicAddressList = new List<Address> { ca1, ca2, ca3, ca4, ca5, ca6, ca7 };


        }
    }
}
