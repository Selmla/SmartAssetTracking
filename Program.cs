using SmartAssetTracking.Models;
using SmartAssetTracking.Services;

var assetService = new AssetService();
var officeService = new OfficeService();

officeService.SeedOffices();

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\n=== Smart Asset Tracking System ===");
    Console.WriteLine("1. Add asset");
    Console.WriteLine("2. Show all assets");
    Console.WriteLine("3. Delete asset");
    Console.WriteLine("4. Update asset");
    Console.WriteLine("5. Search assets");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddAsset();
            break;

        case "2":
            ShowAllAssets();
            break;

        case "3":
            DeleteAsset();
            break;

        case "4":
            UpdateAsset();
            break;

        case "5":
            SearchAssets();
            break;

        case "0":
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

void AddAsset()
{
    string assetType = ReadRequiredString("Enter asset type: ");
    string brand = ReadRequiredString("Enter brand: ");
    string modelName = ReadRequiredString("Enter model name: ");

    decimal purchasePrice = ReadDecimal("Enter purchase price in USD: ");

    string serialNumber = ReadRequiredString("Enter serial number: ");

    DateTime purchaseDate = ReadDate("Enter purchase date (yyyy-mm-dd): ");

    var offices = officeService.GetAllOffices();

    Console.WriteLine("Choose office:");

    foreach (var office in offices)
    {
        Console.WriteLine($"{office.Id}. {office.OfficeName} ({office.Currency})");
    }

    int officeId = ReadInt("Enter office ID: ");

    var asset = new ComputerAsset(
        assetType,
        brand,
        modelName,
        purchaseDate,
        purchasePrice,
        serialNumber
    );

    asset.OfficeId = officeId;

    assetService.AddAsset(asset);

    Console.WriteLine("Asset added successfully!");
}

void ShowAllAssets()
{
    var assets = assetService.GetAllAssets();

    foreach (var asset in assets)
    {
        switch (asset.Status)
        {
            case "RED":
                Console.ForegroundColor = ConsoleColor.Red;
                break;

            case "YELLOW":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;

            default:
                Console.ResetColor();
                break;
        }

        var localPrice = asset.PurchasePriceUsd * asset.Office!.ExchangeRate;

        Console.WriteLine($"{asset.Id} | {asset.AssetType} | {asset.Brand} | {asset.ModelName} | {asset.PurchaseDate:yyyy-MM-dd} | ${asset.PurchasePriceUsd:F2} | {localPrice:F2} {asset.Office.Currency} | {asset.Office.OfficeName}");

        Console.ResetColor();
    }
}

DateTime ReadDate(string message)
{
    DateTime date;

    while (true)
    {
        Console.Write(message);

        string? input = Console.ReadLine();

        if (DateTime.TryParse(input, out date))
        {
            return date;
        }

        Console.WriteLine("Invalid date. Please use yyyy-mm-dd.");
    }
}

void DeleteAsset()
{
    int id = ReadInt("Enter asset ID to delete: ");

    assetService.DeleteAsset(id);

    Console.WriteLine("Asset deleted successfully!");
}

void UpdateAsset()
{
    int id = ReadInt("Enter asset ID to update: ");

    string assetType = ReadRequiredString("Enter new asset type: ");
    string brand = ReadRequiredString("Enter new brand: ");
    string modelName = ReadRequiredString("Enter new model name: ");
    DateTime purchaseDate = ReadDate("Enter new purchase date (yyyy-mm-dd): ");
    decimal purchasePrice = ReadDecimal("Enter new purchase price in USD: ");
    string serialNumber = ReadRequiredString("Enter new serial number: ");

    Console.Write("Enter employee username (optional): ");
    string? employeeUsername = Console.ReadLine();
    var offices = officeService.GetAllOffices();

    Console.WriteLine("Choose new office:");

    foreach (var office in offices)
    {
        Console.WriteLine($"{office.Id}. {office.OfficeName} ({office.Currency})");
    }

    int officeId = ReadInt("Enter office ID: ");

    bool success = assetService.UpdateAsset(
        id,
        assetType,
        brand,
        modelName,
        purchaseDate,
        purchasePrice,
        serialNumber,
        employeeUsername,
        officeId
    );

    if (success)
    {
        Console.WriteLine("Asset updated successfully!");
    }
    else
    {
        Console.WriteLine("Asset not found.");
    }
}

void SearchAssets()
{
    string searchTerm = ReadRequiredString("Enter search term: ");

    var results = assetService.SearchAssets(searchTerm);

    foreach (var asset in results)
    {
        switch (asset.Status)
        {
            case "RED":
                Console.ForegroundColor = ConsoleColor.Red;
                break;

            case "YELLOW":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;

            default:
                Console.ResetColor();
                break;
        }

        var localPrice = asset.PurchasePriceUsd * asset.Office!.ExchangeRate;

        Console.WriteLine($"{asset.Id} | {asset.AssetType} | {asset.Brand} | {asset.ModelName} | {asset.PurchaseDate:yyyy-MM-dd} | ${asset.PurchasePriceUsd:F2} | {localPrice:F2} {asset.Office.Currency} | {asset.Office.OfficeName}");

        Console.ResetColor();
    }

    if (!results.Any())
    {
        Console.WriteLine("No assets found.");
    }
}

decimal ReadDecimal(string message)
{
    decimal value;

    while (true)
    {
        Console.Write(message);
        string? input = Console.ReadLine();

        if (decimal.TryParse(input, out value))
        {
            return value;
        }

        Console.WriteLine("Invalid number. Please try again.");
    }
}

string ReadRequiredString(string message)
{
    while (true)
    {
        Console.Write(message);

        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        Console.WriteLine("Input cannot be empty.");
    }
}

int ReadInt(string message)
{
    int value;

    while (true)
    {
        Console.Write(message);

        string? input = Console.ReadLine();

        if (int.TryParse(input, out value))
        {
            return value;
        }

        Console.WriteLine("Invalid number. Please try again.");
    }
}