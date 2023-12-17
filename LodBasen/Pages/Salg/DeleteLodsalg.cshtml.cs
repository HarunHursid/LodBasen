using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LodBasen.Pages.Salg
{
    public class DeleteLodsalgModel : PageModel
    {
        [BindProperty]
        public Models.Lodsalg lodsalg{ get; set; }

        [BindProperty]
        public int Solgt { get; set; }  

        ISalgService salgService;

        public DeleteLodsalgModel(ISalgService service)
        {
            salgService = service;
        }
        public void OnGet(int id)
        {
            lodsalg = salgService.GetLodsalgById(id);
        }
        public IActionResult OnPost()
        {
            salgService.AfslutOverførsel(lodsalg, Solgt);

            return RedirectToPage("Salg/GetLodsalg");
        }
    }
}
