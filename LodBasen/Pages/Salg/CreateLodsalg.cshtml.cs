using LodBasen.Helpers;
using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace LodBasen.Pages.Salg
{
    [RequireAuth]
    public class CreateLodsalgModel : PageModel
    {

        private readonly ISalgService _salgService;

        public CreateLodsalgModel(ISalgService salgService)
        {
            _salgService = salgService;
        }

        [BindProperty]
        public int SelectedS�lgerId { get; set; }

        [BindProperty]
        public int SelectedModtagerId { get; set; }

        [BindProperty]
        public int SelectedLodseddelId { get; set; }

        [BindProperty]
        public int Antal { get; set; }

        public IEnumerable<SelectListItem> S�lgerOptions { get; set; }
        public IEnumerable<SelectListItem> ModtagerOptions { get; set; }
        public IEnumerable<SelectListItem> LodseddelOptions { get; set; }
        public void OnGet(int s�lgerId, int modtagerId, int lodseddelId)
        {
            // Hent data fra databasen og fyld dropdown-options
            S�lgerOptions = _salgService.GetS�lgere().Select(s => new SelectListItem
            {
                Value = s.S�lgerId.ToString(),
                Text = s.Admin?.Navn.ToString() + s.Leder?.Navn.ToString()
                
            }) ;

            ModtagerOptions = _salgService.GetModtagere().Select(m => new SelectListItem
            {
                Value = m.ModtagerId.ToString(),
                Text = m.Leder?.Navn.ToString() + m.Barn?.Navn.ToString() 
            });

            LodseddelOptions = _salgService.GetLodsedler().Select(l => new SelectListItem
            {
                Value = l.LodseddelId.ToString(),
                Text = l.LodseddelId.ToString()
            });
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Hent s�lger, modtager, og lodseddel fra databasen baseret p� de valgte id'er
            var s�lger = _salgService.GetS�lgerById(SelectedS�lgerId);
            var modtager = _salgService.GetModtagerById(SelectedModtagerId);
            var lodseddel = _salgService.GetLodseddelById(SelectedLodseddelId);

            // Kald AddOverf�rsel-metoden
            _salgService.AddOverf�rsel(s�lger, modtager, lodseddel, Antal);

            // Redirect tilbage til GetLodsalg
            return RedirectToPage("/Salg/GetLodsalg");
        }
    }
}
