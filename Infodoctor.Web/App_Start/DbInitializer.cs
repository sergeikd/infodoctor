using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.WebPages;
using Infodoctor.DAL;
using Infodoctor.Domain;
using Infodoctor.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infodoctor.Web
{
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем роли
            var role1 = new IdentityRole { Name = "user" };
            var role2 = new IdentityRole { Name = "admin" };
            var role3 = new IdentityRole { Name = "moder" };
            var role4 = new IdentityRole { Name = "clinic" };
            var role5 = new IdentityRole { Name = "doctor" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            roleManager.Create(role4);
            roleManager.Create(role5);

            // создаем пользователей
            var admin = new ApplicationUser
            {
                Email = "admin@infodoctor.by",
                UserName = "admin"
            };
            var password = "admin_";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role2.Name);
            }

            var moder = new ApplicationUser
            {
                Email = "moder@infodoctor.by",
                UserName = "moder"
            };
            password = "moder_";
            result = userManager.Create(moder, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(moder.Id, role3.Name);
            }

            for (var i = 0; i < 10; i++)
            {
                var user = new ApplicationUser
                {
                    Email = "user" + i + "@infodoctor.by",
                    UserName = "user" + i
                };
                password = "123456";
                result = userManager.Create(user, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, role1.Name);
                }
            }

            //add list of countries to DB table Countries
            var countriesList = new List<Country>();
            var path = AppDomain.CurrentDomain.BaseDirectory + "/Content/CountriesList.txt";
            using (var sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    countriesList.Add(new Country() { Name = line });
                }
            }
            context.Countries.AddRange(countriesList);

            var clinicReviewList = new List<ClinicReview>();
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
            context.ClinicReviews.AddRange(clinicReviewList);

            var doctorsReviewList = new List<DoctorReview>();
            rnd = new Random();
            ticks = DateTime.Now.Ticks - 100000000000000;
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
                    RatePrice = rnd.Next(3) + 3,
                    RateQuality = rnd.Next(3) + 3
                });
            }
            context.DoctorReviews.AddRange(doctorsReviewList);

            var clinic1 = new Clinic()
            {
                Email = "info@nordin.by",
                Name = "Медицинский центр Нордин",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 1).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 1).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 1).Average(y => y.RatePoliteness),
                ImageName = "aebce66fbc2040eaabf62b41f0db82ec.jpg"
            };
            clinic1.RateAverage = (clinic1.RatePrice + clinic1.RateQuality + clinic1.RatePoliteness)/3;
            var clinic2 = new Clinic()
            {
                Email = string.Empty,
                Name = "Стоматологический центр Дентко",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 2).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 2).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 2).Average(y => y.RatePoliteness),
                ImageName = "c0c2d3318376417a92b0fd525ab57663.jpg"
            };
            clinic2.RateAverage = (clinic2.RatePrice + clinic2.RateQuality + clinic2.RatePoliteness) / 3;
            var clinic3 = new Clinic()
            {
                Email = "kravira@kravira.by",
                Name = "Медицинский центр Кравира",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 3).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 3).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 3).Average(y => y.RatePoliteness)
            };
            clinic3.RateAverage = (clinic3.RatePrice + clinic3.RateQuality + clinic3.RatePoliteness) / 3;
            var clinic4 = new Clinic()
            {
                Email = "medic4@tut.by",
                Name = "4-я городская поликлиника г.Минска",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 4).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 4).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 4).Average(y => y.RatePoliteness)
            };
            clinic4.RateAverage = (clinic4.RatePrice + clinic4.RateQuality + clinic4.RatePoliteness) / 3;
            var clinic5 = new Clinic()
            {
                Email = string.Empty,
                Name = "2-я городская детская клиническая больница» г. Минска",
                RatePrice = clinicReviewList.Where(x => x.ClinicId == 5).Average(y => y.RatePrice),
                RateQuality = clinicReviewList.Where(x => x.ClinicId == 5).Average(y => y.RateQuality),
                RatePoliteness = clinicReviewList.Where(x => x.ClinicId == 5).Average(y => y.RatePoliteness)
            };
            clinic5.RateAverage = (clinic5.RatePrice + clinic5.RateQuality + clinic5.RatePoliteness) / 3;
            var clinicList = new List<Clinic> { clinic1, clinic2, clinic4, clinic5 };
            context.Сlinics.AddRange(clinicList);

            #region Список врачей

            var doc001 = new Doctor()
            {
                Name = "Степанов Степан Степанович",
                Email = "infosuperstepa1999@gmail.com",
                Experience = 14,
                Manipulation = "Может что-то хорошо.",
                RatePrice = doctorsReviewList.Where(x => x.DoctorId == 1).Average(y => y.RatePrice),
                RateQuality = doctorsReviewList.Where(x => x.DoctorId == 1).Average(y => y.RateQuality),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 1).Average(y => y.RatePoliteness),
                Clinics = new List<Clinic> { clinic1 },
                ImageName = "b10a59752b864021bf4cd69e1e26263a.jpg"
            };
            doc001.RateAverage = (doc001.RatePrice + doc001.RateQuality + doc001.RatePoliteness) / 3;
            var doc002 = new Doctor()
            {
                Name = "Степанов Иван Степанович",
                Email = "giperivan2@gmail.com",
                Experience = 20,
                Manipulation = "Может что-то отлично.",
                RatePrice = doctorsReviewList.Where(x => x.DoctorId == 2).Average(y => y.RatePrice),
                RateQuality = doctorsReviewList.Where(x => x.DoctorId == 2).Average(y => y.RateQuality),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 2).Average(y => y.RatePoliteness),
                Clinics = new List<Clinic> { clinic1,clinic2 }
            };
            doc002.RateAverage = (doc002.RatePrice + doc002.RateQuality + doc002.RatePoliteness) / 3;
            var doc003 = new Doctor()
            {
                Name = "Степанов Степан Иванович",
                Email = "darmaed19@gmail.com",
                Experience = 2,
                Manipulation = "Может что-то нормально.",
                RatePrice = doctorsReviewList.Where(x => x.DoctorId == 3).Average(y => y.RatePrice),
                RateQuality = doctorsReviewList.Where(x => x.DoctorId == 3).Average(y => y.RateQuality),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 3).Average(y => y.RatePoliteness),
                Clinics = new List<Clinic> { clinic1 }
            };
            doc003.RateAverage = (doc003.RatePrice + doc003.RateQuality + doc003.RatePoliteness) / 3;
            var doc004 = new Doctor()
            {
                Name = "Иванов Степан Степанович",
                Email = "tainiidoctor2@gmail.com",
                Experience = 14,
                Manipulation = "Может что-то хорошо.",
                RatePrice = doctorsReviewList.Where(x => x.DoctorId == 4).Average(y => y.RatePrice),
                RateQuality = doctorsReviewList.Where(x => x.DoctorId == 4).Average(y => y.RateQuality),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 4).Average(y => y.RatePoliteness),
                Clinics = new List<Clinic> { clinic2 }
            };
            doc004.RateAverage = (doc004.RatePrice + doc004.RateQuality + doc004.RatePoliteness) / 3;
            var doc005 = new Doctor()
            {
                Name = "Степанов Сергей Степанович",
                Email = "123456789@gmail.com",
                Experience = 29,
                Manipulation = "Может что-то отлично.",
                RatePrice = doctorsReviewList.Where(x => x.DoctorId == 5).Average(y => y.RatePrice),
                RateQuality = doctorsReviewList.Where(x => x.DoctorId == 5).Average(y => y.RateQuality),
                RatePoliteness = doctorsReviewList.Where(x => x.DoctorId == 5).Average(y => y.RatePoliteness),
                Clinics = new List<Clinic> { clinic2 }
            };
            doc005.RateAverage = (doc005.RatePrice + doc005.RateQuality + doc005.RatePoliteness) / 3;
            #endregion
            var doctors = new List<Doctor> { doc001, doc002, doc003, doc004, doc005 };
            context.Doctors.AddRange(doctors);

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

            var phonesList = new List<ClinicPhone>{
                phone1, phone2, phone3, phone4, phone5, phone6, phone7, phone8, phone9,
                phone10, phone11, phone12, phone13, phone14, phone15, phone16, phone17, phone18};

            context.ClinicPhones.AddRange(phonesList);
            var ca1 = new CityAddress() { Street = "ул.Сурганова 47Б", Clinic = clinic1, Doctors = new List<Doctor> { doc001, doc002, doc003 }, ClinicPhones = new List<ClinicPhone>() { phone1, phone2 } };
            var ca2 = new CityAddress() { Street = "пр-т. Независимости 58", Clinic = clinic2, Doctors = new List<Doctor> { doc004, doc002, doc005 }, ClinicPhones = new List<ClinicPhone>() { phone3 } };
            var ca3 = new CityAddress() { Street = "пр-т. Победителей 75,", Clinic = clinic3, ClinicPhones = new List<ClinicPhone>() { phone4, phone5 } };
            var ca4 = new CityAddress() { Street = "ул.Скрипникова 11Б,", Clinic = clinic3, ClinicPhones = new List<ClinicPhone>() { phone6, phone7 } };
            var ca5 = new CityAddress() { Street = "ул.Захарова 50Д", Clinic = clinic3, ClinicPhones = new List<ClinicPhone>() { phone8, phone9 } };
            var ca6 = new CityAddress() { Street = "ул.Победителей 93", Clinic = clinic4, ClinicPhones = new List<ClinicPhone>() { phone10, phone11, phone12, phone13, phone14, phone15, phone16 } };
            var ca7 = new CityAddress() { Street = "ул. Нарочанская 17", Clinic = clinic5, ClinicPhones = new List<ClinicPhone>() { phone17, phone18 } };

            var clinicAddressList = new List<CityAddress> { ca1, ca2, ca3, ca4, ca5, ca6, ca7 };
            context.CityAddresses.AddRange(clinicAddressList);


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
            var citiesList = new List<City>{
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
            context.Cities.AddRange(citiesList);

            #region Категории врачей
            var cat001 = new DoctorCategory() { Name = "Без категории", Doctors = new List<Doctor> { doc003 } };
            var cat002 = new DoctorCategory() { Name = "Первая категория", Doctors = new List<Doctor> { doc002 } };
            var cat003 = new DoctorCategory() { Name = "Вторая категории", Doctors = new List<Doctor> { doc001 } };
            var cat004 = new DoctorCategory() { Name = "Высшая категории", Doctors = new List<Doctor> { doc005 } };
            var cat005 = new DoctorCategory() { Name = "Кандидат в доктора медицинских наук", Doctors = new List<Doctor> { doc004 } };
            var cat006 = new DoctorCategory() { Name = "Доктор медицинских наук" };
            #endregion

            var categoties = new List<DoctorCategory>()
            {
                cat001,cat002,cat003,cat004,cat005,cat006
            };
            context.DoctorCategories.AddRange(categoties);

            #region Список специализаций врачей
            var dp059 = new DoctorSpecialization() { Name = "Аллерголог", Doctors = new List<Doctor> { doc002 } };
            var dp148 = new DoctorSpecialization() { Name = "Аллерголог детский", Doctors = new List<Doctor> { doc003 } };
            var dp273 = new DoctorSpecialization() { Name = "Ангиохирург" };
            var dp340 = new DoctorSpecialization() { Name = "Андролог" };
            var dp411 = new DoctorSpecialization() { Name = "Анестезиолог" };
            var dp595 = new DoctorSpecialization() { Name = "Гастроэнтеролог" };
            var dp629 = new DoctorSpecialization() { Name = "Гастроэнтеролог детский" };
            var dp720 = new DoctorSpecialization() { Name = "Гинеколог" };
            var dp848 = new DoctorSpecialization() { Name = "Гинеколог детский" };
            var dp928 = new DoctorSpecialization() { Name = "Гинеколог-эндокринолог" };
            var dp1010 = new DoctorSpecialization() { Name = "Дерматолог" };
            var dp1125 = new DoctorSpecialization() { Name = "Иглорефлексотерапевт" };
            var dp1246 = new DoctorSpecialization() { Name = "Иммунолог" };
            var dp1367 = new DoctorSpecialization() { Name = "Кардиолог" };
            var dp1460 = new DoctorSpecialization() { Name = "Кардиолог детский" };
            var dp1567 = new DoctorSpecialization() { Name = "Кардио-ревматолог детский" };
            var dp168 = new DoctorSpecialization() { Name = "Косметолог" };
            var dp1768 = new DoctorSpecialization() { Name = "Логопед" };
            var dp188 = new DoctorSpecialization() { Name = "Маммолог" };
            var dp1911 = new DoctorSpecialization() { Name = "Массажист", Doctors = new List<Doctor> { doc001 } };
            var dp2087 = new DoctorSpecialization() { Name = "Нарколог" };
            var dp2144 = new DoctorSpecialization() { Name = "Невролог" };
            var dp2257 = new DoctorSpecialization() { Name = "Невролог детский" };
            var dp2393 = new DoctorSpecialization() { Name = "Нейроофтальмология" };
            var dp2472 = new DoctorSpecialization() { Name = "Нефролог" };
            var dp2595 = new DoctorSpecialization() { Name = "Нефролог детский" };
            var dp2654 = new DoctorSpecialization() { Name = "Онколог", Doctors = new List<Doctor> { doc004 } };
            var dp2758 = new DoctorSpecialization() { Name = "Онколог детский" };
            var dp2833 = new DoctorSpecialization() { Name = "Ортопед" };
            var dp2998 = new DoctorSpecialization() { Name = "Оториноларинголог (ЛОР)" };
            var dp3027 = new DoctorSpecialization() { Name = "Оториноларинголог (ЛОР) детский" };
            var dp3164 = new DoctorSpecialization() { Name = "Офтальмолог" };
            var dp320 = new DoctorSpecialization() { Name = "Офтальмолог детский" };
            var dp3318 = new DoctorSpecialization() { Name = "Офтальмолог-эндокринолог" };
            var dp3462 = new DoctorSpecialization() { Name = "Педиатр", Doctors = new List<Doctor> { doc005 } };
            var dp3519 = new DoctorSpecialization() { Name = "Проктолог" };
            var dp3656 = new DoctorSpecialization() { Name = "Психиатр" };
            var dp3767 = new DoctorSpecialization() { Name = "Психолог" };
            var dp386 = new DoctorSpecialization() { Name = "Психотерапевт" };
            var dp3922 = new DoctorSpecialization() { Name = "Пульмонолог" };
            var dp4019 = new DoctorSpecialization() { Name = "Реабилитолог" };
            var dp4180 = new DoctorSpecialization() { Name = "Реаниматолог" };
            var dp4251 = new DoctorSpecialization() { Name = "Ревматолог" };
            var dp4351 = new DoctorSpecialization() { Name = "Рентгенолог" };
            var dp4463 = new DoctorSpecialization() { Name = "Репродуктолог" };
            var dp4557 = new DoctorSpecialization() { Name = "Рефлексотерапевт" };
            var dp4698 = new DoctorSpecialization() { Name = "Сексолог" };
            var dp4751 = new DoctorSpecialization() { Name = "Стоматолог" };
            var dp4813 = new DoctorSpecialization() { Name = "Стоматолог детский" };
            var dp4935 = new DoctorSpecialization() { Name = "Стоматолог-ортодонт" };
            var dp5029 = new DoctorSpecialization() { Name = "Стоматолог-ортопед" };
            var dp514 = new DoctorSpecialization() { Name = "Стоматолог-терапевт" };
            var dp5288 = new DoctorSpecialization() { Name = "Стоматолог-хирург" };
            var dp5385 = new DoctorSpecialization() { Name = "Терапевт" };
            var dp5489 = new DoctorSpecialization() { Name = "Травматолог-ортопед" };
            var dp5515 = new DoctorSpecialization() { Name = "Травматолог-ортопед детский" };
            var dp5691 = new DoctorSpecialization() { Name = "Трихолог" };
            var dp5779 = new DoctorSpecialization() { Name = "Уролог" };
            var dp5867 = new DoctorSpecialization() { Name = "Уролог детский" };
            var dp5915 = new DoctorSpecialization() { Name = "Физиотерапевт" };
            var dp6041 = new DoctorSpecialization() { Name = "Флеболог" };
            var dp6171 = new DoctorSpecialization() { Name = "Хирург" };
            var dp6287 = new DoctorSpecialization() { Name = "Хирург детский" };
            var dp6349 = new DoctorSpecialization() { Name = "Хирург пластический" };
            var dp641 = new DoctorSpecialization() { Name = "Эндокринолог" };
            var dp6546 = new DoctorSpecialization() { Name = "Эндокринолог детский" };
            #endregion


            var doctorSpecialization = new List<DoctorSpecialization>()
            {
                dp059, dp148, dp273, dp340, dp411, dp595, dp629, dp720, dp848, dp928, dp1010, dp1125, dp1246, dp1367, dp1460, dp1567, dp168, dp1768, dp188, dp1911, dp2087, dp2144, dp2257, dp2393, dp2472, dp2595, dp2654, dp2758, dp2833, dp2998, dp3027, dp3164, dp320, dp3318, dp3462, dp3519, dp3656, dp3767, dp386, dp3922, dp4019, dp4180, dp4251, dp4351, dp4463, dp4557, dp4698, dp4751, dp4813, dp4935, dp5029, dp514, dp5288, dp5385, dp5489, dp5515, dp5691, dp5779, dp5867, dp5915, dp6041, dp6171, dp6287, dp6349, dp641, dp6546

            };
            context.DoctorSpecializations.AddRange(doctorSpecialization.OrderBy(d => d.Name));

            #region Очень длинный список специализаций клиник
            var cp10 = new ClinicSpecialization() { Name = "Аллергология", Clinics = new List<Clinic>() { clinic1 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp059 } };
            var cp20 = new ClinicSpecialization() { Name = "Аллергология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp148 } };
            var cp30 = new ClinicSpecialization() { Name = "Ангиохирургия", DoctorSpecializations = new List<DoctorSpecialization>() { dp273 } };
            var cp40 = new ClinicSpecialization() { Name = "Андрология", DoctorSpecializations = new List<DoctorSpecialization>() { dp340 } };
            var cp50 = new ClinicSpecialization() { Name = "Анестезиология", DoctorSpecializations = new List<DoctorSpecialization>() { dp411 } };
            var cp60 = new ClinicSpecialization() { Name = "Гастроэнтерология", Clinics = new List<Clinic>() { clinic1 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp595 } };
            var cp70 = new ClinicSpecialization() { Name = "Гастроэнтерология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp629 } };
            var cp80 = new ClinicSpecialization() { Name = "Гинекология", Clinics = new List<Clinic>() { clinic1, clinic3, clinic4 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp720 } };
            var cp81 = new ClinicSpecialization() { Name = "Гинекология детская", Clinics = new List<Clinic>() { clinic1 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp848 } };
            var cp90 = new ClinicSpecialization() { Name = "Гинекология-эндокринология", DoctorSpecializations = new List<DoctorSpecialization>() { dp928 } };
            var cp100 = new ClinicSpecialization() { Name = "Дерматология", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp1010, dp5691 } };
            var cp110 = new ClinicSpecialization() { Name = "Диагностика лабораторная " };
            var cp120 = new ClinicSpecialization() { Name = "Диагностика ультразвуковая" };
            var cp130 = new ClinicSpecialization() { Name = "Диагностика функциональная" };
            var cp140 = new ClinicSpecialization() { Name = "Диагностика эндоскопическая" };
            var cp150 = new ClinicSpecialization() { Name = "Иглорефлексотерапевтия", DoctorSpecializations = new List<DoctorSpecialization>() { dp1125 } };
            var cp160 = new ClinicSpecialization() { Name = "Иммунология", DoctorSpecializations = new List<DoctorSpecialization>() { dp1246 } };
            var cp170 = new ClinicSpecialization() { Name = "Кардио-ревматология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp1567 } };
            var cp180 = new ClinicSpecialization() { Name = "Кардиология", Clinics = new List<Clinic>() { clinic1, clinic4 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp1367 } };
            var cp190 = new ClinicSpecialization() { Name = "Кардиология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp1460 } };
            var cp200 = new ClinicSpecialization() { Name = "Косметология", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp168 } };
            var cp210 = new ClinicSpecialization() { Name = "Логопедия", Clinics = new List<Clinic>() { clinic1 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp1768 } };
            var cp220 = new ClinicSpecialization() { Name = "Маммология", Clinics = new List<Clinic>() { clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp188 } };
            var cp230 = new ClinicSpecialization() { Name = "Массаж", Clinics = new List<Clinic>() { clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp1911 } };
            var cp239 = new ClinicSpecialization() { Name = "Наркология", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2087 } };
            var cp240 = new ClinicSpecialization() { Name = "Неврология", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2144 } };
            var cp250 = new ClinicSpecialization() { Name = "Неврология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp2257 } };
            var cp260 = new ClinicSpecialization() { Name = "Нефрология", Clinics = new List<Clinic>() { clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2472 } };
            var cp270 = new ClinicSpecialization() { Name = "Нефрология детская", Clinics = new List<Clinic>() { clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2595 } };
            var cp280 = new ClinicSpecialization() { Name = "Нейроофтальмология", DoctorSpecializations = new List<DoctorSpecialization>() { dp2393 } };
            var cp290 = new ClinicSpecialization() { Name = "Онкология", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2654 } };
            var cp300 = new ClinicSpecialization() { Name = "Онкология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp2758 } };
            var cp301 = new ClinicSpecialization() { Name = "Ортопедия", Clinics = new List<Clinic>() { clinic1 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2833 } };
            var cp310 = new ClinicSpecialization() { Name = "Оториноларингология(ЛОР)", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp2998 } };
            var cp320 = new ClinicSpecialization() { Name = "Оториноларингология(ЛОР) детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp3027 } };
            var cp330 = new ClinicSpecialization() { Name = "Офтальмология", Clinics = new List<Clinic>() { clinic1 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp3164 } };
            var cp340 = new ClinicSpecialization() { Name = "Офтальмология детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp320 } };
            var cp350 = new ClinicSpecialization() { Name = "Офтальмология-эндокринология", DoctorSpecializations = new List<DoctorSpecialization>() { dp3318 } };
            var cp360 = new ClinicSpecialization() { Name = "Педиатрия", Clinics = new List<Clinic>() { clinic1, clinic4 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp3462 } };
            var cp370 = new ClinicSpecialization() { Name = "Проктология", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp3519 } };
            var cp380 = new ClinicSpecialization() { Name = "Психотерапия", Clinics = new List<Clinic>() { clinic1, clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp3656, dp3767, dp386 } };
            var cp390 = new ClinicSpecialization() { Name = "Пульмонология", DoctorSpecializations = new List<DoctorSpecialization>() { dp3922 } };
            var cp400 = new ClinicSpecialization() { Name = "Реабилитология", DoctorSpecializations = new List<DoctorSpecialization>() { dp4019 } };
            var cp410 = new ClinicSpecialization() { Name = "Реаниматология", DoctorSpecializations = new List<DoctorSpecialization>() { dp4180 } };
            var cp420 = new ClinicSpecialization() { Name = "Ревматология", DoctorSpecializations = new List<DoctorSpecialization>() { dp4251 } };
            var cp430 = new ClinicSpecialization() { Name = "Рентгенология", DoctorSpecializations = new List<DoctorSpecialization>() { dp4351 } };
            var cp440 = new ClinicSpecialization() { Name = "Репродуктология", DoctorSpecializations = new List<DoctorSpecialization>() { dp4463 } };
            var cp450 = new ClinicSpecialization() { Name = "Рефлексотерапевтия", DoctorSpecializations = new List<DoctorSpecialization>() { dp4557 } };
            var cp460 = new ClinicSpecialization() { Name = "Сексология", DoctorSpecializations = new List<DoctorSpecialization>() { dp4698 } };
            var cp461 = new ClinicSpecialization() { Name = "Стоматология", Clinics = new List<Clinic>() { clinic2, clinic3, clinic4 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp4751 } };
            var cp470 = new ClinicSpecialization() { Name = "Стоматология детская", Clinics = new List<Clinic>() { clinic2 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp4813 } };
            var cp480 = new ClinicSpecialization() { Name = "Стоматология-ортодонтия", Clinics = new List<Clinic>() { clinic2 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp4935 } };
            var cp490 = new ClinicSpecialization() { Name = "Стоматология-ортопедия", Clinics = new List<Clinic>() { clinic2 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp5029 } };
            var cp500 = new ClinicSpecialization() { Name = "Стоматология-терапевтия", Clinics = new List<Clinic>() { clinic2 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp514 } };
            var cp510 = new ClinicSpecialization() { Name = "Стоматология-хирургия", DoctorSpecializations = new List<DoctorSpecialization>() { dp5288 } };
            var cp520 = new ClinicSpecialization() { Name = "Терапия", Clinics = new List<Clinic>() { clinic1, clinic3, clinic4 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp5385 } };
            var cp530 = new ClinicSpecialization() { Name = "Травматология-ортопедия", DoctorSpecializations = new List<DoctorSpecialization>() { dp5489 } };
            var cp540 = new ClinicSpecialization() { Name = "Травматология-ортопедия детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp5515 } };
            var cp550 = new ClinicSpecialization() { Name = "Урология", Clinics = new List<Clinic>() { clinic1, clinic3, clinic4, clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp5779 } };
            var cp560 = new ClinicSpecialization() { Name = "Урология детская", Clinics = new List<Clinic>() { clinic1, clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp5867 } };
            var cp561 = new ClinicSpecialization() { Name = "Физиотерпапия", Clinics = new List<Clinic>() { clinic1, clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp5915 } };
            var cp570 = new ClinicSpecialization() { Name = "Флебология", DoctorSpecializations = new List<DoctorSpecialization>() { dp6041 } };
            var cp580 = new ClinicSpecialization() { Name = "Хирургия", Clinics = new List<Clinic>() { clinic3, clinic4 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp6171 } };
            var cp590 = new ClinicSpecialization() { Name = "Хирургия детская", DoctorSpecializations = new List<DoctorSpecialization>() { dp6287 } };
            var cp600 = new ClinicSpecialization() { Name = "Хирургия пластическая", Clinics = new List<Clinic>() { clinic3 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp6349 } };
            var cp610 = new ClinicSpecialization() { Name = "Эндокринология", Clinics = new List<Clinic>() { clinic1, clinic3, clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp641 } };
            var cp620 = new ClinicSpecialization() { Name = "Эндокринология детская", Clinics = new List<Clinic>() { clinic5 }, DoctorSpecializations = new List<DoctorSpecialization>() { dp6546 } };
            var specializationList = new List<ClinicSpecialization>
            {
                cp10, cp20, cp30, cp40, cp50, cp60, cp70, cp80,  cp90, cp100,
                cp110, cp120, cp130, cp140, cp150, cp160, cp170, cp180, cp190, cp200,
                cp210, cp220, cp230, cp240, cp250, cp260, cp270, cp280, cp290, cp300,
                cp310, cp320, cp330, cp340, cp350, cp360, cp370, cp380, cp390, cp400,
                cp410, cp420, cp430, cp440, cp450, cp460, cp470, cp480, cp490, cp500,
                cp510, cp520, cp530, cp540, cp550, cp560, cp570, cp580, cp590, cp600,
                cp610, cp620,
                cp81, cp301, cp461, cp561,cp239
            };
            #endregion
            context.ClinicSpecializations.AddRange(specializationList.OrderBy(d => d.Name));

            #region Первая версия таблицы специализаций клиник (на всякий случай)
            //var profilesList1 = new List<ClinicSpecialization>
            //{
            //    new ClinicSpecialization() {Name = "Аллергология"},
            //    new ClinicSpecialization() {Name = "Аллергология детская"},
            //    new ClinicSpecialization() {Name = "Ангиохирургия"},
            //    new ClinicSpecialization() {Name = "Андрология"},
            //    new ClinicSpecialization() {Name = "Анестезиология"},
            //    new ClinicSpecialization() {Name = "Гастроэнтерология"},
            //    new ClinicSpecialization() {Name = "Гастроэнтерология детская"},
            //    new ClinicSpecialization() {Name = "Гинекология"},
            //    new ClinicSpecialization() {Name = "Гинекология-эндокринология"},
            //    new ClinicSpecialization() {Name = "Дерматология"},
            //    new ClinicSpecialization() {Name = "Диагностика лабораторная "},
            //    new ClinicSpecialization() {Name = "Диагностика ультразвуковая"},
            //    new ClinicSpecialization() {Name = "Диагностика функциональная"},
            //    new ClinicSpecialization() {Name = "Диагностика эндоскопическая"},
            //    new ClinicSpecialization() {Name = "Иглорефлексотерапевтия"},
            //    new ClinicSpecialization() {Name = "Иммунология"},
            //    new ClinicSpecialization() {Name = "Кардио-ревматология детская"},
            //    new ClinicSpecialization() {Name = "Кардиология"},
            //    new ClinicSpecialization() {Name = "Кардиология детская"},
            //    new ClinicSpecialization() {Name = "Косметология"},
            //    new ClinicSpecialization() {Name = "Логопедия"},
            //    new ClinicSpecialization() {Name = "Маммология"},
            //    new ClinicSpecialization() {Name = "Массаж"},
            //    new ClinicSpecialization() {Name = "Неврология"},
            //    new ClinicSpecialization() {Name = "Неврология детская"},
            //    new ClinicSpecialization() {Name = "Нефрология"},
            //    new ClinicSpecialization() {Name = "Нефрология детская"},
            //    new ClinicSpecialization() {Name = "Нейроофтальмология"},
            //    new ClinicSpecialization() {Name = "Онкология"},
            //    new ClinicSpecialization() {Name = "Онкология детская"},
            //    new ClinicSpecialization() {Name = "Оториноларингология(ЛОР)"},
            //    new ClinicSpecialization() {Name = "Оториноларингология(ЛОР) детская"},
            //    new ClinicSpecialization() {Name = "Офтальмология"},
            //    new ClinicSpecialization() {Name = "Офтальмология детская"},
            //    new ClinicSpecialization() {Name = "Офтальмология-эндокринология"},
            //    new ClinicSpecialization() {Name = "Педиатрия"},
            //    new ClinicSpecialization() {Name = "Проктология"},
            //    new ClinicSpecialization() {Name = "Психотерапевтия"},
            //    new ClinicSpecialization() {Name = "Пульмонология"},
            //    new ClinicSpecialization() {Name = "Реабилитология"},
            //    new ClinicSpecialization() {Name = "Реаниматология"},
            //    new ClinicSpecialization() {Name = "Ревматология"},
            //    new ClinicSpecialization() {Name = "Рентгенология"},
            //    new ClinicSpecialization() {Name = "Репродуктология"},
            //    new ClinicSpecialization() {Name = "Рефлексотерапевтия"},
            //    new ClinicSpecialization() {Name = "Сексология"},
            //    new ClinicSpecialization() {Name = "Стоматология детская"},
            //    new ClinicSpecialization() {Name = "Стоматология-ортопедия"},
            //    new ClinicSpecialization() {Name = "Стоматология-ортодонтия"},
            //    new ClinicSpecialization() {Name = "Стоматология-терапевтия"},
            //    new ClinicSpecialization() {Name = "Стоматология-хирургия"},
            //    new ClinicSpecialization() {Name = "Терапевтия"},
            //    new ClinicSpecialization() {Name = "Травматология-ортопедия"},
            //    new ClinicSpecialization() {Name = "Травматология-ортопедия детская"},
            //    new ClinicSpecialization() {Name = "Урология"},
            //    new ClinicSpecialization() {Name = "Урология детская"},
            //    new ClinicSpecialization() {Name = "Флебология"},
            //    new ClinicSpecialization() {Name = "Хирургия"},
            //    new ClinicSpecialization() {Name = "Хирургия детская"},
            //    new ClinicSpecialization() {Name = "Хирургия пластическая"},
            //    new ClinicSpecialization() {Name = "Эндокринология"},
            //    new ClinicSpecialization() {Name = "Эндокринология детская"}
            //};             
            #endregion

            var cs10 = new ClinicProfile() { Name = "Многопрофильный" };
            var cs20 = new ClinicProfile() { Name = "Многопрофильный детский", Clinics = new List<Clinic>() { clinic5 } };
            var cs30 = new ClinicProfile() { Name = "Многопрофильный лечебно-профилактический", Clinics = new List<Clinic>() { clinic4 } };
            var cs40 = new ClinicProfile() { Name = "Многопрофильный с комплексом аппаратных и аналитических обследований", Clinics = new List<Clinic>() { clinic1 } };
            var cs50 = new ClinicProfile() { Name = "Стоматология", Clinics = new List<Clinic>() { clinic2 } };
            var profilesList = new List<ClinicProfile> { cs10, cs20, cs30, cs40, cs50 };
            //context.ClinicProfiles.AddRange(profilesList);



            //test clinic List
            var testClinicsList = new List<Clinic>();
            for (var i = 1; i < 5; i++) //клиники без рейтинга
            {
                var clinic = new Clinic()
                {
                    CityAddresses = new List<CityAddress>() { new CityAddress() { City = citiesList[i % 5] } },
                    Name = "Test" + i,
                    ClinicSpecializations = new List<ClinicSpecialization>() { new ClinicSpecialization() { Name = specializationList[i % 3].Name } }
                };
                testClinicsList.Add(clinic);
            }
            for (var i = 5; i < 15; i++)//клиники с рейтингом
            {
                var clinic = new Clinic()
                {
                    CityAddresses = new List<CityAddress>() { new CityAddress () {City = citiesList[i%5]} },
                    Name = "Test" + i,
                    ClinicSpecializations = new List<ClinicSpecialization>(){ new ClinicSpecialization() { Name = specializationList[i%3].Name } },
                    RatePoliteness = rnd.Next(5) + 1,
                    RatePrice = rnd.Next(5) + 1,
                    RateQuality = rnd.Next(5) + 1
                };
                clinic.RateAverage = (clinic.RatePoliteness + clinic.RatePrice + clinic.RateQuality) /3;
                testClinicsList.Add(clinic);
            }
            context.Сlinics.AddRange(testClinicsList);
            base.Seed(context);
        }
    }
}