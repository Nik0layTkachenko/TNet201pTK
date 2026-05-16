using System;
using System.Collections.Generic;

namespace SmartHome.Infrastructure.Models
{
    public class RoomModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<DeviceModel> Devices { get; set; } = new List<DeviceModel>();
    }

    public abstract class DeviceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public Guid? RoomId { get; set; }
        public RoomModel Room { get; set; }
        public ICollection<UserGroupModel> UserGroups { get; set; } = new List<UserGroupModel>();
    }

    public class LightBulbModel : DeviceModel
    {
        public int Brightness { get; set; }
        public string Color { get; set; }
        public BulbSettingsModel Settings { get; set; }
    }

    public class BulbSettingsModel
    {
        public Guid Id { get; set; }
        public string PowerSavingMode { get; set; }
        public Guid LightBulbId { get; set; }
        public LightBulbModel LightBulb { get; set; }
    }

    public class ThermostatModel : DeviceModel
    {
        public double TargetTemperature { get; set; }
    }

    public class UserGroupModel
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<DeviceModel> Devices { get; set; } = new List<DeviceModel>();
    }
}