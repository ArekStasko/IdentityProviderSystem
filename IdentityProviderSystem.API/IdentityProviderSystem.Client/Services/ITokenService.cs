﻿namespace IdentityProviderSystem.Client.Services;

public interface ITokenService
{
    Task<bool> ValidateToken(string token);
}