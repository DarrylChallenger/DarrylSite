using DarrylSite.Models;
using DarrylSite.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(DarrylSite.Startup))]
namespace DarrylSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();//https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
            ConfigureStripe();
        }
        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            /*
             * 
             * https://www.linkedin.com/learning/asp-dot-net-mvc-5-essential-training/asp-dot-net-identity-and-the-user-manager
             * https://www.linkedin.com/learning/asp-dot-net-mvc-5-essential-training/authenticating-users-with-facebook
             * https://www.linkedin.com/learning/asp-dot-net-mvc-5-essential-training/using-role-based-authorization
             * https://www.linkedin.com/learning/asp-dot-net-mvc-5-essential-training/seeding-roles-and-assignments
             * 
             */
            Models.ApplicationDbContext context = new Models.ApplicationDbContext(); //https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97

            string s = context.Database.Connection.DataSource.ToString();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<Models.ApplicationUser>(new UserStore<ApplicationUser>(context));

            
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin")) //
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                
                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "DZone720@nyc.rr.com";
                user.Email = "DZone720@nyc.rr.com";

                string userPWD = "Administrator1!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }

                ApplicationUser u = new ApplicationUser();
                u.UserName = "darryl.challenger@gmail.com";
                u.Email = "darryl.challenger@gmail.com";
                string uPWD = "Administrator1!";
                IdentityResult irc, irr;
                irc = UserManager.Create(u, uPWD);
                if (irc.Succeeded)
                {
                    irr = UserManager.AddToRole(u.Id, "Admin");
                }

                u.UserName = "Reguser@gmail.com";
                u.Email = "Reguser@gmail.com";
                uPWD = "reguser";
                irc = UserManager.Create(u, uPWD);
            }



            // creating Creating Visitor role    
            if (!roleManager.RoleExists("Visitor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Visitor";
                roleManager.Create(role);

            }
            /*
            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
            */
        }

        private void ConfigureStripe()
        {
            StripeConstants.publicKey = ConfigurationManager.AppSettings["publicKey"];
            StripeConstants.secretKey = ConfigurationManager.AppSettings["secretKey"];
        }
    }
}
