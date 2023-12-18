using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Leder
{
    [RequireAuth(RequiredRole = "Admin")]
    public class CreateLederModel : PageModel
    {
        [BindProperty]
        public Models.Leder leder { get; set; } = new Models.Leder();

        [BindProperty]
        public List<string> GruppeNavnOptions { get; set; }

        [BindProperty]
        public string SelectedGruppeNavn { get; set; }

        public void OnGet(int id)
        {
            leder = LederService.GetLederById(id);

            GruppeNavnOptions = LederService.GetAllGruppeNavn();
        }
        ILederService LederService;
        public CreateLederModel(ILederService service)
        {
            this.LederService = service;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            leder.Gruppe = LederService.GetGruppeByGruppeNavn(SelectedGruppeNavn);
            leder.Gruppe.GruppeNavn = SelectedGruppeNavn;

            LederService.AddLeder(leder);
            return RedirectToPage("GetLeder");
        }
    }
}
