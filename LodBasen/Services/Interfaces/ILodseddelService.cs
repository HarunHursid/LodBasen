using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ILodseddelService
    {
        public IEnumerable<Lodseddel> GetLodsedler();
    }
}
