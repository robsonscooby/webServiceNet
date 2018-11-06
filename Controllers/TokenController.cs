using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using webServiceNet.Models;

namespace webServiceNet.Controllers
{
    [Route("api/[Controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] Usuario request)
        {
            if(request.Nome == "Robson" && request.Senha == "vectr@")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Nome)
                };

                //recebe uma instancia da classe SymmetricSecurityKey
                //armazenando a chave de criptografia usada na criacao do token
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                
                //recebe um objeto do tipo SigningCredentials contendo a chave de
                //criptografia e o algoritmo de seguranca empregados na geracao
                //de assinaturas digitais para os tokens
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            
                var token = new JwtSecurityToken(
                    issuer: "robson.net",
                    audience: "robson.net",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new 
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return BadRequest("Credenciais invalidas...");
        } 
    }
}