using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Data.Models;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [ForeignKey(nameof(Vehicle))]
    public int VehicleID { get; set; }
    public Vehicle? Vehicle { get; set; }
    
    [ForeignKey(nameof(Locations))]
    public int LocationId { get; set; }
    public Locations? Locations { get; set; }
    
    public DateTime ReservationDate { get; set; }   
    public DateTime PickupDate { get; set; }
    public DateTime DropoffDate { get; set; }
    public decimal TotalAmmount { get; set; }
    public string Status { get; set; }
}