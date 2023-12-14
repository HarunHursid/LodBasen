using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


    namespace LodBasen.Pages.Salg
    {
    public class CreateLodsalgModel : PageModel
    {
        private readonly ISalgService _salgService;

        public CreateLodsalgModel(ISalgService salgService)
        {
            _salgService = salgService;
        }

        [BindProperty]
        public int SelectedSælgerId { get; set; }

        [BindProperty]
        public int SelectedModtagerId { get; set; }

        [BindProperty]
        public int SelectedLodseddelId { get; set; }

        [BindProperty]
        public int Antal { get; set; }

        public IActionResult OnPost()
        {

            _salgService.AddOverførsel(SelectedSælgerId, SelectedModtagerId, SelectedLodseddelId, Antal);




            return RedirectToPage("/SuccessPage");
        }
    }
}
