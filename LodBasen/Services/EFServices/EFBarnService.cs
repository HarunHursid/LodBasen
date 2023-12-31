﻿using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
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

        public void AddBarnToModtager(Barn barn)
        {
            Modtager modtager = new Modtager();
            modtager.BarnId = barn.BarnId;
            context.Modtagere.Add(modtager);
        }
        public void RemoveBarnFromModtager(Barn barn)
        {
            Modtager modtager = context.Set<Modtager>().Where(b => b.BarnId == barn.BarnId).FirstOrDefault();
            context.Modtagere.Remove(modtager);
            context.SaveChanges();
        }
        public void AddBarn(Barn barn)
        {
            context.Børn.Add(barn);
            context.SaveChanges();
            AddBarnToModtager(barn);
            context.SaveChanges();
        }

        public void UpdateBarn(Barn barn)
        {
            context.Børn.Update(barn);
            context.SaveChanges();
        }

        public IEnumerable<Barn> GetBørnByGruppeId(int id)
        {
            return this.context.Set<Barn>().Where(b => b.GruppeId == id).Include(g => g.Gruppe).AsNoTracking().ToList();
        }

        public Barn GetBarnById(int id)
        {
            return context.Børn.Include(b => b.Gruppe).FirstOrDefault(b => b.BarnId == id);

        }
        public Gruppe GetGruppeById(int id)
        {
            return context.Grupper.FirstOrDefault(g => g.GruppeId == id);
        }

        public void DeleteBarn(Barn barn)
        {
            if (barn != null)
            {
                RemoveBarnFromModtager(barn);
                context.SaveChanges();
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
            return context.Børn.Where(b => b.Navn.Contains(search)).AsQueryable();
        }

        public IQueryable<Barn> GetBørnQuery()
        {
            return context.Børn.AsQueryable();
        }

        public IEnumerable<int> GetSuperSælgerBarn()
        {
            IEnumerable<Barn> børnListe = context.Børn.AsNoTracking().ToList();

            IEnumerable<int> solgtListe = børnListe.Select(item => item.Solgt).ToList();

            solgtListe = solgtListe.OrderByDescending(x => x).ToList();

            return solgtListe;
        }
    }
}
