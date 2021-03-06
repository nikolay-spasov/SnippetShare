﻿namespace SnippetShare.Tests
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SnippetShare.Controllers;
    using SnippetShare.Domain.Entities;
    using SnippetShare.Domain.Repositories.Abstract;
    using SnippetShare.Instrastructure.WebSecurity;
    using SnippetShare.Models;

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void CanShowSnippet()
        {
            Mock<ISnippetRepository> repo = new Mock<ISnippetRepository>();
            repo.Setup(m => m.Snippets).Returns(new Snippet[]
            {
                new Snippet { Id = 1, Title = "T1", Content = "C1", UserId = null, User = null },
            }.AsQueryable());

            HomeController controller = new HomeController(repo.Object, null);

            var result = (controller.Show(1) as ViewResult).Model as ShowVM;

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("T1", result.Title);
            Assert.AreEqual("C1", result.Content);
            Assert.IsNull(result.UserId);
            Assert.IsNull(result.UserName);
        }

        [TestMethod]
        public void CanShowSnippetRaw()
        {
            Mock<ISnippetRepository> repo = new Mock<ISnippetRepository>();
            repo.Setup(m => m.GetById(It.Is<long>(x => x == 1)))
                .Returns(new Snippet { Id = 1, Title = "T1", Content = "C1", UserId = null, User = null });

            Mock<IWebSecurity> webSec = new Mock<IWebSecurity>();

            HomeController controller = new HomeController(repo.Object, webSec.Object);

            var result = controller.Raw(1) as PartialViewResult;
            var model = result.Model as string;

            Assert.AreEqual("~/Views/Home/DisplayTemplates/_SnippetContent.cshtml", result.ViewName);
            Assert.AreEqual("C1", model);
        }

        [TestMethod]
        public void CanShowSnippetEmbedded()
        {
            Mock<ISnippetRepository> repo = new Mock<ISnippetRepository>();
            repo.Setup(m => m.GetById(It.Is<long>(x => x == 1)))
                .Returns(new Snippet { Id = 1, Title = "T1", Content = "C1", UserId = null, User = null });

            HomeController controller = new HomeController(repo.Object, null);

            var result = controller.Embedded(1) as ViewResult;
            var model = result.Model as string;

            Assert.AreEqual("C1", model);
        }

        [TestMethod]
        public void CanEditSnippet()
        {
            Snippet sn = new Snippet
            {
                Id = 1,
                Title = "T1",
                Content = "C1",
                UserId = 1
            };
            Mock<ISnippetRepository> repo = new Mock<ISnippetRepository>();
            repo.Setup(m => m.GetById(It.Is<long>(x => x == 1))).Returns(sn);

            var snippet = new Snippet();
            repo.Setup(m => m.Update(It.Is<Snippet>(x => x.Id == 1))).Callback<Snippet>(x =>
                {
                    sn.Id = x.Id;
                    sn.Title = x.Title;
                    sn.Content = x.Content;
                });

            Mock<IWebSecurity> webSec = new Mock<IWebSecurity>();
            webSec.Setup(x => x.CurrentUserId).Returns(1);

            EditVM vm = new EditVM
            {
                Id = 1,
                Title = "TT1",
                Content = "CC1",
            };

            var controller = new HomeController(repo.Object, webSec.Object);
            var result = controller.Edit(vm) as RedirectResult;

            Assert.AreEqual("TT1", sn.Title);
            Assert.AreEqual("CC1", sn.Content);
        }

        [TestMethod]
        public void UserCannotEditOtherUsersSnippets()
        {
            Snippet sn = new Snippet
            {
                Id = 1,
                Title = "T1",
                Content = "C1",
                UserId = 1
            };
            Mock<ISnippetRepository> repo = new Mock<ISnippetRepository>();
            repo.Setup(m => m.GetById(It.Is<long>(x => x == 1))).Returns(sn);

            var snippet = new Snippet();
            repo.Setup(m => m.Update(It.Is<Snippet>(x => x.Id == 1))).Callback<Snippet>(x =>
            {
                sn.Id = x.Id;
                sn.Title = x.Title;
                sn.Content = x.Content;
            });

            Mock<IWebSecurity> webSec = new Mock<IWebSecurity>();
            webSec.Setup(x => x.CurrentUserId).Returns(2);

            EditVM vm = new EditVM
            {
                Id = 1,
                Title = "TT1",
                Content = "CC1",
            };

            var controller = new HomeController(repo.Object, webSec.Object);
            var result = controller.Edit(vm) as HttpUnauthorizedResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(401, result.StatusCode);
        }

        [TestMethod]
        public void CanPreviewSnippet()
        {
            var controller = new HomeController(null, null);

            var result = controller.Preview("C1") as PartialViewResult;
            var model = result.Model as string;
            var viewName = result.ViewName;

            Assert.AreEqual("C1", model);
            Assert.AreEqual("~/Views/Home/DisplayTemplates/_SnippetContent.cshtml", viewName);
        }

        [TestMethod]
        public void InvalidSnippetIdShouldReturn404NotFound()
        {
            Mock<ISnippetRepository> repo = new Mock<ISnippetRepository>();
            repo.Setup(m => m.Snippets).Returns(new Snippet[]
            {
                new Snippet { Id = 1, Title = "T1", Content = "C1", DatePublished = DateTime.Now, UserId = 1 },
            }.AsQueryable<Snippet>);

            var controller = new HomeController(repo.Object, null);
            var result = controller.Show(2) as HttpNotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
