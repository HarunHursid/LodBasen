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
            return context.Børn;
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

        //public void UpdateBarn(Barn barn)
        //{
        //    var existingBarn = context.Børn.Find(barn.BarnId);
        //    if (existingBarn != null)
        //    {
        //        //context.Entry(existingBarn).CurrentValues.SetValues(barn);
        //        existingBarn.Navn = barn.Navn;
        //        context.SaveChanges();
        //    }
        //}
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
            return context.Børn.Find(id);
        }

        public void DeleteBarn(Barn barn)
        {
            if (barn != null)
            {
                context.Børn.Remove(barn);
                context.SaveChanges();
            }
        }
    }
}
