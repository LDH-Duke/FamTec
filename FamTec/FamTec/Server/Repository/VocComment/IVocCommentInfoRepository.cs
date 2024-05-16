using FamTec.Shared.Model;

namespace FamTec.Server.Repository.VocComment
{
    public interface IVocCommentInfoRepository
    {
        /// <summary>
        /// VOC COMMENT 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<VocCommentsTb?> AddAsync(VocCommentsTb? model);

        /// <summary>
        /// VOC COMMENT 리스트 전체 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<VocCommentsTb>?> GetAllList();

        /// <summary>
        /// VOC COMMENT 리스트 VOCID로 조회
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        ValueTask<List<VocCommentsTb>?> GetVocIDList(int? vocid);

        /// <summary>
        /// VOC COMMENT 리스트 userid로 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<List<VocCommentsTb>?> GetVocUserIDList(string? userid);

        /// <summary>
        /// VOC COMMENT ID로 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<VocCommentsTb?> GetVocInfo(int? id);

        /// <summary>
        /// VOC COMMENT 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditVocCommentInfo(VocCommentsTb? model);
        
        /// <summary>
        /// VOC COMMENT 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteVocCommentInfo(VocCommentsTb? model);

    }
}
