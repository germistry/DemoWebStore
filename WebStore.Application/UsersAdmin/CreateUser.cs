using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebStore.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;
        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Action(Request request)
        {
            var managerUser = new IdentityUser()
            {
                UserName = request.UserName
            };

            await _userManager.CreateAsync(managerUser, "password");

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);
            
            return true;
        }

        public class Request
        {
            public string UserName { get; set; }
        }
    }
}
