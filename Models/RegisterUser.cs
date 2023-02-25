using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nyneo_Web.Models;

public class RegisterUser
{
    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;


    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;


    [Required]
    [DisplayName("User Name")]
    public string Username { get; set; } = null!;


    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{8,20}$", ErrorMessage = "Mật khẩu ít nhất 8 kí tự, phải có 1 kí tự đặc biệt, chữ số và kí tự viết hoa")]
    public string Password { get; set; } = null!;


    [Required(ErrorMessage = "Password confirmation can not be empty")]
    [DisplayName("Confirm Password")]
    [PasswordPropertyText]
    [Compare("Password", ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;



    [Required]
    public string Gender { get; set; } = null!;


    [Required]
    public string Email { get; set; } = null!;

}
