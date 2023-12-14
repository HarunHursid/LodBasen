using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ISalgService
    {
        void AddLodsalg();
        int GetAntalFromAdmin(int? adminId);
        int GetAntalFromLeder(int? lederId);
        public int GetAntalFromLodseddel(int? lodseddelId);
        public IEnumerable<Lodsalg> GetLodsalgssamling();

        public Lodseddel GetLodseddelById(int lodseddelId);

        public void AddOverførsel(Sælger sælger, Modtager modtager, Lodseddel lodsedel, int Antal);


    }
}
