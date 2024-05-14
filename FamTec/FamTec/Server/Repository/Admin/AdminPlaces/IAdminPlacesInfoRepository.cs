using FamTec.Shared.DTO;
using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Admin.AdminPlaces
{
    public interface IAdminPlacesInfoRepository
    {
        ValueTask<AdminPlacesTb> AddAsync(AdminPlacesTb model); // 추가

        ValueTask<List<AdminPlacesTb>> GetUserAllAsync(string userid); // USERID로 전체조회
        ValueTask<List<AdminPlacesTb>> GetPlaceAllAsync(string placecd); // PLACECD로 전체조회
        ValueTask<bool> DeleteAdminPlacesAsync(AdminPlacesTb model); // 삭제

        ValueTask<bool> EditAdminPlacesAsync(AdminPlacesTb model); // 수정

    }
}
