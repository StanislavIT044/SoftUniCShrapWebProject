namespace WindowToTheSociety.Services.Data.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Search;
    using Xunit;

    public class SearchTests
    {
        [Fact]
        public async Task TestSearchUser()
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

            ISearchService searchService = new SearchService(usersMockRepo.Object, pageMockRepo.Object, photoMockRepo.Object);

            usersList.Add(new ApplicationUser
            {
                Id = "123",
                FirstName = "Test",
                Surname = "Test",
            });

            usersList.Add(new ApplicationUser
            {
                Id = "1234",
                FirstName = "xxx",
                Surname = "xxx",
            });

            SearchViewModel user = searchService.Search("Test");

            Assert.Equal("123", user.Users.FirstOrDefault(x => x.Id == "123").Id);
        }
    }
}
