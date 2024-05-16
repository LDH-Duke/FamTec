using FamTec.Shared.Model;

namespace FamTec.Server.Repository.EnergyUsages
{
    public interface IEnergyUsageInfoRepository
    {
        /// <summary>
        /// 에너지사용량 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<EnergyUsagesTb?> AddAsync(EnergyUsagesTb? model);

        /// <summary>
        /// 전체 에너지사용량 리스트 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<EnergyUsagesTb>?> GetAllList();

        /// <summary>
        /// ID에 해당하는 에너지사용량 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<EnergyUsagesTb?> GetUsagesInfo(int? id);

        /// <summary>
        /// 에너지사용량 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditUsagesInfo(EnergyUsagesTb? model);

        /// <summary>
        /// 에너지사용량 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteUsagesInfo(EnergyUsagesTb? model);

    }
}
