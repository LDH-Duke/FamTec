using FamTec.Server.Services.Unit;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.Unit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private IUnitService UnitService;
        private SessionInfo sessioninfo;

        public UnitController(IUnitService _unitservice)
        {
            this.UnitService = _unitservice;

            this.sessioninfo = new SessionInfo();
        }

        [HttpGet]
        [Route("SelectUnitList")]
        public async ValueTask<IActionResult> GetUnitList()
        {
            ResponseModel<UnitsDTO>? model = await UnitService.GetUnitList(sessioninfo);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddUnitInfo")]
        public async ValueTask<IActionResult> AddUnitInfo([FromBody]UnitsDTO dto)
        {
            ResponseModel<UnitsDTO>? model = await UnitService.AddUnitService(dto, sessioninfo);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteUnitInfo")]
        public async ValueTask<IActionResult> DeleteUnitInfo([FromBody]UnitsDTO dto)
        {
            ResponseModel<string>? model = await UnitService.DeleteUnitService(dto, sessioninfo);
            return Ok(model);
        }



    }
}
