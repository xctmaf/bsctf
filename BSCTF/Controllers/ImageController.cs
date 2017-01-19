namespace BSCTF.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using Web.Application.Handlers.Image;

    [RoutePrefix("Image")]
    public class ImageController : BaseController
    {
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
}