using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FlowMeterLibr.Requests;
using FlowMeterLibr.TO;
using FlowMeterLibr.Сommunication;
using HidLibrary;

namespace FlowMeterLibr
{
    public class FlowMeterManager : IDisposable
    {
        private const int VendorId = 0x0483;
        private const int ProductId = 0x5711;


        private const int MinimumLengthData = 3;
        private static readonly Stack<Requets> lastRequests = new Stack<Requets>();
        private bool attached;
        private bool connectedToDriver;
        private readonly bool debugPrintRawMessages = false;

        public HidDevice device;
        private bool disposed;

        

        /// <summary>
        ///     Closes the connection to the device.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Occurs when a PowerMate device is attached.
        /// </summary>
        public event EventHandler DeviceAttached;

        /// <summary>
        ///     Occurs when a PowerMate device is removed.
        /// </summary>
        public event EventHandler DeviceRemoved;

        public event EventHandler<FlowMeterEventArgs> TimeChange;
        public event EventHandler<FlowMeterEventArgs> ConfigGet;

        /// <summary>
        ///     Attempts to connect to a PowerMate device.
        ///     After a successful connection, a DeviceAttached event will normally be sent.
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
            if (attached == false)
            {
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
                        foreach (var t in report.Data)
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
                        foreach (var t in report.Data)
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


        /// <summary>
        ///     Closes the connection to the device.
        ///     FIXME: Verify that this also shuts down any thread waiting for device data. 2012-06-07 thammer.
        /// </summary>
        public void CloseDevice()
        {
            device.CloseDevice();
            connectedToDriver = false;
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
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