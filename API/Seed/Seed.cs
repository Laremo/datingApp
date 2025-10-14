using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.DTOs;
using API.DTOS;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Seed;

public class Seed
{
    public static async Task SeedUsers(AppDataContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var seedUsersData = await File.ReadAllTextAsync("Data/seedUserData.json");
        var seedUsers = JsonSerializer.Deserialize<List<SeedUserDto>>(seedUsersData);

        if (seedUsers == null)
        {
            Console.WriteLine("No seed data available");
            return;
        }

        using var hmac = new HMACSHA512();

        foreach (var seedUser in seedUsers)
        {
            var user = new AppUser
            {
                Id = seedUser.Id,
                Email = seedUser.Email,
                DisplayName = seedUser.DisplayName,
                ImageUrl = seedUser.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = seedUser.Id,
                    DisplayName = seedUser.DisplayName,
                    Gender = seedUser.Gender,
                    City = seedUser.City,
                    Country = seedUser.Country,
                    Description = seedUser.Description,
                    BirthDay = seedUser.BitrhDay,
                    ImageUrl = seedUser.ImageUrl,
                    LastActive = seedUser.LastActive,
                    Created = seedUser.Created
                }
            };

            user.Member.Photos.Add(new Photo
            {
                Url = seedUser.ImageUrl!,
                MemberId = seedUser.Id,
            });

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }

}
