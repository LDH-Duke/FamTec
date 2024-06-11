using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FamTec.Server.Tokens
{
    public class TokenComm : ITokenComm
    {
        public AdminSettingModel? TokenConvert(HttpRequest? token)
        {
            if (token is not null)
            {
                string? accessToken = token.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                var authSigningKey = Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4");
                
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(authSigningKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                int split = validatedToken.ToString()!.IndexOf('.') + 1;

                string payload = validatedToken.ToString()!.Substring(split, validatedToken.ToString()!.Length - split);
                JObject? jobj = JObject.Parse(payload.ToString());

                AdminSettingModel model = new AdminSettingModel();
                if (jobj["UserIdx"] is not null)
                    model.UserIdx = Convert.ToInt32(jobj["UserIdx"]!.ToString());
                
                if(jobj["Name"] is not null)
                    model.UserName = jobj["Name"]!.ToString();
                
                if(jobj["AdminIdx"] is not null)
                    model.AdminIdx = Convert.ToInt32(jobj["AdminIdx"]!.ToString());
                
                if (jobj["DepartIdx"] is not null)
                    model.DepartmentIdx = Convert.ToInt32(jobj["DepartIdx"]!.ToString());
                
                if (jobj["jti"] is not null)
                    model.Jti = jobj["jti"]!.ToString();
                
                if (jobj["DepartmentName"] is not null)
                    model.DepartmentName = jobj["DepartmentName"].ToString();

                if (jobj["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] is not null)
                    model.Role = jobj["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]!.ToString();

                if (jobj["exp"] is not null)
                    model.Exp = jobj["exp"]!.ToString();

                if (jobj["iss"] is not null)
                    model.iss = jobj["iss"]!.ToString();

                if (jobj["aud"] is not null)
                    model.aud = jobj["aud"]!.ToString();

                if (model is not null)
                {
                    return model;
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
    }
}
