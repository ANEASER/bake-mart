namespace BakeMart.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Role { get; set; }
    public required string Address { get; set; }
    public required string Password { get; set; }
    public required string ActiveStatus { get; set; }
    public required string ImageLink { get; set; }
}
