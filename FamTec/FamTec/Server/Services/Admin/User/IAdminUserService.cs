using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Admin.User
{
    public interface IAdminUserService
    {
        /// <summary>
        /// 관리자 전체리스트 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> GetAllAdminList();
        
        /// <summary>
        /// 매개변수로 넘어온 USERID에 해당하는 관리자 정보 출력
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> GetAdminInfo(string adminid);

        /// <summary>
        /// 매개변수로 넘어온 관리자DTO 데이터베이스에 저장
        /// </summary>
        /// <param name="dto">관리자DTO</param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> AddAdminInfo(AdminsDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 관리자DTO 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> UpdateAdminInfo(AdminsDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 관리자DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> DeleteAdminInfo(AdminsDTO dto);


    }
}
