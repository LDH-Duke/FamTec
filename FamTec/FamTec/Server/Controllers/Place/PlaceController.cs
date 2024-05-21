using FamTec.Server.Controllers.Object;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Controllers.Place
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly WorksContext _workContext;

        

        public PlaceController(WorksContext workContext) 
        {
            _workContext = workContext;
        }

        [HttpGet]
        [Route("allplace")]
        public async Task<IActionResult> FindAllPlace()
        {
            try
            {
                Console.WriteLine("사업장 조회");
                List<PlaceTb> res = await _workContext.PlaceTbs.ToListAsync();
                List<PlaceDTO> placeList = res.Select(placeTb => new PlaceDTO{
                    PlaceCd = placeTb.PlaceCd,
                    Name = placeTb.Name,
                    Note = placeTb.Note,
                    ContractNum = placeTb.ContractNum,
                    ContractDt = placeTb.ContractDt,
                    Status = placeTb.Status,
                }).ToList();

                return Ok(new { message = "사업장 조회 성공", data = placeList, code = 200});
            }catch (Exception ex)
            {
                Console.WriteLine("[Place][Controller] 사업장 조회 에러!!\n "+ ex);
                return Problem("[Place][Controller] 사업장 조회 에러!!\n"+ ex);
            }
            
        }
    }
}
