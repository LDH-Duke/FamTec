using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Admin.AdminPlaces
{
    public class AdminPlaceInfoRepository : IAdminPlacesInfoRepository
    {
        private readonly WorksContext context;

        public AdminPlaceInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 관리자 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminPlaceTb?> AddAsync(AdminPlaceTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlaceTbs.Add(model);
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
                throw;
            }
        }

        /// <summary>
        /// 전체 사업장 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<AdminPlaceTb>?> GetAllList()
        {
            try
            {
                List<AdminPlaceTb> model = await context.AdminPlaceTbs.Where(m => m.DelYn != 1).ToListAsync();
                if (model == null)
                    return null;
                else
                    return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 관리자 PLACECODE에 해당하는 전체 사업장 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<List<AdminPlaceTb>?> GetAllPlaceList(int? placeid)
        {
            try
            {
                if(placeid is not null)
                {
                    List<AdminPlaceTb>? model = await context.AdminPlaceTbs
                        .Where(m => 
                        m.PlaceId.Equals(placeid) && 
                        m.DelYn != 1).ToListAsync();

                    if (model is [_, ..])
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
        /// 관리자 USERID에 해당하는 전체 사업장 출력
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<List<AdminPlaceTb>?> GetAllUserList(int? admintbid)
        {
            try
            {
                if(admintbid is not null)
                {
                    List<AdminPlaceTb>? model = await context.AdminPlaceTbs
                        .Where(m => m.AdminTbId.Equals(admintbid) && 
                        m.DelYn != 1).ToListAsync();

                    if (model is [_, ..])
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
        /// 테이블 정보에 해당하는 단일 사업장모델 반환
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminPlaceTb?> GetPlaceInfo(int? admintbid, int? placeid)
        {
            try
            {
                if(admintbid is not null && placeid is not null)
                {
                    AdminPlaceTb? model = await context.AdminPlaceTbs
                        .FirstOrDefaultAsync(m => 
                        m.AdminTbId.Equals(admintbid) && 
                        m.PlaceId.Equals(placeid) && 
                        m.DelYn != 1);

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
        /// 해당하는 관리자 사업장 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteAdminPlacesInfo(AdminPlaceTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlaceTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 해당하는 관리자 사업장 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditAdminPlacesInfo(AdminPlaceTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlaceTbs.Update(model);
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
