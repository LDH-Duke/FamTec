using FamTec.Server.Services;
using FamTec.Server.Services.Admin.Place;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers.Admin.AdminPlaces
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPlaceController : ControllerBase
    {
        private IAdminPlaceService AdminPlaceService;
        private ILogService LogService;

        public AdminPlaceController(IAdminPlaceService _adminplaceservice, ILogService _logservice)
        {
            this.AdminPlaceService = _adminplaceservice;
            this.LogService = _logservice;
        }
        
        /// <summary>
        /// 전체 사업장 리스트 조회 [수정완료]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllWorksList")]
        public async ValueTask<IActionResult> GetAllPlaceList()
        {
            try
            {
                ResponseList<AllPlaceDTO>? model = await AdminPlaceService.GetAllWorksService();

                if (model is not null)
                {
                    if (model.code == 200)
                    {
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest(model);
                    }
                }
                else
                {
                    return BadRequest(model);
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 매니저리스트 전체 반환 [수정완료]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllManagerList")]
        public async ValueTask<IActionResult> GetAllManagerList()
        {
            try
            {
                ResponseList<ManagerListDTO>? model = await AdminPlaceService.GetAllManagerListService();

                if (model is not null)
                {
                    if (model.code == 200)
                    {
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest(model);
                    }
                }
                else
                {
                    return BadRequest(model);
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 선택된 매니저가 관리하는 사업장 LIST반환 * (확인함)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MyWorks/{id?}")]
        public async ValueTask<IActionResult> GetMyWorks(int id)
        {
            ResponseList<AdminPlaceDTO> model = await AdminPlaceService.GetMyWorksService(id);
            return Ok(model);
        }

        /// <summary>
        /// 해당사업장을 관리하는 관리자 LIST 반환 * [수정완료]
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DetailWorks")]
        public async ValueTask<IActionResult> DetailWorks([FromQuery]int placeid)
        {
            try
            {
                ResponseUnit<PlaceDetailDTO>? res = await AdminPlaceService.GetPlaceService(placeid);

                if (res is not null)
                {
                    if (res.code == 200)
                    {
                        return Ok(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 사업장 생성시 관리자할당 [수정완료]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddWorks")]
        public async ValueTask<IActionResult> AddWorks([FromBody]AddPlaceDTO dto)
        {
            try
            {
                ResponseUnit<int?> model = await AdminPlaceService.AddPlaceService(dto);

                if (model is not null)
                {
                    if (model.code == 200)
                    {
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest(model);
                    }
                }
                else
                {
                    return BadRequest(model);
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 사압정에 매니저 추가 [수정완료]
        /// </summary>
        /// <param name="placemanager"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPlaceManager")]
        public async ValueTask<IActionResult> AddPlaceManager([FromBody]AddPlaceManagerDTO<ManagerListDTO> placemanager)
        {
            try
            {
                ResponseUnit<bool> model = await AdminPlaceService.AddPlaceManagerService(placemanager);

                if (model is not null)
                {
                    if (model.code == 200)
                    {
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest(model);
                    }
                }
                else
                {
                    return BadRequest(model);
                }
            }catch(Exception ex)
            {
                LogService.LogMessage(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 사업장 삭제 [수정완료]
        /// </summary>
        /// <param name="DeletePlace"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DeleteWorks")]
        public async ValueTask<IActionResult> DeleteWorks([FromBody]List<int> DeletePlace)
        {
            try
            {
                ResponseUnit<bool> model = await AdminPlaceService.DeleteManagerPlaceService(DeletePlace);

                if (model is not null)
                {
                    if (model.code == 200)
                    {
                        return Ok(model);
                    }
                    else if (model.code == 401)
                    {
                        return Ok(model);
                    }
                    else
                    {
                        return BadRequest(model);
                    }
                }
                else
                {
                    return BadRequest(model);
                }
            }catch(Exception ex)
            {
                LogService.LogMessage(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
