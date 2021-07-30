using System;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }


        public void CreateDefaultRolesAndUsers()
        {

            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            IdentityRole role = new IdentityRole();
            if(!rolemanager.RoleExists("Admins"))
            { 
                role.Name = "Admins";
                rolemanager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Mohamed Malik";
                user.Email = "momalik665@gmail.com";
                var check = usermanager.Create(user, "momali@2K");
                if(check.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "Admins");
                }
                
            }

        }
        
    }
}
