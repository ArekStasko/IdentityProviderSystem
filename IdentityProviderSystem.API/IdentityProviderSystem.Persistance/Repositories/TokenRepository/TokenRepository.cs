using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Persistance.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Persistance.Repositories.TokenRepository;

public class TokenRepository : ITokenRepository
{
    private readonly ITokenDataContext _cotnext;
    private readonly ILogger<ITokenRepository> _logger;
    
    public TokenRepository(ITokenDataContext cotnext, ILogger<ITokenRepository> logger)
    {
        _cotnext = cotnext;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Remove(int Id)
    {
        try
        {
            var tokenToDelete = await _cotnext.Tokens.FirstOrDefaultAsync(t => t.Id == Id);
            if (tokenToDelete == null)
            {
                _logger.LogError("There is no token with {id} Id", Id);
                return new Result<bool>(false);
            }

            _cotnext.Tokens.Remove(tokenToDelete);
            await _cotnext.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Remove token repository method failed with an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<IToken>> Create(IToken token)
    {
        try
        {
           var result = await _cotnext.Tokens.AddAsync((Token)token);
           await _cotnext.SaveChangesAsync();
           return new Result<IToken>(result.Entity);
        }
        catch (Exception e)
        {
            _logger.LogError("Create token repository method failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<IToken>> Update(IToken token)
    {
        try
        {
            var tokenToUpdate = await _cotnext.Tokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            if (tokenToUpdate == null)
            {
                _logger.LogError("There is no token with {id} Id", token.Id);
                return new Result<IToken>(new NullReferenceException());
            }

            await _cotnext.SaveChangesAsync();
            return new Result<IToken>(tokenToUpdate);
        }
        catch (Exception e)
        {
            _logger.LogError("Update token repository method failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<IToken>> Get(int userId)
    { 
        try
        {
            var token = await _cotnext.Tokens.FirstOrDefaultAsync(t => t.UserId == userId);
            if (token == null)
            {
                _logger.LogError("User with {id} Id doesnt have any valid tokens");
                return new Result<IToken>(new NullReferenceException());
            }
            return new Result<IToken>(token);
        }
        catch (Exception e)
        {
            _logger.LogError("Get token repository method failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }
}