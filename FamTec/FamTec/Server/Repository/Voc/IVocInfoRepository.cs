using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Voc
{
    public interface IVocInfoRepository
    {
        /// <summary>
        /// VOC 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<VocTb?> AddAsync(VocTb? model);
        
        /// <summary>
        /// VOC 전체 리스트 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<VocTb>?> GetAllList();

        /// <summary>
        /// 빌딩코드에 해당하는 VOC 전체 리스트 조회
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <returns></returns>
        ValueTask<List<VocTb>?> GetVocList(string? buildingcd);

        /// <summary>
        /// VOC ID에 해당하는 VOC정보 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<VocTb?> GetVocInfo(int? id);

        /// <summary>
        /// VOC정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditVocInfo(VocTb? model);

        /// <summary>
        /// VOC 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteVocInfo(VocTb? model);

    }
}
