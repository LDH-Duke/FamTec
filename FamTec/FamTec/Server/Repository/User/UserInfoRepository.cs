using FamTec.Server.Databases;
using FamTec.Server.Services;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace FamTec.Server.Repository.User
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly WorksContext context;
        private ILogService LogService;

        public UserInfoRepository(WorksContext _context, ILogService _logservice)
        {
            this.context = _context;
            this.LogService = _logservice;
        }

        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UserTb?> AddAsync(UserTb? model)
        {
            try
            {
                if (model is not null)
                {
                    UserTb? search = await context.UserTbs.FirstOrDefaultAsync(m => m.UserId == model.UserId);
                    if (search is null)
                    {
                        context.UserTbs.Add(model);
                        await context.SaveChangesAsync();
                        return model;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

 

        /// <summary>
        /// 유저 INDEX로 유저테이블 조회
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public async ValueTask<UserTb?> GetUserIndexInfo(int? useridx)
        {
            try
            {
                if(useridx is not null)
                {
                    UserTb? model = await context.UserTbs.FirstOrDefaultAsync(m => m.Id == useridx && m.DelYn != 1);
                    
                    if(model is not null)
                        return model;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// 유저테이블 모델 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteUserInfo(UserTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.UserTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }


        /// <summary>
        /// USERID + PASSWORD에 해당하는 모델반환
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<UserTb?> GetUserInfo(string? userid, string? password)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid) && !String.IsNullOrWhiteSpace(password))
                {
                    UserTb? model = await context.UserTbs
                        .FirstOrDefaultAsync(m => 
                        m.UserId!.Equals(userid) &&
                        m.Password!.Equals(password));

                    if (model is not null)
                        return model;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// 아이디 중복검사
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UserTb?> UserIdCheck(string? userid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid))
                {
                    UserTb? model = await context.UserTbs.FirstOrDefaultAsync(m => m.UserId == userid);
                    if(model is not null)
                    {
                        return model;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }
    }
}
