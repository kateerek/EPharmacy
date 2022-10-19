using System.Threading.Tasks;
using EPharmacy.Data.Entities;
using EPharmacy.Data.Entities.Users;
using EPharmacy.ServerApp.Models.Account.Requests;
using EPharmacy.ServerApp.Models.Account.Responses;

namespace EPharmacy.ServerApp.Services.Account
{
    public interface IAccountService
    {
        Task<LoginResponse> GetClaimsIdentity(LoginRequest loginRequest);
        Task<LoginResponse> GetClaimsIdentity(string jwtToken);
        Task<UserData> GetUserData(string userName);
        Task<bool> ChangeUserData(string userName, UserData userData);
        Task<bool> ChangeUserPassword(string userName,string currentPassword, string newPassword);
        Task<ApplicationUser> FindUserByName(string userName);
        Task<bool> UserExists(string userName);

        Task<bool> CreateUserWithRole(RegistrationRequest registrationRequest, string role);
    }
}