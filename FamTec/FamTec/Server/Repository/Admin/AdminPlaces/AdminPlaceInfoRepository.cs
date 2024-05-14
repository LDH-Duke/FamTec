using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Admin.AdminPlaces
{
    public class AdminPlaceInfoRepository : IAdminPlacesInfoRepository
    {
        private readonly FmsContext context;

        public AdminPlaceInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminPlacesTb> AddAsync(AdminPlacesTb model)
        {
            try
            {
                context.AdminPlacesTbs.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async ValueTask<List<AdminPlacesTb>> GetPlaceAllAsync(string placecd)
        {
            List<AdminPlacesTb>? model = await context.AdminPlacesTbs.Where(m => m.PlacecodeCd == placecd).ToListAsync();
            
            if (model is [_, ..])
            {
                return model;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async ValueTask<List<AdminPlacesTb>> GetUserAllAsync(string userid)
        {
            List<AdminPlacesTb>? model = await context.AdminPlacesTbs.Where(m => m.UsersUserid == userid).ToListAsync();

            if(model is [_, ..])
            {
                return model;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async ValueTask<bool> DeleteAdminPlacesAsync(AdminPlacesTb model)
        {
            if(model is not null)
            {
                context.AdminPlacesTbs.Update(model);
                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async ValueTask<bool> EditAdminPlacesAsync(AdminPlacesTb model)
        {
            if (model is not null)
            {
                context.AdminPlacesTbs.Update(model);
                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

 
    }
}
