using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages
{
    [RequireAuth(RequiredRole = "Admin")]
    public class GetModtagereModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public IEnumerable<Models.Modtager> Modtagere { get; set; }

        IModtagerService modtagereService { get; set; }
        public GetModtagereModel(IModtagerService service)
        {
            modtagereService = service;
        }
        public void OnGet()
        {
            Modtagere = modtagereService.GetModtagere();
        }
       
    }
}
