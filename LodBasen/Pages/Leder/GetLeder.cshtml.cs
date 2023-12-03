using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Services.Interfaces;

namespace LodBasen.Pages.Leder
{
    public class GetLederModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public IEnumerable<Models.Leder> Ledere { get; set; }

        ILederService lederService { get; set; }
        public GetLederModel(ILederService service)
        {
            lederService = service;
        }
        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Search))
            {
                Ledere = lederService.GetLedere(Search);
            }
            else
                Ledere = lederService.GetLedere();
        }
    }
}
