namespace PayloadApp.Models
{
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
}