using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Room
{
    public interface IRoomInfoRepository
    {
        /// <summary>
        /// 공간 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<RoomTb?> AddAsync(RoomTb? model);

      
        
    }
}
