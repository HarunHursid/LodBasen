using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LodBasen.Services.EFServices
{
    public class EFSalgService : ISalgService
    {
        public lodbasen_dk_db_lodbasenContext context;

        public EFSalgService(lodbasen_dk_db_lodbasenContext service) 
        { 
            context = service;
        }

        public int Udleveret { get; set; }

        public IEnumerable<Sælger> Sælgere { get; set; }

        public IEnumerable<Modtager> Modtagere { get; set; }

        public int GetAntalFromAdmin() 
        {
            return Convert.ToInt32(context.Sælgere.Include(a => a.Admin.Antal).AsNoTracking());
        }

        public int GetAntalFromLeder() 
        {
            return Convert.ToInt32(context.Sælgere.Include(l => l.Leder.Antal).AsNoTracking());
        }

        public int GetAntalFromLodseddel() 
        {
            return Convert.ToInt32(context.Lodsedler.Include(a => a.Antal).AsNoTracking());
        }

        //public void Overførsel(Sælger sælger, Modtager modtager, int antal)
        //{
        //    if () 
        //    { 
            
        //    }
        //}


    }
}
