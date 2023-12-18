using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Gruppe
{
    [RequireAuth(RequiredRole = "Admin")]
    public class CreateGruppeModel : PageModel
    {

        [BindProperty]
        public Models.Gruppe Gruppe { get; set; } = new Models.Gruppe();
        public void OnGet(int id)
        {
            Gruppe.GruppeId = id;
        }
        IGruppeService GruppeService;
        public CreateGruppeModel(IGruppeService service)
        {
            this.GruppeService = service;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            GruppeService.AddGruppe(Gruppe);
            return RedirectToPage("GetGruppe");
        }
    }
}
