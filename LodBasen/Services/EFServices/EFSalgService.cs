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

            Admin? Admin = context.Set<Admin>().FirstOrDefault(a => a.AdminId == sælger.AdminId);
            Leder? LederSælger = context.Set<Leder>().FirstOrDefault(l => l.LederId == sælger.LederId);
            Leder? LederModtager = context.Set<Leder>().FirstOrDefault(l => l.LederId == modtager.LederId);
            Barn? Barn = context.Set<Barn>().FirstOrDefault(b => b.BarnId == modtager.BarnId);

            if (sælger.AdminId != null && modtager.LederId != null)
            {
                if (CheckAntalIsOkAdmin(Admin, antal)/*sælger.Admin.Antal >= antal && (sælger.Admin.Antal - antal) > 0*/)
                {
                    Admin.Antal = Admin.Antal - antal;
                    Admin.Udleveret = Admin.Udleveret + antal;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    LederModtager.Antal = LederModtager.Antal + LederModtager.Antal + antal;
                    context.Ledere.Update(LederModtager);
                    context.SaveChanges();
                    context.Lodsalgssamling.Add(nyOverførsel);
                    context.SaveChanges();
                    //lodseddel.Antal -= antal;

                }
            }
            else if (sælger.AdminId != null && modtager.BarnId != null)
            {
                if (CheckAntalIsOkAdmin(Admin, antal)/*sælger.Admin.Antal >= antal && (sælger.Admin.Antal - antal) > 0*/)
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
            else if (sælger.LederId != null && modtager.BarnId != null)
            {
                if (CheckAntalIsOkLeder(LederSælger, antal)/*sælger.Leder.Antal >= antal && (sælger.Leder.Antal - antal) > 0*/)
                {
                    LederSælger.Antal = LederSælger.Antal - antal;
                    LederSælger.Udleveret = LederSælger.Antal + antal;
                    context.Ledere.Update(LederSælger);
                    context.SaveChanges();
                    Barn.Antal = Barn.Antal + antal;
                    context.Børn.Update(Barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Add(nyOverførsel);
                    context.SaveChanges();
                    //lodseddel.Antal -= antal;
                }
            }

            // Opdater antallet på den relevante entitet (Admin, Barn eller Leder)
            //if (sælger.Admin != null)
            //{
            //    sælger.Admin.Antal += antal;
            //}
            //else if (modtager.Barn != null)
            //{
            //    modtager.Barn.Antal += antal;
            //}
            //else if (modtager.Leder != null)
            //{
            //    modtager.Leder.Antal += antal;
            //}

            // Gem ændringer i databasen
            //context.Lodsalgssamling.Add(nyOverførsel);
            //context.SaveChanges();
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






        //public Sælger GetSælgerById(int id)
        //{
        //    return (Sælger)context.Sælgere.Where(s => s.SælgerId.Equals(id)).AsNoTracking();  
        //}

        //public Modtager GetModtagerById(int id)
        //{
        //    return (Modtager)context.Modtagere.Where(s => s.ModtagerId.Equals(id)).AsNoTracking();
        //}


        public Lodsalg GetLodsalgById(int id)
        {
            return context.Set<Lodsalg>().FirstOrDefault(l => l.LodsalgsId == id);
        }

        public void AfslutOverførsel(Lodsalg lodsalg, int solgt)
        {
            Modtager modtager = context.Set<Modtager>().FirstOrDefault(m => m.ModtagerId.Equals(lodsalg.ModtagerId));
            Sælger sælger = context.Set<Sælger>().FirstOrDefault(s => s.SælgerId.Equals(lodsalg.SælgerId));
            Lodseddel lodseddel = context.Set<Lodseddel>().FirstOrDefault(l => l.LodseddelId.Equals(lodsalg.LodseddelId));

            Admin? Admin = context.Set<Admin>().FirstOrDefault(a => a.AdminId == sælger.AdminId);
            Leder? LederSælger = context.Set<Leder>().FirstOrDefault(l => l.LederId == sælger.LederId);
            Leder? LederModtager = context.Set<Leder>().FirstOrDefault(l => l.LederId == modtager.LederId);
            Barn? Barn = context.Set<Barn>().FirstOrDefault(b => b.BarnId == modtager.BarnId);


            //Admin? Admin = (Admin)context.Admins.Where(a => a.AdminId.Equals(lodsalg.Sælger.AdminId));
            //Leder? LederSælger = (Leder)context.Ledere.Where(l => l.LederId.Equals(lodsalg.Sælger.LederId));
            //Leder? LederModtager = (Leder)context.Ledere.Where(l => l.LederId.Equals(lodsalg.Sælger.LederId));
            //Barn? barn = (Barn)context.Børn.Where(b => b.BarnId.Equals(lodsalg.Modtager.BarnId));

            if (lodsalg != null)
            {   
                //kan først afslutte lodsalg hvis lederen ikke har nogen udleverede lodsedler til børn
                if (modtager.LederId != null && sælger.AdminId != null && modtager.Leder.Udleveret == 0)
                {
                    
                    Admin.Antal += modtager.Leder.Antal;
                    LederModtager.Antal = 0;
                    context.Ledere.Update(LederModtager);
                    context.SaveChanges();
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg);
                    context.SaveChanges();
                    
                }
                if (lodsalg.Modtager.BarnId != null && lodsalg.Sælger.LederId != null && solgt <= modtager.Barn.Antal)
                {
                    //ikke solgte lodsedler kommer retur til leder og solgte bliver lagt i lodseddel tabel
                    LederSælger.Antal += Barn.Antal - solgt;
                    LederSælger.Udleveret -= Barn.Antal;
                    lodseddel.Solgt += solgt;
                    Barn.Solgt += solgt;
                    context.Admins.Update(Admin);
                    context.SaveChanges();
                    context.Ledere.Update(LederSælger);
                    context.SaveChanges();
                    context.Børn.Update(Barn);
                    context.SaveChanges();
                    context.Lodsalgssamling.Remove(lodsalg);
                    context.SaveChanges();
                }
                if (lodsalg.Modtager.BarnId != null && lodsalg.Sælger.AdminId != null && solgt <= modtager.Barn.Antal)
                {
                    Admin.Antal += Barn.Antal - solgt;
                    lodseddel.Solgt += solgt;
                    Barn.Solgt += solgt;
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

