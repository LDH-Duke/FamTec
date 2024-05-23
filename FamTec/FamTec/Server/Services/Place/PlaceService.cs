using FamTec.Server.Repository.Place;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Place;
using System.Xml.Serialization;

namespace FamTec.Server.Services.Place
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceInfoRepository PlaceInfoRepository;

        ResponseOBJ<PlacesDTO> Response;
        Func<string, PlacesDTO, int, ResponseModel<PlacesDTO>> FuncResponseOBJ;
        Func<string, List<PlacesDTO>, int, ResponseModel<PlacesDTO>> FuncResponseList;


        public PlaceService(IPlaceInfoRepository _placeinforepository)
        {
            this.PlaceInfoRepository = _placeinforepository;

            Response = new ResponseOBJ<PlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

       


        /// <summary>
        /// 사업장정보 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> GetPlaceListService()
        {
            try
            {
                List<PlaceTb>? model = await PlaceInfoRepository.GetAllList();

                if(model is [_, ..])
                {
                    return FuncResponseList("전체데이터 조회 성공", model.Select(e => new PlacesDTO
                    {
                        PlaceIndex = e.Id,
                        PlaceCd = e.PlaceCd,
                        Name = e.Name,
                        CONTRACT_NUM = e.ContractNum,
                        ContractDT = e.ContractDt,
                        CancelDT = e.CancelDt,
                        Status = e.Status
                    }).ToList(), 200);
                }
                else
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }


   


  
    }
}
