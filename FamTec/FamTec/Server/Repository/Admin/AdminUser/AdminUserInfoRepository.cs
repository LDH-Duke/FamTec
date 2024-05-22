using FamTec.Server.Databases;
using FamTec.Shared;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Admin.AdminUser
{
    public class AdminUserInfoRepository : IAdminUserInfoRepository
    {
        private readonly WorksContext context;

        public AdminUserInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 관리자 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminTb?> AddAdminUserInfo(AdminTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.AdminTbs.Add(model);
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
        public async ValueTask<List<AdminTb>?> GetAllList()
        {
            try
            {
                List<AdminTb>? model = await context.AdminTbs
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
        /// 매개변수의 관리자ID에 해당하는 관리자모델 모델 조회
        /// </summary>
        /// <param name="adminuseridx"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<AdminTb?> GetAdminUserInfo(int? usertbid)
        {
            try
            {
                if (usertbid is not null)
                {
                    AdminTb? model = await context.AdminTbs.FirstOrDefaultAsync(m => m.UserTbId.Equals(usertbid) && m.DelYn != 1);
                    if (model is not null)
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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 매개변수의 곤리자ID에 해당하는 관리자모델 리스트 조회
        /// </summary>
        /// <param name="departmentidx"></param>
        /// <returns></returns>
        public async ValueTask<List<AdminTb>?> GetAdminDepartment(int? departmnetid)
        {
            try
            {
                List<AdminTb>? model = await context.AdminTbs
                    .Where(m => 
                    m.DepartmentTbId.Equals(departmnetid) && 
                    m.DelYn != 1).ToListAsync();

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
        public async ValueTask<AdminTb?> GetAdminInfo(int? usertbid, int? departmentid)
        {
            try
            {
                if(usertbid is not null && departmentid is not null)
                {
                    AdminTb? model = await context.AdminTbs
                        .FirstOrDefaultAsync(m => m.UserTbId.Equals(usertbid) && 
                        m.DepartmentTbId.Equals(departmentid) && 
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

        /// <summary>>
        /// 관리자 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditAdminInfo(AdminTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminTbs.Update(model);
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
        public async ValueTask<bool?> DeleteAdminInfo(AdminTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.AdminTbs.Update(model);
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
