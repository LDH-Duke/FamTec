using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.User
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly FmsContext context;

        public UserInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UsersTb> AddAsync(UsersTb model)
        {
            try
            {
                context.UsersTbs.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 사용자 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<UsersTb>> GetAllAsync()
        {
            try
            {
                return await context.UsersTbs.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

   

        /// <summary>
        /// 사용자 ID 검색 - 사용자 단일모델 반환
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UsersTb> GetByUserInfo(string userid)
        {
            try
            {
                UsersTb? model = await context.UsersTbs.FirstOrDefaultAsync(m => m.UserId == userid);
                if(model == null)
                {
                    return null;
                }
                else
                {
                    return model;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

      
        /// <summary>
        /// 사용자 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> EditAsync(UsersTb model)
        {
            try
            {
                if(model is not null)
                {
                    context.UsersTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        /// <summary>
        /// 사용자 정보 삭제
        /// </summary>
        /// <param name="tguserid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteUserIdAsync(UsersTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.UsersTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
