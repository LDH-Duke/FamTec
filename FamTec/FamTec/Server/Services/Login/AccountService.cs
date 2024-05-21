using FamTec.Server.Repository.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FamTec.Server.Services.Login
{
    public class AccountService : IAccountService
    {
        private readonly IUserInfoRepository UserInfoRepository;


        public AccountService(IUserInfoRepository _userinfoRepository)
        {
            this.UserInfoRepository = _userinfoRepository;
        }

        public async ValueTask<ResponseModel<UsersDTO>> LoginService(string userid, string password, bool isadmin)
        {
            /*
            try
            {
                if(!String.IsNullOrWhiteSpace(userid) && !String.IsNullOrWhiteSpace(password))
                {
                    UserTb? model = await UserInfoRepository.GetUserInfo(userid, password);
                    
                    if(model is not null)
                    {
                        if (isadmin) // 관리자로 로그인 할것이다 체크
                        {
                            if(model.AdminYn == 1)
                            {
                                // 해당 계정은 관리자계정이면서 - 관리자로 접근을했기때문에 접근 OK
                                // 해당 계정의 사업장 List정보 반환
                                //
                            }
                            else
                            {
                                // 해당 계정은 관리자가 아닌데 - 관리자로 접근했기때문에 접근 NO
                            }
                        }
                        else
                        {
                            // Model - 단일반환
                        }
                    }
                    else
                    {
                        // 로그인 정보가 잘못되었습니다.
                    }
                }
                else
                {
                    // 매개변수 잘못됨
                }

            }
            catch(Exception ex)
            {

            }
            */
            return null;
        }
    }
}
