using Newtonsoft.Json.Linq;

namespace FamTec.Server.Tokens
{
    public interface ITokenComm
    {
        public AdminSettingModel? TokenConvert(string? token);
    }
}
