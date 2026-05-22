namespace SmartAssetTracking.Models;

public class Asset
{
    public int Id { get; set; }

    public string AssetType { get; set; }
    public string Brand { get; set; }
    public string ModelName { get; set; }

    public DateTime PurchaseDate { get; set; }
    public decimal PurchasePriceUsd { get; set; }

    public string SerialNumber { get; set; }
    public string? EmployeeUsername { get; set; }
    
    public int OfficeId { get; set; }
    public Office? Office { get; set; }

    public Asset(
        string assetType,
        string brand,
        string modelName,
        DateTime purchaseDate,
        decimal purchasePriceUsd,
        string serialNumber,
        string? employeeUsername = null)
    {
        AssetType = assetType;
        Brand = brand;
        ModelName = modelName;
        PurchaseDate = purchaseDate;
        PurchasePriceUsd = purchasePriceUsd;
        SerialNumber = serialNumber;
        EmployeeUsername = employeeUsername;
    }

    public Asset()
    {
        AssetType = "";
        Brand = "";
        ModelName = "";
        SerialNumber = "";
    }

    public string Status
    {
        get
        {
            var endOfLife = PurchaseDate.AddYears(3);
            var monthsLeft = (endOfLife - DateTime.Now).Days / 30;

            if (monthsLeft < 3)
                return "RED";

            if (monthsLeft < 6)
                return "YELLOW";

            return "NORMAL";
        }
    }

}