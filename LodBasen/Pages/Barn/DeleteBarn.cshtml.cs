using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Services.Interfaces;
using LodBasen.Models;
using LodBasen.Helpers;

namespace LodBasen.Pages.Barn
{
    [RequireAuth(RequiredRole = "Admin")]
    public class DeleteBarnModel : PageModel
    {
        [BindProperty]
        public Models.Barn barn { get; set; }

        IBarnService barnService;

        public DeleteBarnModel(IBarnService service)
        {
            barnService = service;
        }
        public void OnGet(int id)
        {
            barn = barnService.GetBarnById(id);
        }
        public IActionResult OnPost()
        {
            barnService.DeleteBarn(barn);

            return RedirectToPage("GetBarn");
        }
    }
}
