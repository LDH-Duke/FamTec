using FamTec.Shared.Server.DTO;
using Microsoft.AspNetCore.Http;

namespace FamTec.Server.Services.Voc
{
    public interface IVocService
    {
        /// <summary>
        /// 민원 추가
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public ValueTask<ResponseUnit<string>?> AddVocService(string obj, IFormFile[] image);
    }
}
