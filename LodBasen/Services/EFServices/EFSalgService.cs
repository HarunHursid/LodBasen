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

        public void AddLodsalg(Lodsalg lodsalg)
        {
            throw new NotImplementedException();
        }

        public int GetAntalFromAdmin(int? adminId)
        {
            return Convert.ToInt32(context.Sælgere.Include(a => a.Admin.Antal).Where(a => a.Admin.AdminId.Equals(adminId)).AsNoTracking());
            
        }

        public int GetAntalFromLeder(int? lederId)
        {
            return Convert.ToInt32(context.Sælgere.Include(l => l.Leder.Antal).Where(l => l.Leder.LederId.Equals(lederId)).AsNoTracking());
        }

        public int GetAntalFromLodseddel(int? lodseddelId)
        {
            return Convert.ToInt32(context.Lodsedler.Include(l => l.Antal).Where(l => l.LodseddelId.Equals(lodseddelId)).AsNoTracking());
        }
        public IEnumerable<Sælger> GetSælgere()
        {
            return context.Sælgere.ToList();
        }
        public IEnumerable<Modtager> GetModtagere()
        {
            return context.Modtagere.ToList();
        }
        public IEnumerable<Lodseddel> GetLodsedler()
        {
            return context.Lodsedler.ToList();
        }
        public Sælger GetSælgerById(int sælgerId)
        {
            return (Sælger)context.Set<Sælger>().Where(s => s.SælgerId == sælgerId);
        }
        public Modtager GetModtagerById(int modtagerId)
        {
            return (Modtager)context.Set<Modtager>().Where(m => m.ModtagerId == modtagerId);
        }
        public Lodseddel GetLodseddelById(int lodseddelId)
        {
            return (Lodseddel)context.Set<Lodseddel>().Where(l => l.LodseddelId == lodseddelId);
        }

        public void AddOverførsel(Sælger sælger, Modtager modtager, Lodseddel lodseddel, int antal)
        {
            Lodsalg lodsalg = new Lodsalg(sælger.SælgerId, modtager.ModtagerId, lodseddel.LodseddelId);
            context.Lodsalgssamling.AddAsync(lodsalg);
            context.SaveChanges();

            if (sælger.AdminId !=null && modtager.LederId != null)
            {
                if(GetAntalFromAdmin(sælger.AdminId) >= antal && (GetAntalFromAdmin(sælger.AdminId) - antal)>0) 
                {
                    sælger.Admin.Antal = GetAntalFromAdmin(sælger.AdminId) - antal;
                    sælger.Admin.Udleveret += antal;
                    context.Admins.Update(sælger.Admin);
                    context.SaveChanges();
                    modtager.Leder.Antal = antal;
                    context.Ledere.Update(modtager.Leder);
                    context.SaveChanges();
                    //lodseddel.Antal -= antal;
                    
                }
            }
            else if (sælger.AdminId !=null && modtager.BarnId != null)
            {
                if (GetAntalFromAdmin(sælger.AdminId) >= antal && (GetAntalFromAdmin(sælger.AdminId) - antal) > 0)
                {
                    sælger.Admin.Antal = GetAntalFromAdmin(sælger.AdminId) - antal;
                    sælger.Admin.Udleveret += antal;
                    context.Admins.Update(sælger.Admin);
                    context.SaveChanges();
                    modtager.Barn.Antal = antal;
                    context.Børn.Update(modtager.Barn);
                    context.SaveChanges();
                }
            }
            else if (sælger.LederId != null && modtager.BarnId != null)
            {
                if (GetAntalFromAdmin(sælger.LederId) >= antal && (GetAntalFromLeder(sælger.LederId) - antal) > 0)
                {
                    sælger.Leder.Antal = GetAntalFromLeder(sælger.LederId) - antal;
                    sælger.Leder.Udleveret += antal;
                    context.Ledere.Update(sælger.Leder);
                    context.SaveChanges();
                    modtager.Barn.Antal = antal;
                    context.Børn.Update(modtager.Barn);
                    context.SaveChanges();
                    //lodseddel.Antal -= antal;
                }
            }
        }

        public String GetSælgerNamebyId(int Id)
        {
            {
                foreach (var sælger in Sælgere)
                {
                    if (Id == sælger.AdminId)
                    {
                        return sælger.Admin.Navn;
                    }
                    else if (Id == sælger.LederId)
                    {
                        return sælger.Leder.Navn;
                    }
                }
            }
            return null;
        }
        public String GetModtagerNamebyId(int Id)
        {
            {
                foreach (var modtager in Modtagere)
                {
                    if (Id == modtager.LederId)
                    {
                        return modtager.Leder.Navn;
                    }
                    else if (Id == modtager.BarnId)
                    {
                        return modtager.Barn.Navn;
                    }
                }
            }
            return null;
        }
        

    }
}
