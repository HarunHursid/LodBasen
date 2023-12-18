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

        public Modtager GetModtagerIdByLeder(int lederId)
        {
            return (Modtager)context.Set<Modtager>().Where(l => l.LederId == lederId).Include(a => a.Leder);
        }

        public Modtager GetModtagerIdByBarn(int barnId)
        {
            return (Modtager)context.Set<Modtager>().Where(b => b.BarnId == barnId).Include(b => b.Barn);
        }
    }
}
