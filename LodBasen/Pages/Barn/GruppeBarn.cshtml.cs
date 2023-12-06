using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Barn
{
    public class GruppeBarnModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public string GruppeNavn { get; set; }
        public IEnumerable<Models.Barn> B�rn { get; set; }

        IBarnService barnService { get; set; }
        public GruppeBarnModel(IBarnService service)
        {
            barnService = service;
        }
        public void OnGet(int id)
        {
            //B�rn = barnService.GetB�rnByGruppeId(id);
            var gruppe = barnService.GetGruppeById(id);
            GruppeNavn = gruppe?.GruppeNavn;
            B�rn = barnService.GetB�rnByGruppeId(id);
        }
    }
}
