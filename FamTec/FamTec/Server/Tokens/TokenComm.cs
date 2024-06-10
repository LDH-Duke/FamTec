using Newtonsoft.Json.Linq;

namespace FamTec.Server.Tokens
{
    public class TokenComm : ITokenComm
    {
        public AdminSettingModel? TokenConvert(string? token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                int split = token.IndexOf('.') + 1;

                string payload = token.Substring(split, token.Length - split);
                JObject? jobj = JObject.Parse(payload.ToString());

                AdminSettingModel model = new AdminSettingModel();
                if (jobj["UserIdx"] is not null)
                    model.UserIdx = Convert.ToInt32(jobj["UserIdx"]!.ToString());
                
                if(jobj["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] is not null)
                    model.UserName = jobj["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]!.ToString();
                
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
