using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Fidele.Models.Roles;

namespace Test_Fidele.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleVM role)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role.Role));
            if (result.Succeeded) 
            { 
                return Ok(result);
            }
            return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                return BadRequest();
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return Problem();
        }
    }
}
