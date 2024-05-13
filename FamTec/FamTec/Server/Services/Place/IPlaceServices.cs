using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Place
{
    public interface IPlaceServices
    {
        /// <summary>
        /// 사업장 전체조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseObject<PlacesDTO>> GetAllUserListService();

        /// <summary>
        /// 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<PlacesDTO>> AddPlaceService(PlacesDTO dto);

        /// <summary>
        /// 사업장 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<PlacesDTO>> UpdatePlaceService(PlacesDTO dto);

        /// <summary>
        /// 사업장 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<PlacesDTO>> DeletePlaceService(PlacesDTO dto);

    }
}
