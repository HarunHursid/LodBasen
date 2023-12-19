using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Services.Interfaces;
using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Helpers;

namespace LodBasen.Pages.Barn
{
    [RequireAuth(RequiredRole = "Admin")]
    public class UpdateBarnModel : PageModel
    {
        [BindProperty]
        public Models.Barn barn { get; set; }
        [BindProperty]
        public List<string> GruppeNavnOptions { get; set; }

        [BindProperty]
        public string SelectedGruppeNavn { get; set; }

        public void OnGet(int id)
        {
            barn = BarnService.GetBarnById(id);
            GruppeNavnOptions = BarnService.GetAllGruppeNavn();
        }

        IBarnService BarnService;
        public UpdateBarnModel(IBarnService service)
        {
            this.BarnService = service;
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!string.IsNullOrEmpty(SelectedGruppeNavn))
            {
                Models.Gruppe gruppe = BarnService.GetGruppeByGruppeNavn(SelectedGruppeNavn);
                barn.GruppeId = gruppe?.GruppeId ?? 0;
            }
            BarnService.UpdateBarn(barn);
            return RedirectToPage("GetBarn");
        }
    }
}
