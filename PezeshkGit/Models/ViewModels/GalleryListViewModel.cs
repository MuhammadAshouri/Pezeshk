using System.Collections.Generic;

namespace PezeshkGit.Models.ViewModels
{
    public class GalleryListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Gallery> Galleries { get; set; }
    }
}