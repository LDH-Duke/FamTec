using FamTec.Server.Databases;
using FamTec.Server.Repository.Interfaces;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly FmsContext context;

        public UserInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }


        public async ValueTask<UsersTb> AddAsync(UsersTb model)
        {
            try
            {
                context.UsersTbs.Add(model);
                await context.SaveChangesAsync();
                return model;
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async ValueTask<UsersTb> GetByUserId(string userid)
        {
            try
            {
                return await context.UsersTbs.FirstOrDefaultAsync(m => m.UserId == userid);
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
