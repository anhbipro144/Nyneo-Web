namespace Nyneo_Web.Models;

public class LoginUser
{

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? returnUrl { get; set; }

}
