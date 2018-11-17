using System.Threading.Tasks;

namespace AuthorizationDemo
{
    public interface IAuthorizer
    {
        Task Authorize();
    }
}
