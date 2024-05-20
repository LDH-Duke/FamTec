using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Unit
{
    public interface IUnitInfoRepository
    {
        /// <summary>
        /// 단위 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<UnitTb?> AddAsync(UnitTb? model);

        /// <summary>
        /// 단위 전체 출력
        /// </summary>
        /// <returns></returns>
        ValueTask<List<UnitTb>?> GetAllList();

        /// <summary>
        /// 해당 사업장코드에 해당하는 모든 단위 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        ValueTask<List<UnitTb>?> GetUnitList(int? placetbid);

        /// <summary>
        /// 단위 인덱스에 해당하는 모델 조회
        /// </summary>
        /// <param name="unitidx"></param>
        /// <returns></returns>
        ValueTask<UnitTb?> GetUnitInfo(int? unitidx, int? placetbid);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditUnitInfo(UnitTb? model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteUnitInfo(UnitTb? model);

    }
}
