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

        public IEnumerable<Sælger> Sælgere { get; set; }

        public IEnumerable<Modtager> Modtagere { get; set; }

        public IEnumerable<Lodsalg> Lodsalgssamling { get; set; }


        public IEnumerable<Lodsalg> GetLodsalgssamling()
        {
            Lodsalgssamling = context.Lodsalgssamling.Include(m => m.Modtager.Barn).Include(m => m.Modtager.Leder).Include(s => s.Sælger.Admin).Include(s => s.Sælger.Leder);
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
            return context.Sælgere.Include(a => a.Admin).Include(l => l.Leder).ToList();
        }

        public IEnumerable<Modtager> GetModtagere()
        {
            return context.Modtagere.Include(l => l.Leder).Include(b => b.Barn).ToList();
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

            Admin? Admin = context.Set<Admin>().FirstOrDefault(a => a.AdminId == sælger.AdminId);
            Leder? LederSælger = context.Set<Leder>().FirstOrDefault(l => l.LederId == sælger.LederId);
            Leder? LederModtager = context.Set<Leder>().FirstOrDefault(l => l.LederId == modtager.LederId);
            Barn? Barn = context.Set<Barn>().FirstOrDefault(b => b.BarnId == modtager.BarnId);

            if (sælger.AdminId != null && modtager.LederId != null)
            {
                if (CheckAntalIsOkAdmin(Admin, antal))
                {
                    Admin.Antal = Admin.Antal - antal;
                    Admin.Udleveret = Admin.Udleveret + antal;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    LederModtager.Antal = LederModtager.Antal + antal;
                    context.Ledere.Update(LederModtager);
                    context.SaveChanges();
                    context.Lodsalgssamling.Add(nyOverførsel);
                    context.SaveChanges();
                }
            }
            if (sælger.AdminId != null && modtager.BarnId != null)
            {
                if (CheckAntalIsOkAdmin(Admin, antal))
                {
                    Admin.Antal = Admin.Antal - antal;
                    Admin.Udleveret = Admin.Udleveret + antal;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    Barn.Antal = Barn.Antal + antal;
                    context.Børn.Update(Barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Add(nyOverførsel);
                    context.SaveChanges();
                }
            }
            if (sælger.LederId != null && modtager.BarnId != null)
            {
                if (CheckAntalIsOkLeder(LederSælger, antal))
                {
                    LederSælger.Antal = LederSælger.Antal - antal;
                    LederSælger.Udleveret = LederSælger.Udleveret + antal;
                    context.Ledere.Update(LederSælger);
                    context.SaveChanges();
                    Barn.Antal = Barn.Antal + antal;
                    context.Børn.Update(Barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Add(nyOverførsel);
                    context.SaveChanges();
                }
            }
        }

        public bool CheckAntalIsOkAdmin(Admin admin, int overførtAntal)
        {
            if (admin.Antal >= overførtAntal && admin.Antal > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckAntalIsOkLeder(Leder leder, int overførtAntal)
        {
            if (leder.Antal >= overførtAntal && leder.Antal > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Lodsalg GetLodsalgById(int id)
        {
            return context.Set<Lodsalg>().Include(l => l.Sælger).Include(m => m.Modtager).Include(l => l.Lodseddel).FirstOrDefault(l => l.LodsalgsId == id);
        }

        public void AfslutOverførsel(Lodsalg lodsalg, int solgtInput)
        {
            if (lodsalg != null)
            {
                Admin? Admin = context.Admins.FirstOrDefault(a => a.AdminId == lodsalg.Sælger.AdminId);
                Leder? LederSælger = context.Set<Leder>().FirstOrDefault(l => l.LederId == lodsalg.Sælger.LederId);
                Leder? LederModtager = context.Set<Leder>().FirstOrDefault(l => l.LederId == lodsalg.Modtager.LederId);
                Barn? Barn = context.Set<Barn>().FirstOrDefault(b => b.BarnId == lodsalg.Modtager.BarnId);

                //kan først afslutte lodsalg hvis lederen ikke har nogen udleverede lodsedler til børn
                if (lodsalg.Modtager.LederId != null && lodsalg.Sælger.AdminId != null && lodsalg.Modtager.Leder.Udleveret == 0)
                {
                    Admin.Antal = Admin.Antal + LederModtager.Antal;
                    Admin.Udleveret = Admin.Udleveret - LederModtager.Antal;
                    LederModtager.Antal = 0;
                    context.Ledere.Update(LederModtager);
                    context.SaveChanges();
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg);
                    context.SaveChanges();
                }
                else if (lodsalg.Modtager.BarnId != null && lodsalg.Sælger.LederId != null && solgtInput <= lodsalg.Modtager.Barn.Antal)
                {
                    //ikke solgte lodsedler kommer retur til leder og solgte bliver lagt i lodseddel tabel under solgt
                    LederSælger.Antal = LederSælger.Antal + (Barn.Antal - solgtInput);
                    LederSælger.Udleveret = LederSælger.Udleveret - Barn.Antal;
                    lodsalg.Lodseddel.Solgt = lodsalg.Lodseddel.Solgt + solgtInput;
                    Barn.Solgt = Barn.Solgt + solgtInput;
                    Barn.Antal = 0;
                    context.Ledere.Update(LederSælger);
                    context.SaveChanges();
                    context.Børn.Update(Barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg);
                    context.SaveChanges();
                }
                else if (lodsalg.Modtager.BarnId != null && lodsalg.Sælger.AdminId != null && solgtInput <= lodsalg.Modtager.Barn.Antal)
                {
                    Admin.Antal = Admin.Antal + (Barn.Antal - solgtInput);
                    Admin.Udleveret = Admin.Udleveret - Barn.Antal;
                    lodsalg.Lodseddel.Solgt = lodsalg.Lodseddel.Solgt + solgtInput;
                    Barn.Solgt = Barn.Solgt + solgtInput;
                    Barn.Antal = 0;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    context.Børn.Update(Barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg);
                    context.SaveChanges();
                }
            }
        }
    }
}

