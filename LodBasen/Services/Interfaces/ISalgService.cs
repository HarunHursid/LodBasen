using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ISalgService
    {
        void AddLodsalg(object lodsalg);
        int GetAntalFromAdmin();
        int GetAntalFromLeder();
        public int GetAntalFromLodseddel();
        public IEnumerable<Lodsalg> GetLodsalgssamling();
        
    }
}
