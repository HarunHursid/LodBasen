using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Leder
{
    public class UpdateLederModel : PageModel
    {
        [BindProperty]
        public Models.Leder leder { get; set; }

        public void OnGet(int id)
        {
            leder = LederService.GetLederById(id);
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
            LederService.UpdateLeder(leder);
            return RedirectToPage("GetLeder");
        }
    }
}
