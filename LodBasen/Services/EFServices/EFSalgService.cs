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

        public IEnumerable<Lodsalg> Lodsalgssamling { get; set; }

        public IEnumerable<Lodsalg> GetLodsalgssamling()
        {
            Lodsalgssamling = context.Lodsalgssamling.Include(m => m.Modtager.Barn).Include(m => m.Modtager.Leder);
            return Lodsalgssamling;
        }

        public void AddLodsalg(object lodsalg)
        {
            throw new NotImplementedException();
        }

        public int GetAntalFromAdmin()
        {
            return Convert.ToInt32(context.Sælgere.Include(a => a.Admin).AsNoTracking());
        }

        public int GetAntalFromLeder()
        {
            return Convert.ToInt32(context.Sælgere.Include(l => l.Leder).AsNoTracking());
        }

        public int GetAntalFromLodseddel()
        {
            return Convert.ToInt32(context.Lodsedler.AsNoTracking());
        }

        //public void Overførsel(Sælger sælger, Modtager modtager, int antal)
        //{


        //}
    }
}
