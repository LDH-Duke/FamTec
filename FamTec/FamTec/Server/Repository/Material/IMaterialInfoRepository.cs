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
    }
}
