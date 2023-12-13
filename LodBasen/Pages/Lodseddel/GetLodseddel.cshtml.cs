using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Lodseddel
{
    public class GetLodseddelModel : PageModel
    {
        [BindProperty]
        
        public IEnumerable<Models.Lodseddel> Lodsedler { get; set; }

        ILodseddelService lodseddelService { get; set; }

        public GetLodseddelModel(ILodseddelService service)
        {
            lodseddelService = service;
        }
        public void OnGet()
        {
            Lodsedler = lodseddelService.GetLodsedler();
        }
    }
   
    
}
