namespace WindowToTheSociety.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using Xunit;

    public class UsersTests
    {
        [Fact]
        public void TestGetUserById()
        {
            List<Photo> photosList = new List<Photo>();
            Mock<IRepository<Photo>> photoMockRepo = new Mock<IRepository<Photo>>();
            photoMockRepo.Setup(x => x.All()).Returns(photosList.AsQueryable());
            photoMockRepo.Setup(x => x.AddAsync(It.IsAny<Photo>())).Callback(
                (Photo photo) => photosList.Add(photo));

            List<ApplicationUser> usersList = new List<ApplicationUser>();
            Mock<IRepository<ApplicationUser>> usersMockRepo = new Mock<IRepository<ApplicationUser>>();
            usersMockRepo.Setup(x => x.All()).Returns(usersList.AsQueryable());
            usersMockRepo.Setup(x => x.AddAsync(It.IsAny<ApplicationUser>())).Callback(
                (ApplicationUser applicationUser) => usersList.Add(applicationUser));

            List<Post> postsList = new List<Post>();
            Mock<IRepository<Post>> postsMockRepo = new Mock<IRepository<Post>>();
            postsMockRepo.Setup(x => x.All()).Returns(postsList.AsQueryable());
            postsMockRepo.Setup(x => x.AddAsync(It.IsAny<Post>())).Callback(
                (Post post) => postsList.Add(post));

            IUsersSurvice usersSurvice = new UsersService(usersMockRepo.Object, photoMockRepo.Object, postsMockRepo.Object);

            usersList.Add(new ApplicationUser
            {
                Id = "1234",
                BirthDate = DateTime.UtcNow,
            });

            usersList.Add(new ApplicationUser
            {
                Id = "123",
                BirthDate = DateTime.UtcNow,
            });

            ApplicationUser user = usersSurvice.GetUserById("123");

            Assert.Equal("123", user.Id);
        }
    }
}
