using FamTec.Server.Databases;
using FamTec.Server.Repository.Interfaces;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Repository
{
    public class PlaceInfoRepository : IPlaceInfoRepository
    {
        private readonly FmsContext context;

        public PlaceInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<PlacesTb> AddAsync(PlacesTb model)
        {
            try
            {
                context.PlacesTbs.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<PlacesTb>> GetAllAsync()
        {
            try
            {
                return await context.PlacesTbs.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// USERID로 사업장 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<List<PlacesTb>> GetUsersPlaceCode(string userid)
        {
            //List<PlacesTb>? model = await context.PlacesTbs.Where(m => m.User)
            return null;
        }
    }
}
