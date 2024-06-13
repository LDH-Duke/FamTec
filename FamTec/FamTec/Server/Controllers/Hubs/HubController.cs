using FamTec.Server.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FamTec.Server.Controllers.Hubs
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<BroadcastHub> HubContext;

        public HubController(IHubContext<BroadcastHub> _hubcontext)
        {
            this.HubContext = _hubcontext;
        }

        [HttpGet]
        [Route("VocHub")]
        public async Task<IActionResult> VocHub()
        {
            
            int type = 0;
            switch(type)
            {
                case 1:
                    /* 기계 */
                    break;
                case 2:
                    /* 전기 */
                    break;
                case 3:
                    /* 승강 */
                    break;
                case 4:
                    /* 소방 */
                    break;
                case 5:
                    /* 건축 */
                    break;
                case 6:
                    /* 통신 */
                    break;
                case 7:
                    /* 미화 */
                    await HubContext.Clients.Group("SanitationRoom").SendAsync("ReceiveMessage", "컨트롤러 에서 보낸 메시지");
                    
                    break;
                case 8:
                    /* 보안 */
                    break;
                case 9:
                    /* 기타 */
                    break;

            }

            return Ok();

        }

    }
}
