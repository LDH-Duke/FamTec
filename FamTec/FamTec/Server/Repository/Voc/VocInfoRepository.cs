using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Repository.Voc
{
    public class VocInfoRepository : IVocInfoRepository
    {
        private readonly FmsContext context;

        public VocInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// VOC 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// VOC 전체 리스트 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<VocTb>?> GetAllList()
        {
            try
            {
                List<VocTb>? model = await context.VocTbs.Where(m => m.DelYn != true).ToListAsync();
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
        /// 빌딩코드에 해당하는 VOC 전체 리스트 조회
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <returns></returns>
        public async ValueTask<List<VocTb>?> GetVocList(string? buildingcd)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(buildingcd))
                {
                    List<VocTb>? model = await context.VocTbs.Where(m => m.BuildingCd == buildingcd && m.DelYn != true).ToListAsync();

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
                Console.WriteLine(ex);
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// VOC ID에 해당하는 VOC정보 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async ValueTask<VocTb?> GetVocInfo(int? id)
        {
            try
            {
                if(id is not null)
                {
                    VocTb? model = await context.VocTbs.FirstOrDefaultAsync(m => m.Id == id && m.DelYn != true);
                    
                    if (model == null)
                        return null;
                    else
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
        /// VOC 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteVocInfo(VocTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.VocTbs.Update(model);
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
        /// VOC 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditVocInfo(VocTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.VocTbs.Update(model);
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
