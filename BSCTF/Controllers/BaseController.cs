namespace BSCTF.Controllers
{
    using System.Web;
    using System.Web.Http;
    using Web.Application.Models.User.Output;

    public abstract class BaseController : ApiController
    {
        protected static UserModel CurrentUser => ((CustomIdentity) HttpContext.Current.User.Identity).User;
    }
}