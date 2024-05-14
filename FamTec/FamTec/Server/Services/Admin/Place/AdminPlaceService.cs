using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Admin.Place
{
    /// <summary>
    /// 관리자 사업장 서비스
    /// </summary>
    public class AdminPlaceService : IAdminPlaceService
    {
        /// <summary>
        /// 관리자 사업장 전체 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> GetAllWorks()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 관리자 USERID에 해당하는 전체 관리자 사업장 모델리스트 출력
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> GetUserIDWorksList(string userid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 사업장 코드에 해당하는 전체 관리자 사업장 모델리스트 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> GetPlaceCDWorksList(string placecd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 관리자 사업장 DTO에 해당하는 내용 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> UpdateAdminWorks(AdminPlacesDTO dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 관리자 사업장 DTO에 해당하는 내용 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminPlacesDTO>> DeleteAdminWorks(AdminPlacesDTO dto)
        {
            throw new NotImplementedException();
        }

      

     

    

     
    }
}
