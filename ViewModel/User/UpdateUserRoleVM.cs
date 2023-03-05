namespace Nyneo_Web.Models;

public class UpdateUserRoleVM
{
    public User? user { get; set; }

    public IEnumerable<Role>? roleList { get; set; }

    public string? roleName { get; set; }
}

