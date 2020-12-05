using System.Collections.Generic;

namespace Pezeshk.Models.ViewModels
{
    public class GalleryListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Gallery> Galleries { get; set; }
    }
}