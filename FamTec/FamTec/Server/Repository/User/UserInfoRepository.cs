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
        /// 추가
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
        /// 전체조회
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
        /// ID 검색 - 단일모델 반환
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UsersTb> GetByUserIdAsync(string userid)
        {
            try
            {
                UsersTb? model = await context.UsersTbs.FirstOrDefaultAsync(m => m.UserId == userid);
                if(model is not null)
                {
                    return model;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="tguserid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteUserIdAsync(string tguserid, string userid)
        {
            try
            {
                UsersTb? model = await context.UsersTbs.FirstOrDefaultAsync(m => m.UserId == tguserid);
                if(model is not null)
                {
                    model.DelDt = DateTime.Now;
                    model.DelYn = true;
                    model.DelUser = userid;

                    context.UsersTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> EditAsync(UsersTb model, string userid)
        {
            try
            {
                if(model is not null)
                {
                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = userid;

                    context.UsersTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}
