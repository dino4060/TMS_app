using back_end_for_TMS.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace back_end_for_TMS.Business;

public class TokenService(IConfiguration config)
{
    public string CreateToken(AppUser user, IList<string> roles)
    {
        // 1. Lấy thông tin từ cấu hình
        // Sử dụng GetValue để đảm bảo nếu thiếu key sẽ báo lỗi rõ ràng
        var signingKey = config.GetValue<string>("JWT:SigningKey")
            ?? throw new ArgumentNullException("JWT:SigningKey", "Missing SigningKey in configuration");

        // Đảm bảo key đủ độ dài cho HS512 (512 bits = 64 bytes)
        var keyBytes = Encoding.UTF8.GetBytes(signingKey);
        if (keyBytes.Length < 64)
        {
            // Bạn có thể hạ xuống HS256 nếu key ngắn, hoặc báo lỗi để dev biết mà sửa appsettings
            throw new InvalidOperationException("SigningKey must be at least 64 characters long for HS512.");
        }

        var key = new SymmetricSecurityKey(keyBytes);

        // 2. Tạo danh sách Claims (thông tin định danh)
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(JwtRegisteredClaimNames.GivenName, user.UserName ?? ""),
            new(JwtRegisteredClaimNames.NameId, user.Id) // Quan trọng để Identify User sau này
        };

        // Thêm các Role vào Claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // 3. Cấu hình chữ ký (Credentials)
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // 4. Cấu hình nội dung Token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"]
        };

        // 5. Tạo và trả về chuỗi Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}
