namespace BakeMart.Entities;

public class Item{
    public int Id { get; set; }
    public required string ItemName { get; set; }
    public int Price { get; set; }
    public required string Description { get; set; }
    public required string ImageLink { get; set; }
}