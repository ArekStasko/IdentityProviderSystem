using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Persistance.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Persistance.Repositories.AccessTokenRepository;

public class AccessTokenRepository : IAccessTokenRepository
{
    private readonly ITokenDataContext _context;
    private readonly ILogger<IAccessTokenRepository> _logger;
    
    public AccessTokenRepository(ITokenDataContext context, ILogger<IAccessTokenRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Remove(int Id)
    {
        try
        {
            var tokenToDelete = await _context.Tokens.FirstOrDefaultAsync(t => t.Id == Id);
            if (tokenToDelete == null)
            {
                _logger.LogError("There is no token with {id} Id", Id);
                return new Result<bool>(false);
            }

            _context.Tokens.Remove(tokenToDelete);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Remove token repository method failed with an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<IAccessToken>> Create(IAccessToken token)
    {
        try
        {
           var result = await _context.Tokens.AddAsync((AccessToken)token);
           await _context.SaveChangesAsync();
           return new Result<IAccessToken>(result.Entity);
        }
        catch (Exception e)
        {
            _logger.LogError("Create token repository method failed with an exception: {e}", e);
            return new Result<IAccessToken>(e);
        }
    }

    public async Task<Result<IAccessToken>> Update(IAccessToken token)
    {
        try
        {
            var tokenToUpdate = await _context.Tokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            if (tokenToUpdate == null)
            {
                _logger.LogError("There is no token with {id} Id", token.Id);
                return new Result<IAccessToken>(new NullReferenceException());
            }

            await _context.SaveChangesAsync();
            return new Result<IAccessToken>(tokenToUpdate);
        }
        catch (Exception e)
        {
            _logger.LogError("Update token repository method failed with an exception: {e}", e);
            return new Result<IAccessToken>(e);
        }
    }

    public async Task<Result<IAccessToken>> Get(int userId)
    { 
        try
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == userId);
            if (token == null)
            {
                _logger.LogError("User with {id} Id doesnt have any valid tokens");
                return new Result<IAccessToken>(new NullReferenceException());
            }
            return new Result<IAccessToken>(token);
        }
        catch (Exception e)
        {
            _logger.LogError("Get token repository method failed with an exception: {e}", e);
            return new Result<IAccessToken>(e);
        }
    }
    
    public async Task<Result<IList<IAccessToken>>> Get()
    { 
        try
        {
            var tokens = await _context.Tokens.ToListAsync<IAccessToken>();
            return new Result<IList<IAccessToken>>(tokens);
        }
        catch (Exception e)
        {
            _logger.LogError("Get token repository method failed with an exception: {e}", e);
            return new Result<IList<IAccessToken>>(e);
        }
    }
}