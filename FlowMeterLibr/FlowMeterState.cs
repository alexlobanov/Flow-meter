using System;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr
{
    public class FlowMeterState
    {
        public FlowMeterState(DateTime dateTime, FlowCommands usingCommand)
        {
            DateTime = new FlowDateStruct();
            DateTime.ConvertedDateTime = dateTime;
            UsingCommand = usingCommand;
            Valid = true;
        }

        public FlowMeterState(FlowConfigStruct config, FlowCommands usingCommand)
        {
            Config = config;
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

        public FlowDateStruct DateTime { get; }

        public FlowConfigStruct Config { get; }


        public bool Valid { get; }

        public FlowCommands UsingCommand { get; }
    }
}