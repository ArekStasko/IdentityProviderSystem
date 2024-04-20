using AutoMapper;
using IdentityProviderSystem.Domain.Models.Salt;
using IdentityProviderSystem.Persistance.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Persistance.Repositories.SaltRepository;

public class SaltRepository : ISaltRepository
{
    public readonly ISaltDataContext _context;
    public readonly ILogger<ISaltRepository> _logger;
    
    public SaltRepository(ISaltDataContext context, ILogger<ISaltRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Delete()
    {
        try
        {
            var saltToDelete = await _context.Salts.FirstOrDefaultAsync();
            if (saltToDelete == null)
            {
                _logger.LogError("There is no salt");
                return new Result<bool>(true);
            }

            _context.Salts.Remove(saltToDelete);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Delete salt repository method failed with exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<bool>> Add(string saltValue)
    {
        try
        {
            var salt = new Salt()
            {
                DateOfGeneration = DateTime.Now,
                SaltValue = saltValue
            };
            await _context.Salts.AddAsync(salt);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Add salt repository method failed with exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<ISalt>> GetCurrent()
    {
        try
        {
            var currentSalt = await _context.Salts.FirstOrDefaultAsync();
            if (currentSalt == null)
            {
                _logger.LogError("There is no any salt for now");
                return new Result<ISalt>(new Salt());
            }

            return new Result<ISalt>(currentSalt);
        }
        catch (Exception e)
        {
            _logger.LogError("Get current salt repository method failed with exception: {e}", e);
            return new Result<ISalt>(e);
        }
    }
}