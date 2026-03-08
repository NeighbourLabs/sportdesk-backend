using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class UserService(IUserRepository repository) 
    : ServiceBase<User, IUserRepository>(repository), IUserService;