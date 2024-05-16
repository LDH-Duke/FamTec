using FamTec.Server.Repository.Unit;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;

namespace FamTec.Server.Services.Unit
{
    public class UnitServices : IUnitServices
    {
        private readonly IUnitInfoRepository UnitInfoRepository;

        ResponseOBJ<UnitsDTO> Response;
        Func<string, UnitsDTO, int, ResponseModel<UnitsDTO>> FuncResponseOBJ;
        Func<string, List<UnitsDTO>, int, ResponseModel<UnitsDTO>> FuncResponseList;

        public UnitServices(IUnitInfoRepository _unitinforepository)
        {
            this.UnitInfoRepository = _unitinforepository;

            Response = new ResponseOBJ<UnitsDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

        /// <summary>
        /// 단위 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UnitsDTO>> GetAllUnitList()
        {
            throw new NotImplementedException();
        }

        public ValueTask<ResponseModel<UnitsDTO>> AddUnitInfo(UnitsDTO dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ResponseModel<UnitsDTO>> DeleteUnitInfo(UnitsDTO dto)
        {
            throw new NotImplementedException();
        }

  

        public ValueTask<ResponseModel<UnitsDTO>> UpdateUnitInfo(UnitsDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
