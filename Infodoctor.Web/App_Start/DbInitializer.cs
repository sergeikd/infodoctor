using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Infodoctor.DAL;
using Infodoctor.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using static System.String;

namespace Infodoctor.Web
{
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var dbInitializerExtention = new DbInitializerExtention();
            List<ClinicReview> clinicReviewList;
            List<DoctorReview> doctorReviewList;
            List<Clinic> clinicList;
            List<Doctor> doctorsList;
            List<Phone> phonesList;
            List<Address> clinicAddressList;
            List<City> citiesList;
            List<DoctorCategory> categoriesList;
            List<ImageFile> imagesList;
            List<Language> langs;
            //List<ImageFile> imagesList;
            dbInitializerExtention.PrepareLists(out langs, out clinicReviewList, out doctorReviewList, out clinicList, out doctorsList, out phonesList, out clinicAddressList,
                out citiesList, out categoriesList, out imagesList);
            context.Images.AddRange(imagesList);
            context.ClinicReviews.AddRange(clinicReviewList);
            context.DoctorReviews.AddRange(doctorReviewList);
            context.Сlinics.AddRange(clinicList);
            context.Doctors.AddRange(doctorsList);
            context.ClinicPhones.AddRange(phonesList);
            context.ClinicAddresses.AddRange(clinicAddressList);
            context.Cities.AddRange(citiesList);
            context.DoctorCategories.AddRange(categoriesList);
            context.Languages.AddRange(langs);

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
                UserName = "admin",
                EmailConfirmed = true
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
                UserName = "Vlad_admin",
                EmailConfirmed = true
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
                UserName = "moder",
                EmailConfirmed = true
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
                UserName = "Vlad_moder",
                EmailConfirmed = true
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
                    UserName = "user" + i,
                    EmailConfirmed = true
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
                    countriesList.Add(new Country() { LocalizedCountries = new List<LocalizedCountry>() { new LocalizedCountry() { Name = line, Language = langs.First(l => l.Code == "ru") } } });
                }
            }
            context.Countries.AddRange(countriesList);

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

            #region Новый список специализаций врачей
            /*
            var dp059 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Аллерголог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Doctors = new List<Doctor> { doc002 }
            };
            var dp148 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Аллерголог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Doctors = new List<Doctor> { doc003 }
            };
            var dp273 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Ангиохирург" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp340 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Андролог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp411 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Анестезиолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp595 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Гастроэнтеролог" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp629 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Гастроэнтеролог детский" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp720 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Гинеколог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp848 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Гинеколог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp928 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Гинеколог-эндокринолог" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1010 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Дерматолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1125 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Иглорефлексотерапевт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1246 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Иммунолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1367 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Кардиолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1460 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Кардиолог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1567 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Кардио-ревматолог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp168 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Косметолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1768 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Логопед",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp188 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Маммолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp1911 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Массажист",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Doctors = new List<Doctor> { doc001 }
            };
            var dp2087 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Нарколог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2144 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Невролог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2257 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Невролог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2393 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Нейроофтальмология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2472 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Нефролог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2595 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Нефролог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2654 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Онколог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Doctors = new List<Doctor> { doc004 }
            };
            var dp2758 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Онколог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2833 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Ортопед",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp2998 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Оториноларинголог (ЛОР)",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3027 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Оториноларинголог (ЛОР) детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3164 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Офтальмолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp320 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Офтальмолог детский" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3318 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Офтальмолог-эндокринолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3462 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Педиатр",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Doctors = new List<Doctor> { doc005 }
            };
            var dp3519 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Проктолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3656 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Психиатр",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3767 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Психолог" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp386 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Психотерапевт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp3922 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Пульмонолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4019 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Реабилитолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4180 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Реаниматолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4251 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Ревматолог" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4351 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Рентгенолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4463 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Репродуктолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4557 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Рефлексотерапевт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4698 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Сексолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4751 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Стоматолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4813 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Стоматолог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp4935 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Стоматолог-ортодонт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5029 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Стоматолог-ортопед",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp514 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Стоматолог-терапевт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5288 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Стоматолог-хирург",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5385 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Терапевт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5489 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Травматолог-ортопед",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5515 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Травматолог-ортопед детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5691 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Трихолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5779 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Уролог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5867 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Уролог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp5915 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Физиотерапевт",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp6041 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Флеболог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp6171 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Хирург",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp6287 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Хирург детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp6349 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Хирург пластический",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp641 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Эндокринолог",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var dp6546 = new DoctorSpecializationMultiLang()
            {
                LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>()
                {
                    new LocalizedDoctorSpecialization()
                    {
                        Name = "Эндокринолог детский",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
             */
            #endregion

            #region Старый список специализаций врачей
            var dp059 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Аллерголог", Language = langs.First(l => l.Code == "ru") } }, Doctors = new List<Doctor> { doc002 } };
            var dp148 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Аллерголог детский", Language = langs.First(l => l.Code == "ru") } }, Doctors = new List<Doctor> { doc003 } };
            var dp273 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Ангиохирург", Language = langs.First(l => l.Code == "ru") } } };
            var dp340 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Андролог", Language = langs.First(l => l.Code == "ru") } } };
            var dp411 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Анестезиолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp595 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Гастроэнтеролог", Language = langs.First(l => l.Code == "ru") } } };
            var dp629 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Гастроэнтеролог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp720 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Гинеколог", Language = langs.First(l => l.Code == "ru") } } };
            var dp848 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Гинеколог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp928 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Гинеколог-эндокринолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1010 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Аллерголог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1125 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Дерматолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1246 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Иммунолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1367 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Кардиолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1460 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Кардиолог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp1567 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Кардио-ревматолог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp168 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Косметолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1768 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Логопед", Language = langs.First(l => l.Code == "ru") } } };
            var dp188 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Маммолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp1911 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Массажист", Language = langs.First(l => l.Code == "ru") } }, Doctors = new List<Doctor> { doc001 } };
            var dp2087 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Нарколог", Language = langs.First(l => l.Code == "ru") } } };
            var dp2144 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Невролог", Language = langs.First(l => l.Code == "ru") } } };
            var dp2257 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Невролог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp2393 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Нейроофтальмология", Language = langs.First(l => l.Code == "ru") } } };
            var dp2472 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Нефролог", Language = langs.First(l => l.Code == "ru") } } };
            var dp2595 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Нефролог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp2654 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Онколог", Language = langs.First(l => l.Code == "ru") } }, Doctors = new List<Doctor> { doc004 } };
            var dp2758 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Онколог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp2833 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Ортопед", Language = langs.First(l => l.Code == "ru") } } };
            var dp2998 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Оториноларинголог (ЛОР)", Language = langs.First(l => l.Code == "ru") } } };
            var dp3027 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Оториноларинголог (ЛОР) детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp3164 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Офтальмолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp320 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Офтальмолог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp3318 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Офтальмолог-эндокринолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp3462 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Педиатр", Language = langs.First(l => l.Code == "ru") } }, Doctors = new List<Doctor> { doc005 } };
            var dp3519 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Проктолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp3656 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Психиатр", Language = langs.First(l => l.Code == "ru") } } };
            var dp3767 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Психолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp386 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Психотерапевт", Language = langs.First(l => l.Code == "ru") } } };
            var dp3922 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Пульмонолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4019 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Реабилитолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4180 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Реаниматолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4251 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Ревматолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4351 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Рентгенолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4463 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Репродуктолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4557 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Рефлексотерапевт", Language = langs.First(l => l.Code == "ru") } } };
            var dp4698 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Сексолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4751 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Стоматолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp4813 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Стоматолог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp4935 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Стоматолог-ортодонт", Language = langs.First(l => l.Code == "ru") } } };
            var dp5029 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Стоматолог-ортопед", Language = langs.First(l => l.Code == "ru") } } };
            var dp514 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Стоматолог-терапевт", Language = langs.First(l => l.Code == "ru") } } };
            var dp5288 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Стоматолог-хирург", Language = langs.First(l => l.Code == "ru") } } };
            var dp5385 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Терапевт", Language = langs.First(l => l.Code == "ru") } } };
            var dp5489 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Травматолог-ортопед", Language = langs.First(l => l.Code == "ru") } } };
            var dp5515 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Травматолог-ортопед детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp5691 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Трихолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp5779 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Уролог", Language = langs.First(l => l.Code == "ru") } } };
            var dp5867 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Уролог детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp5915 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Физиотерапевт", Language = langs.First(l => l.Code == "ru") } } };
            var dp6041 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Флеболог", Language = langs.First(l => l.Code == "ru") } } };
            var dp6171 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Хирург", Language = langs.First(l => l.Code == "ru") } } };
            var dp6287 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Хирург детский", Language = langs.First(l => l.Code == "ru") } } };
            var dp6349 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Хирург пластический", Language = langs.First(l => l.Code == "ru") } } };
            var dp641 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Эндокринолог", Language = langs.First(l => l.Code == "ru") } } };
            var dp6546 = new DoctorSpecialization() { LocalizedDoctorSpecializations = new List<LocalizedDoctorSpecialization>() { new LocalizedDoctorSpecialization() { Name = "Эндокринолог детский", Language = langs.First(l => l.Code == "ru") } } };
            #endregion


            var doctorSpecialization = new List<DoctorSpecialization>()
            {
                dp059, dp148, dp273, dp340, dp411, dp595, dp629, dp720, dp848, dp928, dp1010, dp1125, dp1246, dp1367, dp1460, dp1567, dp168, dp1768, dp188, dp1911, dp2087,
                dp2144, dp2257, dp2393, dp2472, dp2595, dp2654, dp2758, dp2833, dp2998, dp3027, dp3164, dp320, dp3318, dp3462, dp3519, dp3656, dp3767, dp386, dp3922, dp4019,
                dp4180, dp4251, dp4351, dp4463, dp4557, dp4698, dp4751, dp4813, dp4935, dp5029, dp514, dp5288, dp5385, dp5489, dp5515, dp5691, dp5779, dp5867, dp5915, dp6041,
                dp6171, dp6287, dp6349, dp641, dp6546

            };
            context.DoctorSpecializations.AddRange(doctorSpecialization.OrderBy(d => d.Id));

            #region Очень длинный список специализаций клиник
            var cp10 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Аллергология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp059 }
            };
            var cp20 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Аллергология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp148 }
            };
            var cp30 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Ангиохирургия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp273 }
            };
            var cp40 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Андрология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp340 }
            };
            var cp50 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Анестезиология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp411 }
            };
            var cp60 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Гастроэнтерология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp595 }
            };
            var cp70 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Гастроэнтерология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp629 }
            };
            var cp80 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Гинекология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3, clinic4 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp720 }
            };
            var cp81 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Гинекология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp848 }
            };
            var cp90 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Гинекология-эндокринология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp928 }
            };
            var cp100 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Дерматология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1010, dp5691 }
            };
            var cp110 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Диагностика лабораторная ",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var cp120 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Диагностика ультразвуковая",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var cp130 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Диагностика функциональная" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var cp140 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Диагностика эндоскопическая" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                }
            };
            var cp150 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Иглорефлексотерапевтия" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1125 }
            };
            var cp160 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Иммунология" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1246 }
            };
            var cp170 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Кардио-ревматология детская" ,
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1567 }
            };
            var cp180 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Кардиология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic4 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1367 }
            };
            var cp190 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Кардиология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1460 }
            };
            var cp200 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Косметология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp168 }
            };
            var cp210 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Логопедия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1768 }
            };
            var cp220 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Маммология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp188 }
            };
            var cp230 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Массаж",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp1911 }
            };
            var cp239 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Наркология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2087 }
            };
            var cp240 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Неврология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2144 }
            };
            var cp250 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Неврология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2257 }
            };
            var cp260 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Нефрология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2472 }
            };
            var cp270 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Нефрология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2595 }
            };
            var cp280 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Нейроофтальмология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2393 }
            };
            var cp290 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Онкология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2654 }
            };
            var cp300 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Онкология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2758 }
            };
            var cp301 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Ортопедия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2833 }
            };
            var cp310 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Оториноларингология(ЛОР)",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp2998 }
            };
            var cp320 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Оториноларингология(ЛОР) детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3027 }
            };
            var cp330 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Офтальмология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3164 }
            };
            var cp340 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Офтальмология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp320 }
            };
            var cp350 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Офтальмология-эндокринология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3318 }
            };
            var cp360 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Педиатрия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic4 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3462 }
            };
            var cp370 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Проктология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3519 }
            };
            var cp380 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Психотерапия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3656, dp3767, dp386 }
            };
            var cp390 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Пульмонология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp3922 }
            };
            var cp400 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Реабилитология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4019 }
            };
            var cp410 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Реаниматология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4180 }
            };
            var cp420 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Ревматология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4251 }
            };
            var cp430 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Рентгенология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4351 }
            };
            var cp440 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Репродуктология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4463 }
            };
            var cp450 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Рефлексотерапевтия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4557 }
            };
            var cp460 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Сексология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4698 }
            };
            var cp461 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Стоматология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic2, clinic3, clinic4 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4751 }
            };
            var cp470 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Стоматология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic2 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4813 }
            };
            var cp480 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Стоматология-ортодонтия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic2 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp4935 }
            };
            var cp490 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Стоматология-ортопедия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic2 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5029 }
            };
            var cp500 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Стоматология-терапевтия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic2 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp514 }
            };
            var cp510 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Стоматология-хирургия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5288 }
            };
            var cp520 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Терапия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3, clinic4 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5385 }
            };
            var cp530 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Травматология-ортопедия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5489 }
            };
            var cp540 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Травматология-ортопедия детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5515 }
            };
            var cp550 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Урология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3, clinic4, clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5779 }
            };
            var cp560 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Урология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5867 }
            };
            var cp561 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Физиотерпапия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp5915 }
            };
            var cp570 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Флебология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp6041 }
            };
            var cp580 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Хирургия",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic3, clinic4 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp6171 }
            };
            var cp590 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Хирургия детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp6287 }
            };
            var cp600 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Хирургия пластическая",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic3 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp6349 }
            };
            var cp610 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Эндокринология",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic1, clinic3, clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp641 }
            };
            var cp620 = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = new List<LocalizedClinicSpecialization>()
                {
                    new LocalizedClinicSpecialization()
                    {
                        Name = "Эндокринология детская",
                        Language = langs.First(l=>l.Code=="ru")
                    }
                },
                Clinics = new List<Clinic>() { clinic5 },
                DoctorSpecializations = new List<DoctorSpecialization>() { dp6546 }
            };
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
            context.ClinicSpecializations.AddRange(specializationList.OrderBy(d => d.Id));

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
            //        ClinicAddresses = new List<Address>() { new Address() { City = citiesList[i % 5] } },
            //        Name = "Test" + i,
            //        ClinicSpecializations = new List<ClinicSpecialization>() { new ClinicSpecialization() { Name = specializationList[i % 3].Name } }
            //    };
            //    testClinicsList.Add(clinic);
            //}
            //for (var i = 5; i < 15; i++)//клиники с рейтингом
            //{
            //    var clinic = new Clinic()
            //    {
            //        ClinicAddresses = new List<Address>() { new Address () {City = citiesList[i%5]} },
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

            var art1 = new Article()
            {
                Content = @"<p><img src = ""http://infodoctor.somee.com/Content/Images/Article/a1446be69b8a47d0b37083cf761124ae.jpg""></p>< p>Процедура экстракорпорального оплодотворения (ЭКО)&nbsp;&ndash;вспомогательная репродуктивная технология, при которой отдельные этапы зачатия и развития эмбриона происходят вне организма женщины.&nbsp;Малыши, появившиеся на свет, в результате&nbsp;применения метода ЭКО&nbsp;ничем не отличаются от детей, зачатых естественным путем.&nbsp;&nbsp;</p><p>Клиническими показаниями к проведению&nbsp;оплодотворения данным методом&nbsp;&nbsp;являются&nbsp; нарушения проходимости маточных труб,&nbsp;эндометриоз,&nbsp;поликистоз&nbsp;яичников, мужское и возрастное бесплодие, а также бесплодие&nbsp;по неустановленной причине.&nbsp;&nbsp;</p><p>Существует&nbsp;несколько видов&nbsp;экстракорпорального оплодотворения.&nbsp;Наиболее частый&nbsp;&ndash; искусственная&nbsp;инсеминация, который представляет собой&nbsp;введение концентрированной спермы&nbsp;в&nbsp;цервикальный канал или&nbsp;полость матки&nbsp;в период овуляции.&nbsp;Далее&nbsp;сперматозоиды естественным путем проходят по маточным трубам и оплодотворяют яйцеклетку.&nbsp;Искусственную&nbsp;инсеминацию&nbsp;используют в случае&nbsp;полного или частичного&nbsp;мужского бесплодия.&nbsp;</p><p>Гораздо более сложным видом является так называемый &laquo;протокол ЭКО&raquo;, при котором оплодотворение&nbsp;яйцеклеток&nbsp;происходит&nbsp;вне организма женщины, а сформированный впоследствии эмбрион подсаживают&nbsp;в полость матки. Он&nbsp;эффективен при всех видах бесплодия, однако требует длительного подготовительного периода, в ходе которого происходит&nbsp;поэтапное&nbsp;введение&nbsp;лекарственных средств, при помощи&nbsp;чего&nbsp;в яичниках женщины созревает и&nbsp;овулируется&nbsp;сразу несколько яйцеклеток.&nbsp;Это необходимо&nbsp;в целях увеличения шансов наступления беременности.&nbsp;&nbsp;</p><p>Существуют факторы, при которых&nbsp;экстракорпоральное оплодотворение&nbsp;не&nbsp;может быть проведено&nbsp;&ndash; это&nbsp;наличие&nbsp;патологии развития матки,&nbsp;онкологических, воспалительных и инфекционных заболеваний,&nbsp;сахарного диабета,&nbsp;почечной недостаточности, пороков сердца,&nbsp;психических заболеваний.&nbsp;&nbsp;</p><p>Важно помнить, что любое медицинское вмешательство сопряжено с определенными рисками.&nbsp;Так,&nbsp;в ходе проведения ЭКО&nbsp;возможно развитие аллергической реакции на&nbsp;применяемые&nbsp;лекарственные средства.&nbsp;&nbsp;Осложнением процедуры считается также увеличения&nbsp;шанса&nbsp;развития многоплодной беременности, что&nbsp;в свою очередь может привести к преждевременным родам или замиранию беременности. Усиленная гормональная нагрузка, которой подвергается организм женщины при проведении процедуры, может привести к снижению иммунитета.&nbsp;Необходимо&nbsp;также&nbsp;знать, что с возрастом эффективность данного метода&nbsp;значительно снижается: так, после 45 лет&nbsp; успешность проведения процедуры равна&nbsp;всего лишь 1,5 %.&nbsp;&nbsp;</p><p>В настоящее время услуги экстракорпорального оплодотворения предоставляют&nbsp;центры репродуктивной медицины, частные медицинские клиники, республиканские научно-практические центры.&nbsp;</p><p><img src = ""http://infodoctor.somee.com/Content/Images/Article/86f2b071c4e1463d9454c084b1ead451.jpg""></p>",
                Title = "Метод ЭКО. Что нужно знать?",
                PublishDate = DateTime.Now.AddDays(-1),
                Author = "admin"
            };
            var art2 = new Article()
            {
                Content = @"<p><img src = ""http://infodoctor.somee.com/Content/Images/Article/4f4fe30e48b74d6288b512d5c660197e.jpg""></p><p>Остеомиелит (от греч.&nbsp;''воспаление костного мозга'')&nbsp;&minus;&nbsp;инфекционное заболевание, поражающее костную ткань и костной мозг и сопровождающееся гнойно-воспалительным процессом, вследствие чего развивается общая интоксикация организма. Бактериальными возбудителями заболевания являются стрептококки и стафилококки, которые могут проникать в организм несколькими путями: вследствие загрязнения мягких тканей при открытой травме, ранении или с током крови по кровеносным сосудам при наличии в организме хронической инфекции. Предрасполагающими факторами возникновения остеомиелита являются наличие онкологического заболевания, сахарного диабета, нарушения функций печени и почек, а также злоупотребление алкоголем, курение, плохое питание. Различают 3 клинические формы остеомиелита, в зависимости от чего выделяют и различные проявления заболевания.&nbsp;</p><p>&nbsp;Первыми симптомами септико-пиемической формы является повышение температуры, слабость, озноб, тошнота, наблюдается увеличение печени и селезенки. Спустя 1-2 суток появляется сильная боль, отечность, покраснение в области пораженной кости, формируется гнойное воспаление. При дальнейшем развитии заболевания велик риск возникновения межмышечной флегмоны. Местная форма остеомиелита при своевременном начале лечения характеризуется наиболее легким течением и проявляется в преобладании симптомов костного поражения при удовлетворительном общем самочувствии. Наиболее редкой и опасной формой остеомиелита является&nbsp;токсическая. Заболевание развивается в кратчайшие сроки и характеризуется менингеальными симптомами, судорогами, потерей сознания, значительным снижением артериального давления, велик риск развития острой&nbsp;сердечно-сосудистой&nbsp;недостаточности. Опасность токсической формы заключается также и в сложности диагностирования заболевания вследствие отсутствия на начальном этапе специфических симптомов костного поражения.&nbsp;&nbsp;</p><p>Однако нужно знать, что, несмотря на быстрое развитие и зачастую тяжелое течение остеомиелита, своевременная диагностика и правильное комплексное лечение позволяют избежать его прогрессирования, снижают&nbsp;риск развития возможных осложнений, перехода заболевания в хроническую форму.&nbsp;</p><p>Диагностика остеомиелита осуществляется на основании осмотра, сбора анамнеза, а также при помощи лабораторных и инструментальных методов. С этой целью применяется рентгенография, компьютерная и магниторезонансная томография, УЗИ, пункция кости. Наличие воспалительного процесса в организме определяют по данным общего анализа крови и мочи, биохимического анализа крови.&nbsp;</p><p>Лечение заболевания длительное, в некоторых случаях курс может занимать около&nbsp; полугода, однако прогноз полного выздоровления, при условии своевременного начала лечения, благоприятный. Лечение остеомиелита осуществляется только в условиях стационара больничных организаций здравоохранения, куда пациент направляется с предварительным или установленным ранее диагнозом. Обязательным является проведение курса антибиотикотерапии в сочетании с хирургическим вмешательством, которое необходимо при развитии гнойных процессов, появлении свищей. Основные этапы оперативного лечения&nbsp;&minus;&nbsp;санация пораженного участка, удаление отмерших тканей, проведение дренажа. Немаловажным в период лечения является ограничение двигательной активности, проведение&nbsp;иммуностимуляции, а также правильное питание. Особое внимание стоит уделить продуктам с высоким содержанием белка, железа и кальция.&nbsp;&nbsp;</p><p>По окончании лечения с целью тонизирования организма, восстановления функционирования пораженной части тела рекомендованы физиотерапевтические процедуры и лечебная физкультура. Весьма эффективны в период реабилитации электрофорез,&nbsp;магнитотерапия, парафинотерапия, инфракрасный лазер.&nbsp;</p><p>Важно помнить, что любое заболевание легче предупредить, чем лечить! Профилактикой остеомиелита является своевременное лечение очагов инфекции, немедленное обращение за медицинской помощью в случае травм, переломов, правильное питание, отказ от вредных привычек. При ухудшении самочувствия, появлении первых признаков заболевания необходимо срочно обращаться к врачу, не заниматься самолечением: это может привести к необратимым последствиям!&nbsp;</p>",
                Title = "Остеомиелит не приговор!",
                PublishDate = DateTime.Now.AddDays(-1),
                Author = "admin"
            };
            var articles = new List<Article>() { art1, art2 };

            context.Articles.AddRange(articles.OrderBy(d => d.PublishDate));
            var userName = userManager.Users.First(u => u.UserName == "admin");
            var comment1 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art1
            };
            var comment2 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art1
            };
            var comment3 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art2
            };
            var comment4 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art2
            };
            var comments = new List<ArticleComment>() { comment1, comment2, comment3, comment4 };

            context.ArticleComments.AddRange(comments.OrderBy(d => d.PublishTime));

            var resortRevs = new List<ResortReview>();

            var rnd = new Random();
            var ticks = DateTime.Now.Ticks - 100000000000000;
            for (var i = 0; i < 126; i++)
            {
                resortRevs.Add(new ResortReview()
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

            /*

            var resorts = new List<Resort>();
            var resAdrs = new List<ResortAddress>();
            var resPhones = new List<ResortPhone>();

            //санаторий 1
            var nums1 = new List<ResortPhone>()
            {
                new ResortPhone() { Description = "", Number = "375(1641) 38-2-19" },
                new ResortPhone() { Description = "Факс", Number = "375(1641) 38-2-22" },
                new ResortPhone() { Description = "Мтс", Number = "375 (29) 866-86-69" },
                new ResortPhone() { Description = "", Number = "375 (29) 366-86-67" }
            };
            resPhones.AddRange(nums1);

            var adr1 = new ResortAddress()
            {
                Country = "Беларусь",
                City = citiesList.First(c => string.Equals(c.Name, "Брестская область",
                    StringComparison.CurrentCultureIgnoreCase)),
                Street = "урочище \"Сосновый бор\"",
                Phones = nums1
            };
            resAdrs.Add(adr1);


            var resortReviewSubList = resortRevs.Take(3).ToList();

            var resort1 = new Resort()
            {
                Name = "Санаторий \"Буг\"",
                Email = "bug-marketing@mail.ru",
                Site = "http://sunbug.by/",
                Specialisations = @"Медицинская база, Диагностическая база, Лечебные комплексы",
                Address = adr1,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort1);

            //санаторий 2
            var nums2 = new List<ResortPhone>()
            {
                new ResortPhone() { Description = "для звонков из РБ", Number = "8 (01641) 68-222" },
                new ResortPhone() { Description = "для звонков из РБ", Number = "8 (01641) 68-333" },
                new ResortPhone() { Description = "для звонков из РФ", Number = "8 10 (375 1641) 68-222" },
                new ResortPhone() { Description = "для звонков из РФ", Number = "8 10 (375 1641) 68-333" }
            };
            resPhones.AddRange(nums2);

            var adr2 = new ResortAddress()
            {
                Country = "Беларусь",
                City = citiesList.First(c => string.Equals(c.Name, "Брестская область",
                    StringComparison.CurrentCultureIgnoreCase)),
                Street = "Жабинковский район, 1,6 км севернее д. Чижевщина",
                Phones = nums2
            };
            resAdrs.Add(adr2);

            resortReviewSubList = resortRevs.Skip(3).Take(3).ToList();

            var resort2 = new Resort()
            {
                Name = "Санаторий \"Надзея\"",
                Email = "mtv@brest.gas.by, nadzeya@brest.gas.by, san@brest.gas.by",
                Site = "http://www.nadzeya.com/",
                Specialisations = @"Водолечение, ЛФК Галотерапия, Электросветолечение, Теплолечение, Ручной массаж, Фиточай, Небулайзерная ингаляционная терапия, Галотерапия, Косметология, УЗИ",
                Address = adr2,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort2);

            //санаторий 3
            var nums3 = new List<ResortPhone>()
            {
                new ResortPhone() { Description = "международный", Number = "8 (10-375-212) 29 72 39" },
                new ResortPhone() { Description = "международный", Number = "8 (10-375-212) 29 73 35" },
                new ResortPhone() { Description = "международный", Number = "8 (10-375-212) 29 73 24" },
                new ResortPhone() { Description = "по Беларуси", Number = "8 (10-375-212) 29 72 39" },
                new ResortPhone() { Description = "по Беларуси", Number = "8 (10-375-212) 29 73 35" },
                new ResortPhone() { Description = "по Беларуси", Number = "8 (10-375-212) 29 73 24" },
            };
            resPhones.AddRange(nums3);

            var adr3 = new ResortAddress()
            {
                Country = "Беларусь",
                City = citiesList.First(c => string.Equals(c.Name, "Витебская область",
                    StringComparison.CurrentCultureIgnoreCase)),
                Street = "Витебский район, д. Малые ",
                Phones = nums3
            };
            resAdrs.Add(adr3);

            resortReviewSubList = resortRevs.Skip(6).Take(3).ToList();

            var resort3 = new Resort()
            {
                Name = "Санаторий \"Лётцы\"",
                Email = "letzy1@mail.ru",
                Site = "http://letcy.ru/",
                Specialisations = @"Диетотерапия, Бальнеолечение, Теплолечение, Аппаратная физиотерапия, Массаж, Климатолечение, Галотерапия, Рефлексотерапия, Лечебная физкультура, Космтология, Спа, УЗД, УЗИ",
                Address = adr3,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort3);

            //санаторий 4
            var nums4 = new List<ResortPhone>()
            {
                new ResortPhone() { Description = "", Number = "375 2157 33463" },
                new ResortPhone() { Description = "", Number = "375 29 3442040" },
                new ResortPhone() { Description = "", Number = "375 2157 33458" }
            };
            resPhones.AddRange(nums4);

            var adr4 = new ResortAddress()
            {
                Country = "Беларусь",
                City = citiesList.First(c => string.Equals(c.Name, "Витебская область",
                    StringComparison.CurrentCultureIgnoreCase)),
                Street = "д. Будачи, Докшицкий р-н",
                Phones = nums4
            };
            resAdrs.Add(adr4);

            resortReviewSubList = resortRevs.Skip(9).Take(3).ToList();

            var resort4 = new Resort()
            {
                Name = "Санаторий \"Боровое\"",
                Email = "",
                Site = "http://www.sanatorium-borovoe.com/",
                Specialisations = @" магнитно-резонансная томография
                стоматология терапевтическая(за исключением помощи при острой зубной боли и консультации врача - стоматолога)
                рентгенология стоматологическая
                мини - сауна «Кедровая бочка»
                локальная криотерапия лица «Криоджет»
                ванны грязевые сапропелевые
                ванны с пантогематогеном
                косметические услуги
                бильярд
                аквамассаж(аквакапсула «Aqua -PT Pro Turbo»);
                АПК «Андро - Гин«;
                ароматерапия;
                бальнеолечение;
                галотерапия(спелеолечение);
                гидромассаж рук и ног(аппарат «Aquaroll Pro»);
                гидротерапия кишечника
                грязелечение;
                иглорефлексотерапия;
                ингаляции;
                карбокситерапия;
                криотерапия локальная;
                ЛФК;
                массаж аппаратный;
                массаж ручной;
                общая магнитотерапия -ОМТ(аппарат «Магнитотурботрон ЭОЛ»);
                озонотерапия;
                озокерито - парафинолечение;
                питьевое лечение минеральными водами(собственные уникальные минеральные источники);
                психологическая разгрузка;
                пневмокомпрессионная терапия(аппарат «LYMPHA -MAT DIGITAL GRADIENT»);
                СПА - терапия(СПА - капсула «NEoQi»);
                сухие углекислые ванны;
                сухой флоатинг(аппарат «NUVOLA SPA JET»);
                ударно - волновая терапия экстракорпоральная(аппарат «EMS Swiss DolorClast»);
                физиотерапевтическое лечение;
                фитотерапия;
                сауна(финская, турецкая, инфракрасная);
                электросон;
                Диагностические процедуры:

                ультразвуковая диагностика;
                функциональная диагностика;
                фиброгастродуаденоскопия;
                ректосигмоскопия, колоноскопия; ",
                Address = adr4,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort4);


            context.ResortPhones.AddRange(resPhones);
            context.ResortAddresses.AddRange(resAdrs);
            context.Resorts.AddRange(resorts);

    */

            base.Seed(context);
        }
    }
}