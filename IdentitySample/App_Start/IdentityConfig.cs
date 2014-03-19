using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IdentitySample
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // You want to create a new database if the Model changes
    // public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    public class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        private void InitializeIdentityForEF(MyDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var myinfo = new MyUserInfo() { FirstName = "Pranav", LastName = "Rastogi" };
            string name = "Admin";
            string password = "123456";

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create admin=Admin with password=123456
            var admin = new ApplicationUser();
            admin.UserName = name;
            admin.HomeTown = "Seattle";
            admin.MyUserInfo = myinfo;
            var adminresult = UserManager.Create(admin, password);
            
            //Add admin Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(admin.Id, name);
            }

            // -- Added and modified by MF
            string test = "mark";
            string int422 = "INT422";

            //Create Role INT422 if it does not exist and add User mark to it
            if (!RoleManager.RoleExists(int422))
            {
                var roleresult = RoleManager.Create(new IdentityRole(int422));
            }
            //RoleManager.Create(new IdentityRole(int422));
            //UserManager.Create(new ApplicationUser() { UserName = test });

            //Create User=mark with password=123456 
            var user = new ApplicationUser();
            var userinfo = new MyUserInfo() { FirstName = "Mark", LastName = "Fernandes" };
            user.UserName = test;
            user.HomeTown = "Toronto";
            user.MyUserInfo = userinfo;
            var userresult = UserManager.Create(user, password);

            //Add User test to Role INT422
            if (userresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, int422);
            }

            //

        }
    }
}