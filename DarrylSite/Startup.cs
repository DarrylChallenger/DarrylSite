using DarrylSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DarrylSite.Startup))]
namespace DarrylSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();//https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        }
        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            Models.ApplicationDbContext context = new Models.ApplicationDbContext(); //https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<Models.ApplicationUser>(new UserStore<ApplicationUser>(context));

            
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                
                //Here we create a Admin super user who will maintain the website                  

                var user = new Models.ApplicationUser();
                user.UserName = "Darryl";
                user.Email = "DZone720@nyc.rr.com";

                string userPWD = "Admin1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
            
            // creating Creating Manager role    
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
    }
}
