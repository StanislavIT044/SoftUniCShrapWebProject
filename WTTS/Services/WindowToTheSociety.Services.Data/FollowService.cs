namespace WindowToTheSociety.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;

    public class FollowService : IFollowService
    {
        private readonly IRepository<Following> followingRepository;

        public FollowService(IRepository<Following> followingRepository)
        {
            this.followingRepository = followingRepository;
        }

        public async Task Follow(string currentUser, string id)
        {
            List<Following> relations = this.followingRepository.All().ToList();
            Following relation = relations.FirstOrDefault(x => x.UserId == currentUser && x.FollowedUserId == id);

            if (!relations.Contains(relation))
            {
                Following following = new Following
                {
                    UserId = currentUser,
                    FollowedUserId = id,
                };

                await this.followingRepository.AddAsync(following);
                await this.followingRepository.SaveChangesAsync();
            }
        }

        public async Task Unfollow(string currentUser, string id)
        {
            List<Following> relations = this.followingRepository.All().ToList();
            Following relation = relations.FirstOrDefault(x => x.UserId == currentUser && x.FollowedUserId == id);

            if (relations.Contains(relation))
            {
                this.followingRepository.Delete(relation);
                await this.followingRepository.SaveChangesAsync();
            }
        }
    }
}
