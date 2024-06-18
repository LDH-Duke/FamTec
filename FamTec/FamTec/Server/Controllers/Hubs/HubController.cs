using FamTec.Server.Hubs;
using FamTec.Server.Services.Voc;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.Server;
using FamTec.Shared.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Net;

namespace FamTec.Server.Controllers.Hubs
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<BroadcastHub> HubContext;

        private readonly IWebHostEnvironment _env;

        private IVocService VocService;


        public HubController(IHubContext<BroadcastHub> _hubcontext, IVocService _vocservice, IWebHostEnvironment env)
        {
            this.HubContext = _hubcontext;
            this.VocService = _vocservice;
            this._env = env;

        }

        [HttpPost]
        [Route("Files")]
        public async Task<IActionResult> UploadFile([FromForm]string obj, [FromForm]List<IFormFile> files)
        {
            JObject? jobj = JObject.Parse(obj);

            ResponseUnit<string>? model = await VocService.AddVocService(obj, files);


            if (model is not null)
            {
                if (model.code == 200)
                {
                    int Voctype = Int32.Parse(jobj["Type"]!.ToString()); // 종류

                    switch (Voctype)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 7:
                            await HubContext.Clients.Group("SanitationRoom").SendAsync("ReceiveVoc", "알람방생?");
                            return Ok(model);
                    }


                    return Ok(model);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
