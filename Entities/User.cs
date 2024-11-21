using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace RS1_2024_25.API.Data.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username {get; set;}
    public string Email { get; set; }
    public string Password {get; set;}
    public string Gender {get; set;}
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string ProfilePicture { get; set; }
    public bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}