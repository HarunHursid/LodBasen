using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Services.EFServices
{
    public class EFLodseddelService : ILodseddelService
    {
        public lodbasen_dk_db_lodbasenContext context;

        public EFLodseddelService(lodbasen_dk_db_lodbasenContext service)
        {
            context = service;
        }


        public IEnumerable<Lodseddel> Lodsedler { get; set; }

        public IEnumerable<Lodseddel> GetLodsedler()
        {
            Lodsedler = context.Lodsedler;
            return Lodsedler;
        }

        

    }
}
