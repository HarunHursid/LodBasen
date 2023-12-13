using LodBasen.Models;



namespace LodBasen.Services.Interfaces
{
    public interface IModtagerService
    {
        public IEnumerable<Modtager> Modtagere { get; set; }

        public IEnumerable<Modtager> GetModtagere();
    }
}
