﻿using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface IAdminService
    {
        public IEnumerable<Admin> GetAdmins();

        public void AddAdmin(Admin admin);

        public void UpdateLeder(Admin admin);

        public Admin GetAdminById(int id);
        
        public void DeleteAdmin(Admin admin);
        
    }
}
