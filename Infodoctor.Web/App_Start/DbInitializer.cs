﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Infodoctor.DAL;
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
            var adminVlad = new ApplicationUser
            {
                Email = "v.korbut8@gmail.com",
                UserName = "Vlad_admin"
            };
            password = "1234qw";
            result = userManager.Create(adminVlad, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(adminVlad.Id, role2.Name);
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
            var moderVlad = new ApplicationUser
            {
                Email = "asdrudes@gmail.com",
                UserName = "Vlad_moder"
            };
            password = "1234qw";
            result = userManager.Create(moderVlad, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(moderVlad.Id, role3.Name);
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

            var dbInitializerExtention = new DbInitializerExtention();
            List<ClinicReview> clinicReviewList;
            List<DoctorReview> doctorsReviewList;
            List<Clinic> clinicList;
            List<Doctor> doctorsList;
            List<ClinicPhone> phonesList;
            List<CityAddress> clinicAddressList;
            List<City> citiesList;
            List<DoctorCategory> categoriesList;
            dbInitializerExtention.PrepareLists(out clinicReviewList, out doctorsReviewList, out clinicList, out doctorsList, out phonesList, out clinicAddressList, out citiesList, out categoriesList);
            context.ClinicReviews.AddRange(clinicReviewList);
            context.DoctorReviews.AddRange(doctorsReviewList);
            context.Сlinics.AddRange(clinicList);
            context.Doctors.AddRange(doctorsList);
            context.ClinicPhones.AddRange(phonesList);
            context.CityAddresses.AddRange(clinicAddressList);
            context.Cities.AddRange(citiesList);
            context.DoctorCategories.AddRange(categoriesList);

            var clinic1 = clinicList[0];
            var clinic2 = clinicList[1];
            var clinic3 = clinicList[2];
            var clinic4 = clinicList[3];
            var clinic5 = clinicList[4];

            var doc001 = doctorsList[0];
            var doc002 = doctorsList[1];
            var doc003 = doctorsList[2];
            var doc004 = doctorsList[3];
            var doc005 = doctorsList[4];

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
                dp059, dp148, dp273, dp340, dp411, dp595, dp629, dp720, dp848, dp928, dp1010, dp1125, dp1246, dp1367, dp1460, dp1567, dp168, dp1768, dp188, dp1911, dp2087,
                dp2144, dp2257, dp2393, dp2472, dp2595, dp2654, dp2758, dp2833, dp2998, dp3027, dp3164, dp320, dp3318, dp3462, dp3519, dp3656, dp3767, dp386, dp3922, dp4019,
                dp4180, dp4251, dp4351, dp4463, dp4557, dp4698, dp4751, dp4813, dp4935, dp5029, dp514, dp5288, dp5385, dp5489, dp5515, dp5691, dp5779, dp5867, dp5915, dp6041,
                dp6171, dp6287, dp6349, dp641, dp6546

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

            //var aaa = specializationList.OrderBy(d => d.Name);
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

            //var cs10 = new ClinicProfile() { Name = "Многопрофильный" };
            //var cs20 = new ClinicProfile() { Name = "Многопрофильный детский", Clinics = new List<Clinic>() { clinic5 } };
            //var cs30 = new ClinicProfile() { Name = "Многопрофильный лечебно-профилактический", Clinics = new List<Clinic>() { clinic4 } };
            //var cs40 = new ClinicProfile() { Name = "Многопрофильный с комплексом аппаратных и аналитических обследований", Clinics = new List<Clinic>() { clinic1 } };
            //var cs50 = new ClinicProfile() { Name = "Стоматология", Clinics = new List<Clinic>() { clinic2 } };
            //var profilesList = new List<ClinicProfile> { cs10, cs20, cs30, cs40, cs50 };
            //context.ClinicProfiles.AddRange(profilesList);



            ////test clinic List
            //var testClinicsList = new List<Clinic>();
            //for (var i = 1; i < 5; i++) //клиники без рейтинга
            //{
            //    var clinic = new Clinic()
            //    {
            //        CityAddresses = new List<CityAddress>() { new CityAddress() { City = citiesList[i % 5] } },
            //        Name = "Test" + i,
            //        ClinicSpecializations = new List<ClinicSpecialization>() { new ClinicSpecialization() { Name = specializationList[i % 3].Name } }
            //    };
            //    testClinicsList.Add(clinic);
            //}
            //for (var i = 5; i < 15; i++)//клиники с рейтингом
            //{
            //    var clinic = new Clinic()
            //    {
            //        CityAddresses = new List<CityAddress>() { new CityAddress () {City = citiesList[i%5]} },
            //        Name = "Test" + i,
            //        ClinicSpecializations = new List<ClinicSpecialization>(){ new ClinicSpecialization() { Name = specializationList[i%3].Name } },
            //        RatePoliteness = rnd.Next(5) + 1,
            //        RatePrice = rnd.Next(5) + 1,
            //        RateQuality = rnd.Next(5) + 1
            //    };
            //    clinic.RateAverage = (clinic.RatePoliteness + clinic.RatePrice + clinic.RateQuality) /3;
            //    testClinicsList.Add(clinic);
            //}
            //context.Сlinics.AddRange(testClinicsList);
            base.Seed(context);
        }
    }
}