using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ISalgService
    {
        int GetAntalFromAdmin();
        int GetAntalFromLeder();
        public int GetAntalFromLodseddel();
    }
}
