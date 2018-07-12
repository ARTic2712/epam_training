using System;
using SalesSystem.Interfaces;
using SalesSystem.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
namespace SalesSystem.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext db;
        private RepositoryT<ApplicationUser> _users;
        private RepositoryT<ApplicationRole> _roles;
        private RepositoryT<ApplicationUserRoles > _userRoles;

        private RepositoryT<Product > _products;
        private RepositoryT<Sale> _sales;
        private bool disposed = false;
        public EFUnitOfWork(string connectionString )
        {
            db =new ApplicationDbContext(connectionString);
        }
        public EFUnitOfWork()
        {
            db = new ApplicationDbContext();
        }
        public IRepository<ApplicationUser> Users
        {
            get
            {
                if (_users == null) _users = new RepositoryT<ApplicationUser> (db);
                return _users;
            }
        }
        public IRepository<ApplicationRole> Roles
        {
            get
            {
                if (_roles == null) _roles = new RepositoryT<ApplicationRole>(db);
                return _roles;
            }
        }
        public IRepository<ApplicationUserRoles > UserRoles
        {
            get
            {
                if (_userRoles == null) _userRoles = new RepositoryT<ApplicationUserRoles >(db);
                return _userRoles;
            }
        }
        public IRepository<Product> Products
        {
            get
            {
                if (_products == null) _products = new RepositoryT<Product>(db);
                return _products;
            }
        }
        public IRepository<Sale> Sales
        {
            get
            {
                if (_sales == null) _sales = new  RepositoryT<Sale> (db);
                return _sales;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return new SalesSystem.ApplicationUserManager(new UserStore<SalesSystem.Entities.ApplicationUser>(db)); 
            }
        }
        

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
