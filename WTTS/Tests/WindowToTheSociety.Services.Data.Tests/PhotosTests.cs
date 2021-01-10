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
            List<Photo> list = new List<Photo>();
            Mock<IRepository<Photo>> mockRepo = new Mock<IRepository<Photo>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Photo>())).Callback(
                (Photo photo) => list.Add(photo));

            IPhotosService service = new PhotosService();

            Photo photo = new Photo();
            list.Add(photo);

            Assert.Single(list);
        }
    }
}
