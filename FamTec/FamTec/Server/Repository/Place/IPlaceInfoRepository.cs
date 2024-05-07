using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Place
{
    public interface IPlaceInfoRepository
    {
        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<PlacesTb> AddAsync(PlacesTb model);

        /// <summary>
        /// 전제조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<PlacesTb>> GetAllAsync();

        /// <summary>
        /// USERID로 사업장 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<List<PlacesTb>> GetUsersPlaceCode(string userid);

    }
}
