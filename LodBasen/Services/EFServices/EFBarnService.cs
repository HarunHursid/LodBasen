using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        //public Barn GetBørnByGruppeId(int id)
        //{
        //    Barn børn = context.Set<Barn>()
        //        //.Include(b => b.Børn)
        //         .AsNoTracking()
        //        .FirstOrDefault(b => b.GruppeId == id);
        //    return børn;

        //}
    }
}
