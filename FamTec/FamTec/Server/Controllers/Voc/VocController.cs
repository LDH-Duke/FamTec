using FamTec.Server.Services.Voc;
using FamTec.Server.Tokens;
using FamTec.Shared.Client.DTO.Normal.Voc;
using FamTec.Shared.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FamTec.Server.Controllers.Voc
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocController : ControllerBase
    {
        private IVocService VocService;
        private ITokenComm TokenComm;

        public VocController(IVocService _vocservice, ITokenComm _tokencomm)
        {
            this.VocService = _vocservice;
            this.TokenComm = _tokencomm;
        }

        [HttpGet]
        [Route("GetVocList")]
        public async ValueTask<IActionResult> GetVocList([FromQuery]int placeidx, [FromQuery]string date)
        {
            JObject? jobj = TokenComm.TokenConvert(HttpContext.Request);

            ResponseList<ListVoc>? model = await VocService.GetVocList(jobj, placeidx, date);

            // placeidx
            // date = 2024

            return Ok(model);
        }
    }
}
