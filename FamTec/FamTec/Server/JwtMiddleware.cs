using FamTec.Server.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FamTec.Server
{
    public class JwtMiddleware
    {
        
        private readonly RequestDelegate Next;
        private ITokenComm TokenComm;
        //private const string APIKEYNAME = "Authorization";
        
        
        public JwtMiddleware(RequestDelegate _next, ITokenComm _tokencomm)
        {
            this.Next = _next;
            this.TokenComm = _tokencomm;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            // API 키 Configuration 수정
            if(!context.Request.Headers.TryGetValue("Authorization", out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyMiddleware)");
                return;
            }

            string? accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(accessToken is null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("jwt token validation failed");
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4");

            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(authSigningKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            
            var jwtToken = (JwtSecurityToken)validatedToken;


            // 토큰분해
            JObject? jobj = TokenComm.TokenConvert(context.Request);
            

            int AdminYN = Int32.Parse(jobj["AdminYN"].ToString());
            
            if(AdminYN == 1) // 관리자
            {
                context.Items.Add("UserIdx", jobj["UserIdx"].ToString());
                context.Items.Add("Name", jobj["Name"].ToString());
                context.Items.Add("AlarmYN", jobj["AlarmYN"].ToString()); // 알람유무
                context.Items.Add("AdminYN", jobj["AdminYN"].ToString()); // 관리자 유무
                context.Items.Add("UserType", jobj["UserType"].ToString()); // 사용자 타입(유저 or 관리자)
                context.Items.Add("AdminIdx", jobj["AdminIdx"].ToString());
                context.Items.Add("Jti", jobj["Jti"].ToString());
                context.Items.Add("Role", jobj["Role"].ToString());
            
                JObject item = new JObject();
                
                JObject parse = new JObject(JObject.Parse(jobj["UserPerms"].ToString()));
                item.Add("UserPerm_Basic", parse["UserPerm_Basic"].ToString());
                item.Add("UserPerm_Machine", parse["UserPerm_Machine"].ToString());
                item.Add("UserPerm_Elec", parse["UserPerm_Elec"].ToString());
                item.Add("UserPerm_Lift", parse["UserPerm_Lift"].ToString());
                item.Add("UserPerm_Fire", parse["UserPerm_Fire"].ToString());
                item.Add("UserPerm_Construct", parse["UserPerm_Construct"].ToString());
                item.Add("UserPerm_Network", parse["UserPerm_Network"].ToString());
                item.Add("UserPerm_Beauty", parse["UserPerm_Beauty"].ToString());
                item.Add("UserPerm_Security", parse["UserPerm_Security"].ToString());
                item.Add("UserPerm_Material", parse["UserPerm_Material"].ToString());
                item.Add("UserPerm_Energy", parse["UserPerm_Energy"].ToString());
                item.Add("UserPerm_User", parse["UserPerm_User"].ToString());
                item.Add("UserPerm_Voc", parse["UserPerm_Voc"].ToString());
                context.Items.Add("UserPerms", item.ToString());

                item = new JObject();
                parse = new JObject(JObject.Parse(jobj["VocPerms"].ToString()));
                item.Add("VocMachine", parse["VocMachine"].ToString());
                item.Add("VocElec", parse["VocElec"].ToString());
                item.Add("VocLift", parse["VocLift"].ToString());
                item.Add("VocFire", parse["VocFire"].ToString());
                item.Add("VocConstruct", parse["VocConstruct"].ToString());
                item.Add("VocNetwork", parse["VocNetwork"].ToString());
                item.Add("VocBeauty", parse["VocBeauty"].ToString());
                item.Add("VocSecurity", parse["VocSecurity"].ToString());
                item.Add("VocDefault", parse["VocDefault"].ToString());
                context.Items.Add("VocPerms", item.ToString());

                string? PlaceIdx = Convert.ToString(jobj["PlacePerms"]);
                if(!String.IsNullOrWhiteSpace(PlaceIdx))
                {
                    // 사업장 까지 선택한 상태
                    item = new JObject();
                    parse = new JObject(JObject.Parse(jobj["PlacePerms"].ToString()));
                    item.Add("PlaceIdx", parse["PlaceIdx"].ToString());
                    item.Add("PlaceName", parse["PlaceName"].ToString());
                    item.Add("PlacePerm_Machine", parse["PlacePerm_Machine"].ToString());
                    item.Add("PlacePerm_Lift", parse["PlacePerm_Lift"].ToString());
                    item.Add("PlacePerm_Fire", parse["PlacePerm_Fire"].ToString());
                    item.Add("PlacePerm_Construct", parse["PlacePerm_Construct"].ToString());
                    item.Add("PlacePerm_Network", parse["PlacePerm_Network"].ToString());
                    item.Add("PlacePerm_Beauty", parse["PlacePerm_Beauty"].ToString());
                    item.Add("PlacePerm_Security", parse["PlacePerm_Security"].ToString());
                    item.Add("PlacePerm_Material", parse["PlacePerm_Material"].ToString());
                    item.Add("PlacePerm_Energy", parse["PlacePerm_Energy"].ToString());
                    item.Add("PlacePerm_Voc", parse["PlacePerm_Voc"].ToString());

                    context.Items.Add("PlacePerms", item.ToString());

                }
            }
            else // 아님 - 일반유저
            {
                context.Items.Add("UserIdx", jobj["UserIdx"].ToString());
                context.Items.Add("Name", jobj["Name"].ToString());
                context.Items.Add("AlarmYN", jobj["AlarmYN"].ToString());
                context.Items.Add("AdminYN", jobj["AdminYN"].ToString());
                context.Items.Add("UserType", jobj["UserType"].ToString());
                context.Items.Add("Jti", jobj["Jti"].ToString());
                context.Items.Add("Role", jobj["Role"].ToString());
                
                JObject item = new JObject();

                JObject parse = new JObject(JObject.Parse(jobj["UserPerms"].ToString()));
                item.Add("UserPerm_Basic", parse["UserPerm_Basic"].ToString());
                item.Add("UserPerm_Machine", parse["UserPerm_Machine"].ToString());
                item.Add("UserPerm_Elec", parse["UserPerm_Elec"].ToString());
                item.Add("UserPerm_Lift", parse["UserPerm_Lift"].ToString());
                item.Add("UserPerm_Fire", parse["UserPerm_Fire"].ToString());
                item.Add("UserPerm_Construct", parse["UserPerm_Construct"].ToString());
                item.Add("UserPerm_Network", parse["UserPerm_Network"].ToString());
                item.Add("UserPerm_Beauty", parse["UserPerm_Beauty"].ToString());
                item.Add("UserPerm_Security", parse["UserPerm_Security"].ToString());
                item.Add("UserPerm_Material", parse["UserPerm_Material"].ToString());
                item.Add("UserPerm_Energy", parse["UserPerm_Energy"].ToString());
                item.Add("UserPerm_User", parse["UserPerm_User"].ToString());
                item.Add("UserPerm_Voc", parse["UserPerm_Voc"].ToString());
                context.Items.Add("UserPerms", item.ToString());

                item = new JObject();
                parse = new JObject(JObject.Parse(jobj["VocPerms"].ToString()));
                item.Add("VocMachine", parse["VocMachine"].ToString());
                item.Add("VocElec", parse["VocElec"].ToString());
                item.Add("VocLift", parse["VocLift"].ToString());
                item.Add("VocFire", parse["VocFire"].ToString());
                item.Add("VocConstruct", parse["VocConstruct"].ToString());
                item.Add("VocNetwork", parse["VocNetwork"].ToString());
                item.Add("VocBeauty", parse["VocBeauty"].ToString());
                item.Add("VocSecurity", parse["VocSecurity"].ToString());
                item.Add("VocDefault", parse["VocDefault"].ToString());
                context.Items.Add("VocPerms", item.ToString());

                /* 사업장 권한 */
                item = new JObject();
                parse = new JObject(JObject.Parse(jobj["PlacePerms"].ToString()));
                item.Add("PlaceIdx", parse["PlaceIdx"].ToString());
                item.Add("PlaceName", parse["PlaceName"].ToString());
                item.Add("PlacePerm_Machine", parse["PlacePerm_Machine"].ToString());
                item.Add("PlacePerm_Lift", parse["PlacePerm_Lift"].ToString());
                item.Add("PlacePerm_Fire", parse["PlacePerm_Fire"].ToString());
                item.Add("PlacePerm_Construct", parse["PlacePerm_Construct"].ToString());
                item.Add("PlacePerm_Network", parse["PlacePerm_Network"].ToString());
                item.Add("PlacePerm_Beauty", parse["PlacePerm_Beauty"].ToString());
                item.Add("PlacePerm_Security", parse["PlacePerm_Security"].ToString());
                item.Add("PlacePerm_Material", parse["PlacePerm_Material"].ToString());
                item.Add("PlacePerm_Energy", parse["PlacePerm_Energy"].ToString());
                item.Add("PlacePerm_Voc", parse["PlacePerm_Voc"].ToString());
                context.Items.Add("PlacePerms", item.ToString());
            }

            await Next(context);
            return;
        }

    }
}
