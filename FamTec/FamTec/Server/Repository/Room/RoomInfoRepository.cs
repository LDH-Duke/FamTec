using FamTec.Server.Databases;
using FamTec.Shared;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Room;
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

        public async Task GetRoomList(int? placeid)
        {
            RoomManagementDTO dto = await context.
        }


    }
}
