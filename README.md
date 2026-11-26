# ApplicationAPIDemo

An ASP.NET Core Web API that stores and serves the data for the budgetSystem application: members, dependents, assets and finances. It reads and writes a SQL Server database through Entity Framework Core and exchanges JSON over HTTPS.

## Tech stack

| Part | Technology |
| --- | --- |
| Framework | ASP.NET Core Web API (C#) |
| Data access | Entity Framework Core |
| Database | SQL Server Express |
| API description | `AddOpenApi()` (JSON document at `/openapi/v1.json`) |

### NuGet packages

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.AspNetCore.OpenApi`

## Project structure

```
ApplicationAPIDemo/
├── Controllers/     Members, Assets, Dependents (HTTP endpoints)
├── Models/          Entity classes that become database tables
├── Data/            DatabaseContext (the link to the database)
├── Migrations/      History of the database structure
├── Program.cs       Service registration and request pipeline
└── appsettings.json Configuration
```

## Requirements

- .NET SDK (the version this project targets)
- SQL Server Express and SQL Server Management Studio (SSMS)
- Visual Studio 2022 with the **ASP.NET and web development** workload, or the `dotnet` command line

## Setup

### 1. Configure the connection string

`appsettings.json` is committed with a placeholder. Put your real connection string in a file named `appsettings.Development.json` in the project folder. That file is not committed.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_PC\\SQLEXPRESS;Initial Catalog=CommunityDB;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

- The section name must be exactly `ConnectionStrings` and the key exactly `DefaultConnection`. `Program.cs` looks for those names.
- The double backslash is required in JSON.

### 2. Create the database

In Visual Studio open **Tools → NuGet Package Manager → Package Manager Console**, set **Default project** to `ApplicationAPIDemo` and run:

```
Update-Database
```

Or from a terminal in the project folder:

```
dotnet ef database update
```

This creates the database and all tables from the files in `Migrations/`.

To reset the database (this deletes all data):

```
Drop-Database
Update-Database
```

### 3. Run the API

In Visual Studio: set `ApplicationAPIDemo` as the startup project, choose the **https** profile and press **F5**.

From a terminal:

```
dotnet run --launch-profile https
```

The console prints the address, for example:

```
Now listening on: https://localhost:7278
```

Use the port your console shows. Stop the API with **Shift+F5** in Visual Studio or **Ctrl+C** in the terminal, and restart it after every code change.

## Endpoints

Base address: `https://localhost:7278`

Adjust this table to match your controllers.

### Members

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/Members` | List all members |
| POST | `/api/Members/AddMember` | Add a member |
| PUT | `/api/Members/UpdateMember/{keylink}` | Update a member |
| DELETE | `/api/Members/DeleteMember/{keylink}` | Delete a member and their dependents |

### Dependents

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/Dependents` | List all dependents |
| GET | `/api/Dependents/ByLink/{link}` | List the dependents of one member |

### Assets

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/Assets` | List all assets |
| POST | `/api/Assets/AddAssets` | Add an asset |
| DELETE | `/api/Assets/DeleteAsset/{name}` | Delete an asset by name |

## Data format

JSON property names are not case-sensitive, but they must match the model's property names.

### Member

```json
{
  "name": "Thabo",
  "surname": "Nkosi",
  "id": "1002145123457",
  "phonenumber": "0839876543",
  "email": "thabo@example.com",
  "address": "45 Church St, Pretoria",
  "dob": "2010-02-14",
  "jod": "2026-02-01",
  "keylink": "def456uvw"
}
```

`keylink` links a member to their dependents. Send dates as `yyyy-MM-dd`.

### Asset

```json
{
  "name": "Laptop",
  "amount": "15000"
}
```

`amount` is currently stored as text. Change it to `decimal` in the model if you need to total or compare it, then add a migration.

## Testing

A browser can only send GET requests, so a browser gives `405 Method Not Allowed` on POST, PUT and DELETE endpoints. Use Postman, a `.http` file in Visual Studio, or `curl.exe`.

Add an asset (PowerShell):

```
'{"name":"Laptop","amount":"15000"}' | Out-File -Encoding ascii asset.json
curl.exe -k -i -X POST https://localhost:7278/api/Assets/AddAssets -H "Content-Type: application/json" -d "@asset.json"
```

Delete an asset:

```
curl.exe -k -i -X DELETE https://localhost:7278/api/Assets/DeleteAsset/Laptop
```

`-k` skips the development certificate warning and `-i` prints the status code.

## Status codes

| Code | Meaning |
| --- | --- |
| 200 | Success, with data returned |
| 204 | Success, nothing to return (deletes) |
| 400 | The JSON does not match the model. The response body names the field. |
| 404 | No route or no row matches |
| 405 | The route exists but not for that HTTP method |
| 415 | The `Content-Type: application/json` header is missing |
| 500 | Server or database error. Read the console output. |

## Troubleshooting

| Problem | Likely cause |
| --- | --- |
| `The ConnectionString property has not been initialized` | `ConnectionStrings` or `DefaultConnection` is misspelled, or the JSON in `appsettings` is invalid |
| `A suitable constructor ... could not be located` | A controller constructor is `private`. Make it `public`. |
| `Unable to resolve service for type DbContextOptions` | `DatabaseContext` must take `DbContextOptions<DatabaseContext>` and be registered with `AddDbContext<DatabaseContext>` |
| `Violation of PRIMARY KEY constraint` | A text field such as a name is the primary key. Use an auto-generated `Id`. |
| Port already in use | An old copy of the API is still running. End it in Task Manager. |
| Changes have no effect | The API was not restarted after the code change |

## Notes

- Never commit real member data. It contains ID numbers and phone numbers.
- Do not commit `bin`, `obj`, `.vs`, database files or `appsettings.Development.json`.
- Keep the repository private while the project is in development.
