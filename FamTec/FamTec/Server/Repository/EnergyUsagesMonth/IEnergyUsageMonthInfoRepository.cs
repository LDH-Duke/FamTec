using FamTec.Shared.Model;

namespace FamTec.Server.Repository.EnergyUsagesMonth
{
    public interface IEnergyUsageMonthInfoRepository
    {
        /// <summary>
        /// 해당년도 검침기록 생성
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<EnergyMonthUsageTb?> AddAsync(EnergyMonthUsageTb? model);

        /// <summary>
        /// 전체검침리스트 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<EnergyMonthUsageTb>?> GetAllList();

        /// <summary>
        /// 계량기별 전체년도 검침기록 리스트 조회
        /// </summary>
        /// <param name="readerid"></param>
        /// <returns></returns>
        ValueTask<List<EnergyMonthUsageTb>?> GetEnergyMonthList(int? readerid);

        /// <summary>
        /// 해당년도 전체 검침리스트 조회
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        ValueTask<List<EnergyMonthUsageTb>?> GetEnergyYearList(int? year);

        /// <summary>
        /// 계량기 해당 년도 검침기록 조회
        /// </summary>
        /// <param name="readerid"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        ValueTask<EnergyMonthUsageTb?> GetEnergyInfo(int? readerid, int? year);

        /// <summary>
        /// 계량기 검침기록 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditEnergyInfo(EnergyMonthUsageTb? model);

        /// <summary>
        /// 계량기 검침기록 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteEnergyInfo(EnergyMonthUsageTb? model);
    }
}

/*
        Meter_ReaderID -> List
        Year -> List
        MeterReaderID + YEAR -> Model
*/
