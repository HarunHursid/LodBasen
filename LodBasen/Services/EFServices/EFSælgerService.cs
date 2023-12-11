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
            Sælgere = context.Sælgere.Include(a => a.Admin).Include(l => l.Leder);
            return Sælgere;
        }

        public Sælger GetSælgerById(int id)
        {
            Sælger sælger = (Sælger)context.Sælgere.Include(a => a.Admin).Where(s => s.AdminId == id).Include(l => l.Leder).Where(l => l.LederId == id);
            return sælger;
        }

        //public String GetSælgerNamebyId(int Id)
        //{
        //    {
        //        foreach (var sælger in Sælgere)
        //        {
        //            if (Id == sælger.AdminId)
        //            {
        //                return sælger.Admin.Navn;
        //            }
        //            else if (Id == sælger.LederId)
        //            {
        //                return sælger.Leder.Navn;
        //            }
        //        }
        //    }
        //    return null;
        //}

    }
}
