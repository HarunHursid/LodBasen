using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace LodBasen.Pages
{
    public class GetSuperSælgerModel : PageModel
    {
        [RequireNoAuth]
        
        [BindProperty(SupportsGet = true)]
        public IEnumerable<int> SuperSælgere { get; set; }

        IBarnService SuperSælgerService { get; set; }
        public GetSuperSælgerModel(IBarnService service)
        {
            SuperSælgerService = service;
        }
        public void OnGet()
        {
            SuperSælgere = SuperSælgerService.GetBørn().ToList().Select(item => item.Solgt).ToList().OrderByDescending(x => x).ToList();
        }
    }
}
