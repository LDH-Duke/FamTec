using FamTec.Server.Databases;
using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Material
{
    public class MaterialInfoRepository : IMaterialInfoRepository
    {
        private readonly WorksContext context;

        public MaterialInfoRepository(WorksContext _context)
        {
            this.context = context;
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
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
