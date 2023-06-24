using PayloadApp.ViewModels;

namespace PayloadApp.Contracts
{
    public interface IPayloadService
    {
        Task<bool> StorePayload(PayloadDto payloadDto);
    }
}