using FamTec.Server.Hubs;
using FamTec.Server.Services.Voc;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace FamTec.Server.Controllers.Hubs
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<BroadcastHub> HubContext;

        private IVocService VocService;


        public HubController(IHubContext<BroadcastHub> _hubcontext, IVocService _vocservice)
        {
            this.HubContext = _hubcontext;
            this.VocService = _vocservice;
        }

        [HttpPost]
        [Route("VocHub")]
        public async Task<IActionResult> VocHub([FromForm]string obj, [FromForm]IFormFile[] image)
        {
            ResponseUnit<string>? model = await VocService.AddVocService(obj, image);

            if(model is not null)
            {
                if(model.code == 200)
                {
                    await HubContext.Clients.Group("SanitationRoom").SendAsync("ReceiveVoc", model.data.ToString());
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

            /*
            JObject? jobj = JObject.Parse(obj.ToString());
            int type = Int32.Parse(jobj["Type"]!.ToString()); // 종류
            string Name = jobj["Name"]!.ToString(); // 이름
            string PhoneNumber = jobj["PhoneNumber"]!.ToString(); // 전화번호
            string Title = jobj["Title"]!.ToString(); // 제목
            string Contents = jobj["Contents"]!.ToString(); // 내용


            if (image == null || image.Count() == 0)
                return BadRequest("파일이 잘못됨");

            // 확장자 검사
            for (int i = 0; i < image.Count(); i++)
            {
                string tempName = image[i].FileName;
                string tempextenstion = Path.GetExtension(tempName);

                string[] allowedExtensions = { ".jpg", ".png", ".bmp" };

                if (!allowedExtensions.Contains(tempextenstion))
                    return BadRequest("이미지 파일이 아닙니다.");
            }

            foreach(var item in image)
            {
                string newFileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}"; // 이름은 바꾸고 확장자는 그대로
                string filePath = Path.Combine(CommPath.VocFileImages, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await item.CopyToAsync(fileStream);
                }
            }
            
            if(type == 7)
            {
                Console.WriteLine("전송");
                await HubContext.Clients.Group("SanitationRoom").SendAsync("ReceiveMessage", "민원이 등록되었습니다");
            }
            */


            return Ok();

        }

    }
}
