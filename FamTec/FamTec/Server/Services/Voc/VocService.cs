using FamTec.Server.Repository.Alarm;
using FamTec.Server.Repository.Building;
using FamTec.Server.Repository.User;
using FamTec.Server.Repository.Voc;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using Newtonsoft.Json.Linq;

namespace FamTec.Server.Services.Voc
{
    public class VocService : IVocService
    {
        private readonly IVocInfoRpeository VocInfoRepository;
        private readonly IBuildingInfoRepository BuildingInfoRepository;
        private readonly IUserInfoRepository UserInfoRepository;
        private readonly IAlarmInfoRepository AlarmInfoRepository;

        private ILogService LogService;

        public VocService(IVocInfoRpeository _vocinforepository,
            IBuildingInfoRepository _buildinginforepository,
            IUserInfoRepository _userinforepository,
            IAlarmInfoRepository _alarminforepository,
            ILogService _logservice)
        {
            this.VocInfoRepository = _vocinforepository;
            this.BuildingInfoRepository = _buildinginforepository;
            this.UserInfoRepository = _userinforepository;
            this.AlarmInfoRepository = _alarminforepository;
            this.LogService = _logservice;
        }


        public async ValueTask<ResponseUnit<string>?> AddVocService(string obj, IFormFile[] image)
        {
            JObject? jobj = JObject.Parse(obj);
            int Voctype = Int32.Parse(jobj["Type"]!.ToString()); // 종류
            string VocName = jobj["Name"]!.ToString(); // 이름
            string VocPhoneNumber = jobj["PhoneNumber"]!.ToString(); // 전화번호
            string VocTitle = jobj["Title"]!.ToString(); // 제목
            string VocContents = jobj["Contents"]!.ToString(); // 내용
            int Vocbuildingidx = Int32.Parse(jobj["buildingidx"]!.ToString()); // 건물 인덱스

            BuildingTb? buildingck = await BuildingInfoRepository.GetBuildingInfo(Vocbuildingidx); // 넘어온 해당 건물이 있는지 먼저 CHECK / 해당 건물이 속해있는 사업장 INDEX 반환

            VocTb? model = new VocTb()
            {
                Type = Voctype, // 종류
                Name = VocName, // 이름
                Phone = VocPhoneNumber, // 전화번호
                Title = VocTitle, // 제목
                Content = VocContents, // 내용
                BuildingTbId = Vocbuildingidx
            };
            
            if (image is [_, ..])
            {
                // 확장자 검사
                for (int i = 0; i < image.Count(); i++)
                {
                    string tempName = image[i].FileName;
                    string tempextenstion = Path.GetExtension(tempName);

                    string[] allowedExtensions = { ".jpg", ".png", ".bmp",".jpeg" };

                    if (!allowedExtensions.Contains(tempextenstion))
                        return new ResponseUnit<string>() { message = "파일 형식이 잘못되었습니다.", data = null, code = 200 };
                }
            }

            // 민원 테이블 발생
            if (buildingck is not null)
            {
                for (int i = 0; i < image.Count(); i++)
                {
                    string newFileName = $"{Guid.NewGuid()}{Path.GetExtension(image[i].FileName)}"; // 이름은 바꾸고 확장자는 그대로
                    string filePath = Path.Combine(CommPath.VocFileImages, newFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await image[i].CopyToAsync(fileStream);
                        
                        if(i == 0)
                            model.Image1 = newFileName;
                        if(i == 1)
                            model.Image2 = newFileName;
                        if(i == 2)
                            model.Image3 = newFileName;
                    }
                }

                VocTb? result = await VocInfoRepository.AddAsync(model);

                if (result is not null)
                {
                    // 알람 발생시키는곳
                    switch (Voctype)
                    {
                        case 1:
                            break;

                        case 7: // 미화
                            List<UserTb>? userlist = await UserInfoRepository.GetVocBeautyList(buildingck.PlaceTbId);

                            if (userlist is [_, ..])
                            {
                                foreach (var user in userlist)
                                {
                                    // 사용자 수만큼 Alarm 테이블에 Insert
                                    AlarmTb alarm = new AlarmTb()
                                    {
                                        CreateDt = DateTime.Now,
                                        CreateUser = VocName,
                                        UpdateDt = DateTime.Now,
                                        UpdateUser = VocName,
                                        UserTbId = user.Id,
                                        VocTbId = result.Id
                                    };

                                    AlarmTb? alarm_result = await AlarmInfoRepository.AddAsync(alarm);
                                }

                                return new ResponseUnit<string>() { message = "요청이 정상 처리되었습니다.", data = VocTitle, code = 200 };
                            }
                            else
                            {
                                return new ResponseUnit<string>() { message = "요청이 처리되지 않았습니다.", data = VocTitle, code = 200 };
                            }
                    }

                }
                // UserTable Select 한번해야함.. --> 권한 검사. Alarm 테이블에 넣어줄거. 
                // result --> index 뽑아서 Alarm 테이블에 추가하고 Switch 문해서 쏘면 될듯.
            }

            return new ResponseUnit<string>() { message = "요청이 정상 처리되었습니다.", data = VocTitle, code = 200 };
             
        }
    }
}
