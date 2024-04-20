﻿using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public interface ITokenService
{
    public Task<Result<IToken>> Get(int userId);
    public Task<Result<IToken>> Generate(int userId);
    public Task<Result<bool>> CheckExp(string token);
}