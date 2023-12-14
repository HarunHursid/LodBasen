using LodBasen.Models;
using LodBasen.Pages;
using LodBasen.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using NuGet.Common;
using System.Linq;

namespace LodBasen.Services.EFServices
{
    public class EFSælgerService : ISælgerService
    {
        lodbasen_dk_db_lodbasenContext context;
        public EFSælgerService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }
        public IEnumerable<Sælger> Sælgere { get; set; }

        //public void AddSælger() 
        //{
        //    context.Sælgere.Add();
        //    context.SaveChanges();
        //}

        public IEnumerable<Sælger> GetSælgere()
        {
            return Sælgere = context.Sælgere.Include(a => a.Admin).Include(l => l.Leder).AsNoTracking().ToList();

        }

        public Sælger GetSælgerIdByAdmin(int adminId) 
        {
            return (Sælger)context.Set<Sælger>().Where(a => a.AdminId == adminId).Include(a => a.Admin);
        }

        public Sælger GetSælgerIdByLeder(int lederId)
        {
            return (Sælger)context.Set<Sælger>().Where(l => l.LederId == lederId).Include(l => l.Leder);
        }

     

    }
}
