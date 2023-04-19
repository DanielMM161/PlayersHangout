﻿namespace Backend.Src.Services;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IJwtTokenService
{
    Task<SignInResponseDTO> GenerateToken(User user);
}
