using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Leder
{
    [RequireAuth(RequiredRole = "Admin")]
    public class UpdateLederModel : PageModel
    {
        [BindProperty]
        public Models.Leder leder { get; set; }

        [BindProperty]
        public List<string> GruppeNavnOptions { get; set; }

        [BindProperty]
        public string SelectedGruppeNavn { get; set; }

        public void OnGet(int id)
        {
            leder = LederService.GetLederById(id);

            GruppeNavnOptions = LederService.GetAllGruppeNavn();
            SelectedGruppeNavn = leder.Gruppe?.GruppeNavn;
        }

        ILederService LederService;
        public UpdateLederModel(ILederService service)
        {
            this.LederService = service;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!string.IsNullOrEmpty(SelectedGruppeNavn))
            {
                LodBasen.Models.Gruppe gruppe = LederService.GetGruppeByGruppeNavn(SelectedGruppeNavn);
                leder.GruppeId = gruppe?.GruppeId ?? 0;
            }

            LederService.UpdateLeder(leder);
            return RedirectToPage("GetLeder");
        }
    }
}
