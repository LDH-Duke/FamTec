using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Admin.AdminUser
{
    public class AdminUserInfoRepository : IAdminUserInfoRepository
    {
        private readonly FmsContext context;

        public AdminUserInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 관리자 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminsTb?> AddAsync(AdminsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.AdminsTbs.Add(model);
                    await context.SaveChangesAsync();
                    return model;
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
        /// 관리자 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<AdminsTb>?> GetAllList()
        {
            try
            {
                List<AdminsTb>? model = await context.AdminsTbs.Where(m => m.DelYn != true).ToListAsync();
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
        /// 관리자 ID 검색 - 관리자 단일모델 반환
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public async ValueTask<AdminsTb?> GetAdminInfo(string? adminid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(adminid))
                {
                    AdminsTb? model = await context.AdminsTbs.FirstOrDefaultAsync(m => m.UserId == adminid && m.DelYn != true);
                    if (model == null)
                        return null;
                    else
                        return model;
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
        /// 관리자 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditAdminInfo(AdminsTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminsTbs.Update(model);
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


        /// <summary>
        /// 관리자 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteAdminInfo(AdminsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.AdminsTbs.Update(model);
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

    }
}
