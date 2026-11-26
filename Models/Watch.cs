namespace PMS.Models;

public class Watch
{
    public int Id { get; set; }

    public required string Brand { get; set; }

    public required string Model { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Description { get; set; }
}
