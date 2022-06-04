using System.Text;
using NetCore.Extensions;
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
        public async Task<IActionResult> WebhookReceive()
        {
            try
            {
                var rawContent = await Request.GetRawBodyStringAsync();
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
