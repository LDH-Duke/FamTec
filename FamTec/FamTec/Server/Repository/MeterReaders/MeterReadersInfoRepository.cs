using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.MeterReaders
{
    public class MeterReadersInfoRepository : IMeterReadersInfoRepository
    {
        private readonly FmsContext context;

        public MeterReadersInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 계량기 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<MeterReadersTb?> AddAsync(MeterReadersTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.MeterReadersTbs.Add(model);
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
        /// 전체 계량기 리스트 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<MeterReadersTb>?> GetAllList()
        {
            try
            {
                List<MeterReadersTb>? model = await context.MeterReadersTbs.Where(m => m.DelYn != true).ToListAsync();

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
        /// 건물코드에 해당하는 전체 계량기 리스트 조회
        /// </summary>
        /// <param name="buildingCD"></param>
        /// <returns></returns>
        public async ValueTask<List<MeterReadersTb>?> GetReaderList(string? buildingCD)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(buildingCD))
                {
                    List<MeterReadersTb>? model = await context.MeterReadersTbs.Where(m => m.BuildingCd == buildingCD && m.DelYn != true).ToListAsync();
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
        /// 계량기 ID에 해당하는 계량기 정보 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<MeterReadersTb?> GetReaderInfo(int? id)
        {
            try
            {
                if(id is not null)
                {
                    MeterReadersTb? model = await context.MeterReadersTbs.FirstOrDefaultAsync(m => m.Id == id && m.DelYn != true);

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
        /// 계량기 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteReaderInfo(MeterReadersTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.MeterReadersTbs.Update(model);
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
        /// 계량기 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditReaderInfo(MeterReadersTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.MeterReadersTbs.Update(model);
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
