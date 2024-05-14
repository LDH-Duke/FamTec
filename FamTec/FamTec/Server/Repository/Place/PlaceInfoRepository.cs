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
        private readonly FmsContext context;

        public PlaceInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<PlacesTb> AddAsync(PlacesTb model)
        {
            try
            {
                context.PlacesTbs.Add(model);
                await context.SaveChangesAsync();

                return model;
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
        public async ValueTask<List<PlacesTb>> GetAllAsync()
        {
            try
            {
                return await context.PlacesTbs.ToListAsync();
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
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<PlacesTb> GetByPlaceInfo(string placecd)
        {
            try
            {
                PlacesTb? model = await context.PlacesTbs.FirstOrDefaultAsync(m => m.PlaceCd == placecd);
                if (model == null)
                {
                    return null;
                }
                else
                {
                    return model;
                }
            }catch(Exception ex)
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
        public async ValueTask<bool> DeletePlaceCDAsync(PlacesTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.PlacesTbs.Update(model);
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
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> EditAsync(PlacesTb model)
        {
            try
            {
                if(model is not null)
                {
                    context.PlacesTbs.Update(model);
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
                throw new ArgumentException();
            }
        }

    


    }
}
