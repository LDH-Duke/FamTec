using FamTec.Server.Repository;
using FamTec.Server.Repository.Interfaces;
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
        public async ValueTask<List<BuildingsTb>> GetBuildingAll(string code)
        {
            var result = await BuildingInfoRepository.GetByPlaceCDAsync(code);
            return result;
        }
    }
}

