using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Services.Interfaces;
using LodBasen.Models;
using LodBasen.Services.EFServices;

namespace LodBasen.Pages.Barn
{
    public class UpdateBarnModel : PageModel
    {
        [BindProperty]
        public Models.Barn barn { get; set; }

        public void OnGet(int id)
        {
            //barn.BarnId = id;
            barn = BarnService.GetBarnById(id);
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
            BarnService.UpdateBarn(barn);
            return RedirectToPage("GetBarn");
        }
    }
}
