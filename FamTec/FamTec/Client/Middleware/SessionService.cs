using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;


namespace FamTec.Client.Middleware
{
    public class SessionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _key = "DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4";
        public SessionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetClaimValue(string token, string claimType)
        {
        

            if(token == null)
            {
                return null;
            }
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var encryptToken = handler.ReadJwtToken(token);

            var claim = encryptToken.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }

        public class SessionStatus
        {
            public bool IsActive { get; set; }
        }
    }
}
