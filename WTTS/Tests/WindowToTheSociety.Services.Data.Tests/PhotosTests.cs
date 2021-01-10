namespace WindowToTheSociety.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using Xunit;

    public class PhotosTests
    {
        [Fact]
        public async Task TestCreate()
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

            List<Page> pagesList = new List<Page>();
            Mock<IRepository<Page>> pageMockRepo = new Mock<IRepository<Page>>();
            pageMockRepo.Setup(x => x.All()).Returns(pagesList.AsQueryable());
            pageMockRepo.Setup(x => x.AddAsync(It.IsAny<Page>())).Callback(
                (Page page) => pagesList.Add(page));

            List<Post> postsList = new List<Post>();
            Mock<IRepository<Post>> postsMockRepo = new Mock<IRepository<Post>>();
            postsMockRepo.Setup(x => x.All()).Returns(postsList.AsQueryable());
            postsMockRepo.Setup(x => x.AddAsync(It.IsAny<Post>())).Callback(
                (Post post) => postsList.Add(post));

            IUsersSurvice usersSurvice = new UsersService(usersMockRepo.Object, photoMockRepo.Object, postsMockRepo.Object);
            IPhotosService photosService = new PhotosService(usersMockRepo.Object, photoMockRepo.Object, usersSurvice, pageMockRepo.Object, postsMockRepo.Object);

            Photo photo = await photosService.CreatePhoto("", "123", null, (PhotoType)1);

            Assert.Equal("123", photo.ApplicationUserId);
            Assert.Equal(null, photo.PageId);
        }

        [Fact]
        public async Task TestCreateWhenAppUserIdIsNull()
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

            List<Page> pagesList = new List<Page>();
            Mock<IRepository<Page>> pageMockRepo = new Mock<IRepository<Page>>();
            pageMockRepo.Setup(x => x.All()).Returns(pagesList.AsQueryable());
            pageMockRepo.Setup(x => x.AddAsync(It.IsAny<Page>())).Callback(
                (Page page) => pagesList.Add(page));

            List<Post> postsList = new List<Post>();
            Mock<IRepository<Post>> postsMockRepo = new Mock<IRepository<Post>>();
            postsMockRepo.Setup(x => x.All()).Returns(postsList.AsQueryable());
            postsMockRepo.Setup(x => x.AddAsync(It.IsAny<Post>())).Callback(
                (Post post) => postsList.Add(post));

            IUsersSurvice usersSurvice = new UsersService(usersMockRepo.Object, photoMockRepo.Object, postsMockRepo.Object);
            IPhotosService photosService = new PhotosService(usersMockRepo.Object, photoMockRepo.Object, usersSurvice, pageMockRepo.Object, postsMockRepo.Object);

            Photo photo = await photosService.CreatePhoto("", null, "123", (PhotoType)1);

            Assert.Equal("123", photo.PageId);
            Assert.Equal(null, photo.ApplicationUserId);
        }
    }
}
