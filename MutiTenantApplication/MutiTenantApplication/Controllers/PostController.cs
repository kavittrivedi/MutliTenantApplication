using Microsoft.AspNetCore.Mvc;

namespace MutiTenantApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetPostsAsync();
            return View(posts);
        }
    }
}
