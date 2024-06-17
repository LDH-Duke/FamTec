using FamTec.Server.Databases;
using FamTec.Server.Services;
using FamTec.Shared.Model;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Repository.Voc
{
    public class VocInfoRepository : IVocInfoRpeository
    {
        private readonly WorksContext context;
        private ILogService LogService;

        public VocInfoRepository(WorksContext _context, ILogService _logservice)
        {
            this.context = _context;
            this.LogService = _logservice;
        }


        public async ValueTask<VocTb?> AddAsync(VocTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.VocTbs.Add(model);
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
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }
    }
}
