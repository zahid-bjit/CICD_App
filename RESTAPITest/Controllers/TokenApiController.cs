using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Tokens;
using RESTAPITest.DbContext;
using RESTAPITest.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTAPITest.Controllers
{
    public class TokenApiController : ApiController
    {
        public TokenApiController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public Object GetToken(ApiLoginViewModel model)
        {
            string key = "secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "*";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", model.Email));
            permClaims.Add(new Claim("Type", "Admin"));


            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
        [HttpPost]
        public IHttpActionResult Create(ApiLoginViewModel model)
        {
            var manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            var user = manager.FindByName(model.Email);

            var validCredentials = signinManager.UserManager.CheckPassword(user, model.Password);
            if (validCredentials)
            {
                return Ok(new { token = GetToken(model) });
            }
            return Ok("Invalid request!");
        }
    }
}