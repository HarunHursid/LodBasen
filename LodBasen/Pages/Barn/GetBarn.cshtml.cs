using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LodBasen.Models;
using LodBasen.Services.EFServices;
using Microsoft.EntityFrameworkCore;
using LodBasen.Helpers;

namespace LodBasen.Pages.Barn
{
    [RequireAuth]
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < TotalPages);

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
    public class GetBarnModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public PaginatedList<Models.Barn> Børn { get; set; }
        //public IEnumerable<Models.Barn> Børn { get; set; }

        IBarnService barnService { get; set; }
        public GetBarnModel(IBarnService service)
        {
            barnService = service;
        }
        public void OnGet(int? pageIndex)
        {
            const int pageSize = 10; // Adjust as needed

            if (!String.IsNullOrEmpty(Search))
            {
                IQueryable<Models.Barn> searchQuery = barnService.GetBørnQuery(Search);
                Børn = PaginatedList<Models.Barn>.Create(searchQuery.Include(b => b.Gruppe), pageIndex ?? 1, pageSize);
            }
            else
            {
                IQueryable<Models.Barn> allBørnQuery = barnService.GetBørnQuery().Include(b => b.Gruppe);
                Børn = PaginatedList<Models.Barn>.Create(allBørnQuery, pageIndex ?? 1, pageSize);
            }

        }
    }
}
