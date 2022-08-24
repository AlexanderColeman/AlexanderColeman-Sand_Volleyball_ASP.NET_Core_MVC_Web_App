using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.ViewModels
{
    public class EditCourtViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
    }
}
