using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using DMVLaw.Data;
using DMVLaw.Models;

namespace DMVLaw
{
    public class JwtAuthenticationManager
    {
        private readonly string key;

        private readonly IDictionary<string, string> users = new Dictionary<string, string>()
        { 
            {"SKing", "Password11" }, 
            {"AEli", "Password22"}, 
            {"MBalfanz", "Password33"}, 
            {"KPatterson", "Password44"}, 
            {"JFries", "Password55"}, 
            {"SRyman", "Password66"}, 
            {"HFarnsworth", "Password77"}, 
            {"THanks", "Password88"}, 
            {"BZeilman", "Password99"}, 
            {"JScott", "Password00"} 
        };


        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authentication(string username, string password, string role)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
            { return null; }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
