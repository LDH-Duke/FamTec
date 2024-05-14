using FamTec.Shared.DTO;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Services.Admin.Place
{
    /// <summary>
    /// 관리자 사업장 서비스
    /// </summary>
    public interface IAdminPlaceService
    {
        /// <summary>
        /// 관리자 사업장 전체 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> GetAllWorks();

        /// <summary>
        /// 관리자 사업장 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> AddAdminWorksInfo(AdminPlacesDTO dto);

        /// <summary>
        /// 관리자 USERID에 해당하는 전체 관리자 사업장 모델리스트 출력
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> GetUserIDWorksList(string userid);

        /// <summary>
        /// 사업장 코드에 해당하는 전체 관리자 사업장 모델리스트 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> GetPlaceCDWorksList(string placecd);

        /// <summary>
        /// 관리자 사업장 DTO에 해당하는 내용 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> UpdateAdminWorks(AdminPlacesDTO beforedto, AdminPlacesDTO afterdto);

        /// <summary>
        /// 관리자 사업장 DTO에 해당하는 데이터베이스 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> DeleteAdminWorks(AdminPlacesDTO dto);
            
    }
}
