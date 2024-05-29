using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Building;

namespace FamTec.Server.Services.Building
{
    public interface IBuildingService
    {
        /// <summary>
        /// 건물추가
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>?> AddBuildingService(BuildingsDTO? dto, SessionInfo? session);

        /// <summary>
        /// 로그인한 아이디의 사업장의 건물리스트 조회
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>?> GetBuilidngListService(SessionInfo? session);

        /// <summary>
        /// 건물 정보 삭제
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<string>?> DeleteBuildingService(List<int>? index, SessionInfo? session);

    }
}
