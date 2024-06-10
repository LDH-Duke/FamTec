using FamTec.Server.Databases;
using FamTec.Server.Services;
using FamTec.Shared;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Room;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Room
{
    public class RoomInfoRepository : IRoomInfoRepository
    {
        private readonly WorksContext context;
        private ILogService LogService;

        public RoomInfoRepository(WorksContext _context, ILogService _logservice)
        {
            this.context = _context;
            this.LogService = _logservice;
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
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }



        /// <summary>
        /// 층에 해당하는 공간 List 반환
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<List<RoomTb>?> GetRoomList(List<FloorTb?> model)
        {
            try
            {
                if(model is [_, ..])
                {
                    List<RoomTb>? room = await context.RoomTbs.Where(m => m.DelYn != 1).ToListAsync();

                    if(room is [_, ..])
                    {
                        List<RoomTb>? result = (from floortb in model
                                                join roomtb in room
                                                on floortb.Id equals roomtb.FloorTbId
                                                where floortb.DelYn != 1 && roomtb.DelYn != 1
                                                select new RoomTb
                                                {
                                                    Id = roomtb.Id,
                                                    Name = roomtb.Name,
                                                    CreateDt = roomtb.CreateDt,
                                                    CreateUser = roomtb.CreateUser,
                                                    UpdateDt = roomtb.UpdateDt,
                                                    UpdateUser = roomtb.UpdateUser,
                                                    DelYn = roomtb.DelYn,
                                                    DelDt = roomtb.DelDt,
                                                    DelUser = roomtb.DelUser,
                                                    FloorTbId = roomtb.FloorTbId
                                                }).ToList();
                        if (result is [_, ..])
                            return result;
                        else
                            return null;
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
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

    

        /// <summary>
        /// 공간 인덱스로 공간 검색
        /// </summary>
        /// <param name="roomidx"></param>
        /// <returns></returns>
        public async ValueTask<RoomTb?> GetRoomInfo(int? roomidx)
        {
            try
            {
                if(roomidx is not null)
                {
                    RoomTb? model = await context.RoomTbs.FirstOrDefaultAsync(m => m.Id == roomidx);

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
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

    }
}
