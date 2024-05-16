using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Unit
{
    public interface IUnitServices
    {
        /// <summary>
        /// 단위 전체 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<UnitsDTO>> GetAllUnitList();

        /// <summary>
        /// 단위 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UnitsDTO>> AddUnitInfo(UnitsDTO dto);

        /// <summary>
        /// 단위 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UnitsDTO>> UpdateUnitInfo(UnitsDTO dto);

        /// <summary>
        /// 단위 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UnitsDTO>> DeleteUnitInfo(UnitsDTO dto);

    }
}
