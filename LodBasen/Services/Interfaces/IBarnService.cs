using LodBasen.Models;
using System.Security.Cryptography;

namespace LodBasen.Services.Interfaces
{
    public interface IBarnService
    {
        IQueryable<Barn> GetBørnQuery(string search);
        IQueryable<Barn> GetBørnQuery();
        IEnumerable<Barn> GetBørn(string search);
        IEnumerable<Barn> GetBørn();
        void DeleteBarn(Barn barn);
        void AddBarn(Barn barn);
        void UpdateBarn(Barn barn);
        IEnumerable<Barn> GetBørnByGruppeId(int id);
        Barn GetBarnById(int id);
        Gruppe GetGruppeById(int id);
        Gruppe GetGruppeByGruppeNavn(string gruppeNavn);
        List<string> GetAllGruppeNavn();
        IEnumerable<int> GetSuperSælgerBarn();   
    }
}
