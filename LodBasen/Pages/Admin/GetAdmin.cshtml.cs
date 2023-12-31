using LodBasen.Helpers;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodBasen.Pages.Admin
{
    [RequireAuth(RequiredRole = "Admin")]
    public class GetAdminModel : PageModel
    {
        [BindProperty]

        public IEnumerable<Models.Admin> Admins { get; set; }

        public IEnumerable<Models.Lodseddel> LodseddelInfo { get; set; }

        IAdminService AdminService { get; set; }

        public GetAdminModel(IAdminService service)
        {
            AdminService = service;
        }
        public void OnGet()
        {
            LodseddelInfo = AdminService.GetLodsalgForAdmin();
            Admins = AdminService.GetAdmins();

        }
    }
}
