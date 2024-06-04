using FamTec.Server.Databases;
using FamTec.Server.Repository.Place;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;
using FamTec.Shared.Server.DTO.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;


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
        public async ValueTask<bool?> AddAsync(List<AdminPlaceTb>? model)
        {
            try
            {
                if (model is [_, ..])
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        context.AdminPlaceTbs.Add(model[i]);
                    }
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
                return null;
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
        /// 관리자에 해당하는 사업장리스트 반환
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public async ValueTask<List<AdminPlaceTb>?> GetMyWorksModel(int? adminid)
        {
            try
            {
                if(adminid is not null)
                {
                    List<AdminPlaceTb>? adminplacetb = await context.AdminPlaceTbs.Where(m => m.AdminTbId == adminid && m.DelYn != 1).ToListAsync();

                    if(adminplacetb is [_, ..])
                        return adminplacetb;
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
        /// 관리자 로그인후 해당관리자의 사업장리스트 반환
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public async ValueTask<List<PlacesDTO>?> GetLoginWorks(int? adminid)
        {
            try
            {
                if(adminid is not null)
                {
                    List<AdminPlaceTb>? adminplacetb = await context.AdminPlaceTbs.Where(m => m.AdminTbId == adminid && m.DelYn != 1).ToListAsync();

                    if(adminplacetb is [_, ..])
                    {

                        List<PlacesDTO> dto = (from adminplc in adminplacetb
                                               join placetb in context.PlaceTbs.Where(m => m.DelYn != 1).ToList()
                                               on adminplc.PlaceId equals placetb.Id
                                               select new PlacesDTO
                                               {
                                                   PlaceIndex = placetb.Id, // 사업장 인덱스
                                                   PlaceCd = placetb.PlaceCd, // 사업장 코드
                                                   CONTRACT_NUM = placetb.ContractNum, // 계약번호
                                                   Name = placetb.Name, // 사업장명
                                                   Note = placetb.Note, // 비고
                                                   Address = placetb.Address, // 주소
                                                   ContractDT = placetb.ContractDt, // 계약일자
                                                   CancelDT = placetb.CancelDt, // 해약일자
                                                   PermMachine = placetb.PermMachine, // 설비메뉴 권한
                                                   PermLift = placetb.PermLift, // 승강메뉴 권한
                                                   PermFire = placetb.PermFire, // 소방메뉴 권한
                                                   PermConstruct = placetb.PermConstruct, // 건축메뉴 권한
                                                   PermNetwork = placetb.PermNetwrok, // 통신메뉴 권한
                                                   PermBeauty = placetb.PermBeauty, // 미화메뉴 권한
                                                   PermSecurity = placetb.PermSecurity, // 보안메뉴 권한
                                                   PermMaterial = placetb.PermMaterial, // 자재메뉴 권한
                                                   PermEnergy = placetb.PermEnergy, // 에너지메뉴 권한
                                                   Status = placetb.Status // 상태
                                               }).ToList();

                        if(dto is [_, ..])
                        {
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


        /// <summary>
        /// 관리자사업장 리스트 모델에 해당하는 사업장 리스트들 반환
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async ValueTask<List<PlaceTb>?> GetMyWorksDetails(List<AdminPlaceTb>? model)
        {
            try
            {
                if(model is [_, ..])
                {
                    List<PlaceTb>? result =  (from adminplacetb in model
                                            join placetb in context.PlaceTbs.Where(m => m.DelYn != 1)
                                            on adminplacetb.PlaceId equals placetb.Id
                                            where adminplacetb.DelYn != 1 && placetb.DelYn != 1
                                            select new PlaceTb
                                            {
                                                Id = placetb.Id,
                                                PlaceCd = placetb.PlaceCd,
                                                ContractNum = placetb.ContractNum,
                                                Name = placetb.Name,
                                                Tel = placetb.Tel,
                                                Note = placetb.Note,
                                                Address = placetb.Address,
                                                ContractDt = placetb.ContractDt,
                                                PermMachine = placetb.PermMachine,
                                                PermLift = placetb.PermLift,
                                                PermFire = placetb.PermFire,
                                                PermConstruct = placetb.PermConstruct,
                                                PermNetwrok = placetb.PermNetwrok,
                                                PermBeauty = placetb.PermBeauty,
                                                PermSecurity = placetb.PermSecurity,
                                                PermMaterial = placetb.PermMaterial,
                                                PermEnergy = placetb.PermEnergy,
                                                PermVoc = placetb.PermVoc,
                                                CancelDt = placetb.CancelDt,
                                                Status = placetb.Status,
                                                CreateDt = placetb.CreateDt,
                                                CreateUser = placetb.CreateUser,
                                                UpdateDt = placetb.UpdateDt,
                                                UpdateUser = placetb.UpdateUser,
                                                DelYn = placetb.DelYn,
                                                DelDt = placetb.DelDt,
                                                DelUser = placetb.DelUser
                                            }).ToList();

                    if (result is [_, ..])
                        return result;
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
                throw;
            }
        }

        /// <summary>
        /// 사업장번호로 사업장 상세정보조회
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        public async ValueTask<PlaceDetailDTO?> GetWorksInfo(int? placeid)
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
                                                                Id = admintb.Id,
                                                                UserId = usertb.UserId,
                                                                Name = usertb.Name,
                                                                Department = departmenttb.Name
                                                            }).ToList();

                        PlaceDetailDTO PlaceDetail = new PlaceDetailDTO
                        {
                            PlaceInfo = new PlaceInfo
                            {
                                Id = place.Id,
                                PlaceCd = place.PlaceCd,
                                Name = place.Name,
                                Tel = place.Tel,
                                ContractNum = place.ContractNum,
                                ContractDt = place.ContractDt,
                                CancelDt = place.CancelDt,
                                Status = place.Status,
                                Note = place.Note
                            },

                            PlacePerm = new PlacePerm
                            {
                                Id = place.Id,
                                PermMachine = place.PermMachine,
                                PermLift = place.PermLift,
                                PermFire = place.PermFire,
                                PermConstruct = place.PermConstruct,
                                PermNetwork = place.PermNetwrok,
                                PermBeauty = place.PermBeauty,
                                PermSecurity = place.PermSecurity,
                                PermMaterial = place.PermMaterial,
                                PermEnergy = place.PermEnergy,
                                PermVoc = place.PermVoc
                            },
                            ManagerList = ManagerDTO
                        };

                        return PlaceDetail;
                        
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
