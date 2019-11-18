using CVCore.Entities;
using CVCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CVCore.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> CheckLogin(Login login);
        Task<string> GenerateJwtToken(string loginName);
        Task<IdentityResult> CreateUser(ApplicationUser user,string password);
        Task<string> RegisterUser(ApplicationUser user);
        Task SignOutAsync();
    }
}
