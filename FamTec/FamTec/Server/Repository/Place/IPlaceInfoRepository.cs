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
        ValueTask<PlacesTb> AddAsync(PlacesTb model, string userid);

        /// <summary>
        /// 전제조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<PlacesTb>> GetAllAsync();

        /// <summary>
        /// USERID로 사업장 조회 - 리스트 반환
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<List<PlacesTb>> GetUserPlaceCDListAsync(string userid);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool> EditAsync(PlacesTb model, string userid);


        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="placecd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool> DeletePlaceCDAsync(string placecd, string userid);
    }
}
