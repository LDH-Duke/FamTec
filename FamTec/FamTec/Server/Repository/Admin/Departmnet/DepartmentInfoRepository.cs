using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Admin.Departmnet
{
    public class DepartmentInfoRepository : IDepartmentInfoRepository
    {
        private readonly WorksContext context;

        public DepartmentInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }




        /// <summary>
        /// 부서추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<DepartmentTb?> AddAsync(DepartmentTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.DepartmentTbs.Add(model);
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
        /// 부서 전체조회
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<List<DepartmentTb>?> GetAllList()
        {
            try
            {
                List<DepartmentTb>? model = await context.DepartmentTbs.ToListAsync();

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
        /// 부서IDX에 해당하는 단일 모델 반환
        /// </summary>
        /// <param name="departmentidx"></param>
        /// <returns></returns>
        public async ValueTask<DepartmentTb?> GetDepartmentInfo(int? Id)
        {
            try
            {
                if (Id is not null)
                {

                    DepartmentTb? model = await context.DepartmentTbs
                        .FirstOrDefaultAsync(m => m.Id.Equals(Id) &&
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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 부서명에 해당하는 부서 조회
        /// </summary>
        /// <param name="departmentname"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async ValueTask<DepartmentTb?> GetDepartmentInfo(string? Name)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(Name))
                {
                    DepartmentTb? model = await context.DepartmentTbs
                        .FirstOrDefaultAsync(m => m.Name.Equals(Name)
                        && m.DelYn != 1);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }



    }
}
