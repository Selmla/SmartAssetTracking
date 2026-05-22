namespace SmartAssetTracking.Models;

public class ComputerAsset : Asset
{
    public ComputerAsset(
        string assetType,
        string brand,
        string modelName,
        DateTime purchaseDate,
        decimal purchasePriceUsd,
        string serialNumber,
        string? employeeUsername = null)
        : base(
            assetType,
            brand,
            modelName,
            purchaseDate,
            purchasePriceUsd,
            serialNumber,
            employeeUsername)
    {
    }

    public ComputerAsset() : base()
    {
    }
}