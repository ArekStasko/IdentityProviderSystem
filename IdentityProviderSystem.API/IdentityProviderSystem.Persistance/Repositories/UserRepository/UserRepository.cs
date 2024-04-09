using AutoMapper;
using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Persistance.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Persistance.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IUserDataContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<IUserRepository> _logger;
    
    public UserRepository(IUserDataContext context, IMapper mapper,  ILogger<IUserRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Result<int>> Create(IUser user)
    {
        try
        {
            await _context.Users.AddAsync((User)user);
            await _context.SaveChangesAsync();
            return new Result<int>(user.Id);
        }
        catch (Exception e)
        {
            _logger.LogError("Create User failed with an exception: {e}", e);            
            return new Result<int>(e);
        }
    }

    public async Task<Result<bool>> Update(IUser user)
    {
        try
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (userToUpdate == null)
            {
                _logger.LogError("There is no user with {Username} username", user.Username);
                return new Result<bool>(new NullReferenceException("There is no user with provided username"));
            }

            userToUpdate = _mapper.Map<User>(user);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Update User data failed with an exception: {e}", e);            
            return new Result<bool>(e);
        }
    }

    public async Task<Result<User>> Get(string username)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                _logger.LogError("There is no user with {Username} username", user.Username);
                return new Result<User>(new NullReferenceException("There is no user with provided username"));
            }

            return new Result<User>(user);
        }
        catch (Exception e)
        {
            _logger.LogError("Get User data failed with an exception: {e}", e);            
            return new Result<User>(e);
        }
    }
}