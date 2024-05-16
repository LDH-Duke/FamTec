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
        public async ValueTask<UsersTb?> AddAsync(UsersTb? model)
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
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 사용자 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<UsersTb>?> GetAllList()
        {
            try
            {
                List<UsersTb>? model = await context.UsersTbs.Where(m => m.DelYn != true).ToListAsync();
                if (model is [_, ..])
                    return model;
                else
                    return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 사용자 ID 검색 - 사용자 단일모델 반환
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UsersTb?> GetUserInfo(string? userid)
        {
            try
            {
                UsersTb? model = await context.UsersTbs.FirstOrDefaultAsync(m => m.UserId == userid && m.DelYn != true);
                if (model is not null)
                    return model;
                else
                    return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

      
        /// <summary>
        /// 사용자 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditUserInfo(UsersTb? model)
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
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }


        /// <summary>
        /// 사용자 정보 삭제
        /// </summary>
        /// <param name="tguserid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteUserInfo(UsersTb? model)
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }
    }
}
