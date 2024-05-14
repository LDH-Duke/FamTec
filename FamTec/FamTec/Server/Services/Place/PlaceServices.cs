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

        ResponseOBJ<PlacesDTO> Response;
        Func<string, PlacesDTO, int, ResponseModel<PlacesDTO>> FuncResponseOBJ;
        Func<string, List<PlacesDTO>, int, ResponseModel<PlacesDTO>> FuncResponseList;


        public PlaceServices(IPlaceInfoRepository _placeinforepository)
        {
            this.PlaceInfoRepository = _placeinforepository;

            Response = new ResponseOBJ<PlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

        /// <summary>
        /// 사업장 전체리스트 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> GetAllPlaceList()
        {
            List<PlacesTb>? result = await PlaceInfoRepository.GetAllList();

            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new PlacesDTO()
                {
                    PlaceCd = e.PlaceCd,
                    Name = e.Name,
                    CONTRACT_NUM = e.ContractNum,
                    NOTE = e.Note
                }).ToList(), 200);
            }
            else
            {
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }

        }

        /// <summary>
        /// 매개변수로 넘어온 사업장DTO 데이터베이스에 저장
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> AddPlaceInfo(PlacesDTO dto)
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
                        return FuncResponseOBJ("데이터 추가에 실패하였습니다.", null, 404);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터 추가에 성공하였습니다.", new PlacesDTO()
                        {
                            PlaceCd = result.PlaceCd,
                            Name = result.Name,
                            CONTRACT_NUM = result.ContractNum,
                            NOTE = result.Note
                        }, 200);
                    }
                }
                else
                {
                    // 이미 해당 코드로 사업장이 있다.
                    return FuncResponseOBJ("이미 해당코드의 사업장이 존재합니다.", null, 200);
                }
            }
            else
            {
                // 넘어온 데이터가 잘못됐다.
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 매개변수로 넘어온 사업장DTO 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> UpdatePlaceInfo(PlacesDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                PlacesTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd); // 해당 코드로 사업장이 있는지 조회

                if(model == null) // 없음
                {
                    // 없어서 수정못함. return 해야함.
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 404);
                }
                else
                {
                    if(dto.Name != null)
                        model.Name = dto.Name;
                    if(dto.CONTRACT_NUM != null)
                        model.ContractNum = dto.CONTRACT_NUM;
                    if(dto.NOTE != null)
                        model.Note = dto.NOTE;

                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = "김용우";


                    bool result = await PlaceInfoRepository.EditPlaceInfoAsync(model);

                    if(result) // 수정성공
                    {
                        return FuncResponseOBJ("데이터 수정 성공.", new PlacesDTO()
                        {
                            PlaceCd = model.PlaceCd,
                            Name = model.Name,
                            CONTRACT_NUM = model.ContractNum,
                            NOTE = model.Note
                        }, 200);
                    }
                    else // 실패
                    {
                        return FuncResponseOBJ("데이터 수정 실패", null, 404);
                    }

                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 매개변수로 넘어온 사업장DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> DeletePlaceInfo(PlacesDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                PlacesTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd); // 해당 코드로 사업장이 있는지 조회

                if(model == null) // 없음
                {
                    // 없어서 수정못함
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 404);
                }
                else // 있음
                {
                    model.DelDt = DateTime.Now;
                    model.DelUser = "김용우";
                    model.DelYn = true;

                    bool result = await PlaceInfoRepository.DeletePlaceInfoAsync(model);

                    if(result) // 삭제성공
                    {
                        return FuncResponseOBJ("데이터 삭제 성공.", new PlacesDTO()
                        {
                            PlaceCd = model.PlaceCd,
                            Name = model.Name,
                            CONTRACT_NUM = model.ContractNum,
                            NOTE = model.Note
                        }, 200);
                    }
                    else // 실패
                    {
                        return FuncResponseOBJ("데이터 삭제 실패", null, 404);
                    }
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

    }
}
