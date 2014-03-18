namespace SnippetShare.Controllers
{
    using SnippetShare.Domain;
    using SnippetShare.Domain.Repositories.Abstract;
    using SnippetShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using WebMatrix.WebData;
    using SnippetShare.Domain.Repositories.Concrete;

    public class UserController : Controller
    {
        private SnippetShareDbContext db = new SnippetShareDbContext();

        private ISnippetRepository snippetRepo;

        public UserController(ISnippetRepository snippetRepository)
        {
            this.snippetRepo = snippetRepository;
        }

        public ActionResult List()
        {
            var currentUserSnippets = this.snippetRepo.Snippets
                .IncludeMultiple(x => x.User)
                .Where(x => x.UserId == WebSecurity.CurrentUserId)
                .OrderByDescending(x => x.DatePublished)
                .Select(x => new SnippetListVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    DatePublished = x.DatePublished,
                }).ToList();

            return View(currentUserSnippets);
        }
    }
}
