﻿using AutoMapper;
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
    
    public async Task<Result<bool>> Delete(ISalt salt)
    {
        try
        {
            var saltToDelete = await _context.Salts.FirstOrDefaultAsync(s => s.SaltValue == salt.SaltValue);
            if (saltToDelete == null)
            {
                _logger.LogError("There is no any salt with {s} value", salt.SaltValue);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Salts.Remove((Salt)salt);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Delete salt repository method failed with exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<bool>> Add(Guid saltValue)
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
                return new Result<ISalt>(new NullReferenceException());
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