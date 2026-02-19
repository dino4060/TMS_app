using back_end_for_TMS.Business.Types;
using back_end_for_TMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace back_end_for_TMS.Business;

public class AccountService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    TokenService tokenService)
{
    public async Task<AuthResult> RefreshToken(TokenRequestDto dto)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(dto.Token);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await userManager.FindByIdAsync(userId!);
        if (user == null || user.RefreshToken != dto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new AuthResult { Success = false, Errors = ["Invalid refresh token attempt"] };
        }

        var roles = await userManager.GetRolesAsync(user);
        var newAccessToken = tokenService.CreateToken(user, roles);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await userManager.UpdateAsync(user);

        return new AuthResult
        {
            Success = true,
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<AuthResult> Register(RegisterDto dto)
    {
        var user = new AppUser { UserName = dto.Email, Email = dto.Email };
        var result = await userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");

            var roles = await userManager.GetRolesAsync(user);
            var accessToken = tokenService.CreateToken(user, roles);
            var refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        return new AuthResult
        {
            Success = false,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
    }

    public async Task<AuthResult> Login(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null) return new AuthResult { Success = false, Errors = ["User not found"] };

        var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        if (result.Succeeded)
        {
            var roles = await userManager.GetRolesAsync(user);
            var accessToken = tokenService.CreateToken(user, roles);
            var refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        return new AuthResult { Success = false, Errors = ["Invalid password"] };
    }

    public async Task<UserProfile?> GetProfile(ClaimsPrincipal currentUser)
    {
        // Lấy email từ Claim trong Token
        var email = currentUser.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) return null;

        var user = await userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var roles = await userManager.GetRolesAsync(user);

        return new UserProfile
        {
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            Roles = roles.ToList()
        };
    }
}
