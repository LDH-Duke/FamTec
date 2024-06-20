using FamTec.Server.Databases;
using FamTec.Server.Services;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Material
{
    public class MaterialInfoRepository : IMaterialInfoRepository
    {
        private readonly WorksContext context;
        private readonly ILogService LogService;

        public MaterialInfoRepository(WorksContext _context, ILogService _logservice)
        {
            this.context = _context;
            this.LogService = _logservice;
        }

        /// <summary>
        /// 자재 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<MaterialTb?> AddMaterialInfo(MaterialTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.MaterialTbs.Add(model);
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
        
        /// <summary>
        /// 사업장에 속한 자재 리스트 반환
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<List<MaterialTb>?> GetMaterialList(int? placeid)
        {
            try
            {
                if(placeid is not null)
                {
                    List<MaterialTb>? model = await context.MaterialTbs.Where(m => 
                    m.DelYn != 1 && 
                    m.PlaceId == placeid).ToListAsync();

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
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// 자재 ID로 단일 자재모델 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<MaterialTb?> GetMaterialInfo(int? id)
        {
            try
            {
                if(id is not null)
                {
                    MaterialTb? model = await context.MaterialTbs.FirstOrDefaultAsync(m => 
                    m.Id == id && 
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
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                throw new ArgumentNullException();
            }
        }

        
    }
}
