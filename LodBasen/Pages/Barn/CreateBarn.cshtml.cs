using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Services.Interfaces;
using LodBasen.Models;

namespace LodBasen.Pages.Barn
{
    public class CreateBarnModel : PageModel
    {
        [BindProperty]
        public Models.Barn barn { get; set; } = new Models.Barn();
        public void OnGet(int id)
        {
            barn.BarnId = id;
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
            BarnService.AddBarn(barn);
            return RedirectToPage("GetBarn");
        }
    }
}
