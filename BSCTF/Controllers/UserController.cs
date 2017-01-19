namespace BSCTF.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Security;
    using ByndyuSoft.Infrastructure.Common.Extensions;
    using Newtonsoft.Json;
    using Web.Application.Exceprtions;
    using Web.Application.Handlers.Image;
    using Web.Application.Handlers.User;
    using Web.Application.Models.User.Input;

    [RoutePrefix("Image")]
    public class ImageController : BaseController
    {
        private static readonly string RootFolder = HttpContext.Current.Server.MapPath("~/App_Data/uploads");

        private IImageHandler _imageHandler;

        public ImageController(IImageHandler imageHandler)
        {
            this._imageHandler = imageHandler;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add()
        {
            return Ok(await _imageHandler.DoSome(Request.Content));
        }
    }

    [RoutePrefix("User")]
    public class UserController : BaseController
    {
        private readonly IUserHandler _handler;


        public UserController(IUserHandler handler)
        {
            _handler = handler;
        }

        [Authorize]
        [HttpGet]
        [Route("Info")]
        public IHttpActionResult Info()
        {
            return Ok(CurrentUser);
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


            var cryptedLogin =
                MachineKey.Protect(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user)), WebConfigurationManager.AppSettings["cryptoKey"]).ToBase64();
            var cookie = new CookieHeaderValue(WebConfigurationManager.AppSettings["AuthCookieName"], cryptedLogin)
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