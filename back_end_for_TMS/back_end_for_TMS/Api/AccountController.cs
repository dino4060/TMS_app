using back_end_for_TMS.Business;
using back_end_for_TMS.Business.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_for_TMS.Api;

[ApiController]
[Route("api/[controller]")]
public class AccountController(AccountService accountService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("info")]
    public IActionResult GetInfo()
    {
        int i = 0;
        int ii = 2;
        return Ok(new { message = "This is public data" });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterDto dto)
    {
        var result = await accountService.Register(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginDto dto)
    {
        var result = await accountService.Login(dto);
        if (!result.Success) return Unauthorized(result);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Với JWT, Logout chủ yếu diễn ra ở Client (xóa Token). 
        // Endpoint này trả về 200 OK để Client thực hiện xóa logic.
        return Ok(new { message = "Logged out successfully" });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserProfile>> GetMe()
    {
        // 'User' là property có sẵn của ControllerBase, kiểu ClaimsPrincipal
        var profile = await accountService.GetProfile(User);

        if (profile == null) return NotFound();
        return Ok(profile);
    }
}
