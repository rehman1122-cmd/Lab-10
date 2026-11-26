using System.ComponentModel.DataAnnotations;

namespace PMS.Models;

public class WatchRequest
{
    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Model { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "10000000")]
    public decimal Price { get; set; }

    [Range(0, 100000)]
    public int StockQuantity { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}
