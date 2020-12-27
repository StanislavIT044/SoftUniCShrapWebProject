namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Photo> photosRepository;
        private readonly IRepository<Following> followingRepository;
        private readonly IRepository<Post> postRepository;
        private readonly IUsersSurvice usersSurvice;

        public PostsService(
            IRepository<ApplicationUser> usersRepository,
            IRepository<Post> postRepository,
            IRepository<Photo> photosRepository,
            IRepository<Following> followingRepository,
            IUsersSurvice usersSurvice)
        {
            this.usersRepository = usersRepository;
            this.photosRepository = photosRepository;
            this.followingRepository = followingRepository;
            this.usersSurvice = usersSurvice;
            this.postRepository = postRepository;
        }

        public async Task CreatePost(string photoPath, string text, string userId, string pageId)
        {
            Photo photo = this.photosRepository.All().FirstOrDefault(x => x.PictureUrl == photoPath);

            Post post = new Post
            {
                Text = text,
                CreatedOn = DateTime.UtcNow,
            };

            if (userId != null)
            {
                post.ApplicationUserId = userId;
            }
            else if (pageId != null)
            {
                post.PageId = pageId;
            }

            if (photo != null)
            {
                post.PhotoId = photo.Id;
                post.Photo = photo;
            }

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();
        }

        public ListPostsInHomePageViewModel GetPostsForHomePage(string currentUserId)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<Following> followings = this.followingRepository.All().Where(x => x.UserId == currentUserId).ToList();

            foreach (var follow in followings)
            {
                ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == follow.FollowedUserId);

                if (user != null)
                {
                    users.Add(user);
                }
            }

            List<Post> posts = new List<Post>();

            foreach (var user in users)
            {
                posts.AddRange(this.postRepository.All().Where(x => x.ApplicationUserId == user.Id).ToList());
            }

            foreach (var post in posts)
            {
                post.ApplicationUser = this.usersSurvice.GetUserById(post.ApplicationUserId);
                post.Photo = this.photosRepository.All().FirstOrDefault(x => x.Id == post.PhotoId);
            }

            ListPostsInHomePageViewModel viewModel = new ListPostsInHomePageViewModel();
            viewModel.AllPosts = new List<PostsInHomePageViewModel>();

            foreach (var post in posts)
            {
                PostsInHomePageViewModel postViewModel = new PostsInHomePageViewModel
                {
                    Id = post.Id,
                    Text = post.Text,
                    PhotoId = post.PhotoId,
                    ApplicationUserId = post.ApplicationUserId,
                    ApplicationUser = post.ApplicationUser,
                    CreatedOn = post.CreatedOn,
                };

                if (this.photosRepository.All().Where(x => x.PhotoType == (PhotoType)1).FirstOrDefault(x => x.ApplicationUserId == post.ApplicationUserId) != null)
                {
                    postViewModel.UsersProfilePictureUrl = this.photosRepository.All().Where(x => x.PhotoType == (PhotoType)1).FirstOrDefault(x => x.ApplicationUserId == post.ApplicationUserId).PictureUrl;
                }

                if (postViewModel.PhotoId != null)
                {
                    postViewModel.Photo = post.Photo;
                }

                viewModel.AllPosts.Add(postViewModel);
            }

            ListPostsInHomePageViewModel viewModel2 = new ListPostsInHomePageViewModel();
            viewModel2.AllPosts = viewModel.AllPosts.OrderByDescending(x => x.CreatedOn).ToList();

            return viewModel;
        }
    }
}
