using FamTec.Shared.Server.DTO;
using Newtonsoft.Json.Linq;

namespace FamTec.Server.Services.Voc
{
    public class VocService : IVocService
    {



        public async ValueTask<ResponseUnit<string>?> AddVocService(string obj, IFormFile[] image)
        {
            JObject? jobj = JObject.Parse(obj);
            int type = Int32.Parse(jobj["Type"]!.ToString()); // 종류
            string Name = jobj["Name"]!.ToString(); // 이름
            string PhoneNumber = jobj["PhoneNumber"]!.ToString(); // 전화번호
            string Title = jobj["Title"]!.ToString(); // 제목
            string Contents = jobj["Contents"]!.ToString(); // 내용

            if (image is null && image.Count() == 0)
                return new ResponseUnit<string>() { message = "파일이 잘못되었습니다.", data = null, code = 200 };

            // 확장자 검사
            for (int i = 0; i < image.Count(); i++)
            {
                string tempName = image[i].FileName;
                string tempextenstion = Path.GetExtension(tempName);

                string[] allowedExtensions = { ".jpg", ".png", ".bmp" };

                if (!allowedExtensions.Contains(tempextenstion))
                    return new ResponseUnit<string>() { message = "파일 형식이 잘못되었습니다.", data = null, code = 200 };
            }

            // 실제 로직
            foreach (var item in image)
            {
                string newFileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}"; // 이름은 바꾸고 확장자는 그대로
                string filePath = Path.Combine(CommPath.VocFileImages, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await item.CopyToAsync(fileStream);
                }
            }

            
           switch(type)
            {
                case 0:
                    return null;
                case 1:
                    return null;
                case 2:
                    return null;
                case 3:
                    return null;
                case 4:
                    return null;
                case 5:
                    return null;
                case 6:
                    return null;
                case 7:
                    return new ResponseUnit<string>() { message = "요청이 정상 처리되었습니다.", data = Title, code = 200 };
                case 8:
                    return null;
                case 9:
                    return null;
            }

            /*
            if (type == 7)
            {
                Console.WriteLine("전송");
                // 여기서 디비에 넣으면될듯!
                return new ResponseUnit<string>() { message = "요청이 정상 처리되었습니다.", data = Title, code = 200 };
                //await HubContext.Clients.Group("SanitationRoom").SendAsync("ReceiveMessage", "민원이 등록되었습니다");
            }
            */
            return null;

        }
    }
}
