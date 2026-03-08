using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class UserService 
    : ServiceBase<User, UserRepository>, IUserService
{
    public UserService(UserRepository repository)
        : base(repository)
    {
    }
}