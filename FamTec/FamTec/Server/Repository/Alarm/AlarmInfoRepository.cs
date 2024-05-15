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

      

    }
}
