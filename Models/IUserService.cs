using System.Threading.Tasks;


namespace LosCasaAngular.Models{
    public interface IUserService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(string email, string password);
        // ... other user-related methods
    }
}
