using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Alarm
{
    public class AlarmInfoRepository : IAlarmInfoRepository
    {
        private readonly FmsContext context;

        public AlarmInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 알람 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AlarmsTb> AddAsync(AlarmsTb model, string userid)
        {
            try
            {
                model.CreateUser = userid;
                model.CreateDt = DateTime.Now;

                context.AlarmsTbs.Add(model);

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
        /// 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<AlarmsTb>> GetAllAsync()
        {
            try
            {
                return await context.AlarmsTbs.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// VOC 아이디 조회 (리스트반환)
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        public async ValueTask<List<AlarmsTb>> GetByVocIDListAsync(int vocid)
        {
            try
            {
                List<AlarmsTb>? model = await context.AlarmsTbs.Where(m => m.VocId == vocid).ToListAsync();
                if (model is [_, ..])
                {
                    return model;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// VOC 아이디로 조회 (단일모델 반환)
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        public async ValueTask<AlarmsTb> GetByVocIDAsync(int vocid)
        {
            try
            {
                AlarmsTb? model = await context.AlarmsTbs.FirstOrDefaultAsync(m => m.VocId == vocid);
                if (model is not null)
                {
                    return model;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// VOC 아이디로 알람 단일 삭제
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteVocIDAsync(int vocid, string userid)
        {
            try
            {
                AlarmsTb? model = await context.AlarmsTbs.FirstOrDefaultAsync(m => m.VocId == vocid);
                if (model is not null)
                {
                    model.DelDt = DateTime.Now;
                    model.DelYn = true;
                    model.DelUser = userid;

                    context.AlarmsTbs.Update(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<bool> EditAsync(AlarmsTb model, string userid)
        {
            try
            {
                if (model is not null)
                {
                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = userid;

                    context.AlarmsTbs.Update(model);

                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }




    }
}
