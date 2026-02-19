using back_end_for_TMS.Business;
using back_end_for_TMS.Business.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_for_TMS.Api;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController(AccountService accountService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetInfo()
    {
        return Ok(new { message = "This is public data" });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResult>> RefreshToken([FromBody] TokenRequestDto dto)
    {
        var result = await accountService.RefreshToken(dto);
        if (!result.Success) return Unauthorized(result);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterDto dto)
    {
        var result = await accountService.Register(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginDto dto)
    {
        var result = await accountService.Login(dto);
        if (!result.Success) return Unauthorized(result);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserProfile>> GetMe()
    {
        // 'User' là property có sẵn của ControllerBase, kiểu ClaimsPrincipal
        var profile = await accountService.GetProfile(User);

        if (profile == null) return NotFound();
        return Ok(profile);
    }
}
