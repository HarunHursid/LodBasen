using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Services.EFServices
{
    public class EFAdminService : IAdminService
    {
        lodbasen_dk_db_lodbasenContext context;
        public EFAdminService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }
       
        public IEnumerable<Admin> GetAdmins()
        {
            return context.Admins.AsNoTracking().ToList();
        }
        public void AddAdmin(Admin admin)
        {
            context.Admins.Add(admin);
            context.SaveChanges();
        }

        public void UpdateLeder(Admin admin)
        {
            context.Admins.Update(admin);
            context.SaveChanges();
        }


      

        public Admin GetAdminById(int id)
        {
            return context.Admins.FirstOrDefault(a => a.AdminId == id);
        }

        public void DeleteAdmin(Admin admin)
        {
            if (admin != null)
            {
                context.Admins.Remove(admin);
                context.SaveChanges();
            }
        }
    }
}
