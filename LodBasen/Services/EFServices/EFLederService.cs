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
            return context.Ledere.Include(l => l.Gruppe).AsNoTracking().ToList();
        }
        public void AddLederToModtagerAndSælger(Leder leder)
        {
            Modtager modtager = new Modtager();
            modtager.LederId = leder.LederId;
            context.Modtagere.Add(modtager);
            Sælger sælger = new Sælger();
            sælger.LederId = leder.LederId;
            context.Sælgere.Add(sælger);
        }
        public void RemoveLederFromModtagerAndSælger(Leder leder)
        {
            Modtager modtager = context.Set<Modtager>().Where(l => l.LederId == leder.LederId).FirstOrDefault();    
            context.Modtagere.Remove(modtager);
            context.SaveChanges();
            Sælger sælger = context.Set<Sælger>().Where(l => l.LederId == leder.LederId).FirstOrDefault();
            context.Sælgere.Remove(sælger);
            context.SaveChanges();
        }
        public void AddLeder(Leder leder)
        {
            context.Ledere.Add(leder);
            context.SaveChanges();
            AddLederToModtagerAndSælger(leder);
            context.SaveChanges();
        }

        public void UpdateLeder(Leder leder)
        {
            context.Ledere.Update(leder);
            context.SaveChanges();
        }

        public IEnumerable<Leder> GetLedereByGruppeId(int id)
        {

            return this.context.Set<Leder>().Where(l => l.GruppeId == id).Include(g => g.Gruppe).AsNoTracking().ToList();
        }

        public Leder GetLederById(int id)
        {
            return context.Ledere.Include(l => l.Gruppe).FirstOrDefault(l => l.LederId == id);
        }

        public void DeleteLeder(Leder leder)
        {
            if (leder != null)
            {
                RemoveLederFromModtagerAndSælger(leder);
                context.SaveChanges();
                context.Ledere.Remove(leder);
                context.SaveChanges();
            }
        }
        public List<string> GetAllGruppeNavn()
        {
            return context.Grupper.Select(g => g.GruppeNavn).Distinct().ToList();
        }
        public Gruppe GetGruppeByGruppeNavn(string gruppeNavn)
        {
            return context.Grupper.FirstOrDefault(g => g.GruppeNavn == gruppeNavn);
        }
    }
}

