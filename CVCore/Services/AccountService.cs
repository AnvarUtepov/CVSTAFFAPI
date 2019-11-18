using CVCore.Entities;
using CVCore.Interfaces;
using CVCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCore.Services
{
    public class AccountService:IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtToken _jwtToken;
        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,            
            IConfiguration configuration,
            IJwtToken jwtToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;            
            _jwtToken=jwtToken;
        }   
        public async Task<SignInResult> CheckLogin(Login login)
        {    
            return await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
        }
        public async Task<string> GenerateJwtToken(string loginName)
        {    
             var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == loginName);
             var roles = await _userManager.GetRolesAsync(appUser);
             return _jwtToken.GenerateJwtToken(appUser, _configuration,roles);
        }
        public async Task SignOutAsync()
        {    
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> CreateUser(ApplicationUser user,string password)
        {    
             return await _userManager.CreateAsync(user, password);
        }
        public async Task<string> RegisterUser(ApplicationUser user)
        {
             await _signInManager.SignInAsync(user, false);
             var roles = await _userManager.GetRolesAsync(user);
             return _jwtToken.GenerateJwtToken(user, _configuration,roles);
        }
    }
}
