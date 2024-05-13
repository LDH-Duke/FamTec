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
        public async ValueTask<PlacesTb> AddAsync(PlacesTb model, string userid)
        {
            try
            {
                model.CreateUser = userid;
                model.CreateDt = DateTime.Now;
                
                context.PlacesTbs.Add(model);
                
                await context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
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
                throw;
            }
        }

        /// <summary>
        /// UserID로 사업장 조회 - 리스트반환 *[차후개발]
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<List<PlacesTb>> GetUserPlaceCDListAsync(string userid)
        {
            try
            {
                var query = from user in context.UsersTbs
                            where user.UserId == userid
                            join place in context.PlacesTbs
                            on user.PlacecodeCd equals place.PlaceCd
                            select new PlacesTb()
                            { 
                                // 차후 추가
                                Name = place.Name
                            };

                List<PlacesTb> temp = query.ToList();

                foreach(var item in query)
                {
                    Console.WriteLine(item.Name);

                }

                Console.WriteLine();
                

                // 외래키 잡은것들 테스트
                return null;
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="placecd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeletePlaceCDAsync(string placecd, string userid)
        {
            try
            {
                PlacesTb? model = await context.PlacesTbs.FirstOrDefaultAsync(m => m.PlaceCd == placecd);
                if (model is not null)
                {
                    model.DelDt = DateTime.Now;
                    model.DelYn = true;
                    model.DelUser = userid;

                    context.PlacesTbs.Update(model);
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
        public async ValueTask<bool> EditAsync(PlacesTb model, string userid)
        {
            try
            {
                if(model is not null)
                {
                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = userid;

                    context.PlacesTbs.Update(model);

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
