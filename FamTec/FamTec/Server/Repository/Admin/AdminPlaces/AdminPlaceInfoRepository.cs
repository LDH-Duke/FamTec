using FamTec.Server.Databases;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;
using FamTec.Shared.Server.DTO.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FamTec.Server.Repository.Admin.AdminPlaces
{
    public class AdminPlaceInfoRepository : IAdminPlacesInfoRepository
    {
        private readonly WorksContext context;

        public AdminPlaceInfoRepository(WorksContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 관리자 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<AdminPlaceTb?> AddAsync(AdminPlaceTb? model)
        {
            try
            {
                if (model is not null)
                {
                    context.AdminPlaceTbs.Add(model);
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

        /// <summary>
        /// 해당 사업장 삭제
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        public async ValueTask<bool?> DeleteMyWorks(List<int>? placeid, string delName)
        {
            try
            {
                if (placeid is [_, ..])
                {
                    for (int i = 0; i < placeid.Count; i++)
                    {
                        UserTb? usertb = await context.UserTbs.FirstOrDefaultAsync(m => m.DelYn != 1 && m.PlaceTbId == placeid[i]);
                        if (usertb is not null)
                        {
                            return false;
                        }
                        else
                        {
                            AdminPlaceTb? adminplacetb = await context.AdminPlaceTbs.FirstOrDefaultAsync(m => m.DelYn != 1 && m.PlaceId == placeid[i]);

                            if (adminplacetb is not null)
                            {
                                return false;
                            }
                            else
                            {
                                PlaceTb? placetb = await context.PlaceTbs.FirstOrDefaultAsync(m => m.DelYn != 1 && m.Id == placeid[i]);
                                if (placetb is not null)
                                {
                                    placetb.DelYn = 1;
                                    placetb.DelDt = DateTime.Now;
                                    placetb.DelUser = delName;

                                    context.PlaceTbs.Update(placetb);
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

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
                throw;
            }
        }



        /// <summary>
        /// 관리자에 해당하는 사업장 리스트 출력
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public async ValueTask<List<AdminPlaceDTO>?> GetMyWorks(int? adminid)
        {
            try
            {
                if(adminid is not null)
                {
                    List<AdminPlaceTb>? adminplacetb = await context.AdminPlaceTbs.Where(m => m.AdminTbId == adminid && m.DelYn != 1).ToListAsync();

                    if(adminplacetb is [_, ..])
                    {
                        List<PlaceTb>? placetb = await context.PlaceTbs.ToListAsync();
                        if(placetb is [_, ..])
                        {

                            List<AdminPlaceDTO>? result = (from admin in adminplacetb
                                                   join place in placetb
                                                   on admin.PlaceId equals place.Id
                                                   where place.DelYn != 1
                                                   select new AdminPlaceDTO
                                                   {
                                                      AdminPlaceTBID = admin.Id,
                                                      AdminPlaceUserTBID = admin.AdminTbId,
                                                      PlaceTBID = admin.PlaceId,
                                                      PlaceCD = place.PlaceCd,
                                                      Name = place.Name,
                                                      ContractNum = place.ContractNum,
                                                      ContractDT= place.ContractDt,
                                                      CancelDT = place.CancelDt,
                                                      status = place.Status
                                                   }).ToList();
                            if(result is [_, ..])
                            {
                                return result;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
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

        /// <summary>
        /// 사업장번호로 사업장 상세정보조회
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        public async ValueTask<AddPlaceDTO?> GetWorksInfo(int? placeid)
        {
            try
            {
                if (placeid is not null)
                {
                    PlaceTb? place = await context.PlaceTbs.FirstOrDefaultAsync(m => m.Id == placeid && m.DelYn != 1);

                    if (place is not null)
                    {

                        List<ManagerListDTO>? ManagerDTO = (from admintb in context.AdminTbs.ToList()
                                                            join adminplacetb in context.AdminPlaceTbs.Where(m => m.PlaceId == placeid).ToList()
                                                            on admintb.Id equals adminplacetb.AdminTbId
                                                            join usertb in context.UserTbs.ToList()
                                                            on admintb.UserTbId equals usertb.Id
                                                            join departmenttb in context.DepartmentTbs.ToList()
                                                            on admintb.DepartmentTbId equals departmenttb.Id
                                                            where (admintb.DelYn != 1 && adminplacetb.DelYn != 1)
                                                            select new ManagerListDTO
                                                            {
                                                                UserId = usertb.Id,
                                                                UserName = usertb.Name,
                                                                AdminID = admintb.Id,
                                                                Type = admintb.Type,
                                                                Tel = usertb.Phone,
                                                                DepartmentIdx = admintb.DepartmentTbId,
                                                                DepartmentName = departmenttb.Name
                                                            }).ToList();

                        if (ManagerDTO is not null)
                        {

                            AddPlaceDTO? dto = new AddPlaceDTO();
                            dto.ID = place.Id;
                            dto.PlaceCd = place.PlaceCd;
                            dto.ContractNum = place.ContractNum;
                            dto.Name = place.Name;
                            dto.Note = place.Note;
                            dto.Address = place.Address;
                            dto.ContractDT = place.ContractDt;
                            dto.PermMachine = place.PermMachine;
                            dto.PermLift = place.PermLift;
                            dto.PermFire = place.PermFire;
                            dto.PermConstruct = place.PermConstruct;
                            dto.PermNetwork = place.PermNetwrok;
                            dto.PermBeauty = place.PermBeauty;
                            dto.PermSecurity = place.PermSecurity;
                            dto.PermMaterial = place.PermMaterial;
                            dto.PermEnergy = place.PermEnergy;
                            dto.CancelDT = place.CancelDt;
                            dto.Status = place.Status;

                            for (int i = 0; i < ManagerDTO.Count(); i++)
                            {
                                dto.AdminList.Add(new ManagerListDTO
                                {
                                    UserId = ManagerDTO[i].UserId,
                                    UserName = ManagerDTO[i].UserName,
                                    AdminID = ManagerDTO[i].AdminID,
                                    Type = ManagerDTO[i].Type,
                                    Tel = ManagerDTO[i].Tel,
                                    DepartmentIdx = ManagerDTO[i].DepartmentIdx,
                                    DepartmentName = ManagerDTO[i].DepartmentName
                                });
                            }

                            return dto;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
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
