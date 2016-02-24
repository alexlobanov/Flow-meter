using System;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr
{
    public class FlowMeterState
    {
        public FlowMeterState(DateTime dateTime, FlowCommands usingCommand, FlowTypeWork typeWork)
        {
            TypeWork = typeWork;
            DateTime = new FlowDateStruct {ConvertedDateTime = dateTime};
            UsingCommand = usingCommand;
            Valid = true;
        }

        public FlowMeterState(FlowConfigStruct config, FlowCommands usingCommand, FlowTypeWork typeWork)
        {
            TypeWork = typeWork;
            Config = config;
            UsingCommand = usingCommand;
            Valid = true;
        }

        public FlowMeterState(FlowCommonDevInfo devInfo, FlowCommands usingCommand, FlowTypeWork typeWork)
        {
            TypeWork = typeWork;
            DevInfo = devInfo;
            UsingCommand = usingCommand;
            Valid = true;
        }

        public FlowMeterState(FlowModBusSctruct modBus, FlowCommands usingCommands, FlowTypeWork typeWork)
        {
            TypeWork = typeWork;
            ModBus = modBus;
            UsingCommand = usingCommands;
            Valid = true;
        }

        public FlowMeterState(FlowPulseStruct pulse, FlowCommands usingCommand, FlowTypeWork typeWork)
        {
            Pulse = pulse;
            TypeWork = typeWork;
            UsingCommand = usingCommand;
            Valid = true;
        }

        public FlowMeterState(DateTime date)
        {
            DateTime = new FlowDateStruct();
            DateTime.ConvertedDateTime = date;
            Valid = true;
        }

        public FlowMeterState()
        {
            Valid = false;
        }

        public FlowMeterState(FlowTypeWork data)
        {
            DateTime = new FlowDateStruct();
            TypeWork = data;
            Valid = true;
        }

        public FlowPulseStruct Pulse { get;  }

        public FlowModBusSctruct ModBus { get; }

        public FlowCommonDevInfo DevInfo { get; }

        public FlowDateStruct DateTime { get; }

        public FlowConfigStruct Config { get; }

        public FlowTypeWork TypeWork { get; }



        public bool Valid { get; }

        public FlowCommands UsingCommand { get; }
    }
}