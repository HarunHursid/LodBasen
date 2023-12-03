using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Leder
{
    public class CreateLederModel : PageModel
    {
        [BindProperty]
        public Models.Leder leder { get; set; } = new Models.Leder();
        public void OnGet(int id)
        {
            leder.LederId = id;
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
            LederService.AddLeder(leder);
            return RedirectToPage("GetLeder");
        }
    }
}
