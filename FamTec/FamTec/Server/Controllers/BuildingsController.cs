using AutoMapper;
using FamTec.Server.Repository;
using FamTec.Server.Repository.Interfaces;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingInfoRepository BuildingInfoRepository;

        public BuildingsController(IBuildingInfoRepository _buildinginforepository)
        {
            BuildingInfoRepository = _buildinginforepository;
        }

        [HttpGet]
        [Route("SelectBuilding/{code}")]
        public async ValueTask<List<BuildingsDTO>> GetBuildingAll(string code)
        {
            List<BuildingsTb> result = await BuildingInfoRepository.GetByPlaceCDAsync(code);

            // 또는 AutoMapper
            List<BuildingsDTO> dto = result.Select(e => new BuildingsDTO()
            {
                IndexChk = false,
                BuildingCd = e.BuildingCd,
                Name = e.Name,
                Address = e.Address,
                FloorNum = e.FloorNum,
                CompletionDate = e.CompletionDate,
                CreateDt = e.CreateDt
            }).ToList();

            return dto;
        }
    }
}

