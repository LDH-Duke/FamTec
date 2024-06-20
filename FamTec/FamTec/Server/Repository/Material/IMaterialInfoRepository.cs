using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Material
{
    public interface IMaterialInfoRepository
    {
        /// <summary>
        /// 자재 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<MaterialTb?> AddMaterialInfo(MaterialTb? model);

        /// <summary>
        /// 사업장에 속한 자재LIST 출력
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        ValueTask<List<MaterialTb>?> GetMaterialList(int? placeid);

        /// <summary>
        /// 자재 ID로 단일 자재모델 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<MaterialTb?> GetMaterialInfo(int? id);
    }
}
