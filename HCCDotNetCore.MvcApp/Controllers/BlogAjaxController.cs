using HCCDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCCDotNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDbContext _context;

        public BlogAjaxController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            List<BlogModel> lst = _context.Blogs.ToList();
            return View("BlogIndex", lst);
        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Redirect("/Blog");
            }
            return View("BlogEdit", item);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Saving successful...." : "Saving failed....";
            BlogMessageResponseModel model = new BlogMessageResponseModel()
            {
                IsSuccess = result > 0,
                Message = message
            };
            return Json(model);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogModel blog)
        {
            BlogMessageResponseModel model = new BlogMessageResponseModel();
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return Json(model);
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            int result = _context.SaveChanges();
            string message = result > 0 ? "Updating successful...." : "Updating failed....";

            model = new BlogMessageResponseModel()
            {
                IsSuccess = result > 0,
                Message = message
            };
            return Json(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BlogDelete(BlogModel blog)
        {
            BlogMessageResponseModel model = new BlogMessageResponseModel();
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == blog.BlogId);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return Json(model);
            }

            _context.Blogs.Remove(item);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Deleting successful...." : "Deleting failed....";

            model = new BlogMessageResponseModel()
            {
                IsSuccess = result > 0,
                Message = message
            };
            return Json(model);
        }
    }
}
