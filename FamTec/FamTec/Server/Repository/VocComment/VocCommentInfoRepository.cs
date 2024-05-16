using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FamTec.Server.Repository.VocComment
{
    public class VocCommentInfoRepository : IVocCommentInfoRepository
    {
        private readonly FmsContext context;

        public VocCommentInfoRepository(FmsContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// VOC COMMENT 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<VocCommentsTb?> AddAsync(VocCommentsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.VocCommentsTbs.Add(model);
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
        /// VOC COMMENT 리스트 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<List<VocCommentsTb>?> GetAllList()
        {
            try
            {
                List<VocCommentsTb>? model = await context.VocCommentsTbs.Where(m => m.DelYn != true).ToListAsync();
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
        /// VOC COMMENT 리스트 VOCID로 조회
        /// </summary>
        /// <param name="vocid"></param>
        /// <returns></returns>
        public async ValueTask<List<VocCommentsTb>?> GetVocIDList(int? vocid)
        {
            try
            {
                if(vocid is not null)
                {
                    List<VocCommentsTb>? model = await context.VocCommentsTbs.Where(m => m.VocId == vocid && m.DelYn != true).ToListAsync();
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
        /// VOC COMMENT 리스트 USERID로 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
     
        public async ValueTask<List<VocCommentsTb>?> GetVocUserIDList(string? userid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid))
                {
                    List<VocCommentsTb>? model = await context.VocCommentsTbs.Where(m => m.UsersUserid == userid && m.DelYn != true).ToListAsync();
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
        /// VOC COMMENT 정보 ID로 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<VocCommentsTb?> GetVocInfo(int? id)
        {
            try
            {
                if (id is not null)
                {
                    VocCommentsTb? model = await context.VocCommentsTbs.FirstOrDefaultAsync(m => m.Id == id && m.DelYn != true);
                    
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
        /// VOC COMMENT 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> EditVocCommentInfo(VocCommentsTb? model)
        {
            try
            {
                if(model is not null)
                {
                    context.VocCommentsTbs.Update(model);
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
        /// VOC COMMENT 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteVocCommentInfo(VocCommentsTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.VocCommentsTbs.Update(model);
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
