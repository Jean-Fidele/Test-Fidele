using Data.Context;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Fidele.Models.Users;
using Test_Fidele.Utility;

namespace Test_Fidele.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userList = await _userManager.Users.ToListAsync();
            return Ok(userList);
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if(user == null)
            {
                return NotFound(user.Email);
            }
                   
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Firstname = model.Name
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!User.IsInRole(Helper.Admin))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }
            }
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RegisterVM model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Firstname = model.Name
            };

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return StatusCode(500);
        }
    }
}
