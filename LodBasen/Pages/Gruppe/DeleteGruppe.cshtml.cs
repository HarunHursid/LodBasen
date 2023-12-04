using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Gruppe
{
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
        public IActionResult OnPost()
        {
            gruppeService.DeleteGruppe(Gruppe);

            return RedirectToPage("GetGruppe");
        }
    }
}
