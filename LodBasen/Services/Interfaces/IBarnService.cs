using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface IBarnService
    {
        IEnumerable<Barn> GetBørn(string search);
        IEnumerable<Barn> GetBørn();
        void AddBarn(Barn barn);
        //Barn GetBørnByGruppeId(int id);
    }
}
