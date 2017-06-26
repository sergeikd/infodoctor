using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var dbInitializerExtention = new DbInitializerExtention();
            List<ClinicReview> clinicReviewList;
            List<DoctorReview> doctorReviewList;
            List<Clinic> clinicList;
            List<Doctor> doctorsList;
            List<Phone> phonesList;
            List<Address> clinicAddressList;
            List<Country> countriesList;
            List<City> belarusCitiesList;
            List<DoctorCategory> categoriesList;
            List<ImageFile> imagesList;
            List<Language> langs;
            //List<ImageFile> imagesList;
            dbInitializerExtention.PrepareLists(out langs, out clinicReviewList, out doctorReviewList, out clinicList, out doctorsList, out phonesList, out clinicAddressList,
                out belarusCitiesList, out categoriesList, out imagesList, out countriesList);
            context.Images.AddRange(imagesList);
            context.ClinicReviews.AddRange(clinicReviewList);
            context.DoctorReviews.AddRange(doctorReviewList);
            context.Сlinics.AddRange(clinicList);
            context.Doctors.AddRange(doctorsList);
            context.ClinicPhones.AddRange(phonesList);
            context.ClinicAddresses.AddRange(clinicAddressList);
            context.Cities.AddRange(belarusCitiesList);
            context.DoctorCategories.AddRange(categoriesList);
            context.Languages.AddRange(langs);
            context.Countries.AddRange(countriesList);

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

            var art1 = new Article()
            {
                Content = @"<p><img src = ""http://infodoctor.somee.com/Content/Images/Article/a1446be69b8a47d0b37083cf761124ae.jpg""></p>< p>Процедура экстракорпорального оплодотворения (ЭКО)&nbsp;&ndash;вспомогательная репродуктивная технология, при которой отдельные этапы зачатия и развития эмбриона происходят вне организма женщины.&nbsp;Малыши, появившиеся на свет, в результате&nbsp;применения метода ЭКО&nbsp;ничем не отличаются от детей, зачатых естественным путем.&nbsp;&nbsp;</p><p>Клиническими показаниями к проведению&nbsp;оплодотворения данным методом&nbsp;&nbsp;являются&nbsp; нарушения проходимости маточных труб,&nbsp;эндометриоз,&nbsp;поликистоз&nbsp;яичников, мужское и возрастное бесплодие, а также бесплодие&nbsp;по неустановленной причине.&nbsp;&nbsp;</p><p>Существует&nbsp;несколько видов&nbsp;экстракорпорального оплодотворения.&nbsp;Наиболее частый&nbsp;&ndash; искусственная&nbsp;инсеминация, который представляет собой&nbsp;введение концентрированной спермы&nbsp;в&nbsp;цервикальный канал или&nbsp;полость матки&nbsp;в период овуляции.&nbsp;Далее&nbsp;сперматозоиды естественным путем проходят по маточным трубам и оплодотворяют яйцеклетку.&nbsp;Искусственную&nbsp;инсеминацию&nbsp;используют в случае&nbsp;полного или частичного&nbsp;мужского бесплодия.&nbsp;</p><p>Гораздо более сложным видом является так называемый &laquo;протокол ЭКО&raquo;, при котором оплодотворение&nbsp;яйцеклеток&nbsp;происходит&nbsp;вне организма женщины, а сформированный впоследствии эмбрион подсаживают&nbsp;в полость матки. Он&nbsp;эффективен при всех видах бесплодия, однако требует длительного подготовительного периода, в ходе которого происходит&nbsp;поэтапное&nbsp;введение&nbsp;лекарственных средств, при помощи&nbsp;чего&nbsp;в яичниках женщины созревает и&nbsp;овулируется&nbsp;сразу несколько яйцеклеток.&nbsp;Это необходимо&nbsp;в целях увеличения шансов наступления беременности.&nbsp;&nbsp;</p><p>Существуют факторы, при которых&nbsp;экстракорпоральное оплодотворение&nbsp;не&nbsp;может быть проведено&nbsp;&ndash; это&nbsp;наличие&nbsp;патологии развития матки,&nbsp;онкологических, воспалительных и инфекционных заболеваний,&nbsp;сахарного диабета,&nbsp;почечной недостаточности, пороков сердца,&nbsp;психических заболеваний.&nbsp;&nbsp;</p><p>Важно помнить, что любое медицинское вмешательство сопряжено с определенными рисками.&nbsp;Так,&nbsp;в ходе проведения ЭКО&nbsp;возможно развитие аллергической реакции на&nbsp;применяемые&nbsp;лекарственные средства.&nbsp;&nbsp;Осложнением процедуры считается также увеличения&nbsp;шанса&nbsp;развития многоплодной беременности, что&nbsp;в свою очередь может привести к преждевременным родам или замиранию беременности. Усиленная гормональная нагрузка, которой подвергается организм женщины при проведении процедуры, может привести к снижению иммунитета.&nbsp;Необходимо&nbsp;также&nbsp;знать, что с возрастом эффективность данного метода&nbsp;значительно снижается: так, после 45 лет&nbsp; успешность проведения процедуры равна&nbsp;всего лишь 1,5 %.&nbsp;&nbsp;</p><p>В настоящее время услуги экстракорпорального оплодотворения предоставляют&nbsp;центры репродуктивной медицины, частные медицинские клиники, республиканские научно-практические центры.&nbsp;</p><p><img src = ""http://infodoctor.somee.com/Content/Images/Article/86f2b071c4e1463d9454c084b1ead451.jpg""></p>",
                Title = "Метод ЭКО. Что нужно знать?",
                PublishDate = DateTime.Now.AddDays(-1),
                Author = "admin",
                Language = langs.First(l => l.Code == "ru")
            };
            var art2 = new Article()
            {
                Content = @"<p><img src = ""http://infodoctor.somee.com/Content/Images/Article/4f4fe30e48b74d6288b512d5c660197e.jpg""></p><p>Остеомиелит (от греч.&nbsp;''воспаление костного мозга'')&nbsp;&minus;&nbsp;инфекционное заболевание, поражающее костную ткань и костной мозг и сопровождающееся гнойно-воспалительным процессом, вследствие чего развивается общая интоксикация организма. Бактериальными возбудителями заболевания являются стрептококки и стафилококки, которые могут проникать в организм несколькими путями: вследствие загрязнения мягких тканей при открытой травме, ранении или с током крови по кровеносным сосудам при наличии в организме хронической инфекции. Предрасполагающими факторами возникновения остеомиелита являются наличие онкологического заболевания, сахарного диабета, нарушения функций печени и почек, а также злоупотребление алкоголем, курение, плохое питание. Различают 3 клинические формы остеомиелита, в зависимости от чего выделяют и различные проявления заболевания.&nbsp;</p><p>&nbsp;Первыми симптомами септико-пиемической формы является повышение температуры, слабость, озноб, тошнота, наблюдается увеличение печени и селезенки. Спустя 1-2 суток появляется сильная боль, отечность, покраснение в области пораженной кости, формируется гнойное воспаление. При дальнейшем развитии заболевания велик риск возникновения межмышечной флегмоны. Местная форма остеомиелита при своевременном начале лечения характеризуется наиболее легким течением и проявляется в преобладании симптомов костного поражения при удовлетворительном общем самочувствии. Наиболее редкой и опасной формой остеомиелита является&nbsp;токсическая. Заболевание развивается в кратчайшие сроки и характеризуется менингеальными симптомами, судорогами, потерей сознания, значительным снижением артериального давления, велик риск развития острой&nbsp;сердечно-сосудистой&nbsp;недостаточности. Опасность токсической формы заключается также и в сложности диагностирования заболевания вследствие отсутствия на начальном этапе специфических симптомов костного поражения.&nbsp;&nbsp;</p><p>Однако нужно знать, что, несмотря на быстрое развитие и зачастую тяжелое течение остеомиелита, своевременная диагностика и правильное комплексное лечение позволяют избежать его прогрессирования, снижают&nbsp;риск развития возможных осложнений, перехода заболевания в хроническую форму.&nbsp;</p><p>Диагностика остеомиелита осуществляется на основании осмотра, сбора анамнеза, а также при помощи лабораторных и инструментальных методов. С этой целью применяется рентгенография, компьютерная и магниторезонансная томография, УЗИ, пункция кости. Наличие воспалительного процесса в организме определяют по данным общего анализа крови и мочи, биохимического анализа крови.&nbsp;</p><p>Лечение заболевания длительное, в некоторых случаях курс может занимать около&nbsp; полугода, однако прогноз полного выздоровления, при условии своевременного начала лечения, благоприятный. Лечение остеомиелита осуществляется только в условиях стационара больничных организаций здравоохранения, куда пациент направляется с предварительным или установленным ранее диагнозом. Обязательным является проведение курса антибиотикотерапии в сочетании с хирургическим вмешательством, которое необходимо при развитии гнойных процессов, появлении свищей. Основные этапы оперативного лечения&nbsp;&minus;&nbsp;санация пораженного участка, удаление отмерших тканей, проведение дренажа. Немаловажным в период лечения является ограничение двигательной активности, проведение&nbsp;иммуностимуляции, а также правильное питание. Особое внимание стоит уделить продуктам с высоким содержанием белка, железа и кальция.&nbsp;&nbsp;</p><p>По окончании лечения с целью тонизирования организма, восстановления функционирования пораженной части тела рекомендованы физиотерапевтические процедуры и лечебная физкультура. Весьма эффективны в период реабилитации электрофорез,&nbsp;магнитотерапия, парафинотерапия, инфракрасный лазер.&nbsp;</p><p>Важно помнить, что любое заболевание легче предупредить, чем лечить! Профилактикой остеомиелита является своевременное лечение очагов инфекции, немедленное обращение за медицинской помощью в случае травм, переломов, правильное питание, отказ от вредных привычек. При ухудшении самочувствия, появлении первых признаков заболевания необходимо срочно обращаться к врачу, не заниматься самолечением: это может привести к необратимым последствиям!&nbsp;</p>",
                Title = "Остеомиелит не приговор!",
                PublishDate = DateTime.Now.AddDays(-1),
                Author = "admin",
                Language = langs.First(l => l.Code == "ru")
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
                Article = art1,
                Language = langs.First(l => l.Code == "ru")
            };
            var comment2 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art1,
                Language = langs.First(l => l.Code == "ru")
            };
            var comment3 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art2,
                Language = langs.First(l => l.Code == "ru")
            };
            var comment4 = new ArticleComment()
            {
                IsApproved = true,
                PublishTime = DateTime.Now.AddDays(-1),
                Text = "Спасибо. Очень информативно.",
                UserId = userName.Id,
                UserName = "admin",
                Article = art2,
                Language = langs.First(l => l.Code == "ru")
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
                    IsApproved = true,
                    Language = langs.First(l => l.Code == "ru")
                });
            }

            var resorts = new List<Resort>();
            var resAdrs = new List<ResortAddress>();
            var resPhones = new List<ResortPhone>();

            //санаторий 1
            var nums1 = new List<ResortPhone>()
            {
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "", Number = "375(1641) 38-2-19" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "Факс", Number = "375(1641) 38-2-22" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"),  Description = "Мтс", Number = "375 (29) 866-86-69"  }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "", Number = "375 (29) 366-86-67" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "", Number = "375(1641) 38-2-19" }
                    }
                }
            };
            resPhones.AddRange(nums1);

            var adr1 = new ResortAddress()
            {
                Phones = nums1,
                Localized = new List<LocalizedResortAddress>()
                {
                    new LocalizedResortAddress()
                    {
                        Country = "Беларусь",
                        City = belarusCitiesList.First(c => string.Equals(c.LocalizedCities.First(l=>l.Language.Code.ToLower()=="ru").Name, "Брестская область",
                            StringComparison.CurrentCultureIgnoreCase)),
                        Street = "урочище \"Сосновый бор\"",

                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                }
            };
            resAdrs.Add(adr1);

            var resortReviewSubList = resortRevs.Take(3).ToList();

            var resort1 = new Resort()
            {
                Email = "bug-marketing@mail.ru",
                Site = "http://sunbug.by/",
                Address = adr1,
                Reviews = resortReviewSubList,
                Localized = new List<LocalizedResort>()
                {
                    new LocalizedResort()
                    {
                        Name = "Санаторий \"Буг\"",
                        Specialisations = @"Медицинская база, Диагностическая база, Лечебные комплексы",
                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                }
            };
            resorts.Add(resort1);

            //санаторий 2
            var nums2 = new List<ResortPhone>()
            {
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "для звонков из РБ", Number = "8 (01641) 68-222" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"),Description = "для звонков из РБ", Number = "8 (01641) 68-333" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "для звонков из РФ", Number = "8 10 (375 1641) 68-222" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "для звонков из РФ", Number = "8 10 (375 1641) 68-333" }
                    }
                }
            };
            resPhones.AddRange(nums2);

            var adr2 = new ResortAddress()
            {
                Phones = nums2,
                Localized = new List<LocalizedResortAddress>()
                {
                    new LocalizedResortAddress()
                    {
                        Country = "Беларусь",
                        City = belarusCitiesList.First(c => string.Equals(c.LocalizedCities.First(l=>l.Language.Code.ToLower()=="ru").Name, "Брестская область",
                            StringComparison.CurrentCultureIgnoreCase)),
                        Street = "Жабинковский район, 1,6 км севернее д. Чижевщина",

                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                }
            };
            resAdrs.Add(adr2);

            resortReviewSubList = resortRevs.Skip(3).Take(3).ToList();

            var resort2 = new Resort()
            {
                Localized = new List<LocalizedResort>()
                {
                    new LocalizedResort()
                    {
                        Name = "Санаторий \"Надзея\"",
                        Specialisations = @"Водолечение, ЛФК Галотерапия, Электросветолечение, Теплолечение, Ручной массаж, Фиточай, Небулайзерная ингаляционная терапия, Галотерапия, Косметология, УЗИ",
                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                },
                Email = "mtv@brest.gas.by, nadzeya@brest.gas.by, san@brest.gas.by",
                Site = "http://www.nadzeya.com/",
                Address = adr2,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort2);

            //санаторий 3
            var nums3 = new List<ResortPhone>()
            {
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "международный", Number = "8 (10-375-212) 29 72 39" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "международный", Number = "8 (10-375-212) 29 73 35" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "международный", Number = "8 (10-375-212) 29 73 24" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "по Беларуси", Number = "8 (10-375-212) 29 72 39" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "по Беларуси", Number = "8 (10-375-212) 29 73 35" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "по Беларуси", Number = "8 (10-375-212) 29 73 24" }
                    }
                }
            };
            resPhones.AddRange(nums3);

            var adr3 = new ResortAddress()
            {
                Phones = nums3,
                Localized = new List<LocalizedResortAddress>()
                {
                    new LocalizedResortAddress()
                    {
                        Country = "Беларусь",
                        City = belarusCitiesList.First(c => string.Equals(c.LocalizedCities.First(l => l.Language.Code.ToLower() == "ru").Name, "Витебская область",
                            StringComparison.CurrentCultureIgnoreCase)),
                        Street = "Витебский район, д. Малые ",

                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                }
            };
            resAdrs.Add(adr3);

            resortReviewSubList = resortRevs.Skip(6).Take(3).ToList();

            var resort3 = new Resort()
            {
                Localized = new List<LocalizedResort>()
                {
                    new LocalizedResort()
                    {
                        Name = "Санаторий \"Лётцы\"",
                        Specialisations = @"Диетотерапия, Бальнеолечение, Теплолечение, Аппаратная физиотерапия, Массаж, Климатолечение, Галотерапия, Рефлексотерапия, Лечебная физкультура, Космтология, Спа, УЗД, УЗИ",
                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                },
                Email = "letzy1@mail.ru",
                Site = "http://letcy.ru/",
                Address = adr3,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort3);

            //санаторий 4
            var nums4 = new List<ResortPhone>()
            {
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "", Number = "375 2157 33463" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "", Number = "375 29 3442040" }
                    }
                },
                new ResortPhone()
                {
                    Localized = new List<LocalizedResortPhone>()
                    {
                        new LocalizedResortPhone(){ Language = langs.First(l => l.Code.ToLower() == "ru"), Description = "", Number = "375 2157 33458" }
                    }
                }
            };
            resPhones.AddRange(nums4);

            var adr4 = new ResortAddress()
            {
                Phones = nums4,
                Localized = new List<LocalizedResortAddress>()
                {
                    new LocalizedResortAddress()
                    {
                        Country = "Беларусь",
                        City = belarusCitiesList.First(c => string.Equals(c.LocalizedCities.First(l => l.Language.Code.ToLower() == "ru").Name, "Витебская область",
                            StringComparison.CurrentCultureIgnoreCase)),
                        Street = "д. Будачи, Докшицкий р-н",

                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                }
            };
            resAdrs.Add(adr4);

            resortReviewSubList = resortRevs.Skip(9).Take(3).ToList();

            var resort4 = new Resort()
            {
                Localized = new List<LocalizedResort>()
                {
                    new LocalizedResort()
                    {
                        Name = "Санаторий \"Боровое\"",
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
                        Language = langs.First(l => l.Code.ToLower() == "ru")
                    }
                },
                Email = "",
                Site = "http://www.sanatorium-borovoe.com/",

                Address = adr4,
                Reviews = resortReviewSubList
            };
            resorts.Add(resort4);


            context.ResortPhones.AddRange(resPhones);
            context.ResortAddresses.AddRange(resAdrs);
            context.Resorts.AddRange(resorts);

            base.Seed(context);
        }
    }
}