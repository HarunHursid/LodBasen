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
        public int SelectedSælgerId { get; set; }

        [BindProperty]
        public int SelectedModtagerId { get; set; }

        [BindProperty]
        public int SelectedLodseddelId { get; set; }

        [BindProperty]
        public int Antal { get; set; }

        public IEnumerable<SelectListItem> SælgerOptions { get; set; }
        public IEnumerable<SelectListItem> ModtagerOptions { get; set; }
        public IEnumerable<SelectListItem> LodseddelOptions { get; set; }
        public void OnGet(int sælgerId, int modtagerId, int lodseddelId)
        {
            // Hent data fra databasen og fyld dropdown-options
            SælgerOptions = _salgService.GetSælgere().Select(s => new SelectListItem
            {
                Value = s.SælgerId.ToString(),
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
            // Hent sælger, modtager, og lodseddel fra databasen baseret på de valgte id'er
            var sælger = _salgService.GetSælgerById(SelectedSælgerId);
            var modtager = _salgService.GetModtagerById(SelectedModtagerId);
            var lodseddel = _salgService.GetLodseddelById(SelectedLodseddelId);

            // Kald AddOverførsel-metoden
            _salgService.AddOverførsel(sælger, modtager, lodseddel, Antal);

            // Redirect tilbage til GetLodsalg
            return RedirectToPage("/Salg/GetLodsalg");
        }
    }
}
