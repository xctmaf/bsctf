namespace BSCTF.Controllers.Temp
{
    using System.Web.Http;
    using Web.Application.Handlers.Temp;

    public class TempController : ApiController
    {
        private ITempHandler _handler;

        public TempController(ITempHandler handler)
        {
            this._handler = handler;
        }

        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok(_handler.GetHelloMessage());
        }
    }
}