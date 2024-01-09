using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Models;
using LodBasen.Helpers;
using Microsoft.AspNetCore.Authorization;
using Xunit.Sdk;

namespace LodBasen.Pages.Gruppe
{
    [RequireAuth]
	public class GetGruppeModel : PageModel
    {

		[BindProperty(SupportsGet = true)]
		public string Search { get; set; }

		private IGruppeService gruppeService;

		public GetGruppeModel(IGruppeService service)
		{
			gruppeService = service;
		}
		public IEnumerable<Models.Gruppe> Grupper { get; set; } = new List<Models.Gruppe>();
		public void OnGet()
		{
				if (!String.IsNullOrEmpty(Search))
				{
					Grupper = gruppeService.GetGrupper(Search);
				}
				else
				{
					Grupper = gruppeService.GetGrupper();
				}
			
		}
	}
}
