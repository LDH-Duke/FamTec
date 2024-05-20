using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Room
{
    public class RoomInfoRepository : IRoomInfoRepository
    {
        private readonly WorksContext context;

        public RoomInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 공간정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<RoomTb?> AddAsync(RoomTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.RoomTbs.Add(model);
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
        /// 공간리스트 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<RoomTb>?> GetAllList()
        {
            try
            {
                List<RoomTb>? model = await context.RoomTbs
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
        /// 인덱스에 해당하는 공간정보 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<RoomTb?> GetRommInfo(int? id)
        {
            try
            {
                if (id is not null)
                {
                    RoomTb? model = await context.RoomTbs
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
        /// 공간이름에 해당하는 정보조회
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async ValueTask<RoomTb?> GetRoomInfo(string? name, int? floortbid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(name))
                {
                    RoomTb? model = await context.RoomTbs
                        .FirstOrDefaultAsync(m => 
                        m.FloorTbId.Equals(floortbid) &&
                        m.Name.Equals(name));
                    
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
        /// 층ID에 해당하는 공간리스트 반환
        /// </summary>
        /// <param name="floorid"></param>
        /// <returns></returns>
        public async ValueTask<List<RoomTb>?> GetRoomsList(int? floortbid)
        {
            try
            {
                if(floortbid is not null)
                {
                    List<RoomTb>? model = await context.RoomTbs
                        .Where(m => 
                        m.FloorTbId.Equals(floortbid) &&
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
        /// 공간정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditRoomsInfo(RoomTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.RoomTbs.Update(model);
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
        /// 공간정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteRoomsInfo(RoomTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.RoomTbs.Update(model);
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
