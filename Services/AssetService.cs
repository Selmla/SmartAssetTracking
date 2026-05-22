using SmartAssetTracking.Data;
using SmartAssetTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartAssetTracking.Services;

public class AssetService
{
    private readonly AppDbContext _context = new();

    public void AddAsset(Asset asset)
    {
        _context.Assets.Add(asset);
        _context.SaveChanges();
    }

    public void DeleteAsset(int id)
    {
        var asset = _context.Assets.FirstOrDefault(a => a.Id == id);

        if (asset != null)
        {
            _context.Assets.Remove(asset);
            _context.SaveChanges();
        }
    }

    public bool UpdateAsset(
    int id,
    string newAssetType,
    string newBrand,
    string newModelName,
    DateTime newPurchaseDate,
    decimal newPurchasePriceUsd,
    string newSerialNumber,
    string? newEmployeeUsername,
    int officeId)
    {
        var asset = _context.Assets.FirstOrDefault(a => a.Id == id);

        if (asset == null)
        {
            return false;
        }

        asset.AssetType = newAssetType;
        asset.Brand = newBrand;
        asset.ModelName = newModelName;
        asset.PurchaseDate = newPurchaseDate;
        asset.PurchasePriceUsd = newPurchasePriceUsd;
        asset.SerialNumber = newSerialNumber;
        asset.EmployeeUsername = newEmployeeUsername;
        asset.OfficeId = officeId;

        _context.SaveChanges();

        return true;
    }

    public List<Asset> SearchAssets(string searchTerm)
    {
        return _context.Assets
            .Include(a => a.Office)
            .Where(a =>
                a.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                a.ModelName.ToLower().Contains(searchTerm.ToLower()) ||
                a.AssetType.ToLower().Contains(searchTerm.ToLower()))
            .OrderBy(a => a.AssetType)
            .ThenBy(a => a.PurchaseDate)
            .ToList();
    }

    public List<Asset> GetAllAssets()
    {
        return _context.Assets
            .Include(a => a.Office)
            .OrderBy(a => a.AssetType)
            .ThenBy(a => a.PurchaseDate)
            .ToList();
    }

    public List<(string OfficeName, int Count)> GetAssetCountPerOffice()
    {
        return _context.Assets
            .Include(a => a.Office)
            .AsEnumerable()
            .GroupBy(a => a.Office!.OfficeName)
            .Select(g => (OfficeName: g.Key, Count: g.Count()))
            .OrderByDescending(x => x.Count)
            .ToList();
    }

    public List<Asset> GetMostExpensiveAssets(int count = 5)
    {
        return _context.Assets
            .Include(a => a.Office)
            .OrderByDescending(a => a.PurchasePriceUsd)
            .Take(count)
            .ToList();
    }
}