namespace WindowToTheSociety.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class ListPostsInHomePageViewModel
    {
        public ICollection<PostsInHomePageViewModel> AllPosts { get; set; }
    }
}
