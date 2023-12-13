using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Salg
{
    public class GetLodsalgModel : PageModel
    {
        [BindProperty]
        public string Search { get; set; }
        public IEnumerable<Models.Lodsalg> Lodsalgssamling { get; set; }

        ISalgService salgService { get; set; }
        
        public GetLodsalgModel(ISalgService service)
        {
        salgService = service;
        }
        public void OnGet()
        {
            Lodsalgssamling = salgService.GetLodsalgssamling();  
        }
    }
}
