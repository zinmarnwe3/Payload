Payload System
===
This system demonstrates a clean code structure and documentation for building a simple API system with .NET Core. The system receives payloads through a HTTP POST request, stores the main information in a database, handles exceptions, and implements logging. The project is set up to use MSSQL as the database.

## Project Structure
The project structure follows a typical layered architecture:
```
|—— PayloadApp
|    |—— appsettings.Development.json
|    |—— appsettings.json
|    |—— Contracts
|        |—— IPayloadService.cs
|    |—— Controllers
|        |—— PayloadController.cs
|    |—— Logs
|        |—— log-20230624.txt
|    |—— Migrations
|        |—— 20230624121502_InitialCreate.cs
|        |—— 20230624121502_InitialCreate.Designer.cs
|        |—— PayloadDbContextModelSnapshot.cs
|    |—— Models
|        |—— Payload.cs
|        |—— PayloadDbContext.cs
|    |—— PayloadApp.csproj
|    |—— Program.cs
|    |—— Properties
|        |—— launchSettings.json
|    |—— Services
|        |—— PayloadService.cs
|    |—— ViewModels
|        |—— PayloadDto.cs
|—— PayloadApp.sln
```
## Installation and Setup

- Install .NET Core SDK: https://dotnet.microsoft.com/download
- Clone the project repository: git clone https://github.com/zinmarnwe3/Payload.git
- Configure the database connection in appsettings.json.
- Open a terminal or command prompt and navigate to the project directory.
- Run the following commands to apply EF migrations and update the database schema:

### sql
```
dotnet tool install --global dotnet-ef
dotnet ef database update
```
Note: Make sure you have the necessary permissions to create and update the database.
- Build and run the project: dotnet run

By following these steps, the necessary EF migrations will be applied to create the required database schema.

### appsettings.json
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost\\sqlexpress;Initial Catalog=PayloadInfo;User ID=sa;Password=PeaceRose9@;TrustServerCertificate=True"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log-.txt",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}
```

### Contracts/IPayloadService.cs
```
public interface IPayloadService
{
    Task<bool> StorePayload(PayloadDto payloadDto);
}
```

### Controllers/PayloadController.cs
```
[Route("api/[controller]")]
[ApiController]
public class PayloadController : ControllerBase
{
    private readonly ILogger<PayloadController> _logger;
    private readonly IPayloadService _payloadService;

    public PayloadController(ILogger<PayloadController> logger, IPayloadService payloadService)
    {
        _logger = logger;
        _payloadService = payloadService;
    }

    [HttpPost]
    public async Task<IActionResult> ReceivePayload(PayloadDto payloadDto)
    {
        var result = await _payloadService.StorePayload(payloadDto);

        if (result)
        {
            return Ok();
        }
        else
        {
            return StatusCode(500, "An error occurred while storing the payload.");
        }
    }
}
```

### Models/Payload.cs
```
public class Payload
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public bool FullPowerMode { get; set; }
    public bool ActivePowerControl { get; set; }
    public int FirmwareVersion { get; set; }
    public int Temperature { get; set; }
    public int Humidity { get; set; }
    public int Version { get; set; }
    public string MessageType { get; set; } = string.Empty;
    public bool Occupancy { get; set; }
    public int StateChanged { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### Models/PayloadDbContext.cs
```
public class PayloadDbContext : DbContext
{
    public PayloadDbContext(DbContextOptions<PayloadDbContext> options) : base(options)
    {
    }

    public DbSet<Payload> Payloads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the table name and primary key
        modelBuilder.Entity<Payload>().ToTable("Payload");
        modelBuilder.Entity<Payload>().HasKey(p => p.Id);
    }
}
```

### Program.cs
```
var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddDbContext<PayloadDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IPayloadService, PayloadService>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### Properties/launchSettings.json
```
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:22090",
      "sslPort": 44363
    }
  },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5044",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7060;http://localhost:5044",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}

```

### Services/PayloadService.cs
```
public class PayloadService : IPayloadService
{
    private readonly ILogger<PayloadService> _logger;
    private readonly PayloadDbContext _dbContext;

    public PayloadService(ILogger<PayloadService> logger, PayloadDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<bool> StorePayload(PayloadDto payloadDto)
    {
        try
        {
            var payload = new Payload
            {
                DeviceId = payloadDto.DeviceId,
                DeviceType = payloadDto.DeviceType,
                DeviceName = payloadDto.DeviceName,
                GroupId = payloadDto.GroupId,
                DataType = payloadDto.DataType,
                FullPowerMode = payloadDto.Data.FullPowerMode,
                ActivePowerControl = payloadDto.Data.ActivePowerControl,
                FirmwareVersion = payloadDto.Data.FirmwareVersion,
                Temperature = payloadDto.Data.Temperature,
                Humidity = payloadDto.Data.Humidity,
                Version = payloadDto.Data.Version,
                MessageType = payloadDto.Data.MessageType,
                Occupancy = payloadDto.Data.Occupancy,
                StateChanged = payloadDto.Data.StateChanged,
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(payloadDto.Timestamp).UtcDateTime
            };

            _dbContext.Payloads.Add(payload);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Payload received and stored successfully: {DeviceId}", payloadDto.DeviceId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while storing payload: {DeviceId}", payloadDto.DeviceId);
            return false;
        }
    }
}
```

### ViewModels/PayloadDto.cs
```
public class PayloadDto
{
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public PayloadDataDto Data { get; set; } = new PayloadDataDto();
    public long Timestamp { get; set; }
}

public class PayloadDataDto
{
    public bool FullPowerMode { get; set; }
    public bool ActivePowerControl { get; set; }
    public int FirmwareVersion { get; set; }
    public int Temperature { get; set; }
    public int Humidity { get; set; }
    public int Version { get; set; }
    public string MessageType { get; set; } = string.Empty;
    public bool Occupancy { get; set; }
    public int StateChanged { get; set; }
}
```

## API Endpoints
### Payloads
- Method: POST

- Endpoint: /api/Payload

Description: Receive the payload and store the main information (temperature, humidity, occupancy).

Request Payload Example:
```
{
  "deviceId": "ibm-878A66",
  "deviceType": "computer1.0.0",
  "deviceName": "VN1-1-3",
  "groupId": "847b3b2f1b05dc4",
  "dataType": "DATA",
  "data": {
    "fullPowerMode": false,
    "activePowerControl": false,
    "firmwareVersion": 1,
    "temperature": 21,
    "humidity": 53
  },
  "timestamp": 1629369697
}
```
## Exception Handling
- Exception handling is implemented using the ExceptionHandler middleware. It captures and logs any exceptions that occur during API execution and returns appropriate error responses.

## Logging
- Logging is implemented using the Logger class. It captures important events, errors, and other relevant information during the API execution and outputs them to the console or a configured log file.

## Testing
- To test the API, you can use tools like Postman or curl. Send a POST request to the /api/Payload endpoint with the payload in the request body.

- Make sure to provide valid values for each field in the payload, including deviceId, deviceType, deviceName, groupId, dataType, data (with temperature, humidity, and other required properties), and timestamp.

- Verify that the API returns a successful response (HTTP 200 OK) and check the database to confirm that the payload information has been stored correctly.
