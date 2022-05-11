using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebhookReceive.Controllers
{
    public class WebhookController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> WebhookReceive()
        {
            try
            {
                string rawContent;
                using (var contentStream = await Request.Content.ReadAsStreamAsync())
                {
                    contentStream.Seek(0, SeekOrigin.Begin);
                    using (var sr = new StreamReader(contentStream))
                    {
                        rawContent = await sr.ReadToEndAsync();
                    }
                }
                //var model = JsonConvert.DeserializeObject<CustomerUpdateModel>(rawContent);
                return Ok(rawContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}