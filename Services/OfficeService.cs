using SmartAssetTracking.Data;
using SmartAssetTracking.Models;

namespace SmartAssetTracking.Services;

public class OfficeService
{
    private readonly AppDbContext _context = new();

    public void SeedOffices()
    {
        if (_context.Offices.Any())
        {
            return;
        }

        var offices = new List<Office>
        {
            new Office("Sweden Office", "Sweden", "SEK", 10.50m),
            new Office("USA Office", "USA", "USD", 1.00m),
            new Office("Germany Office", "Germany", "EUR", 0.92m),
            new Office("Turkey Office", "Turkey", "TRY", 32.00m)
        };

        _context.Offices.AddRange(offices);
        _context.SaveChanges();
    }

    public List<Office> GetAllOffices()
    {
        return _context.Offices.ToList();
    }
}