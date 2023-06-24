use PayloadInfo;

CREATE TABLE Payload (
    Id INT,
    DeviceId VARCHAR(255),
    DeviceType VARCHAR(255),
    DeviceName VARCHAR(255),
    GroupId VARCHAR(255),
    DataType VARCHAR(255),
    FullPowerMode BIT,
    ActivePowerControl BIT,
    FirmwareVersion INT,
    Temperature INT,
    Humidity INT,
    Version INT,
    MessageType VARCHAR(255),
    Occupancy BIT,
    StateChanged INT,
    Timestamp DATETIME
);
