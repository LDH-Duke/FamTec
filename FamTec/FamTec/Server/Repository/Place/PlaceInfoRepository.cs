using FamTec.Client.Pages.Place;
using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Repository.Place
{
    public class PlaceInfoRepository : IPlaceInfoRepository
    {
        private readonly WorksContext context;

        public PlaceInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<PlaceTb?> AddPlaceInfo(PlaceTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.PlaceTbs.Add(model);
                    await context.SaveChangesAsync();
                    return model;
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
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<PlaceTb>?> GetAllList()
        {
            try
            {
                List<PlaceTb>? model = await context.PlaceTbs.Where(m => m.DelYn != 1).ToListAsync();

                if (model is [_, ..])
                    return model;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 사업장코드로 사업장 조회
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<PlaceTb?> GetByPlaceInfo(string? placecd)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(placecd))
                {
                    PlaceTb? model = await context.PlaceTbs
                        .FirstOrDefaultAsync(m => m.PlaceCd.Equals(placecd) 
                        && m.DelYn != 1);

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
        /// 사업장인덱스로 사업장 조회
        /// </summary>
        /// <param name="placeidx"></param>
        /// <returns></returns>
        public async ValueTask<PlaceTb?> GetByPlaceInfo(int? id)
        {
            try
            {
                if (id is not null)
                {
                    PlaceTb? model = await context.PlaceTbs
                        .FirstOrDefaultAsync(m => m.Id.Equals(id) &&
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
        /// 삭제
        /// </summary>
        /// <param name="placecd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeletePlaceInfo(PlaceTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.PlaceTbs.Update(model);
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
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditPlaceInfo(PlaceTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.PlaceTbs.Update(model);
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
