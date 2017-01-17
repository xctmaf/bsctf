namespace BSCTF.Controllers
{
    using System.Web.Http;
    using Web.Application.Handlers.User;

    public class UserController : ApiController
    {
        private readonly IUserHandler _handler;

        public UserController(IUserHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IHttpActionResult Info()
        {
            return Ok(_handler.GetInfo());
        }
    }
}