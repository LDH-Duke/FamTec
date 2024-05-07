using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Alarm
{
    public interface IAlarmInfoRepository
    {
        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<AlarmsTb> AddAsync(AlarmsTb model, string userid);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<AlarmsTb>> GetAllAsync();

        /// <summary>
        /// VOC 아이디로 내용 조회
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        ValueTask<AlarmsTb> GetByVocIDAsync(int vocid);

        /// <summary>
        /// VOC 아이디로 내용 조회 - 리스트반환
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        ValueTask<List<AlarmsTb>> GetByVocIDListAsync(int vocid);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool> EditAsync(AlarmsTb model, string userid);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        ValueTask<bool> DeleteVocIDAsync(int vocid, string userid);

    }
}
