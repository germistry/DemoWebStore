using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Application.UsersAdmin;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private CreateUser _createUser;
        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }
        
        public async Task<IActionResult> CreateUser([FromBody]CreateUser.Request request)
        {
            await _createUser.Action(request);
            return RedirectToPage("/Admin/Index");
        }
    }
}
