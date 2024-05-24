using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FamTec.Server.Repository.Floor
{
    public class FloorInfoRepository : IFloorInfoRepository
    {
        private readonly WorksContext context;

        public FloorInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 층추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<FloorTb?> AddAsync(FloorTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.FloorTbs.Add(model);
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
        /// 층삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteFloorInfo(FloorTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.FloorTbs.Update(model);
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
                throw;
            }
        }

        /// <summary>
        /// 층 인덱스로 층 검색
        /// </summary>
        /// <param name="flooridx"></param>
        /// <returns></returns>
        public async ValueTask<FloorTb?> GetFloorInfo(int? flooridx)
        {
            try
            {
                if(flooridx is not null)
                {
                    FloorTb? model = await context.FloorTbs.FirstOrDefaultAsync(m => m.Id == flooridx && m.DelYn != 1);

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
                throw;
            }
        }



        /// <summary>
        /// 건물에 해당하는 층 리스트 조회
        /// </summary>
        /// <param name="buildingtbid"></param>
        /// <returns></returns>
        public async ValueTask<List<FloorTb>?> GetFloorList(int? buildingtbid)
        {
            try
            {
                if(buildingtbid is not null)
                {
                    List<FloorTb>? model = await context.FloorTbs.Where(m => m.BuildingTbId == buildingtbid && m.DelYn != 1).ToListAsync();

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
                throw;
            }
        }
    }
}
