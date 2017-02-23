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

            // создаем три роли
            var role1 = new IdentityRole { Name = "user" };
            var role2 = new IdentityRole { Name = "admin" };
            var role3 = new IdentityRole { Name = "moder" };


            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

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

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(moder.Id, role3.Name);
            }

            for (int i = 0; i < 10; i++)
            {
                var user = new ApplicationUser
                {
                    Email = "user" + i + "@infodoctor.by",
                    UserName = "user" + i
                };
                password = "123456";
                result = userManager.Create(user, password);

                // если создание пользователя прошло успешно
                if (result.Succeeded)
                {
                    // добавляем для пользователя роль
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

            //add clinic profiles to Db
            //var profilesList = new List<ClinicProfile>();
            //path = AppDomain.CurrentDomain.BaseDirectory + "/Content/ClinicProfilesList.txt";
            //using (var sr = new StreamReader(path, System.Text.Encoding.Default))
            //{
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        profilesList.Add(new ClinicProfile() { Name = line });
            //    }
            //}
            //context.ClinicProfiles.AddRange(profilesList);


            base.Seed(context);
        }
    }
}