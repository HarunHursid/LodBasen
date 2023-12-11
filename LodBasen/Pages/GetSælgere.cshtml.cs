using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages
{
    public class GetSælgereModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public IEnumerable<Models.Sælger> Sælgere { get; set; }

        ISælgerService sælgerService { get; set; }
        public GetSælgereModel(ISælgerService service)
        {
            sælgerService = service;
        }
        public void OnGet()
        {
            Sælgere = sælgerService.GetSælgere();
        }
    }
}
