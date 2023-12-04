using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Models;


namespace LodBasen.Pages.Barn
{
    public class GetBarnModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public IEnumerable<Models.Barn> B�rn { get; set; }

        IBarnService barnService { get; set; }
        public GetBarnModel(IBarnService service)
        {
            barnService = service;
        }
        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Search))
            {
                B�rn = barnService.GetB�rn(Search);
            }
            else
                B�rn = barnService.GetB�rn();
        }
    }
}
