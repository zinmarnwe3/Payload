using PayloadApp.Contracts;
using PayloadApp.Models;
using PayloadApp.ViewModels;

namespace PayloadApp.Services
{
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
}