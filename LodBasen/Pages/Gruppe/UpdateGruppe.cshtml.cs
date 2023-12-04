using LodBasen.Models;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Gruppe
{
    public class UpdateGruppeModel : PageModel
    {
        [BindProperty]
        public Models.Gruppe gruppe { get; set; } 

        public void OnGet(int id)
        {
            
            gruppe = gruppeService.GetGruppeById(id);
        }

        IGruppeService gruppeService;
        public UpdateGruppeModel(IGruppeService service)
        {
            this.gruppeService = service;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            gruppeService.UpdateGruppe(gruppe);
            return RedirectToPage("GetGruppe");
        }
    }
}
