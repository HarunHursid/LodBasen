using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ILederService
    {
        IEnumerable<Leder> GetLedere(string search);
        IEnumerable<Leder> GetLedere();
        void DeleteLeder(Leder leder);
        void AddLeder(Leder leder);
        void UpdateLeder(Leder leder);
        IEnumerable<Leder> GetLedereByGruppeId(int id);
        Leder GetLederById(int id);
        Gruppe GetGruppeByGruppeNavn(string gruppeNavn);
        List<string> GetAllGruppeNavn();

    }
}
