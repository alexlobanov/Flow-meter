namespace FlowMeterLibr.TO
{
    public enum FlowCommands
    {
        FactoryReset = 2,
        SaveAllSettings2memory = 3,
        DiveceError2Usb = 4,
        MainCfg = 5,
        USmetrVariablesCmd = 10,
        RtcTime = 12,
        PulseCfg = 13,
        ModBusCfg = 15,
        DeviceInfo = 16,
        DeviceInfoStop = -1,
        FormatEEPROM = 6,
        RunCalibrate = 7
    }
}