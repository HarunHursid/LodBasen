using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface IGruppeService
    {
        IEnumerable<Gruppe> GetGrupper(string search);
        IEnumerable<Gruppe> GetGrupper();
        void AddGruppe(Gruppe gruppe);
        void DeleteGruppe(Gruppe gruppe);
        Gruppe GetGruppeById(int id);
    }
}
