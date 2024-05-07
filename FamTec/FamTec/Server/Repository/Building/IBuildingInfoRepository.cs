using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Building
{
    public interface IBuildingInfoRepository
    {
        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<BuildingsTb> AddAsync(BuildingsTb model);

        ValueTask<List<BuildingsTb>> GetByPlaceCDAsync(string placecd);


    }
}
