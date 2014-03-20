﻿namespace SnippetShare.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using SnippetShare.Models;
    using WebMatrix.WebData;
    using SnippetShare.Domain.Repositories.Abstract;
    using SnippetShare.Domain.Entities;
    using SnippetShare.Domain.Repositories.Concrete;
    using SnippetShare.Instrastructure.WebSecurity;
    using System.Web;

    public class HomeController : Controller
    {
        private ISnippetRepository snippetRepo;
        private IWebSecurity webSecurity;

        public HomeController(ISnippetRepository snippetRepository, IWebSecurity webSecurity)
        {
            this.snippetRepo = snippetRepository;
            this.webSecurity = webSecurity;
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
                    Title = vm.Title,
                    Content = vm.Content,
                    DatePublished = DateTime.Now
                };

                if (webSecurity.IsAuthenticated)
                {
                    snippet.UserId = webSecurity.CurrentUserId;
                }

                this.snippetRepo.Add(snippet);

                return RedirectToAction("Show", new { id = snippet.Id });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Show(long id)
        {
            Snippet snippet = this.snippetRepo.Snippets
                .IncludeMultiple(x => x.User)
                .FirstOrDefault(x => x.Id == id);

            if (snippet == null)
            {
                return HttpNotFound("Snippet with id " + id + " was not found");
            }

            ShowVM viewModel = new ShowVM
            {
                Id = snippet.Id,
                Title = snippet.Title,
                Content = snippet.Content,
                DatePublished = snippet.DatePublished,
            };

            if (snippet.User != null)
            {
                viewModel.UserId = snippet.User.UserId;
                viewModel.UserName = snippet.User.UserName;
            }

            return View(viewModel);
        }

        public ActionResult Embedded(long id, string follow = "no-follow")
        {
            Snippet snippet = this.snippetRepo.GetById(id);
            if (snippet == null)
            {
                return HttpNotFound("Snippet with id " + id + " was not found");
            }

            ViewBag.Id = id;
            ViewBag.Follow = follow;

            return View((object)snippet.Content);
        }

        public ActionResult Raw(long id)
        {
            Snippet snippet = this.snippetRepo.GetById(id);
            if (snippet == null)
            {
                return HttpNotFound("Snippet with id " + id + " was not found");
            }

            return PartialView(
                "~/Views/Home/DisplayTemplates/_SnippetContent.cshtml",
                snippet.Content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Preview(string content)
        {
            return PartialView(
                "~/Views/Home/DisplayTemplates/_SnippetContent.cshtml", 
                content);
        }

        [Authorize]
        public ActionResult Edit(long id)
        {
            var snippet = this.snippetRepo.GetById(id);
            if (snippet == null)
            {
                return HttpNotFound("Snippet with id " + id + " was not found");
            }

            int? userId = snippet.UserId;
            if (userId == null || userId != webSecurity.CurrentUserId)
            {
                return new HttpUnauthorizedResult("User is performing unautorized action");
            }

            var vm = new EditVM
            {
                Id = snippet.Id,
                Title = snippet.Title,
                Content = snippet.Content
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(EditVM snippet)
        {
            var dbSnippet = this.snippetRepo.GetById(snippet.Id);
            if (dbSnippet == null)
            {
                return HttpNotFound("Snippet with id " + snippet.Id + " was not found");
            }

            int? userId = dbSnippet.UserId;
            if (userId == null || userId != webSecurity.CurrentUserId)
            {
                return new HttpUnauthorizedResult("User is performing unautorized action");
            }

            if (ModelState.IsValid)
            {
                dbSnippet.Title = snippet.Title;
                dbSnippet.Content = snippet.Content;
                dbSnippet.DatePublished = DateTime.Now;
                this.snippetRepo.Update(dbSnippet);
                return RedirectToAction("Show", new { id = dbSnippet.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
