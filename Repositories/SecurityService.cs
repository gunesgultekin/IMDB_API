using IMDB_API.Context;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IMDB_API.Repositories
{
    public class SecurityService
    {
        private readonly IConfiguration _configuration;

        public SecurityService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public static string ComputeSha512Hash(string rawData)
        {
            // Create a SHA256
            using (SHA512 sha256Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateToken(string username)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            try
            {
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:KEY"]));

                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = new[]
                {
                new Claim(ClaimTypes.UserData, username),
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(1440),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }

            catch (Exception ex)
            {
                return ex.Message;
            }



        }
    }
}
