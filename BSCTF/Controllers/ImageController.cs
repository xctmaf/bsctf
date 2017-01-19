namespace BSCTF.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Web.Application.Handlers.Image;

    [RoutePrefix("Image")]
    public class ImageController : BaseController
    {
        private readonly IImageHandler _imageHandler;

        public ImageController(IImageHandler imageHandler)
        {
            this._imageHandler = imageHandler;
        }

        [HttpPost]
        [Authorize]
        [Route("Add")]
        public async Task<IHttpActionResult> Add()
        {
            if (this.Request.Content.IsMimeMultipartContent() == false)
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            return Ok(await _imageHandler.UploadFiles(Request.Content, HttpContext.Current.Server.MapPath("~/"), CurrentUser.Login));
        }

        [HttpGet]
        [Route("Find")]
        public IHttpActionResult Find(string param)
        {
            return Ok(_imageHandler.Find(param));
        }

        [HttpPost]
        [Authorize]
        [Route("AddFromInstagram")]
        public IHttpActionResult AddFromInstagram(string url)
        {
            return Ok(_imageHandler.AddFromInstagram(url));
        }

        [HttpGet]
        [Authorize]
        [Route("List")]
        public IHttpActionResult List()
        {
            return Ok(_imageHandler.List());
        }
    }
}