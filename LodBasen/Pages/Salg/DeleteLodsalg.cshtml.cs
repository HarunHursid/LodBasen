using LodBasen.Helpers;
using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LodBasen.Pages.Salg
{
    public class DeleteLodsalgModel : PageModel
    {
        [RequireAuth(RequiredRole = "Admin")]

        [BindProperty]
        public Models.Lodsalg lodsalg{ get; set; }

        [BindProperty]
        public int solgtInput { get; set; }  

        ISalgService salgService;

        public DeleteLodsalgModel(ISalgService service)
        {
            salgService = service;
        }
        public void OnGet(int id)
        {
            lodsalg = salgService.GetLodsalgById(id);
        }
        public IActionResult OnPost(int id)
        {
           
            Lodsalg lodsalg = salgService.GetLodsalgById(id);
            salgService.AfslutOverførsel(lodsalg, solgtInput);

            return RedirectToPage("/Salg/GetLodsalg");
        }
    }
}
