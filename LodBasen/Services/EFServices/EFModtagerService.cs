using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Services.EFServices
{
    public class EFModtagerService : IModtagerService
    {
        lodbasen_dk_db_lodbasenContext context;
        public EFModtagerService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }
        public IEnumerable<Modtager> Modtagere { get; set; }

        public IEnumerable<Modtager> GetModtagere()
        {
            Modtagere = context.Modtagere.Include(b => b.Barn).Include(l => l.Leder);
            return Modtagere;
        }
    }
}
