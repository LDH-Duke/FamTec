using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly FmsContext context;

        public RoomRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 공간정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<RoomsTb?> AddAsync(RoomsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.RoomsTbs.Add(model);
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
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<List<RoomsTb>?> GetAllList()
        {
            try
            {
                List<RoomsTb>? model = await context.RoomsTbs.Where(m => m.DelYn != true).ToListAsync();
                if (model == null)
                    return null;
                else
                    return model;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// ID에 해당하는 공간정보 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<RoomsTb?> GetRommInfo(int? id)
        {
            try
            {
                if (id != null)
                {
                    RoomsTb? model = await context.RoomsTbs.FirstOrDefaultAsync(m => m.Id == id && m.DelYn != true);
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
        /// 층ID에 해당하는 공간리스트 반환
        /// </summary>
        /// <param name="floorid"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<List<RoomsTb>?> GetRoomsList(int? floorid)
        {
            try
            {
                if(floorid != null)
                {
                    List<RoomsTb>? model = await context.RoomsTbs.Where(m => m.FloorId == floorid && m.DelYn != true).ToListAsync();
                    
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
        /// 공간정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<bool?> EditRoomsInfo(RoomsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.RoomsTbs.Update(model);
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
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<bool?> DeleteRoomsInfo(RoomsTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.RoomsTbs.Update(model);
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
