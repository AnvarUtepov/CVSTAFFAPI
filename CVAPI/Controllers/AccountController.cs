using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CVCore.Entities;
using CVCore.Interfaces;
using CVCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{    
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(
            IAccountService accountService,
            IMapper mapper
            )
        {
            this._accountService = accountService;
            this._mapper=mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Login model)
        {  
            var result = await _accountService.CheckLogin(model);
            if (result.Succeeded)
            {
                return Ok(await _accountService.GenerateJwtToken(model.UserName));
            }
            return Unauthorized();
        }
        [HttpGet]
        public async Task<ActionResult> SignOut()
        {
            await _accountService.SignOutAsync();
            return Ok();            
        }
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterUser model)
        {
            ApplicationUser appUser = _mapper.Map<RegisterUser,ApplicationUser>(model);            

            IdentityResult result = await _accountService.CreateUser(appUser,model.Password);

            if (result.Succeeded)
            {               
                return Ok(await _accountService.RegisterUser(appUser));
            }
            return StatusCode(500);
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult getUserData()
        {
            var result = new RegisterUser()
            {
                FIO = User.Identity.Name,                                
            };
            return Ok(result);            
        }

    }
}