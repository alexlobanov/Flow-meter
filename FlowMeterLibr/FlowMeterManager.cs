using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.Requests;
using FlowMeterLibr.Structs;
using HidLibrary;
using FlowMeterLibr.TO;
using FlowMeterLibr.Sending;
using FlowMeterLibr.Сommunication;

namespace FlowMeterLibr
{
    public class FlowMeterManager : IDisposable
    {
        private const int VendorId = 0x0483;
        private const int ProductId = 0x5711;


        private const int MinimumLengthData = 3;

        private HidDevice device;
        private bool attached = false;
        private bool connectedToDriver = false;
        private bool debugPrintRawMessages = false;
        private bool disposed = false;
        private static Stack<Requets> lastRequests = new Stack<Requets>();

        /// <summary>
        /// Occurs when a PowerMate device is attached.
        /// </summary>
        public event EventHandler DeviceAttached;

        /// <summary>
        /// Occurs when a PowerMate device is removed.
        /// </summary>
        public event EventHandler DeviceRemoved;
        public event EventHandler<FlowMeterEventArgs> TimeChange;
        public event EventHandler<FlowMeterEventArgs> ConfigGet;


        public FlowMeterManager()
        {
        }

        /// <summary>
        /// Attempts to connect to a PowerMate device.
        /// 
        /// After a successful connection, a DeviceAttached event will normally be sent.
        /// </summary>
        /// <returns>True if a PowerMate device is connected, False otherwise.</returns>
        public bool OpenDevice()
        {
            device = HidDevices.Enumerate(VendorId, ProductId).FirstOrDefault();

            if (device != null)
            {
                connectedToDriver = true;
                device.OpenDevice(DeviceMode.Overlapped, DeviceMode.Overlapped, ShareMode.Exclusive);

                device.Inserted += DeviceAttachedHandler;
                device.Removed += DeviceRemovedHandler;
                
                device.MonitorDeviceEvents = true;

                device.ReadReport(OnReport);

                return true;
            }

            return false;
        }

        private void OnReport(HidReport report)
        {
            
            if (attached == false) {
                Debug.WriteLine("Exit method OnReport");
                return;
            }
            Debug.WriteLine("EnterMethod OnReport");
            if (report.Data.Length >= 3)
            {
                var state = ResponseData.ParseState(report.Data);
                if (!state.Valid)
                {
                    if (debugPrintRawMessages)
                    {
                        Debug.Write("PowerMate raw data: ");
                        foreach (byte t in report.Data)
                        {
                            Debug.Write($"{t:000} ");
                        }
                        Debug.WriteLine("");
                    }
                
                    Debug.WriteLine("Invalid PowerMate state");
                }
                else
                {
                    //lastRequests.Pop();
                    GenerateEvents(state);

                    if (debugPrintRawMessages)
                    {
                        Debug.Write("PowerMate raw data: ");
                        foreach (byte t in report.Data)
                        {
                            Debug.Write($"{t:000} ");
                        }
                        Debug.WriteLine("");
                    }
                }
            }
            
            device.ReadReport(OnReport);
        }

        private void GenerateEvents(FlowMeterState state)
        {
            switch (state.UsingCommand)
            {
                case FlowCommands.FactoryReset:
                    Debug.WriteLine("FactoryReset event");
                    break;
                case FlowCommands.SaveAllSettings2memory:
                    Debug.WriteLine("SaveAllSettings2memory event");
                    break;
                case FlowCommands.DiveceError2Usb:
                    Debug.WriteLine("DiveceError2Usb event");
                    break;
                case FlowCommands.MainCfg:
                    OnGetConfig(state);
                    Debug.WriteLine("MainCfg event");
                    break;
                case FlowCommands.USmetrVariablesCmd:
                    break;
                case FlowCommands.RtcTime:
                    Debug.WriteLine("RtcTime event");
                    OnTimeChange(state);
                    break;
                case FlowCommands.PulseCfg:
                    Debug.WriteLine("PulseCfg event");
                    break;
                case FlowCommands.ModBusCfg:
                    Debug.WriteLine("ModBusCfg event");
                    break;
                case FlowCommands.DeviceInfo:
                    Debug.WriteLine("DeviceInfo event");
                    break;
                case FlowCommands.FormatEEPROM:
                    Debug.WriteLine("FormatEEPROM event");
                    break;
                case FlowCommands.RunCalibrate:
                    Debug.WriteLine("RunCalibrate event");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnGetConfig(FlowMeterState state)
        {
            var handle = ConfigGet;
            handle?.Invoke(this, new FlowMeterEventArgs(state));
        }

        private void OnTimeChange(FlowMeterState state)
        {
            var handle = TimeChange;
            handle?.Invoke(this, new FlowMeterEventArgs(state));
        }

        public void SendDateRequest()
        {
            if (connectedToDriver)
            {
                var request = new DateRequest(ref device);
                lastRequests.Push(request);
            }
        }

        public void SendConfigRequest()
        {
            if (connectedToDriver)
            {
                Debug.WriteLine(device.ToString());
                var request = new ConfigRequest(ref device);
                lastRequests.Push(request);
            }
        }

        private FlowMeterState ParseState(byte[] data)
        {
            if (data.Length <= MinimumLengthData)
                return new FlowMeterState();
            switch (data[0])
            {
                case (byte)FlowCommands.FactoryReset:
                    Debug.WriteLine("Respone FactoryReset");
                    return new FlowMeterState();

                case (byte)FlowCommands.SaveAllSettings2memory:
                    Debug.WriteLine("Respone SaveAllSettings2memory");
                    return new FlowMeterState();

                case (byte)FlowCommands.DiveceError2Usb:
                    Debug.WriteLine("Respone DiveceError2Usb");
                    return new FlowMeterState();

                case (byte)FlowCommands.MainCfg:
                    var CnfStructResived = new FlowConfigStruct(data);
                    Debug.WriteLine("Respone MainCfg");
                    return new FlowMeterState(CnfStructResived, FlowCommands.MainCfg);

                case (byte)FlowCommands.USmetrVariablesCmd:
                    Debug.WriteLine("Respone USmetrVariablesCmd");
                    return new FlowMeterState();

                case (byte)FlowCommands.RtcTime:
                    Debug.WriteLine("Respone RtcTime");
                    var dateStructResived = new FlowDateStruct(data);
                    return new FlowMeterState(dateStructResived.GetDateTime(), FlowCommands.RtcTime);

                case (byte)FlowCommands.PulseCfg:
                    Debug.WriteLine("Respone PulseCfg");
                    return new FlowMeterState();

                case (byte)FlowCommands.ModBusCfg:
                    Debug.WriteLine("Respone ModBusCfg");
                    return new FlowMeterState();

                case (byte)FlowCommands.DeviceInfo:
                    Debug.WriteLine("Respone DeviceInfo");
                    return new FlowMeterState();

                case (byte)FlowCommands.FormatEEPROM:
                    Debug.WriteLine("Respone FormatEEPROM");
                    return new FlowMeterState();

                case (byte)FlowCommands.RunCalibrate:
                    Debug.WriteLine("Respone RunCalibrate");
                    return new FlowMeterState();

                default:
                    throw new ArgumentOutOfRangeException();
                    
            }
        }


        /// <summary>
        /// Closes the connection to the device.
        /// 
        /// FIXME: Verify that this also shuts down any thread waiting for device data. 2012-06-07 thammer.
        /// </summary>
        public void CloseDevice()
        {
            device.CloseDevice();
            connectedToDriver = false;
        }

        /// <summary>
        /// Closes the connection to the device.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    CloseDevice();
                }

                disposed = true;
            }
        }

        ~FlowMeterManager()
        {
            Dispose(false);
        }

        private void DeviceAttachedHandler()
        {
            attached = true;

            if (DeviceAttached != null)
                DeviceAttached(this, EventArgs.Empty);
            // device.Read(OnRead);
            device.ReadReport(OnReport, 1000);
        }

        private void DeviceRemovedHandler()
        {
            attached = false;
            if (DeviceRemoved != null)
                DeviceRemoved(this, EventArgs.Empty);
        }
    }
}
