using AutoMapper;
using FamTec.Server.Repository.Building;
using FamTec.Server.Repository.Place;
using FamTec.Server.Services.Building;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingServices BuildingServices;
        

        public BuildingsController(IBuildingServices _buildingservice)
        {
            BuildingServices = _buildingservice;
        }

        [HttpPost]
        [Route("InsertBuilding")]
        public async ValueTask<IActionResult> AddBuilding([FromBody]BuildingsDTO dto)
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.AddBuildingInfo(dto);
            return Ok(model);
        }

        
        [HttpGet]
        [Route("SelectAllBuilding")]
        public async ValueTask<IActionResult> GetAllBuildingList()
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.GetAllBuildings();
            return Ok(model);
        }


        [HttpGet]
        [Route("GetBdPlace/{placecode?}")]
        public async ValueTask<IActionResult> GetBdPlace(string placecode)
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.GetBuildingList(placecode);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetBdCode/{bdcode?}")]
        public async ValueTask<IActionResult> GetBdInfo(string bdcode)
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.GetBuildingInfo(bdcode);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddBuilding")]
        public async ValueTask<IActionResult> AddBdInfo([FromBody]BuildingsDTO dto)
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.AddBuildingInfo(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("UpdateBuilding")]
        public async ValueTask<IActionResult> UpdateBdInfo([FromBody]BuildingsDTO dto)
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.UpdateBuildingInfo(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteBuilding")]
        public async ValueTask<IActionResult> DeleteBdInfo([FromBody] BuildingsDTO dto)
        {
            ResponseModel<BuildingsDTO>? model = await BuildingServices.DeleteBuildingInfo(dto);
            return Ok(model);
        }

    }
}

