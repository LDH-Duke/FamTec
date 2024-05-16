using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace FamTec.Server.Repository.SocketRoom
{
    public class SocketRoomsInfoRepository : ISocketRoomsInfoRepository
    {
        private readonly FmsContext context;

        public SocketRoomsInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 소켓 그룹 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<SocketRoomsTb?> AddAsync(SocketRoomsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.SocketRoomsTbs.Add(model);
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
        /// 소켓 그룹 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<SocketRoomsTb>?> GetAllList()
        {
            try
            {
                List<SocketRoomsTb>? model = await context.SocketRoomsTbs.Where(m => m.DelYn != true).ToListAsync();
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
        /// 소켓 RoomCode에 해당하는 소켓그룹 조회
        /// </summary>
        /// <param name="roomcode"></param>
        /// <returns></returns>
        public async ValueTask<SocketRoomsTb?> GetSocketRoomInfo(string roomcode)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(roomcode))
                {
                    SocketRoomsTb? model = await context.SocketRoomsTbs.FirstOrDefaultAsync(m => m.RoomCd == roomcode && m.DelYn != true);
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
        /// 소켓 그룹 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditRoomsInfo(SocketRoomsTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.SocketRoomsTbs.Update(model);
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
        /// 소켓 그룹 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteRoomsInfo(SocketRoomsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.SocketRoomsTbs.Update(model);
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
