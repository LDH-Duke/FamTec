using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Admin.User
{
    public interface IAdminUserService
    {
        /// <summary>
        /// 관리자 전체 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> GetAllUserListService();
        
        /// <summary>
        /// 해당 관리자 USER 정보 조회
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> GetAdminUserService(string adminid);

        /// <summary>
        /// 관리자 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> AddAdminUserSerivce(AdminsDTO dto);

        /// <summary>
        /// 관리자 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> UpdateAdminUserService(AdminsDTO dto);

        /// <summary>
        /// 관리자 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AdminsDTO>> DeleteAdminUserService(AdminsDTO dto);


    }
}
