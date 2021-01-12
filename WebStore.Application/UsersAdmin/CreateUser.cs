using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.UsersAdmin
{
    public class CreateUser
    {
        private readonly IUserManager _userManager;
        public CreateUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Action(Request request)
        {
            await _userManager.CreateManagerUser(request.UserName, request.Password);
            return true;
        }

        public class Request
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
