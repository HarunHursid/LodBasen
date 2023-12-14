﻿using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ISalgService
    {
        void AddLodsalg(Lodsalg lodsalg);
        int GetAntalFromAdmin(int? adminId);
        int GetAntalFromLeder(int? lederId);
        public int GetAntalFromLodseddel(int? lodseddelId);
        public IEnumerable<Lodsalg> GetLodsalgssamling();
        public Lodseddel GetLodseddelById(int lodseddelId);

        public void AddOverførsel(Sælger sælger, Modtager modtager, Lodseddel lodseddel, int Antal);



        public String GetSælgerNamebyId(int Id);
        public String GetModtagerNamebyId(int Id);

        


    }
}
