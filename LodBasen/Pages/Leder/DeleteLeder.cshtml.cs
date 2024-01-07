using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Leder
{
    [RequireAuth(RequiredRole = "Admin")]
    public class DeleteLederModel : PageModel
        {
            [BindProperty]
            public Models.Leder leder { get; set; }

            ILederService lederService;

            public DeleteLederModel(ILederService service)
            {
                lederService = service;
            }
            public void OnGet(int id)
            {
                leder = lederService.GetLederById(id);
            }
            public IActionResult OnPost()
            {
                lederService.DeleteLeder(leder);
                return RedirectToPage("GetLeder");
            }
        }
}
