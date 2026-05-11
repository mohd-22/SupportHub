using ClientTicketingSystem.Application.Dtos;
using ClientTicketingSystem.Application.Helpers;
using ClientTicketingSystem.Application.Services.Interfaces;
using ClientTicketingSystem.CORE.Models;
using ClientTicketingSystem.DATA.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientTicketingSystem.Application.Services;
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {

        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }
    public async Task<ApiResponse<string>> LoginAsync(LoginDto request)
    {
        var user = await _unitOfWork.Users.FindAsync(u => u.UserName == request.EmailOrUsername);

        if (user == null)
            return new ApiResponse<string>
            {
                Success = false,
                Message = "User not found",
                StatusCode = 404
            };

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password) == PasswordVerificationResult.Failed)
            return new ApiResponse<string>
            {
                Success = false,
                Message = "Invalid password",
                StatusCode = 401
            };
        var Cheakuser = await _unitOfWork.Users.FindAsync(u => u.UserName == request.EmailOrUsername);

        if (Cheakuser != null && !Cheakuser.IsActive)
        {
            return new ApiResponse<string>
            {
                Success = false,
                Message = "User is not active",
                StatusCode = 401
            };
        }

        return new ApiResponse<string>
        {
            Success = true,
            Message = "Login successful",
            StatusCode = 200,
            Data = CreateToken(user)
        };
    }

    public async Task<ApiResponse<UserRegistraionDto>> RigisterUserAsync(UserRegistraionDto request)
    {
        if (await _unitOfWork.Users.AnyAsync(u => u.UserName == request.UserName))
        {
            return new ApiResponse<UserRegistraionDto>
            {
                Success = false,
                Message = "User name already exists",
                StatusCode = 400
            };
        }
        if (await _unitOfWork.Users.AnyAsync(u => u.Email == request.Email))
        {
            return new ApiResponse<UserRegistraionDto>
            {
                Success = false,
                Message = "Email already exists",
                StatusCode = 400
            };
        }
        if (await _unitOfWork.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
        {
            return new ApiResponse<UserRegistraionDto>
            {
                Success = false,
                Message = "Phone number already exists",
                StatusCode = 400
            };
        }



        var user = new User();
        user.UserName = request.UserName;
        user.FullName = request.FullName;
        user.Email = request.Email;
        var hashedPassword = new PasswordHasher<User>().
        HashPassword(user, request.Password);
        user.HashedPassword = hashedPassword;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;
        user.DateOfBirth = request.DateOfBirth;
        user.Gender = request.Gender;
        user.CreatedDate = DateTime.UtcNow;


        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return new ApiResponse<UserRegistraionDto>
        {
            Success = true,
            Message = "User created successfully",
            StatusCode = 200,
            Data = request
        };
    }
    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            new Claim(ClaimTypes.Name, user.UserName),

            new Claim(ClaimTypes.Email, user.Email),

            new Claim(ClaimTypes.Role, user.Role.ToString()),

             new Claim("FullName", user.FullName)
        };

        var key = new SymmetricSecurityKey(
         Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:SecretKey")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var TokenDescirptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
            audience: _configuration.GetValue<string>("JwtSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(TokenDescirptor);
    }

}
