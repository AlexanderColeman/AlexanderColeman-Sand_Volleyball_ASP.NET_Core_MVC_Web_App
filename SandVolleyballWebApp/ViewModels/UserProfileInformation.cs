using SandVolleyballWebApp.Data.Enum;

namespace SandVolleyballWebApp.ViewModels
{
	public class UserProfileInformation
	{
        public string? Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
		public string? UserName { get; set; }
        public PlayStyleGroup? PlayStyleGroup { get; set; }
    }
}
