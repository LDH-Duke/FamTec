using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Abstractions;
using System.Reflection;

namespace FamTec.Server.Repository.Building
{
    public class BuildingInfoRepository : IBuildingInfoRepository
    {
        private readonly WorksContext context;

        public BuildingInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 건물 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<BuildingTb?> AddAsync(BuildingTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.BuildingTbs.Add(model);
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
        public async ValueTask<List<BuildingTb>?> GetAllList()
        {
            try
            {
                List<BuildingTb>? model = await context.BuildingTbs
                    .Where(m => m.DelYn != 1).ToListAsync();

                if (model is [_, ..])
                    return model;
                else
                    return null;
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
        public async ValueTask<List<BuildingTb>?> GetBuildingList(int? placeidx)
        {
            try
            {
                if (placeidx is not null)
                {
                    List<BuildingTb>? result = await context.BuildingTbs
                        .Where(m => 
                        m.PlaceTbId == placeidx &&
                        m.DelYn != 1).ToListAsync();
                    
                    if (result is [_, ..])
                        return result;

                    else
                        return null;
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
        /// 해당사업장의 건물코드에 해당하는 건물 출력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<BuildingTb?> GetBuildingInfo(string? buildingcode, int? placeid)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(buildingcode) && placeid is not null)
                {
                    BuildingTb? model = await context.BuildingTbs
                        .FirstOrDefaultAsync(m =>
                        m.BuildingCd.Equals(buildingcode) &&
                        m.PlaceTbId.Equals(placeid) &&
                        m.DelYn != 1);

                    if (model is not null)
                        return model;
                    
                    else
                        return null;
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
        public async ValueTask<bool?> EditBuildingInfo(BuildingTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.BuildingTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
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
        /// 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteBuildingInfo(BuildingTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.BuildingTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    return null;
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
