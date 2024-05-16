using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.EnergyUsagesMonth
{
    public class EnergyUsageMonthInfoRepository : IEnergyUsageMonthInfoRepository
    {
        private readonly FmsContext context;

        public EnergyUsageMonthInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 해당년도 검침기록 생성 - 얘는 그냥 Row 공간 만들어주기용
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<EnergyMonthUsageTb?> AddAsync(EnergyMonthUsageTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.EnergyMonthUsageTbs.Add(model);
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
        /// 전체검침리스트 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<EnergyMonthUsageTb>?> GetAllList()
        {
            try
            {
                List<EnergyMonthUsageTb>? model = await context.EnergyMonthUsageTbs.Where(m => m.DelYn != true).ToListAsync();
                
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
        /// 계량기별 전체년도 검침기록 리스트 조회
        /// </summary>
        /// <param name="readerid"></param>
        /// <returns></returns>
        public async ValueTask<List<EnergyMonthUsageTb>?> GetEnergyMonthList(int? readerid)
        {
            try
            {
                if (readerid != null)
                {
                    List<EnergyMonthUsageTb>? model = await context.EnergyMonthUsageTbs.Where(m => m.MeterReaderId == readerid && m.DelYn != true).ToListAsync();

                    if (model is [_, ..])
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
        /// 해당년도 전체 검침리스트 조회
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async ValueTask<List<EnergyMonthUsageTb>?> GetEnergyYearList(int? year)
        {
            try
            {
                if (year != null)
                {
                    List<EnergyMonthUsageTb>? model = await context.EnergyMonthUsageTbs.Where(m => m.Year == year && m.DelYn != true).ToListAsync();

                    if (model is [_, ..])
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
        /// 계량기 해당 년도 검침기록 조회
        /// </summary>
        /// <param name="readerid"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async ValueTask<EnergyMonthUsageTb?> GetEnergyInfo(int? readerid, int? year)
        {
            try
            {
                if(readerid != null && year != null)
                {
                    EnergyMonthUsageTb? model = await context.EnergyMonthUsageTbs.FirstOrDefaultAsync(m => m.MeterReaderId == readerid && m.Year == year && m.DelYn != true);

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
        /// 계량기 검침기록 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditEnergyInfo(EnergyMonthUsageTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.EnergyMonthUsageTbs.Update(model);
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
        /// 계량기 검침기록 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteEnergyInfo(EnergyMonthUsageTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.EnergyMonthUsageTbs.Update(model);
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
