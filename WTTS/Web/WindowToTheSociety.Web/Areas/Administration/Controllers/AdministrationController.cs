namespace WindowToTheSociety.Web.Areas.Administration.Controllers
{
    using WindowToTheSociety.Common;
    using WindowToTheSociety.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
