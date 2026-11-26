# PMS - Elite Watches API

A beginner-friendly ASP.NET Core REST API for managing an elite watch inventory.
It uses controllers, Entity Framework Core, and a local SQLite database.

## Run the API

From this folder, run:

```bash
dotnet run
```

The API uses `http://localhost:5265` with the included launch profile. The SQLite
database (`pms.db`) is created in this folder the first time the app starts and
is seeded with two example watches. Edit the connection string in
`appsettings.json` to change the database location.

In Development, the OpenAPI document is available at
`http://localhost:5265/openapi/v1.json`. The `PMS.http` file contains requests
you can run from the VS Code REST Client extension.

## Routes

| Method | Route | Success response |
| --- | --- | --- |
| GET | `/api/watches` | `200 OK` with a list |
| GET | `/api/watches/{id}` | `200 OK` with one watch |
| POST | `/api/watches` | `201 Created` with the new watch and its `Location` |
| PUT | `/api/watches/{id}` | `204 No Content` |
| DELETE | `/api/watches/{id}` | `204 No Content` |

Unknown IDs return `404 Not Found` with a Problem Details response. Invalid
request bodies return `400 Bad Request` with validation details.

## Example

Send this JSON body to `POST /api/watches`:

```json
{
  "brand": "Seiko",
  "model": "Presage",
  "price": 499.99,
  "stockQuantity": 8,
  "description": "Automatic dress watch with a cream dial."
}
```

Brand and model are required. Prices must be greater than zero, stock quantity
cannot be negative, and text fields have maximum lengths.

The database uses `EnsureCreated` for this small starter project. For a larger
project or when changing the schema, use EF Core migrations.
