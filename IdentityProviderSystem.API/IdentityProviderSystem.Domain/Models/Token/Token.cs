﻿namespace IdentityProviderSystem.Domain.Models.Token;

public class Token : IToken, ITokenResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Secret { get; set; }
    public bool Alive { get; set; }
    public string Value { get; set; }
}