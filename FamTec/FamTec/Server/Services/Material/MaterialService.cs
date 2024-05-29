using FamTec.Server.Repository.Material;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Material;

namespace FamTec.Server.Services.Material
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialInfoRepository MaterialInfoRepository;

        ResponseOBJ<AddMaterialDTO> Response;
        Func<string, AddMaterialDTO, int, ResponseModel<AddMaterialDTO>> FuncResponseOBJ;
        Func<string, List<AddMaterialDTO>, int, ResponseModel<AddMaterialDTO>> FuncResponseList;

        public MaterialService(IMaterialInfoRepository _materialinforepository)
        {
            this.MaterialInfoRepository = _materialinforepository;

            Response = new ResponseOBJ<AddMaterialDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }


        public async ValueTask<ResponseModel<AddMaterialDTO>?> AddMaterialService(AddMaterialDTO? dto, SessionInfo? sessioninfo)
        {
            try
            {
                if (dto is not null && sessioninfo is not null)
                {
                    MaterialTb model = new MaterialTb
                    {
                        Category = dto.Category,
                        MaterialCode = dto.MaterialCode,
                        Name = dto.Name,
                        Unit = dto.Unit,
                        Standard = dto.Standard,
                        Mfr = dto.Mfr,
                        SafeNum = dto.SafeNum,
                        CreateDt = DateTime.Now,
                        CreateUser = sessioninfo.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = sessioninfo.Name,
                        PlaceId = sessioninfo.selectPlace
                    };

                    MaterialTb? result = await MaterialInfoRepository.AddMaterialInfo(model);
                    if(result is not null)
                    {
                        return FuncResponseOBJ("품목 데이터가 추가되었습니다.", new AddMaterialDTO
                        {
                            Category = result.Category,
                            MaterialCode = result.MaterialCode,
                            Name = result.Name,
                            Unit = result.Unit,
                            Standard = result.Standard,
                            Mfr = result.Mfr,
                            SafeNum = result.SafeNum,
                            //Place

                        }, 200);
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
