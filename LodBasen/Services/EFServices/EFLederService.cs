using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LodBasen.Services.EFServices
{
    public class EFLederService : ILederService
    {
        lodbasen_dk_db_lodbasenContext context;
        public EFLederService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }
        public IEnumerable<Leder> GetLedere(string search)
        {
            return this.context.Set<Leder>().Where(l => l.Navn.StartsWith(search)).AsNoTracking().ToList();
        }
        public IEnumerable<Leder> GetLedere()
        {
            return context.Ledere;
        }
        public void AddLeder(Leder leder)
        {
            context.Ledere.Add(leder);
            context.SaveChanges();
        }

        public void UpdateLeder(Leder leder)
        {
            context.Ledere.Update(leder);
            context.SaveChanges();
        }

        //public void UpdateLeder(Leder leder)
        //{
        //    var existingLeder = context.Ledere.Find(leder.LederId);
        //    if (existingLeder != null)
        //    {
        //        //context.Entry(existingLeder).CurrentValues.SetValues(leder);
        //        existingLeder.Navn = leder.Navn;
        //        context.SaveChanges();
        //    }
        //}
        public IEnumerable<Leder> GetLedereByGruppeId(int id)
        {
            //IEnumerable<Leder> listLedere = new IEnumerable<Leder>();
            // listLedere = context.Set<Leder>().Where(l => l.GruppeId == id)
            //      //.Include(l => l.Ledere)
            //      .AsNoTracking()
            //     .FirstOrDefault(l => l.GruppeId == id);
            // return ledere;

            return this.context.Set<Leder>().Where(l => l.GruppeId == id).Include(g => g.Gruppe).AsNoTracking().ToList();
        }

        public Leder GetLederById(int id)
        {
            return context.Ledere.Find(id);
        }

        public void DeleteLeder(Leder leder)
        {
            if (leder != null)
            {
                context.Ledere.Remove(leder);
                context.SaveChanges();
            }
        }
    }
}

