using HCCDotNetCore.MvcApp.Models;
using HCCDotNetCore.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HCCDotNetCore.MvcApp.Controllers
{
    public class BlogDapperController : Controller
    {
        private readonly DapperService _dapperService;

        public BlogDapperController(DapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public IActionResult Index()
        {
            string query = "select * from tbl_blog;";
            var lst = _dapperService.Query<BlogModel>(query);
            return View(lst);
        }
    }
}
