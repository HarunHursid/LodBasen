using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ISalgService
    {
        void AddLodsalg();
        int GetAntalFromAdmin();
        int GetAntalFromLeder();
        public int GetAntalFromLodseddel();
        public IEnumerable<Lodsalg> GetLodsalgssamling();

        public void AddOverførsel(Sælger sælger, Modtager modtager, int Antal);
        
    }
}
