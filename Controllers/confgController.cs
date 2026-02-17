using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace مشروع_قبل_الشغل.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class confgController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IOptionsMonitor<AttachmentsOpetions> opetion1;
       

        public confgController( IConfiguration configuration,IOptionsMonitor<AttachmentsOpetions> opetion)
        {
            this.configuration = configuration;
            opetion1 = opetion;
        }
        [HttpGet]
        public ActionResult GetConfg()
        {
            var confg = new
            {
                allowhostes = configuration["AllowedHosts"],
                loging = configuration["Logging"],
                conecctionstring = configuration.GetConnectionString("DefaultConnection"),
               attchments = opetion1.CurrentValue.AllowExtentions,
            };
            return Ok(confg);
        }
    }
}
