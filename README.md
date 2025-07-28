# NZWalks API

**NZWalks API** is a dynamic .NET 8.0 backend crafted to fuel adventure, connecting outdoor enthusiasts with New Zealand's breathtaking walking trails. This robust API powers seamless management of trail and region data, secure user authentication, and vibrant image uploads, all backed by SQL Server. Whether you're exploring iconic routes or contributing new trails with stunning visuals, NZWalks API is your ultimate companion for discovering New Zealand's natural wonders.

## App Idea

NZWalks API is designed to be the backbone of an immersive platform celebrating New Zealand’s diverse landscapes, from the rugged peaks of the Southern Alps to the serene coastal paths of the Bay of Plenty. It enables users to explore detailed trail information, including difficulty levels, distances, and regional highlights, while supporting rich media uploads to showcase scenic beauty. With role-based access, adventurers (`Reader` role) can browse trails, while contributors (`Writer` role) can add or update trail data, fostering a community-driven hub for outdoor exploration. Whether integrated into a mobile app or web platform, NZWalks API empowers users to plan their next hike, share experiences, and preserve the spirit of New Zealand’s wild places.

## Features

- **Dynamic Trail Management**: Perform CRUD operations on walks with advanced filtering by name, distance, difficulty, region, or region code, plus sorting and pagination for tailored exploration.
- **Region Exploration**: Manage New Zealand regions (e.g., Auckland, Waikato) with filtering by name or code and flexible sorting options.
- **Secure Authentication**: JWT-based authentication with `Reader` and `Writer` roles ensures controlled access to data and actions.
- **Vivid Image Uploads**: Upload trail or region images (`.jpg`, `.jpeg`, `.png`, up to 10MB) to enrich the visual experience.
- **Robust Logging**: Serilog captures detailed logs to console and daily files (`Logs/NzWalks_Log.txt`) for seamless debugging.
- **Interactive API Docs**: Swagger UI offers intuitive endpoint exploration in development mode.

## Tech Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **Key Libraries**:
  - AutoMapper for seamless DTO mapping
  - Microsoft.AspNetCore.Identity for secure authentication
  - Serilog for advanced logging
  - Swashbuckle for Swagger-powered API documentation
- **Tools**: Visual Studio 2022, .NET CLI

## API Endpoints

- **Authentication**:
  - `POST /api/Auth/Register`: Register users with username, password, and optional roles.
  - `POST /api/Auth/Login`: Authenticate and receive a JWT token.

- **Regions**:
  - `GET /api/Regions`: List all regions with optional filtering (`filterOn`, `filterQuery`) and sorting (`isAscending`).
  - `GET /api/Regions/{id:Guid}`: Retrieve a region by ID.
  - `POST /api/Regions`: Create a new region (requires `Writer` role).
  - `PUT /api/Regions/{id:Guid}`: Update a region (requires `Writer` role).
  - `DELETE /api/Regions/{id}`: Delete a region (requires `Writer` role).

- **Walks**:
  - `GET /api/Walks`: Fetch walks with filtering (`filterOn`, `filterQuery`), sorting (`sortBy`, `isAscending`), and pagination (`pageNumber`, `pageSize`).
  - `GET /api/Walks/{id:Guid}`: Get a walk by ID.
  - `POST /api/Walks`: Create a new walk (requires `Writer` role).
  - `PUT /api/Walks/{id:Guid}`: Update a walk (requires `Writer` role).
  - `DELETE /api/Walks/{id:Guid}`: Delete a walk (requires `Writer` role).

- **Images**:
  - `POST /api/Images/Upload`: Upload images for walks or regions (`.jpg`, `.jpeg`, `.png`, max 10MB).

## Configuration

- **Database**: Configure connection strings in `appsettings.json` for `NZWalksDb` (trail data) and `NZWalksAuthDb` (authentication).
- **JWT**: Update `Jwt:Key`, `Issuer`, and `Audience` in `appsettings.json` (secure `Key` in production).
- **Logging**: Customize Serilog settings in `Program.cs` for log levels or outputs.
- **Images**: Ensure the `Images` folder exists and is writable for uploads.

## Security Notes

- **JWT Key**: Store `Jwt:Key` in a secure vault (e.g., Azure Key Vault) for production environments.
- **Authorization**: Enable `[Authorize]` attributes in controllers to enforce `Reader`/`Writer` role-based access.
- **Database**: Use `TrustServerCertificate=True` only in development; configure SSL properly for production.

## Contributing

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/your-feature`).
3. Commit changes (`git commit -m "Add your feature"`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Submit a pull request.

## License

Licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
