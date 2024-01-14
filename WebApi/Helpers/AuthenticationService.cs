using AutoMapper;
using BL.Services;
using Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Helpers
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IStringCryptorDecryptor _stringCryptorDecryptor;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public AppSettings _appSettings { get; }
        private readonly IMapper _mapper;

        public AuthenticationService(IStringCryptorDecryptor stringCryptorDecryptor, IUserService userService, IRoleService roleService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _stringCryptorDecryptor = stringCryptorDecryptor;
            _userService = userService;
            _roleService = roleService;
            _appSettings = appSettings.Value;
            _mapper = mapper;

        }
        private AuthenticatedUserDto buildAuthenticatedUser(User user)
        {
            var authenticateduserDto = this._mapper.Map<AuthenticatedUserDto>(user);
            var menus = _roleService.GetMenusByRoleId(user.RoleId).ToList();
            var menusDto = _mapper.Map<List<MenuDto>>(menus);

            authenticateduserDto.Menus = menusDto;
            return authenticateduserDto;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userService.GetUserByUserName(model.Username);

            // return null if user not found
            if (user == null) return null;

            else
            {
                var decryptedUserPwd = _stringCryptorDecryptor.DecryptString(user.CryptedPassword);
                if (decryptedUserPwd != model.Password)
                    return null;

                //authentication successful so generate jwt token
                var token = generateJwtToken(user);

                // build BuildAuthenticatedUserDto
                var authenticatedUserDto = buildAuthenticatedUser(user);

                return new AuthenticateResponse(authenticatedUserDto, token);

            }           
            
        }
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
