using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace SalesSystem.Entities
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        public ApplicationUser()
            :base()
        { }
        public ApplicationUser(string Email, string UserName, string FirstName, string SecondName)
        {
           this.UserName  = UserName;
            this.Email = Email;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
        }
        public ApplicationUser(string Email, string UserName)
        {
            this.UserName = UserName;
            this.Email = Email;
            this.FirstName = "ExternalLoginConfirmation";
            this.SecondName = "ExternalLoginConfirmation";
        }
    }
    public class ApplicationRole: IdentityRole
    {

    }
    public class ApplicationUserRoles: IdentityUserRole
    {
        public ApplicationUserRoles()
            :base()
        { }
        public ApplicationUserRoles (ApplicationUser user, ApplicationRole role)
        {
            this.UserId = user.Id;
            this.RoleId = role.Id;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private DbSet<Product> Products { get; set; }
        private DbSet<Sale> Sales { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public ApplicationDbContext(string connectionString) : base(connectionString)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}