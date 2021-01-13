using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.UI.ViewModels.Admin;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> CreateUser([FromBody] CreateManagerUserViewModel viewModel)
        {
            var managerUser = new IdentityUser()
            {
                UserName = viewModel.Username
            };

            await _userManager.CreateAsync(managerUser, "password");

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);
                     
                       
            return RedirectToPage("/Admin/Index");
        }
    }
}
