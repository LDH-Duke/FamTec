using FamTec.Server.Repository.Place;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceInfoRepository PlaceInfoRepository;
        

        public PlacesController(IPlaceInfoRepository _placeinforepository)
        {
            PlaceInfoRepository = _placeinforepository;
        }

      
     

    }
}
