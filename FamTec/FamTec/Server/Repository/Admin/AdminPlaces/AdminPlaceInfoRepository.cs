﻿using FamTec.Server.Databases;
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
        public async ValueTask<AdminPlacesTb?> AddAsync(AdminPlacesTb? model)
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
        public async ValueTask<List<AdminPlacesTb>?> GetAllList()
        {
            try
            {
                List<AdminPlacesTb> model = await context.AdminPlacesTbs.Where(m => m.DelYn != true).ToListAsync();
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
        public async ValueTask<List<AdminPlacesTb>?> GetAllPlaceList(string? placecd)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(placecd))
                {
                    List<AdminPlacesTb>? model = await context.AdminPlacesTbs.Where(m => m.PlacecodeCd == placecd && m.DelYn != true).ToListAsync();

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
        public async ValueTask<List<AdminPlacesTb>?> GetAllUserList(int? userid)
        {
            try
            {
                if(userid is not null)
                {
                    List<AdminPlacesTb>? model = await context.AdminPlacesTbs.Where(m => m.UserId == userid && m.DelYn != true).ToListAsync();

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
        public async ValueTask<AdminPlacesTb?> GetPlaceInfo(int? userid, string? placecd)
        {
            try
            {
                if(userid is not null && !String.IsNullOrWhiteSpace(placecd))
                {
                    AdminPlacesTb? model = await context.AdminPlacesTbs.FirstOrDefaultAsync(m => m.UserId == userid && m.PlacecodeCd == placecd && m.DelYn != true);

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
        public async ValueTask<bool?> DeleteAdminPlacesInfo(AdminPlacesTb? model)
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
        public async ValueTask<bool?> EditAdminPlacesInfo(AdminPlacesTb? model)
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
