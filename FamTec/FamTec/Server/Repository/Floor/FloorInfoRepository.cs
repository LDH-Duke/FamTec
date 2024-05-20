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
        /// 층 추가
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
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 층 정보 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<FloorTb>?> GetAllList()
        {
            try
            {
                List<FloorTb>? model = await context.FloorTbs
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
        /// 해당 건물코드에 해당하는 층정보 조회
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<List<FloorTb>?> GetFloorList(int? buildingtbid)
        {
            try
            {
                if (buildingtbid is not null)
                {
                    List<FloorTb>? model = await context.FloorTbs
                        .Where(m => m.BuildingTbId.Equals(buildingtbid) && 
                        m.DelYn != 1).ToListAsync();

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
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 층 인덱스에 해당하는 층모델 정보 조회
        /// </summary>
        /// <param name="flooridx"></param>
        /// <returns></returns>
        public async ValueTask<FloorTb?> GetFloorIndexInfo(int? id)
        {
            try
            {
                if(id is not null)
                {
                    FloorTb? model = await context.FloorTbs
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
        /// 층 정보 수정
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
        public async ValueTask<bool?> EditFloorInfo(FloorTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.FloorTbs.Update(model);
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

  
    }
}
