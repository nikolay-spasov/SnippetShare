using SnippetShare.DataAccess;
using SnippetShare.DataAccess.Entities;
using SnippetShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SnippetShare.Controllers
{
    public class HomeController : Controller
    {
        private SnippetShareDbContext db = new SnippetShareDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateSnippet()
        {
            var viewModel = new CreateSnippetVM();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateSnippet(CreateSnippetVM vm)
        {
            if (ModelState.IsValid)
            {
                Snippet snippet = new Snippet
                {
                    Content = vm.Content,
                    DatePublished = DateTime.Now
                };

                if (WebSecurity.IsAuthenticated)
                {
                    snippet.UserId = WebSecurity.CurrentUserId;
                }

                db.Snippets.Add(snippet);
                db.SaveChanges();

                return RedirectToAction("Show", new { id = snippet.Id });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Show(int id)
        {
            Snippet snippet = db.Snippets.Include("User").FirstOrDefault(x => x.Id == id);

            ShowVM viewModel = new ShowVM
            {
                Id = snippet.Id,
                Content = snippet.Content,
                DatePublished = snippet.DatePublished,
            };

            return View(viewModel);
        }
    }
}
