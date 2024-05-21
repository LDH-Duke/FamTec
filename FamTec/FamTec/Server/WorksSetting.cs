using FamTec.Server.Databases;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server
{
    public class WorksSetting : DbContext
    {
        private readonly WorksContext context;

        enum LevelCode : ushort
        {
            시스템관리자 = 100,
            마스터 = 200,
            매니저 = 300,
        }

        public WorksSetting()
        {
            context = new WorksContext();
        }

        public async ValueTask DefaultSetting()
        {
            DepartmentTb? department = new DepartmentTb();
            department.Name = "에스텍시스템";
            department.CreateDt = DateTime.Now;
            department.CreateUser = LevelCode.시스템관리자.ToString();
            department.UpdateDt = DateTime.Now;
            department.UpdateUser = LevelCode.시스템관리자.ToString();
            department.DelYn = 0;

            DepartmentTb? selectDepartment = await context.DepartmentTbs
                .FirstOrDefaultAsync(m =>
                m.Name!.Equals("에스텍시스템") && 
                m.DelYn != 1);

            if(selectDepartment is null)
            {
                context.DepartmentTbs.Add(department);
                await context.SaveChangesAsync();
            }
            else
            {
                if (department.Name != selectDepartment.Name)
                {
                    selectDepartment.Name = department.Name;
                    selectDepartment.UpdateDt = DateTime.Now;
                    selectDepartment.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if (department.DelYn != selectDepartment.DelYn)
                {
                    selectDepartment.DelYn = department.DelYn;
                    selectDepartment.UpdateDt = DateTime.Now;
                    selectDepartment.UpdateUser = LevelCode.시스템관리자.ToString();
                }

                context.DepartmentTbs.Update(selectDepartment);
                await context.SaveChangesAsync();

                selectDepartment = await context.DepartmentTbs.FirstOrDefaultAsync(m => m.Name!.Equals("에스텍시스템") && m.DelYn != 1);
            }

            UserTb? user = new UserTb();
            user.UserId = "Admin";
            user.Password = "stecdev1234!";
            user.Name = "시스템개발파트";
            user.Email = "stecdev@s-tec.co.kr";
            user.Phone = "010-0000-0000";
            user.PermBuilding = 2; // 수정권한
            user.PermEquipment = 2; // 수정권한
            user.PermMaterial = 2; // 수정권한
            user.PermEnergy = 2; // 수정권한
            user.PermOffice = 2;
            user.PermComp = 2;
            user.PermConst = 2;
            user.PermClaim = 2;
            user.PermSys = 2;
            user.PermEmployee = 2;
            user.PermLawCk = 2;
            user.PermLawEdu = 2;
            user.AdminYn = 1;
            user.AlramYn = 1;
            user.Status = 1; // 0 : 퇴직 / 1 : 재직
            user.CreateDt = DateTime.Now;
            user.CreateUser = LevelCode.시스템관리자.ToString();
            user.UpdateDt = DateTime.Now;
            user.UpdateUser = LevelCode.시스템관리자.ToString();
            user.DelYn = 0;
            

            
            UserTb? selectUser = await context.UserTbs.FirstOrDefaultAsync(m => m.UserId!.Equals(user.UserId) && m.Password!.Equals(user.Password));
            if(selectUser is null)
            {
                
                context.UserTbs.Add(user);
                await context.SaveChangesAsync();
                
                selectUser = await context.UserTbs.FirstOrDefaultAsync(m => m.UserId!.Equals(user.UserId) && m.Password!.Equals(user.Password));
            }
            else
            {
                if (user.UserId != selectUser.UserId)
                {
                    selectUser.UserId = user.UserId;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if (user.Name != selectUser.Name)
                {
                    selectUser.Name = user.Name;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.Email != selectUser.Email)
                {
                    selectUser.Email = user.Email;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.Phone != selectUser.Phone)
                {
                    selectUser.Phone = user.Phone;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermBuilding != selectUser.PermBuilding)
                {
                    selectUser.PermBuilding = user.PermBuilding;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermEquipment != selectUser.PermEquipment)
                {
                    selectUser.PermEquipment = user.PermEquipment;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermMaterial != selectUser.PermMaterial)
                {
                    selectUser.PermMaterial = user.PermMaterial;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermEnergy != selectUser.PermEnergy)
                {
                    selectUser.PermEnergy = user.PermEnergy;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermOffice != selectUser.PermOffice)
                {
                    selectUser.PermOffice = user.PermOffice;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermComp != selectUser.PermComp)
                {
                    selectUser.PermComp = user.PermComp;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermConst != selectUser.PermConst)
                {
                    selectUser.PermConst = user.PermConst;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermClaim != selectUser.PermClaim)
                {
                    selectUser.PermClaim = user.PermClaim;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermSys != selectUser.PermSys)
                {
                    selectUser.PermSys = user.PermSys;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermEmployee != selectUser.PermEmployee)
                {
                    selectUser.PermEmployee = user.PermEmployee;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermLawCk != selectUser.PermLawCk)
                {
                    selectUser.PermLawCk = user.PermLawCk;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.PermLawEdu != selectUser.PermLawEdu)
                {
                    selectUser.PermLawEdu = user.PermLawEdu;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.AdminYn != selectUser.AdminYn)
                {
                    selectUser.AdminYn = user.AdminYn;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.AlramYn != selectUser.AlramYn)
                {
                    selectUser.AlramYn = user.AlramYn;
                    selectUser.UpdateDt = DateTime.Now;
                    selectUser.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(user.DelYn != selectUser.DelYn)
                {
                    selectUser.DelYn = user.DelYn;

                }

                context.UserTbs.Update(selectUser);
                await context.SaveChangesAsync();

                selectUser = await context.UserTbs.FirstOrDefaultAsync(m => m.UserId!.Equals(user.UserId) && m.Password!.Equals(user.Password));
            }

            AdminTb? admin = new AdminTb();
            admin.Type = LevelCode.시스템관리자.ToString();
            admin.CreateDt = DateTime.Now;
            admin.CreateUser = LevelCode.시스템관리자.ToString();
            admin.UpdateDt = DateTime.Now;
            admin.UpdateUser = LevelCode.시스템관리자.ToString();
            admin.UserTbId = selectUser!.Id;
            admin.DepartmentTbId = selectDepartment!.Id;

            AdminTb? selectAdmin = await context.AdminTbs.FirstOrDefaultAsync(m => m.UserTbId.Equals(selectUser.Id));
            
            if(selectAdmin is null)
            {
                context.AdminTbs.Add(admin);
                await context.SaveChangesAsync();
            }
            else
            {
                if (selectAdmin.Type != admin.Type)
                {
                    selectAdmin.Type = admin.Type;
                    selectAdmin.UpdateDt = DateTime.Now;
                    selectAdmin.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(selectAdmin.UserTbId != selectUser.Id)
                {
                    selectAdmin.UserTbId = selectUser.Id;
                    selectAdmin.UpdateDt = DateTime.Now;
                    selectAdmin.UpdateUser = LevelCode.시스템관리자.ToString();
                }
                if(selectAdmin.DepartmentTbId != selectDepartment.Id)
                {
                    selectAdmin.DepartmentTbId = selectDepartment.Id;
                    selectAdmin.UpdateDt = DateTime.Now;
                    selectAdmin.UpdateUser = LevelCode.시스템관리자.ToString();
                }

                context.UserTbs.Update(selectUser);
                await context.SaveChangesAsync();

            }
        }
    }
}
