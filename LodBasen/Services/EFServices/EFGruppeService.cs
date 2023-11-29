using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Services.EFServices
{
    public class EFGruppeService:IGruppeService
    {
        lodbasen_dk_db_lodbasenContext context;
        public EFGruppeService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }
        public void AddGruppe(Gruppe gruppe)
        {
            context.Grupper.Add(gruppe);
            context.SaveChangesAsync();
        }
        public Gruppe GetGruppeById(int id)
        {
                return context.Grupper.Find(id);
        }
        public void DeleteGruppe(Gruppe gruppe)
        {
            if (gruppe != null)
            {
                context.Grupper.Remove(gruppe);
                context.SaveChanges();
            }
        }
        public IEnumerable<Gruppe> GetGrupper(string search)
        {
            return this.context.Set<Gruppe>().Where(g => g.GruppeNavn.StartsWith(search)).AsNoTracking().ToList();
        }
        public IEnumerable<Gruppe> GetGrupper()
        {
            return context.Grupper;
        }
    }
}
