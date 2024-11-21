using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models;

public class Admin
{
    [Key]
    public int AdminId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
}