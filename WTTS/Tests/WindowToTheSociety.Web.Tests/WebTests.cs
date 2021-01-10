namespace WindowToTheSociety.Web.Tests
{
    using Microsoft.AspNetCore.Mvc.Testing;

    using Xunit;

    public class WebTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> server;

        public WebTests(WebApplicationFactory<Startup> server)
        {
            this.server = server;
        }
    }
}
