using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Gruppe
{
    [RequireAuth(RequiredRole = "Admin")]
    public class DeleteGruppeModel : PageModel
    {

        [BindProperty]
        public Models.Gruppe Gruppe { get; set; }

        IGruppeService gruppeService;

        public DeleteGruppeModel(IGruppeService service)
        {
            gruppeService = service;
        }
        public void OnGet(int id)
        {
            Gruppe = gruppeService.GetGruppeById(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await gruppeService.DeleteGruppe(Gruppe);
            return RedirectToPage("GetGruppe");
        }
    }
}
