using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.ViewModels
{
    public class CreateCourtViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public string AppUserId { get; set; }

    }
}
