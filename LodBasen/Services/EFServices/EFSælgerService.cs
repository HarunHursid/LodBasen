using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
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

        public IEnumerable<Sælger> GetSælgere()
        {
            //return context.Sælgere.Include(a => a.Admin).Include(l => l.Leder);
            return context.Sælgere.Include(a => a.Admin).Include(l => l.Leder);
        }

        //public string GetSalesPersonNameById(int sælgerId)
        //{
        //    var sælger = context.Sælgere
        //        .Include(sp => sp.Admin)
        //        .Include(sp => sp.Leder)
        //        .FirstOrDefault(sp => sp.SælgerId == sælgerId);

        //    return sælger?.Admin.Navn + sælger?.Leder.Navn;
        //}

        //public String GetSælgerNameById(int sælgerId)
        //{
        //    {
        //        String sælgernavn = Convert.ToString(context.Sælgere.Include(a => a.Admin.Navn).Where<Sælger> (s => s.AdminId != null));
        //    }

        //}

    }
}
