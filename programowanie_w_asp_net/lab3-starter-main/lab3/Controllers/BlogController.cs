using lab3.Models;
using lab3.Models.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab3.Controllers
{
    public class BlogController : Controller
    {
        public BlogController(BlogContext _context)
        {
            _context = _context;
        }
        private readonly BlogContext _context;


        public async Task<IActionResult> Index()
        {
            var entities = await _context.Articles.ToListAsync();
            var viewModels = entities.Select(e => e.ToViewModel());
            return View(viewModels);
        }

        public async Task<IActionResult> Article(string id)
        {
            var post = await _context.Articles
            .Include(a => a.Comments)
            .FirstOrDefaultAsync(post => post.Id == id);
            if (post == null) return NotFound();
            var viewModel = post.ToViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateBlogArticleModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogArticleModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (await _context.Articles.AnyAsync(p => p.Id == viewModel.Id))
            {
                ModelState.AddModelError(nameof(viewModel.Id), "Id must be unique.");
                return View(viewModel);
            }

            var entity = viewModel.ToEntity();
            _context.Articles.Add(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Article), new { id = entity.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateBlogCommentModel viewModel)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(p => p.Id == viewModel.ArticleId);
            if (article == null) return NotFound();
            var comment = viewModel.ToEntity();
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Article), new { id = article.Id });
        }
    }
}
