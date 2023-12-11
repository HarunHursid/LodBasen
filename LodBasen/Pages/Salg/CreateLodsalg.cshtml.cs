using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Salg
{
    public class CreateLodsalgModel : PageModel
    {
        [BindProperty(SupportsGet = true)]

        public string Search { get; set; }
        public IEnumerable<Models.Lodsalg> Lodsalgssamling { get; set; }

        ISalgService SalgService { get; set; }
        //public GetLodsalgModel(ISalgService service)
        //{
        //    SalgService = service;
        //}

        //public void OnGet(int id)
        //{
    
        //}

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    SalgService.AddLodsalg(lodsalg);
        //    return RedirectToPage("GetLodsalg");
        //}
    }
}
