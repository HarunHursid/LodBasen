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
            return context.Set<Sælger>().FirstOrDefault(s => s.SælgerId == sælgerId);
        }
        public Modtager GetModtagerById(int modtagerId)
        {
            return context.Set<Modtager>().FirstOrDefault(m => m.ModtagerId == modtagerId);
        }

        public Lodseddel GetLodseddelById(int lodseddelId)
        {
            return context.Set<Lodseddel>().FirstOrDefault(l => l.LodseddelId == lodseddelId);
        }
        public void AddOverførsel(Sælger sælger, Modtager modtager, Lodseddel lodseddel, int antal)
        {
            // Opret en ny overførsel
            var nyOverførsel = new Lodsalg();

            nyOverførsel.Sælger = sælger;
            nyOverførsel.Modtager = modtager;
            nyOverførsel.Lodseddel = lodseddel;

            // Opdater antallet på den relevante entitet (Admin, Barn eller Leder)
            if (sælger.Admin != null)
            {
                sælger.Admin.Antal += antal;
            }
            else if (modtager.Barn != null)
            {
                modtager.Barn.Antal += antal;
            }
            else if (modtager.Leder != null)
            {
                modtager.Leder.Antal += antal;
            }

            // Gem ændringer i databasen
            context.Lodsalgssamling.Add(nyOverførsel);
            context.SaveChanges();
        }

        //public void AddOverførsel(Sælger sælger, Modtager modtager, Lodseddel lodseddel, int antal)
        //{
        //    Lodsalg lodsalg = new Lodsalg(sælger.SælgerId, modtager.ModtagerId, lodseddel.LodseddelId);
        //    context.Lodsalgssamling.AddAsync(lodsalg);
        //    context.SaveChanges();

        //    if (sælger.AdminId != null && modtager.LederId != null)
        //    {
        //        if (GetAntalFromAdmin(sælger.AdminId) >= antal && (GetAntalFromAdmin(sælger.AdminId) - antal) > 0)
        //        {
        //            sælger.Admin.Antal = GetAntalFromAdmin(sælger.AdminId) - antal;
        //            sælger.Admin.Udleveret += antal;
        //            context.Admins.Update(sælger.Admin);
        //            context.SaveChanges();
        //            modtager.Leder.Antal += antal;
        //            context.Ledere.Update(modtager.Leder);
        //            context.SaveChanges();
        //            //lodseddel.Antal -= antal;

        //        }
        //    }
        //    else if (sælger.AdminId != null && modtager.BarnId != null)
        //    {
        //        if (GetAntalFromAdmin(sælger.AdminId) >= antal && (GetAntalFromAdmin(sælger.AdminId) - antal) > 0)
        //        {
        //            sælger.Admin.Antal = GetAntalFromAdmin(sælger.AdminId) - antal;
        //            sælger.Admin.Udleveret += antal;
        //            context.Admins.Update(sælger.Admin);
        //            context.SaveChanges();
        //            modtager.Barn.Antal = antal;
        //            context.Børn.Update(modtager.Barn);
        //            context.SaveChanges();
        //        }
        //    }
        //    else if (sælger.LederId != null && modtager.BarnId != null)
        //    {
        //        if (GetAntalFromAdmin(sælger.LederId) >= antal && (GetAntalFromLeder(sælger.LederId) - antal) > 0)
        //        {
        //            sælger.Leder.Antal = GetAntalFromLeder(sælger.LederId) - antal;
        //            sælger.Leder.Udleveret += antal;
        //            context.Ledere.Update(sælger.Leder);
        //            context.SaveChanges();
        //            modtager.Barn.Antal = antal;
        //            context.Børn.Update(modtager.Barn);
        //            context.SaveChanges();
        //            //lodseddel.Antal -= antal;
        //        }
        //    }
        //}

        //public Sælger GetSælgerById(int id)
        //{
        //    return (Sælger)context.Sælgere.Where(s => s.SælgerId.Equals(id)).AsNoTracking();  
        //}

        //public Modtager GetModtagerById(int id)
        //{
        //    return (Modtager)context.Modtagere.Where(s => s.ModtagerId.Equals(id)).AsNoTracking();
        //}


        //public void AddOverførsel(int sælgerId, int modtagerId, int lodseddelId, int antal)
        //{
        //    Lodsalg lodsalg = new Lodsalg(sælgerId, modtagerId, lodseddelId);
        //    context.Lodsalgssamling.AddAsync(lodsalg);
        //    context.SaveChanges();
        //    Sælger sælger = GetSælgerById(sælgerId);
        //    Modtager modtager = GetModtagerById(modtagerId);

        //    if (sælger.AdminId != null && modtager.LederId != null)
        //    {
        //        if (GetAntalFromAdmin(sælger.AdminId) >= antal && (GetAntalFromAdmin(sælger.AdminId) - antal) > 0)
        //        {
        //            sælger.Admin.Antal = GetAntalFromAdmin(sælger.AdminId) - antal;
        //            sælger.Admin.Udleveret += antal;
        //            context.Admins.Update(sælger.Admin);
        //            context.SaveChanges();
        //            modtager.Leder.Antal += antal;
        //            context.Ledere.Update(modtager.Leder);
        //            context.SaveChanges();
        //            //lodseddel.Antal -= antal;

        //        }
        //    }
        //    else if (sælger.AdminId != null && modtager.BarnId != null)
        //    {
        //        if (GetAntalFromAdmin(sælger.AdminId) >= antal && (GetAntalFromAdmin(sælger.AdminId) - antal) > 0)
        //        {
        //            sælger.Admin.Antal = GetAntalFromAdmin(sælger.AdminId) - antal;
        //            sælger.Admin.Udleveret += antal;
        //            context.Admins.Update(sælger.Admin);
        //            context.SaveChanges();
        //            modtager.Barn.Antal = antal;
        //            context.Børn.Update(modtager.Barn);
        //            context.SaveChanges();
        //        }
        //    }
        //    else if (sælger.LederId != null && modtager.BarnId != null)
        //    {
        //        if (GetAntalFromAdmin(sælger.LederId) >= antal && (GetAntalFromLeder(sælger.LederId) - antal) > 0)
        //        {
        //            sælger.Leder.Antal = GetAntalFromLeder(sælger.LederId) - antal;
        //            sælger.Leder.Udleveret += antal;
        //            context.Ledere.Update(sælger.Leder);
        //            context.SaveChanges();
        //            modtager.Barn.Antal = antal;
        //            context.Børn.Update(modtager.Barn);
        //            context.SaveChanges();
        //            //lodseddel.Antal -= antal;
        //        }
        //    }
        //}

        public void AfslutOverførsel(int lodsalgsId, int solgt)
        {
            Lodsalg lodsalg = (Lodsalg)context.Lodsalgssamling.Where(l => l.LodsalgsId.Equals(lodsalgsId));
            Modtager modtager = (Modtager)context.Modtagere.Where(m => m.ModtagerId.Equals(lodsalg.ModtagerId));
            Sælger sælger = (Sælger)context.Sælgere.Where(s => s.SælgerId.Equals(lodsalg.SælgerId));
            Lodseddel lodseddel = (Lodseddel)context.Lodsedler.Where(l => l.LodseddelId.Equals(lodsalg.LodseddelId));
            Admin Admin = (Admin)context.Admins.Where(a => a.AdminId.Equals(lodsalg.Sælger.AdminId));
            Leder Leder = (Leder)context.Ledere.Where(l => l.LederId.Equals(lodsalg.Sælger.LederId));
            Barn barn = (Barn)context.Børn.Where(b => b.BarnId.Equals(lodsalg.Modtager.BarnId));

            if (lodsalg != null)
            {
                if (modtager.LederId != null && sælger.AdminId != null)
                {
                    //kan først afslutte lodsalg hvis lederen ikke har nogen udleverede lodsedler til børn
                    if (modtager.Leder.Udleveret == 0)
                    {
                        Admin.Antal += modtager.Leder.Antal;
                        Leder.Antal = 0;
                        context.Ledere.Update(Leder);
                        context.SaveChanges();
                        context.Admins.Update(Admin);
                        context.SaveChanges();
                        context.Lodsalgssamling.Remove(lodsalg);
                        context.SaveChanges();
                    }
                }
                if (lodsalg.Modtager.BarnId != null && lodsalg.Sælger.LederId != null)
                {
                    //ikke solgte lodsedler kommer retur til leder og solgte bliver lagt i lodseddel tabel
                    Leder.Antal += barn.Antal - solgt;
                    Leder.Udleveret -= barn.Antal;
                    lodseddel.Solgt += solgt;
                    barn.Solgt += solgt;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    context.Ledere.Update(Leder);
                    context.SaveChanges();
                    context.Børn.Update(barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg); 
                    context.SaveChanges(); 
                }
                if (lodsalg.Modtager.BarnId != null && lodsalg.Sælger.AdminId != null)
                {
                    Admin.Antal += barn.Antal - solgt;
                    lodseddel.Solgt += solgt;
                    barn.Solgt += solgt;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    context.Børn.Update(barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg);
                    context.SaveChanges();  
                }
            }
        }


    }
}
