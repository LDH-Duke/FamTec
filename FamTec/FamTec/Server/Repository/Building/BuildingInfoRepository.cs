using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Building
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
        public async ValueTask<BuildingsTb> AddAsync(BuildingsTb model, string userid)
        {
            try
            {
                model.CreateUser = userid;
                model.CreateDt = DateTime.Now;
                
                context.BuildingsTbs.Add(model);

                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<BuildingsTb>> GetAllAsync()
        {
            try
            {
                return await context.BuildingsTbs.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
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
            try
            {
                List<BuildingsTb>? model = await context.BuildingsTbs.Where(m => m.PlacecodeCd == placecd).ToListAsync();
                if (model is [_, ..])
                {
                    return model;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 빌딩 코드로 빌딩정보 단일 삭제
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteBuildingCDAsync(string buildingcd, string userid)
        {
            try
            {
                BuildingsTb? model = await context.BuildingsTbs.FirstOrDefaultAsync(m => m.BuildingCd == buildingcd);
                if (model is not null)
                {
                    model.DelDt = DateTime.Now;
                    model.DelYn = true;
                    model.DelUser = userid;

                    context.BuildingsTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<bool> EditAsync(BuildingsTb model, string userid)
        {
            try
            {
                if(model is not null)
                {
                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = userid;

                    context.BuildingsTbs.Update(model);

                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}
