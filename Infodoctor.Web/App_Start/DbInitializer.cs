using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using Infodoctor.DAL;
using Infodoctor.Domain;
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

            var clinic1 = new Clinic()
            {   Email = "info@nordin.by",
                Name = "Медицинский центр Нордин"};

            var clinic2 = new Clinic()
            {
                Email = string.Empty,
                Name = "Стоматологический центр Дентко"};

            var clinic3 = new Clinic()
            {
                Email = "kravira@kravira.by",
                Name = "Медицинский центр Кравира"
            };
            var clinic4 = new Clinic()
            {
                Email = "medic4@tut.by",
                Name = "4-я городская поликлиника г.Минска"};

            var clinic5 = new Clinic()
            {
                Email = string.Empty,
                Name = "2-я городская детская клиническая больница» г. Минска"};

            var clinicList = new List<Clinic> {clinic1, clinic2, clinic4, clinic5};
            context.Сlinics.AddRange(clinicList);
            
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
            var ca1 = new CityAddress() { Street = "ул.Сурганова 47Б", Clinic = clinic1, ClinicPhones = new List<ClinicPhone>() {phone1, phone2} };
            var ca2 = new CityAddress() { Street = "пр-т. Независимости 58", Clinic = clinic2, ClinicPhones = new List<ClinicPhone>() { phone3 } };
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
            var city66 = new City() { Name = "Минск", Addresses = new List<CityAddress>() { ca1, ca2, ca3, ca4, ca5, ca6, ca7 } };
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

            #region Очень длинный список специализаций клиник
            var cp10 = new ClinicSpecialization() { Name = "Аллергология", Clinics = new List<Clinic>() {clinic1}};
            var cp20 = new ClinicSpecialization() { Name = "Аллергология детская"};
            var cp30 = new ClinicSpecialization() { Name = "Ангиохирургия"};
            var cp40 = new ClinicSpecialization() { Name = "Андрология"};
            var cp50 = new ClinicSpecialization() { Name = "Анестезиология"};
            var cp60 = new ClinicSpecialization() { Name = "Гастроэнтерология", Clinics = new List<Clinic>() { clinic1 } };
            var cp70 = new ClinicSpecialization() { Name = "Гастроэнтерология детская"};
            var cp80 = new ClinicSpecialization() { Name = "Гинекология", Clinics = new List<Clinic>() { clinic1, clinic3, clinic4 } };
            var cp81 = new ClinicSpecialization() { Name = "Гинекология детская", Clinics = new List<Clinic>() { clinic1 } };
            var cp90 = new ClinicSpecialization() { Name = "Гинекология-эндокринология"};
            var cp100 = new ClinicSpecialization() { Name = "Дерматология", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp110 = new ClinicSpecialization() { Name = "Диагностика лабораторная "};
            var cp120 = new ClinicSpecialization() { Name = "Диагностика ультразвуковая"};
            var cp130 = new ClinicSpecialization() { Name = "Диагностика функциональная"};
            var cp140 = new ClinicSpecialization() { Name = "Диагностика эндоскопическая"};
            var cp150 = new ClinicSpecialization() { Name = "Иглорефлексотерапевтия"};
            var cp160 = new ClinicSpecialization() { Name = "Иммунология"};
            var cp170 = new ClinicSpecialization() { Name = "Кардио-ревматология детская"};
            var cp180 = new ClinicSpecialization() { Name = "Кардиология", Clinics = new List<Clinic>() { clinic1, clinic4 } };
            var cp190 = new ClinicSpecialization() { Name = "Кардиология детская"};
            var cp200 = new ClinicSpecialization() { Name = "Косметология", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp210 = new ClinicSpecialization() { Name = "Логопедия", Clinics = new List<Clinic>() { clinic1 } };
            var cp220 = new ClinicSpecialization() { Name = "Маммология", Clinics = new List<Clinic>() { clinic3 } };
            var cp230 = new ClinicSpecialization() { Name = "Массаж", Clinics = new List<Clinic>() { clinic3 } };
            var cp240 = new ClinicSpecialization() { Name = "Неврология", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp250 = new ClinicSpecialization() { Name = "Неврология детская"};
            var cp260 = new ClinicSpecialization() { Name = "Нефрология", Clinics = new List<Clinic>() { clinic5 } };
            var cp270 = new ClinicSpecialization() { Name = "Нефрология детская", Clinics = new List<Clinic>() { clinic5 } };
            var cp280 = new ClinicSpecialization() { Name = "Нейроофтальмология"};
            var cp290 = new ClinicSpecialization() { Name = "Онкология", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp300 = new ClinicSpecialization() { Name = "Онкология детская"};
            var cp301 = new ClinicSpecialization() { Name = "Ортопедия", Clinics = new List<Clinic>() { clinic1 } };
            var cp310 = new ClinicSpecialization() { Name = "Оториноларингология(ЛОР)", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp320 = new ClinicSpecialization() { Name = "Оториноларингология(ЛОР) детская"};
            var cp330 = new ClinicSpecialization() { Name = "Офтальмология", Clinics = new List<Clinic>() { clinic1 } };
            var cp340 = new ClinicSpecialization() { Name = "Офтальмология детская"};
            var cp350 = new ClinicSpecialization() { Name = "Офтальмология-эндокринология"};
            var cp360 = new ClinicSpecialization() { Name = "Педиатрия", Clinics = new List<Clinic>() { clinic1, clinic4 } };
            var cp370 = new ClinicSpecialization() { Name = "Проктология", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp380 = new ClinicSpecialization() { Name = "Психотерапия", Clinics = new List<Clinic>() { clinic1, clinic3 } };
            var cp390 = new ClinicSpecialization() { Name = "Пульмонология"};
            var cp400 = new ClinicSpecialization() { Name = "Реабилитология"};
            var cp410 = new ClinicSpecialization() { Name = "Реаниматология"};
            var cp420 = new ClinicSpecialization() { Name = "Ревматология"};
            var cp430 = new ClinicSpecialization() { Name = "Рентгенология"};
            var cp440 = new ClinicSpecialization() { Name = "Репродуктология"};
            var cp450 = new ClinicSpecialization() { Name = "Рефлексотерапевтия"};
            var cp460 = new ClinicSpecialization() { Name = "Сексология"};
            var cp461 = new ClinicSpecialization() { Name = "Стоматология", Clinics = new List<Clinic>() { clinic2, clinic3, clinic4 } };
            var cp470 = new ClinicSpecialization() { Name = "Стоматология детская", Clinics = new List<Clinic>() { clinic2 } };
            var cp480 = new ClinicSpecialization() { Name = "Стоматология-ортодонтия", Clinics = new List<Clinic>() { clinic2 } };
            var cp490 = new ClinicSpecialization() { Name = "Стоматология-ортопедия", Clinics = new List<Clinic>() { clinic2 } };
            var cp500 = new ClinicSpecialization() { Name = "Стоматология-терапевтия", Clinics = new List<Clinic>() { clinic2 } };
            var cp510 = new ClinicSpecialization() { Name = "Стоматология-хирургия"};
            var cp520 = new ClinicSpecialization() { Name = "Терапия", Clinics = new List<Clinic>() { clinic1, clinic3, clinic4 } };
            var cp530 = new ClinicSpecialization() { Name = "Травматология-ортопедия" };
            var cp540 = new ClinicSpecialization() { Name = "Травматология-ортопедия детская"};
            var cp550 = new ClinicSpecialization() { Name = "Урология", Clinics = new List<Clinic>() { clinic1, clinic3, clinic4, clinic5 } };
            var cp560 = new ClinicSpecialization() { Name = "Урология детская", Clinics = new List<Clinic>() { clinic1, clinic5 } };
            var cp561 = new ClinicSpecialization() { Name = "Физиотерпапия", Clinics = new List<Clinic>() { clinic1, clinic5 } };
            var cp570 = new ClinicSpecialization() { Name = "Флебология"};
            var cp580 = new ClinicSpecialization() { Name = "Хирургия", Clinics = new List<Clinic>() { clinic3, clinic4 } };
            var cp590 = new ClinicSpecialization() { Name = "Хирургия детская"};
            var cp600 = new ClinicSpecialization() { Name = "Хирургия пластическая", Clinics = new List<Clinic>() { clinic3 } };
            var cp610 = new ClinicSpecialization() { Name = "Эндокринология", Clinics = new List<Clinic>() { clinic1, clinic3, clinic5 } };
            var cp620 = new ClinicSpecialization() { Name = "Эндокринология детская", Clinics = new List<Clinic>() { clinic5 } };
            var specializationList = new List<ClinicSpecialization>
            {
                cp10, cp20, cp30, cp40, cp50, cp60, cp70, cp80,  cp90, cp100,
                cp110, cp120, cp130, cp140, cp150, cp160, cp170, cp180, cp190, cp200,
                cp210, cp220, cp230, cp240, cp250, cp260, cp270, cp280, cp290, cp300,
                cp310, cp320, cp330, cp340, cp350, cp360, cp370, cp380, cp390, cp400,
                cp410, cp420, cp430, cp440, cp450, cp460, cp470, cp480, cp490, cp500,
                cp510, cp520, cp530, cp540, cp550, cp560, cp570, cp580, cp590, cp600,
                cp610, cp620,
                cp81, cp301, cp461, cp561
            };
            #endregion
            context.ClinicSpecializations.AddRange(specializationList);

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


            base.Seed(context);
        }
    }
}