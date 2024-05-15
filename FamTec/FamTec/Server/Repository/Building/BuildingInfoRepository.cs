using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Abstractions;

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
        /// 건물 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<BuildingsTb> AddAsync(BuildingsTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.BuildingsTbs.Add(model);
                    await context.SaveChangesAsync();
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 건물 전체조회
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<List<BuildingsTb>> GetAllList()
        {
            try
            {
                return await context.BuildingsTbs.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 해당사업장 코드에 해당하는 모든 건물 출력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<List<BuildingsTb>> GetBuildingList(string placecode)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(placecode))
                {
                    List<BuildingsTb>? result = await context.BuildingsTbs.Where(m => m.PlacecodeCd == placecode).ToListAsync();
                    if (result is [_, ..])
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 해당 건물코드에 해당하는 건물 출력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<BuildingsTb> GetBuildingInfo(string buildingcode)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(buildingcode))
                {
                    BuildingsTb? result = await context.BuildingsTbs.FirstOrDefaultAsync(m => m.BuildingCd == buildingcode);
                    if (result is not null)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool> EditBuildingInfo(BuildingsTb model)
        {
            try
            {
                if(model is not null)
                {
                    context.BuildingsTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }


        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteBuildingInfo(BuildingsTb model)
        {
            try
            {
                if (model is not null)
                {
                    context.BuildingsTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }
    }
}
