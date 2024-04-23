using HCCDotNetCore.MvcApp.Models;
using HCCDotNetCore.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HCCDotNetCore.MvcApp.Controllers
{
    public class BlogAdoDotNetController : Controller
    {
        private readonly AdoDotNetService _aodoDotNetService;
        public BlogAdoDotNetController(AdoDotNetService adoDotNetService)
        {
            _aodoDotNetService = adoDotNetService;
        }
        public IActionResult Index()
        {
            string query = "select * from tbl_blog;";
            var lst = _aodoDotNetService.Query<BlogModel>(query);
            return View(lst);
        }
    }
}
