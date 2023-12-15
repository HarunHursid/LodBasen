using LodBasen.Models;

namespace LodBasen.Services.Interfaces
{
    public interface ISælgerService
    {
        public IEnumerable<Sælger> Sælgere { get; set; }
        public IEnumerable<Sælger> GetSælgere();
        

    }
}
