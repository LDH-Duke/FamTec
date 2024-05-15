using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Admin.AdminPlaces
{
    public class AdminPlaceInfoRepository : IAdminPlacesInfoRepository
    {
        private readonly FmsContext context;

        public AdminPlaceInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 관리자 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminPlacesTb> AddAsync(AdminPlacesTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlacesTbs.Add(model);
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
        public async ValueTask<List<AdminPlacesTb>> GetAllList()
        {
            try
            {
                return await context.AdminPlacesTbs.ToListAsync();
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
        public async ValueTask<List<AdminPlacesTb>> GetAllPlaceList(string placecd)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(placecd))
                {
                    List<AdminPlacesTb>? model = await context.AdminPlacesTbs.Where(m => m.PlacecodeCd == placecd).ToListAsync();

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
        public async ValueTask<List<AdminPlacesTb>> GetAllUserList(string userid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid))
                {
                    List<AdminPlacesTb>? model = await context.AdminPlacesTbs.Where(m => m.UsersUserid == userid).ToListAsync();

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
        public async ValueTask<AdminPlacesTb> GetPlaceInfo(string userid, string placecd)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid) && !String.IsNullOrWhiteSpace(placecd))
                {
                    AdminPlacesTb? tb = await context.AdminPlacesTbs.FirstOrDefaultAsync(m => m.UsersUserid == userid && m.PlacecodeCd == placecd);

                    if (tb is not null)
                    {
                        return tb;
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
        /// 해당하는 관리자 사업장 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteAdminPlacesInfo(AdminPlacesTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlacesTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
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
        public async ValueTask<bool> EditAdminPlacesInfo(AdminPlacesTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlacesTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

      
    }
}
