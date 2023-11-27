using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Models;


namespace LodBasen.Pages.Barn
{
    public class GetBarnModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public IEnumerable<Models.Barn> Børn { get; set; }

        IBarnService barnService { get; set; }
        public GetBarnModel(IBarnService service)
        {
            barnService = service;
        }
        public void OnGet()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Børn = barnService.GetBørn(FilterCriteria);
            }
            else
                Børn = barnService.GetBørn();
        }
    }
}
