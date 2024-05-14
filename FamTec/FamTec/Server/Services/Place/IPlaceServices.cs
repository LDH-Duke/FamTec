using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Place
{
    public interface IPlaceServices
    {
        /// <summary>
        /// 사업장 전체리스트 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> GetAllPlaceList();

        /// <summary>
        /// 매개변수로 넘어온 사업장DTO 데이터베이스에 저장
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> AddPlaceInfo(PlacesDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 사업장DTO 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> UpdatePlaceInfo(PlacesDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 사업장DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> DeletePlaceInfo(PlacesDTO dto);

    }
}
