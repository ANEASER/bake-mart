namespace BakeMart.Entities;

public class Promotion
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public required string Description { get; set; }
}