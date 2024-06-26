﻿namespace IdentityProviderSystem.Domain.Models.Token;

public interface IToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Secret { get; set; }
    public bool Alive { get; set; }
    public string Value { get; set; }
}