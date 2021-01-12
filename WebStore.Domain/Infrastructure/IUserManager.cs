using System.Threading.Tasks;

namespace WebStore.Domain.Infrastructure
{
    public interface IUserManager
    {
        Task CreateManagerUser(string username, string password);
    }
}
