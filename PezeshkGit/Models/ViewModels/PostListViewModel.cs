using System.Collections.Generic;

namespace PezeshkGit.Models.ViewModels
{
    public class PostListViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}