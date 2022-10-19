using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Users;
using EPharmacy.ServerApp.Models.Account.Requests;
using EPharmacy.ServerApp.Models.Account.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EPharmacy.ServerApp.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly EPharmacyContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(EPharmacyContext context, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserWithRole(RegistrationRequest registrationRequest, string role)
        {
            var userIdentity = _mapper.Map<ApplicationUser>(registrationRequest);

            var result = await _userManager.CreateAsync(userIdentity, registrationRequest.Password);

            if (!result.Succeeded)
                return false;
            await _userManager.AddToRoleAsync(userIdentity, role);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LoginResponse> GetClaimsIdentity(LoginRequest loginRequest)
        {
            var userToVerify = await _userManager.FindByNameAsync(loginRequest.Email);

            if (userToVerify == null)
                return null;

            var roles = await _userManager.GetRolesAsync(userToVerify);

            if (!await _userManager.CheckPasswordAsync(userToVerify, loginRequest.Password)) return null;
            var token = CreateJWTToken(userToVerify, roles);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return _mapper.Map<LoginResponse>(jwt);
        }

        public async Task<LoginResponse> GetClaimsIdentity(string jwtToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = (JwtSecurityToken)jwtTokenHandler.ReadToken(jwtToken);
            var userEmail = decodedToken.Payload["email"] as string;
            var user = await _userManager.FindByNameAsync(userEmail);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var newToken = jwtTokenHandler.WriteToken(CreateJWTToken(user, roles));
            return _mapper.Map<LoginResponse>(newToken);
        }

        public async Task<UserData> GetUserData(string userName)
        {
            var user = await FindUserByName(userName);
            return _mapper.Map<UserData>(user);
        }

        public async Task<bool> ChangeUserData(string userName, UserData userData)
        {
            var user = await FindUserByName(userName);
            _mapper.Map(userData, user);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;            
        }

        public async Task<bool> ChangeUserPassword(string userName, string currentPassword, string newPassword)
        {
            var user = await FindUserByName(userName);
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded) return false;
            await _userManager.UpdateAsync(user);
            return true;
        }

        private JwtSecurityToken CreateJWTToken(ApplicationUser user, IList<string> roles)
        {
            return new JwtSecurityToken(
                _configuration["Token:issuer"],
                _configuration["Token:audience"],
                GetTokenClaims(user, roles),
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:key"])),
                    SecurityAlgorithms.HmacSha256));
        }

        private static IEnumerable<Claim> GetTokenClaims(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim("role", roles.First())
            };

            return claims;
        }

        public async Task<ApplicationUser> FindUserByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> UserExists(string userName)
        {
            return await FindUserByName(userName) != null ? true : false;
        }
    }
}