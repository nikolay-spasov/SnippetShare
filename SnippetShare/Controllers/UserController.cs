namespace SnippetShare.Controllers
{
    using SnippetShare.Domain;
    using SnippetShare.Domain.Repositories.Abstract;
    using SnippetShare.Models;
    using System.Linq;
    using System.Web.Mvc;
    using WebMatrix.WebData;
    using SnippetShare.Domain.Repositories.Concrete;
    using SnippetShare.Instrastructure;

    public class UserController : Controller
    {
        private const int PageSize = 10;

        private ISnippetRepository snippetRepo;
        private IWebSecurity webSecurity;

        public UserController(ISnippetRepository snippetRepository, IWebSecurity webSecurity)
        {
            this.snippetRepo = snippetRepository;
            this.webSecurity = webSecurity;
        }

        public ActionResult List(int page = 1)
        {
            var snippets = this.snippetRepo.Snippets
                .IncludeMultiple(x => x.User)
                .Where(x => x.UserId == webSecurity.CurrentUserId)
                .OrderByDescending(x => x.DatePublished)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(x => new SnippetVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    DatePublished = x.DatePublished,
                }).ToList();

            PagingInfo info = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = snippetRepo.Snippets
                    .IncludeMultiple(x => x.User)
                    .Where(x => x.UserId == webSecurity.CurrentUserId)
                    .Count()
            };

            var viewModel = new SnippetListVM
            {
                Snippets = snippets,
                PagingInfo = info
            };

            return View(viewModel);
        }
    }
}
