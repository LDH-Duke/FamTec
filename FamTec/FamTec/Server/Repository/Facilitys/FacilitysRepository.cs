using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Facilitys
{
    public class FacilitysRepository : IFacilitysRepository
    {
        private readonly FmsContext context;

        public FacilitysRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 설비추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<FacilitysTb?> AddAsync(FacilitysTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.FacilitysTbs.Add(model);
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
        /// 설비리스트 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<FacilitysTb>?> GetAllList()
        {
            try
            {
                List<FacilitysTb>? model = await context.FacilitysTbs.Where(m => m.DelYn != true).ToListAsync();
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
        /// 설비ID에 해당하는 설비모델정보 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<FacilitysTb?> GetFacilitysInfo(int? id)
        {
            try
            {
                if(id != null)
                {
                    FacilitysTb? model = await context.FacilitysTbs.FirstOrDefaultAsync(m => m.Id == id && m.DelYn != true);
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
        /// 공간테이블 ID에 해당하는 설비 리스트 조회
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<List<FacilitysTb>?> GetFacilitysList(int? roomid)
        {
            try
            {
                if(roomid != null)
                {
                    List<FacilitysTb>? model = await context.FacilitysTbs.Where(m => m.RoomsId == roomid && m.DelYn != true).ToListAsync();
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
        /// 설비정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditFacilitysInfo(FacilitysTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.FacilitysTbs.Update(model);
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
        /// 설비정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteFacilitysInfo(FacilitysTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.FacilitysTbs.Update(model);
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
