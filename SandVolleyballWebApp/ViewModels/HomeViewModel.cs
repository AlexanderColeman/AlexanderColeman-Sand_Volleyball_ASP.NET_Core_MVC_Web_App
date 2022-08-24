using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Court> Courts { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
