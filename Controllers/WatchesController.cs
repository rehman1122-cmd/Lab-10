using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;

namespace PMS.Controllers;

[ApiController]
[Route("api/watches")]
public class WatchesController(PmsDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Watch>>> GetWatches()
    {
        return await db.Watches
            .AsNoTracking()
            .OrderBy(watch => watch.Id)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Watch>> GetWatch(int id)
    {
        var watch = await db.Watches.AsNoTracking().FirstOrDefaultAsync(watch => watch.Id == id);
        if (watch is null)
        {
            return WatchNotFound(id);
        }

        return watch;
    }

    [HttpPost]
    public async Task<ActionResult<Watch>> CreateWatch(WatchRequest request)
    {
        var watch = new Watch
        {
            Brand = request.Brand,
            Model = request.Model,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Description = request.Description
        };

        db.Watches.Add(watch);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWatch), new { id = watch.Id }, watch);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateWatch(int id, WatchRequest request)
    {
        var watch = await db.Watches.FindAsync(id);
        if (watch is null)
        {
            return WatchNotFound(id);
        }

        watch.Brand = request.Brand;
        watch.Model = request.Model;
        watch.Price = request.Price;
        watch.StockQuantity = request.StockQuantity;
        watch.Description = request.Description;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWatch(int id)
    {
        var watch = await db.Watches.FindAsync(id);
        if (watch is null)
        {
            return WatchNotFound(id);
        }

        db.Watches.Remove(watch);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private ObjectResult WatchNotFound(int id)
    {
        return Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Watch not found",
            detail: $"No watch with ID {id} exists.");
    }
}
