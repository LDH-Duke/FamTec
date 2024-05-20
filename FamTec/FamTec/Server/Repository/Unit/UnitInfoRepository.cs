using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Unit
{
    public class UnitInfoRepository : IUnitInfoRepository
    {
        private readonly WorksContext context;

        public UnitInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 단위 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<UnitTb?> AddAsync(UnitTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.UnitTbs.Add(model);
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
        /// 단위 전체 출력
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<UnitTb>?> GetAllList()
        {
            try
            {
                List<UnitTb>? model = await context.UnitTbs
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
        /// 해당 사업장코드에 해당하는 모든 단위 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<List<UnitTb>?> GetUnitList(int? placetbid)
        {
            try
            {
                if (placetbid is not null)
                {
                    List<UnitTb>? model = await context.UnitTbs
                        .Where(m => 
                        m.PlaceTbId.Equals(placetbid) && 
                        m.DelYn != 1).ToListAsync();
                    
                    if(model is [_, ..])
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
        /// 단위 인덱스에 해당하는 모델 조회
        /// </summary>
        /// <param name="unitidx"></param>
        /// <returns></returns>
        public async ValueTask<UnitTb?> GetUnitInfo(int? unitidx, int? placetbid)
        {
            if(unitidx is not null)
            {
                UnitTb? model = await context.UnitTbs
                    .FirstOrDefaultAsync(m => 
                    m.Id.Equals(unitidx) && 
                    m.PlaceTbId.Equals(placetbid) && 
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

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditUnitInfo(UnitTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.UnitTbs.Update(model);
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
        public async ValueTask<bool?> DeleteUnitInfo(UnitTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.UnitTbs.Update(model);
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
