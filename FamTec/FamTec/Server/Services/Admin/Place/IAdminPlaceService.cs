using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Place;

namespace FamTec.Server.Services.Admin.Place
{
    public interface IAdminPlaceService
    {
        public ValueTask<ResponseModel<PlacesDTO>> AddPlaceService(PlacesDTO? dto, SessionInfo session);

        public ValueTask<ResponseModel<AdminPlaceDTO>> GetMyWorksService(int? adminid);
    }
}
