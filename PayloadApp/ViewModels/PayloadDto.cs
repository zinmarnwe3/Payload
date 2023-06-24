namespace PayloadApp.ViewModels
{
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
}