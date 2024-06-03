using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FamTec.Server
{
    public class JwtMiddleware
    {
        
        private readonly RequestDelegate Next;
        private readonly IConfiguration Configuration;
        //private const string APIKEYNAME = "Authorization";
        
        
        public JwtMiddleware(RequestDelegate _next, IConfiguration _configuration)
        {
            this.Next = _next;
            this.Configuration = _configuration;
            
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

            //context.Response.WriteAsync(validatedToken.ToString());
            context.Items.Add("Token", validatedToken.ToString());
            

            await Next(context);
            return;
        }

        

    }
}
