using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Material;

namespace FamTec.Server.Services.Material
{
    public interface IMaterialService
    {
        public ValueTask<ResponseModel<AddMaterialDTO>?> AddMaterialService(AddMaterialDTO? dto, SessionInfo? sessioninfo);
    }
}
