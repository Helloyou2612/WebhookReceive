using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> WebhookReceive()
        {
            try
            {
                var domainName = this.Request.RequestUri.Host;
                var crmDomainName = domainName.Split(new[] { "." }, StringSplitOptions.None)[0];
                string rawContent;
                using (var contentStream = await this.Request.Content.ReadAsStreamAsync())
                {
                    contentStream.Seek(0, SeekOrigin.Begin);
                    using (var sr = new StreamReader(contentStream))
                    {
                        rawContent = await sr.ReadToEndAsync();
                        // use raw content here
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
