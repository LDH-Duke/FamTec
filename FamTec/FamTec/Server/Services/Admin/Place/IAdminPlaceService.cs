using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;

namespace FamTec.Server.Services.Admin.Place
{
    public interface IAdminPlaceService
    {
        /// <summary>
        /// 로그인한 관리자ID 인덱스가 속해있는 사업장 리스트 조회
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlaceDTO>> GetMyWorksService(int? adminid);

        /// <summary>
        /// 전체 사업장 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<AllPlaceDTO>> GetAllWorksService();

        /// <summary>
        /// 전체 관리자리스트 반환
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<ManagerListDTO>> GetAllManagerListService();

        /// <summary>
        /// 사업장 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AddPlaceDTO>> AddPlaceService(AddPlaceDTO? dto, SessionInfo? sessioninfo);

        /// <summary>
        /// 사업장 등록시 관리자 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AddPlaceDTO>> AddPlaceAdminService(AddPlaceDTO? dto, SessionInfo? sessioninfo);

        /// <summary>
        /// 선택 사업장 삭제
        /// </summary>
        /// <param name="placeidx"></param>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<string>> DeletePlaceService(List<int> placeidx, SessionInfo? sessioninfo);

        /// <summary>
        /// 사업장 번호로 사업장 모델 반환
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AddPlaceDTO>?> GetPlaceService(int? placeid);

    }
}
