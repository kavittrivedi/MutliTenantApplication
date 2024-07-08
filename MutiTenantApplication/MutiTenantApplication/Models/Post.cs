namespace MutiTenantApplication.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
