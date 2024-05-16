using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Facilitys
{
    public interface IFacilitysRepository
    {
        /// <summary>
        /// 설비 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<FacilitysTb?> AddAsync(FacilitysTb? model);

        /// <summary>
        /// 설비 전체 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<FacilitysTb>?> GetAllList();

        /// <summary>
        /// 공간테이블 ID에 해당하는 설비 리스트 조회
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        ValueTask<List<FacilitysTb>?> GetFacilitysList(int? roomid);

        /// <summary>
        /// 설비ID에 해당하는 설비모델정보 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<FacilitysTb?> GetFacilitysInfo(int? id);

        /// <summary>
        /// 설비정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditFacilitysInfo(FacilitysTb? model);

        /// <summary>
        /// 설비정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteFacilitysInfo(FacilitysTb? model);

    }
}
