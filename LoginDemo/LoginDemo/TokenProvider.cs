using LoginDemo.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace LoginDemo
{
    public class TokenProvider
    {
        private List<User> UserList = new List<User>();

        public TokenProvider(List<User> users)
        {
            UserList = users;
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.ID.ToString()),
                new Claim("NAME", user.Name),
                new Claim("USERNAME", user.Username),
                new Claim("PASSWORD", user.Password),
                new Claim("GENDER", user.Gender),
                new Claim("DEPARTMENT", user.Department)
            };
        }

        public string LoginUser(string UserID, string Password)
        {
            //Get user details for the user who is trying to login
            var user = UserList.SingleOrDefault(x => x.Username == UserID);

            //Authenticate User, Check if its a registered user in DB
            if (user == null)
                return null;

            //If its registered user, check user password stored in DB
            //For demo, password is not hashed. Its just a string comparison
            //In reality, password would be hashed and stored in DB. Before comparing, hash the password
            if (Password == user.Password)
            {
                //Authentication successful, Issue Token with user credentials
                //Provide the security key which was given in the JWToken configuration in Startup.cs
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                //Generate Token for user
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:45092/",
                    audience: "http://localhost:45092/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
                return null;
        }
    }
}
