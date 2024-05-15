using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FamTec.Server.Repository.Floor
{
    public class FloorInfoRepository : IFloorInfoRepository
    {
        private readonly FmsContext context;

        public FloorInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 층 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<FloorsTb> AddAsync(FloorsTb model)
        {
            try
            {
                if(model is not null)
                {
                    context.FloorsTbs.Add(model);
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
        /// 층 정보 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<FloorsTb>> GetAllList()
        {
            try
            {
                return await context.FloorsTbs.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 해당 건물코드에 해당하는 층정보 조회
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<List<FloorsTb>> GetFloorList(string buildingcd)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(buildingcd))
                {
                    List<FloorsTb>? model = await context.FloorsTbs.Where(m => m.BuildingCd == buildingcd).ToListAsync();

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
        /// 층 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteFloorInfo(FloorsTb model)
        {
            try
            {
                if(model is not null)
                {
                    context.FloorsTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 층 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool> EditFloorInfo(FloorsTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.FloorsTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
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
