using API.DTOS;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions
{
    public static UserResponse ToDto(this AppUser appUser, ITokenService tokenService)
    {
        var token = tokenService.CreateToken(appUser);

        return new UserResponse
        {
            Id = appUser.Id,
            DisplayName = appUser.DisplayName,
            Email = appUser.Email,
            Token = token
        };
    }

    
}
              