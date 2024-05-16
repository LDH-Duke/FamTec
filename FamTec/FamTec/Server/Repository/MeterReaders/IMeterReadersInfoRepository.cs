using FamTec.Shared.Model;

namespace FamTec.Server.Repository.MeterReaders
{
    public interface IMeterReadersInfoRepository
    {
        /// <summary>
        /// 계량기 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<MeterReadersTb?> AddAsync(MeterReadersTb? model);
        
        /// <summary>
        /// 전체 계량기 리스트 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<MeterReadersTb>?> GetAllList();

        /// <summary>
        /// 건물코드에 해당하는 전체 계량기 리스트 조회
        /// </summary>
        /// <param name="buildingCD"></param>
        /// <returns></returns>
        ValueTask<List<MeterReadersTb>?> GetReaderList(string? buildingCD);

        /// <summary>
        /// 계량기 ID에 해당하는 계량기 정보 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<MeterReadersTb?> GetReaderInfo(int? id);

        /// <summary>
        /// 계량기 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditReaderInfo(MeterReadersTb? model);

        /// <summary>
        /// 계량기 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteReaderInfo(MeterReadersTb? model);



    }
}
