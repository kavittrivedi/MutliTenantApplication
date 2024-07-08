using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MutiTenantApplicationApi
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var tenantId = _httpContextAccessor.HttpContext.Items["TenantId"]?.ToString();
            return await _context.Posts.Where(p => p.TenantId == Convert.ToInt32(tenantId)).ToListAsync();
        }
    }
}