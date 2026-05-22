namespace SmartAssetTracking.Models;

public class Office
{
    public int Id { get; set; }

    public string OfficeName { get; set; }
    public string Country { get; set; }
    public string Currency { get; set; }

    public decimal ExchangeRate { get; set; }

    public List<Asset> Assets { get; set; }

    public Office(
        string officeName,
        string country,
        string currency,
        decimal exchangeRate)
    {
        OfficeName = officeName;
        Country = country;
        Currency = currency;
        ExchangeRate = exchangeRate;

        Assets = new List<Asset>();
    }

    public Office()
    {
        OfficeName = "";
        Country = "";
        Currency = "";

        Assets = new List<Asset>();
    }
}