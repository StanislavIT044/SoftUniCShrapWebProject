namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Web.ViewModels.Pages;

    public interface IPagesService
    {
        Task CreatePage(string title, string userId);

        Task AppendPicture(string title, string pictureUrl);

        string GetIdByTitle(string title);

        SelectPagesViewModel GetSelectPagesViewModel(string userId);
    }
}
