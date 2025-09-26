using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Persistance.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Persistance.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IRefreshTokenDataContext _context;
    private readonly ILogger<IRefreshTokenRepository> _logger;

    public RefreshTokenRepository(IRefreshTokenDataContext context, ILogger<IRefreshTokenRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Remove(int Id)
    {
        try
        {
            var tokenToDelete = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Id == Id);
            if (tokenToDelete == null)
            {
                _logger.LogError("There is no refresh token with {id} Id", Id);
                return new Result<bool>(false);
            }

            _context.RefreshTokens.Remove(tokenToDelete);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Remove refresh token repository method failed with an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<IToken>> Create(IToken token)
    {
        try
        {
            var result = await _context.RefreshTokens.AddAsync((RefreshToken)token);
            await _context.SaveChangesAsync();
            return new Result<IToken>(result.Entity);
        }
        catch (Exception e)
        {
            _logger.LogError("Create refresh token repository method failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<IToken>> Update(IToken token)
    {
        try
        {
            var tokenToUpdate = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            if (tokenToUpdate == null)
            {
                _logger.LogError("There is no refresh token with {id} Id", token.Id);
                return new Result<IToken>(new NullReferenceException());
            }

            await _context.SaveChangesAsync();
            return new Result<IToken>(tokenToUpdate);
        }
        catch (Exception e)
        {
            _logger.LogError("Update refresh token repository method failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<IList<IToken>>> Get()
    {
        try
        {
            var tokens = await _context.RefreshTokens.ToListAsync<IToken>();
            return new Result<IList<IToken>>(tokens);
        }
        catch (Exception e)
        {
            _logger.LogError("Get refresh token repository method failed with an exception: {e}", e);
            return new Result<IList<IToken>>(e);
        }
    }
}