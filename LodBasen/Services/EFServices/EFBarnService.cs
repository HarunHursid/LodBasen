using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LodBasen.Services.EFServices
{
    public class EFBarnService : IBarnService
    {
        lodbasen_dk_db_lodbasenContext context;
        public EFBarnService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }
        public IEnumerable<Barn> GetBørn(string search)
        {
            return this.context.Set<Barn>().Where(b => b.Navn.StartsWith(search)).AsNoTracking().ToList();
        }
        public IEnumerable<Barn> GetBørn()
        {
           // return context.Børn;
            return context.Børn.Include(b => b.Gruppe).AsNoTracking().ToList(); ;
        }
        public void AddBarn(Barn barn)
        {
            context.Børn.Add(barn);
            context.SaveChanges();
        }

        public void UpdateBarn(Barn barn)
        {

            context.Børn.Update(barn);
            context.SaveChanges();
        }

        public IEnumerable<Barn> GetBørnByGruppeId(int id)
        {
            //IEnumerable<Barn> listBørn = new IEnumerable<Barn>();
            // listBørn = context.Set<Barn>().Where(b => b.GruppeId == id)
            //      //.Include(b => b.Børn)
            //      .AsNoTracking()
            //     .FirstOrDefault(b => b.GruppeId == id);
            // return børn;

            return this.context.Set<Barn>().Where(b => b.GruppeId == id).Include(g => g.Gruppe).AsNoTracking().ToList();
        }

        public Barn GetBarnById(int id)
        {
            return context.Børn
                .Include(b => b.Gruppe)
                .FirstOrDefault(b => b.BarnId == id);

        }
        public Gruppe GetGruppeById(int id)
        {
            return context.Grupper.FirstOrDefault(g => g.GruppeId == id);
        }

        //public void DeleteBarn(Barn barn)
        //{
        //    if (barn != null)
        //    {
        //        context.Børn.Remove(barn);
        //        context.SaveChanges();
        //    }
        //}
        public void DeleteBarn(Barn barn)
        {
            if (barn != null)
            {
                var modtagere = context.Modtagere.Where(m => m.BarnId == barn.BarnId);
                context.Modtagere.RemoveRange(modtagere);
                context.Børn.Remove(barn);
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
        public IQueryable<Barn> GetBørnQuery(string search)
        {
            // Implement your search logic and return IQueryable
            // For example:
            return context.Børn.Where(b => b.Navn.Contains(search)).AsQueryable();
        }

        public IQueryable<Barn> GetBørnQuery()
        {
            // Implement logic to return all Børn as IQueryable
            return context.Børn.AsQueryable();
        }
    }
}
