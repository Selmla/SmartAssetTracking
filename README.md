# Smart Asset Tracking System

A C# .NET console application for tracking company assets across multiple offices. Built as a school project focused on learning Entity Framework Core.

## Features

- Add, update, delete and search assets
- Assets are linked to offices with different currencies
- Purchase price stored in USD, displayed in local currency
- Color-coded asset status based on age (approaching end-of-life)
- Reports: asset count per office, most expensive assets

## Tech stack

- .NET 10
- Entity Framework Core 10 with SQLite
- LINQ
- Service structure

## Project structure

```
SmartAssetTracking/
├── Data/
│   └── AppDbContext.cs       # EF Core database context
├── Models/
│   ├── Asset.cs              # Base asset model
│   ├── ComputerAsset.cs      # Inherits Asset
│   ├── MobileAsset.cs        # Inherits Asset
│   └── Office.cs             # Office model with currency and exchange rate
├── Services/
│   ├── AssetService.cs       # CRUD + search + reports
│   └── OfficeService.cs      # Office seeding and retrieval
├── Migrations/               # EF Core migrations
└── Program.cs                # Main menu and user interaction
```

## Getting started

**Prerequisites:** .NET 10 SDK

```bash
git clone <repo-url>
cd SmartAssetTracking
dotnet run
```

The SQLite database (`assets.db`) is created automatically on first run. Offices are seeded automatically.

## Menu options

```
1. Add asset
2. Show all assets
3. Delete asset
4. Update asset
5. Search assets
6. Reports
0. Exit
```

## Asset status colors

Assets are color-coded based on time since purchase (3-year lifespan):

| Color  | Meaning                        |
|--------|--------------------------------|
| Red    | Less than 3 months remaining   |
| Yellow | Less than 6 months remaining   |
| White  | More than 6 months remaining   |

## Offices and currencies

The system comes pre-loaded with four offices:

| Office          | Currency | Exchange rate (vs USD) |
|-----------------|----------|------------------------|
| Sweden Office   | SEK      | 10.50                  |
| USA Office      | USD      | 1.00                   |
| Germany Office  | EUR      | 0.92                   |
| Turkey Office   | TRY      | 32.00                  |
