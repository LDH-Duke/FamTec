using FamTec.Server.Databases;
using FamTec.Server.Repository.Interfaces;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository
{
    public class BuildingInfoRepository : IBuildingInfoRepository
    {
        private readonly FmsContext context;

        public BuildingInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<BuildingsTb> AddAsync(BuildingsTb model)
        {
            try
            {
                context.BuildingsTbs.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 사업장코드로 조회
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<List<BuildingsTb>> GetByPlaceCDAsync(string placecd)
        {
            List<BuildingsTb>? model = await context.BuildingsTbs.Where((m => m.PlacecodeCd == placecd)).ToListAsync();
            if(model is not null)
            {
                return model;
            }
            else
            {
                return null;
            }
            
        }
    }
}
