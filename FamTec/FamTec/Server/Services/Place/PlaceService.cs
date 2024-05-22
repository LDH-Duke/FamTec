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
        public async ValueTask<ResponseModel<PlacesDTO>> GetAllPlaceService()
        {
            try
            {
                List<PlaceTb>? model = await PlaceInfoRepository.GetAllList();

                if(model is [_, ..])
                {
                    return FuncResponseList("전체데이터 조회 성공", model.Select(e => new PlacesDTO
                    {
                        PlaceCd = e.PlaceCd,
                        Name = e.Name,
                        CONTRACT_NUM = e.ContractNum,
                        Note = e.Note
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


        /// <summary>
        /// 사업장 인덱스로 사업장 정보 조회 - 단일모델
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> GetByPlaceService(int? id)
        {
            try
            {
                if (id is not null)
                {
                    PlaceTb? model = await PlaceInfoRepository.GetByPlaceInfo(id);

                    if (model is not null)
                    {
                        return FuncResponseOBJ("데이터 조회 성공", new PlacesDTO
                        {
                            PlaceCd = model.PlaceCd,
                            Name = model.Name,
                            CONTRACT_NUM = model.ContractNum,
                            Note = model.Note
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }

                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }

            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

        /// <summary>
        /// 사업장코드로 사업장 정보 조회 - 단일모델
        /// </summary>
        /// <param name="Placecd"></param>
        /// <returns></returns>

        public async ValueTask<ResponseModel<PlacesDTO>> GetByPlaceService(string? Placecd)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(Placecd))
                {
                    PlaceTb? model = await PlaceInfoRepository.GetByPlaceInfo(Placecd);

                    if (model is not null)
                    {
                        return FuncResponseOBJ("데이터 조회 성공", new PlacesDTO
                        {
                            PlaceCd = model.PlaceCd,
                            Name = model.Name,
                            CONTRACT_NUM = model.ContractNum,
                            Note = model.Note
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }

                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

      

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> DeletePlaceService(PlacesDTO? dto)
        {
            try
            {
                if(dto is not null) // 넘어온 DTO NULL 체크
                {
                    PlaceTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd);

                    if(model == null) // 해당 데이터가 조회가 되지 않을때.
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                    else
                    {
                        model.DelDt = DateTime.Now;
                        model.DelUser = "토큰USER";
                        model.DelYn = 1;

                        bool? result = await PlaceInfoRepository.DeletePlaceInfo(model);
                        if (result == true)
                        {
                            return FuncResponseOBJ("데이터 삭제 성공.", new PlacesDTO()
                            {
                                PlaceCd = model.PlaceCd,
                                Name = model.Name,
                                CONTRACT_NUM = model.ContractNum,
                                Note = model.Note
                            }, 200);
                        }
                        else if (result == false)
                        {
                            return FuncResponseOBJ("데이터 삭제 실패", null, 200);
                        }
                        else
                        {
                            return FuncResponseOBJ("데이터 삭제 실패", null, 200);
                        }
                    }
                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> EditPlaceService(PlacesDTO? dto)
        {
            try
            {
                if (dto is not null) // 넘어온 DTO NULL 체크
                {
                    PlaceTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd);

                    if (model == null) // 해당 데이터가 조회가 되지 않을때.
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                    else
                    {
                        model.UpdateDt = DateTime.Now;
                        model.UpdateUser = "토큰USER";

                        bool? result = await PlaceInfoRepository.EditPlaceInfo(model);
                        if (result == true)
                        {
                            return FuncResponseOBJ("데이터 수정 성공.", new PlacesDTO()
                            {
                                PlaceCd = model.PlaceCd,
                                Name = model.Name,
                                CONTRACT_NUM = model.ContractNum,
                                Note = model.Note
                            }, 200);
                        }
                        else if (result == false)
                        {
                            return FuncResponseOBJ("데이터 수정 실패", null, 200);
                        }
                        else
                        {
                            return FuncResponseOBJ("데이터 수정 실패", null, 200);
                        }
                    }
                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }
            catch (Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

  
    }
}
