using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Services.Interfaces;
using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Helpers;

namespace LodBasen.Pages.Barn
{
    [RequireAuth(RequiredRole = "Admin")]
    public class CreateBarnModel : PageModel
    {
        [BindProperty]
        public Models.Barn barn { get; set; } = new Models.Barn();
        [BindProperty]
        public List<string> GruppeNavnOptions { get; set; }

        [BindProperty]
        public string SelectedGruppeNavn { get; set; }

        public void OnGet(int id)
        {
            barn = BarnService.GetBarnById(id);

            GruppeNavnOptions = BarnService.GetAllGruppeNavn();
            //barn.BarnId = id;
        }
        IBarnService BarnService;
        public CreateBarnModel(IBarnService service)
        {
            this.BarnService = service;
        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            barn.Gruppe = BarnService.GetGruppeByGruppeNavn(SelectedGruppeNavn);
            barn.Gruppe.GruppeNavn = SelectedGruppeNavn;

            BarnService.AddBarn(barn);
            return RedirectToPage("GetBarn");

        }
    }
}
