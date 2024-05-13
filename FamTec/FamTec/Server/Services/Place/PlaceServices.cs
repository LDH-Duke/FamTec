using Azure;
using FamTec.Server.Repository.Place;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;

namespace FamTec.Server.Services.Place
{
    public class PlaceServices : IPlaceServices
    {
        private readonly IPlaceInfoRepository PlaceInfoRepository;

        public PlaceServices(IPlaceInfoRepository _placeinforepository)
        {
            this.PlaceInfoRepository = _placeinforepository;
        }

        /// <summary>
        /// 사업장 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseObject<PlacesDTO>> GetAllUserListService()
        {
            List<PlacesTb>? result = await PlaceInfoRepository.GetAllAsync();

            if(result is [_, ..])
            {
                ResponseObject<PlacesDTO> obj = new()
                {
                    Message = "성공",
                    Data = result.Select(e => new PlacesDTO()
                    {
                        PlaceCd = e.PlaceCd,
                        Name = e.Name,
                        CONTRACT_NUM = e.ContractNum,
                        NOTE = e.Note
                    }).ToList(),

                    StatusCode = 200
                };

                return obj;
            }
            else
            {
                ResponseObject<PlacesDTO> obj = new()
                {
                    Message = "데이터가 존재하지 않습니다.",
                    Data = null,
                    StatusCode = 200
                };
                return obj;
            }

        }

        /// <summary>
        /// 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<ResponseObject<PlacesDTO>> AddPlaceService(PlacesDTO dto)
        {
            if(dto is not null) // 넘어온 모델이 NULL이 아니어야함.
            {
                PlacesTb? placechk = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd); // 해당 코드로 사업장이 있는지 조회

                if(placechk == null) // 없음
                {
                    PlacesTb placetb = new PlacesTb()
                    {
                        PlaceCd = dto.PlaceCd,
                        Name = dto.Name,
                        ContractNum = dto.CONTRACT_NUM,
                        Note = dto.NOTE,
                        CreateDt = DateTime.Now
                    };

                    var result = await PlaceInfoRepository.AddAsync(placetb);

                    if(result == null)
                    {
                        // ADD에 실패하였을때.
                        ResponseObject<PlacesDTO> model = new()
                        {
                            Message = "데이터 추가에 실패하였습니다.",
                            Data = null,
                            StatusCode = 404
                        };
                        return model;
                    }
                    else
                    {
                        ResponseObject<PlacesDTO> model = new()
                        {
                            Message = "데이터 추가에 성공하였습니다.",
                            Data = new List<PlacesDTO>()
                            {
                                new PlacesDTO()
                                {
                                    PlaceCd = result.PlaceCd,
                                    Name = result.Name,
                                    CONTRACT_NUM = result.ContractNum,
                                    NOTE = result.Note
                                }
                            },
                            StatusCode = 200
                        };

                        return model;
                    }

                }
                else
                {
                    // 이미 해당 코드로 사업장이 있다.
                    ResponseObject<PlacesDTO> model = new()
                    {
                        Message = "이미 해당코드로 사업장이 존재합니다.",
                        Data = null,
                        StatusCode = 200
                    };
                    return model;
                }
            }
            else
            {
                // 이미 해당 코드로 사업장이 있다.
                ResponseObject<PlacesDTO> model = new()
                {
                    Message = "데이터가 비어있습니다.",
                    Data = null,
                    StatusCode = 404
                };
                return model;
            }

        }

        /// <summary>
        /// 사업장 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<ResponseObject<PlacesDTO>> UpdatePlaceService(PlacesDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                PlacesTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd); // 해당 코드로 사업장이 있는지 조회

                if(model == null) // 없음
                {
                    // 없어서 수정못함. return 해야함.
                    ResponseObject<PlacesDTO> obj = new()
                    {
                        Message = "데이터가 존재하지 않습니다.",
                        Data = null,
                        StatusCode = 404
                    };
                    
                    return obj;
                }
                else
                {
                    model.Name = dto.Name;
                    model.ContractNum = dto.CONTRACT_NUM;
                    model.Note = dto.NOTE;
                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = "김용우";


                    bool result = await PlaceInfoRepository.EditAsync(model);

                    if(result) // 수정성공
                    {
                        ResponseObject<PlacesDTO> obj = new()
                        {
                            Message = "데이터 수정 성공.",
                            Data = new List<PlacesDTO>() 
                            {
                                new PlacesDTO()
                                {
                                    PlaceCd = model.PlaceCd,
                                    Name = model.Name,
                                    CONTRACT_NUM = model.ContractNum,
                                    NOTE = model.Note
                                }
                            },
                            StatusCode = 200
                        };
                        return obj;
                    }
                    else // 실패
                    {
                        ResponseObject<PlacesDTO> obj = new()
                        {
                            Message = "데이터 수정 실패.",
                            Data = null,
                            StatusCode = 404
                        };
                        return obj;
                    }

                }
            }
            else
            {
                ResponseObject<PlacesDTO> obj = new()
                {
                    Message = "데이터가 비어있습니다.",
                    Data = null,
                    StatusCode = 404
                };
                return obj;
            }
        }

        /// <summary>
        /// 사업장 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseObject<PlacesDTO>> DeletePlaceService(PlacesDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                PlacesTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd); // 해당 코드로 사업장이 있는지 조회

                if(model == null) // 없음
                {
                    // 없어서 수정못함
                    ResponseObject<PlacesDTO> obj = new()
                    {
                        Message = "데이터가 존재하지 않습니다.",
                        Data = null,
                        StatusCode = 404
                    };
                    return obj;
                }
                else // 있음
                {
                    model.DelDt = DateTime.Now;
                    model.DelUser = "김용우";
                    model.DelYn = true;

                    bool result = await PlaceInfoRepository.DeletePlaceCDAsync(model);

                    if(result) // 삭제성공
                    {
                        ResponseObject<PlacesDTO> obj = new()
                        {
                            Message = "데이터 삭제 성공.",
                            Data = new List<PlacesDTO>()
                            {
                                new PlacesDTO()
                                {
                                    PlaceCd = model.PlaceCd,
                                    Name = model.Name,
                                    CONTRACT_NUM = model.ContractNum,
                                    NOTE = model.Note
                                }
                            },
                            StatusCode = 200
                        };
                        return obj;
                    }
                    else // 실패
                    {
                        ResponseObject<PlacesDTO> obj = new()
                        {
                            Message = "데이터 삭제 실패.",
                            Data = null,
                            StatusCode = 404
                        };
                        return obj;
                    }
                }
            }
            else
            {
                ResponseObject<PlacesDTO> obj = new()
                {
                    Message = "데이터가 비어있습니다.",
                    Data = null,
                    StatusCode = 404
                };
                return obj;
            }
        }

    }
}
