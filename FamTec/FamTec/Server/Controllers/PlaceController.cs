using FamTec.Server.Repository.User;
using FamTec.Server.Services.Place;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private IPlaceService PlaceService;

        public PlaceController(IPlaceService _placeservice)
        {
            this.PlaceService = _placeservice;
        }

   


    }
}
