using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Building
{
    /// <summary>
    /// 건물 서비스
    /// </summary>
    public interface IBuildingServices
    {
        /// <summary>
        /// 건물 전체 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> GetAllBuildings();

        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 저장
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> AddBuildingInfo(BuildingsDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 사업장Code에 해당하는 건물리스트 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> GetBuildingList(string placecd);

        /// <summary>
        /// 건물코드에 해당하는 건물정보 조회
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> GetBuildingInfo(string buildingcd);


        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> UpdateBuildingInfo(BuildingsDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> DeleteBuildingInfo(BuildingsDTO dto);
    }
}
