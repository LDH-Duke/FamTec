using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Unit
{
    public class UnitInfoRepository : IUnitInfoRepository
    {
        private readonly FmsContext context;

        public UnitInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 단위 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<UnitTb> AddAsync(UnitTb model)
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
        public async ValueTask<List<UnitTb>> GetAllList()
        {
            try
            {
                return await context.UnitTbs.ToListAsync();
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
        public async ValueTask<List<UnitTb>> GetUnitList(string placecd)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(placecd))
                {
                    List<UnitTb>? model = await context.UnitTbs.Where(m => m.PlacecodeCd == placecd).ToListAsync();
                    if(model is [_, ..])
                    {
                        return model;
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
        public async ValueTask<bool> EditUnitInfo(UnitTb model)
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
        public async ValueTask<bool> DeleteUnitInfo(UnitTb model)
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
