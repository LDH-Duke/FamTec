using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Voc
{
    public interface IVocInfoRpeository
    {
        /// <summary>
        /// VOC 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<VocTb?> AddAsync(VocTb? model);
    }
}
