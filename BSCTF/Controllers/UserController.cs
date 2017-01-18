namespace BSCTF.Controllers
{
    using System.Web.Http;
    using Web.Application.Exceprtions;
    using Web.Application.Handlers.User;
    using Web.Application.Models.User.Input;

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

        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterUserModel model)
        {
            if (model == null || ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                _handler.Register(model.Login, model.Password, model.Username);
                return Ok();
            }
            catch (DuplicateUserException)
            {
                return BadRequest("пользователь уже сущесвтует");
            }
        }
    }
}