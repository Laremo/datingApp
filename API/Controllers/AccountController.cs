using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOS;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class AccountController(AppDataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("Register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
    {
        using var hmac = new HMACSHA512();

        if (await EmailExists(request.Email))
        {
            return BadRequest("Email is already in use");
        }

        var user = new AppUser
        {
            DisplayName = request.DisplayName,
            Email = request.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };

        context.Add(user);
        await context.SaveChangesAsync();

        var token = tokenService.CreateToken(user);
        return new UserResponse
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = token
        };

    }

    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(user => user.Email.ToLower() == email.ToLower());
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> login(LoginRequest request)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Email == request.Email);

        if (user == null) return Unauthorized("Invalid email or password");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid email or password");
        }

        var token = tokenService.CreateToken(user);

        return new UserResponse
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = token
        };
    }
}
