using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace FamTec.Server.Repository.User
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly WorksContext context;

        public UserInfoRepository(WorksContext _context)
        {
            this.context = _context;
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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 사용자 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<UserTb>?> GetAllList()
        {
            try
            {
                List<UserTb>? model = await context.UserTbs
                    .Where(m => m.DelYn != 1).ToListAsync();
                
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
                        m.UserId.Equals(userid) &&
                        m.Password.Equals(password));

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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 매개변수 사업장에 해당하는 사용자리스트 조회
        /// </summary>
        /// <param name="placeidx"></param>
        /// <returns></returns>
        public async ValueTask<List<UserTb>?> GetAllUserList(int? placeidx)
        {
            try
            {
                if(placeidx is not null)
                {
                    List<UserTb>? model = await context.UserTbs
                        .Where(m => m.PlaceTbId.Equals(placeidx) &&
                        m.DelYn != 1).ToListAsync();
                    
                    if (model is [_, ..])
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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 사용자 정보 조회
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public async ValueTask<UserTb?> GetUserInfo(int? idx)
        {
            try
            {
                if(idx is not null)
                {
                    UserTb? model = await context.UserTbs
                        .FirstOrDefaultAsync(m => 
                        m.Id == idx && 
                        m.DelYn != 1);

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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }


        /// <summary>
        /// 사용자 ID 검색 - 사용자 단일모델 반환
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<UserTb?> GetUserInfo(string? userid, int? placetbid)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(userid) && placetbid is not null)
                {
                    UserTb? model = await context.UserTbs
                        .FirstOrDefaultAsync(m => 
                        m.UserId.Equals(userid) && 
                        m.PlaceTbId.Equals(placetbid) &&
                        m.DelYn != 1);

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
        public async ValueTask<bool?> EditUserInfo(UserTb? model)
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
        public async ValueTask<bool?> DeleteUserInfo(UserTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.UserTbs.Update(model);
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
