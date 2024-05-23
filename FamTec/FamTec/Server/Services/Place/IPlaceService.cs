using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Place;

namespace FamTec.Server.Services.Place
{
    public interface IPlaceService
    {
        

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<PlacesDTO>> GetPlaceListService();


       

    }
}
