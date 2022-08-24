namespace SandVolleyballWebApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IFormFile? Image { get; set; }
        public string? ProfileImageUrl { get; set; }

    }
}
