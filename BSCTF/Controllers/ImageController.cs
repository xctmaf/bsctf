namespace BSCTF.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Web.Application.Exceprtions;
    using Web.Application.Handlers.Image;

    [RoutePrefix("Image")]
    public class ImageController : BaseController
    {
        private readonly IImageHandler _imageHandler;

        public ImageController(IImageHandler imageHandler)
        {
            _imageHandler = imageHandler;
        }

        [HttpPost]
        [Authorize]
        [Route("Add")]
        public async Task<IHttpActionResult> Add()
        {
            if (this.Request.Content.IsMimeMultipartContent() == false)
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            return Ok(await _imageHandler.UploadFiles(Request.Content, HttpContext.Current.Server.MapPath("~/"), CurrentUser.Login, string.Empty));
        }

        [HttpGet]
        [Route("Find")]
        public IHttpActionResult Find(string param)
        {
            return Ok(_imageHandler.Find(param));
        }

        [HttpPost]
        [Authorize]
        [Route("AddFromUrl")]
        public async Task<IHttpActionResult> AddFromUrl([FromBody]string url)
        {
            try
            {
                var result = await _imageHandler.AddFromUrl(HttpContext.Current.Server.MapPath("~/"), CurrentUser.Login, url);
                return Ok(result);
            }
            catch (UnsupportedUrlException)
            {
                return BadRequest("Некорректная ссылка");
            }
            catch (IncorrectUrlException)
            {
                return BadRequest("Некорректная ссылка");
            }

        }

        [HttpGet]
        [Authorize]
        [Route("List")]
        public IHttpActionResult List()
        {
            return Ok(_imageHandler.List(CurrentUser.Login));
        }
    }
}