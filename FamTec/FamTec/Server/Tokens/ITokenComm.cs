using Newtonsoft.Json.Linq;

namespace FamTec.Server.Tokens
{
    public interface ITokenComm
    {
        public JObject? TokenConvert(HttpRequest? token);

        public List<PlaceTokenModel> AdminTokenModel(JObject jobj);
    }
}
