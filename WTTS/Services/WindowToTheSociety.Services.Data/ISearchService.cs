namespace WindowToTheSociety.Services.Data
{
    using WindowToTheSociety.Web.ViewModels.Search;

    public interface ISearchService
    {
         SearchViewModel Search(string searchStr);
    }
}
