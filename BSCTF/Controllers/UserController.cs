namespace BSCTF.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Security;
    using ByndyuSoft.Infrastructure.Common.Extensions;
    using Web.Application.Exceprtions;
    using Web.Application.Handlers.User;
    using Web.Application.Models.User.Input;

    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private readonly IUserHandler _handler;
        

        public UserController(IUserHandler handler)
        {
            _handler = handler;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Info()
        {
            return Ok(_handler.GetInfo());
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] LoginUserModel model)
        {
            if (model == null || ModelState.IsValid == false)
                return BadRequest(ModelState);

            var user = _handler.Login(model.Login, model.Password);

            if (user == null)
                return Unauthorized();


            var cryptedLogin = MachineKey.Protect(Encoding.UTF8.GetBytes(user.Login), WebConfigurationManager.AppSettings["cryptoKey"]).ToBase64();
//            var cryptedCookieName = MachineKey.Protect(Encoding.UTF8.GetBytes("user"), WebConfigurationManager.AppSettings["cryptoKey"]).ToBase64();
            //            Encoding.UTF8.GetString(MachineKey.Unprotect(Request.Cookies(cookieName).Value.FromBase64ToBytes, "an authentication token")

            var cookie = new CookieHeaderValue("user", cryptedLogin)
                             {
                                 Expires = DateTimeOffset.Now.AddDays(1),
                                 Domain = Request.RequestUri.Host,
                                 Path = "/"
                             };

            var response = Request.CreateResponse(HttpStatusCode.OK, user);
            response.Headers.AddCookies(new[] {cookie});
            return ResponseMessage(response);
        }

        [HttpPost]
        [Route("Register")]
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