using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.EnergyUsages
{
    public class EnergyUsageInfoRepository : IEnergyUsageInfoRepository
    {
        private readonly FmsContext context;

        public EnergyUsageInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 에너지사용량 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<EnergyUsagesTb?> AddAsync(EnergyUsagesTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.EnergyUsagesTbs.Add(model);
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
        /// 전체 에너지사용량 리스트 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<EnergyUsagesTb>?> GetAllList()
        {
            try
            {
                List<EnergyUsagesTb>? model = await context.EnergyUsagesTbs.Where(m => m.DelYn != true).ToListAsync();
                
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
        /// ID에 해당하는 에너지사용량 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<EnergyUsagesTb?> GetUsagesInfo(int? id)
        {
            try
            {
                if (id is not null)
                {
                    EnergyUsagesTb? model = await context.EnergyUsagesTbs.FirstOrDefaultAsync(m => m.Id == id && m.DelYn != true);
                    
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
        /// 에너지사용량 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditUsagesInfo(EnergyUsagesTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.EnergyUsagesTbs.Add(model);
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
        /// 에너지사용량 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteUsagesInfo(EnergyUsagesTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.EnergyUsagesTbs.Add(model);
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
