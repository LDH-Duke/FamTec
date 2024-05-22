using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Place;

namespace FamTec.Server.Services.Place
{
    public interface IPlaceService
    {
        

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> GetAllPlaceService();


        /// <summary>
        /// 사업장 인덱스로 사업장 정보 조회 - 단일모델
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> GetByPlaceService(int? id);

        /// <summary>
        /// 사업장코드로 사업장 정보 조회 - 단일모델
        /// </summary>
        /// <param name="Placecd"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> GetByPlaceService(string? Placecd);

        /// <summary>
        /// 사업장 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> EditPlaceService(PlacesDTO? dto);

        /// <summary>
        /// 사업장 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> DeletePlaceService(PlacesDTO? dto);

    }
}
