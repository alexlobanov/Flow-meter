using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr
{
    public class FlowMeterState
    {
        private FlowCommands usingCommand;

        private FlowDateStruct dateStruct;
        private FlowConfigStruct configStruct;
        

        private bool valid;
       


        public FlowMeterState(DateTime dateTime, FlowCommands usingCommand)
        {
            this.dateStruct = new FlowDateStruct();
            this.dateStruct.ConvertedDateTime = dateTime;
            this.usingCommand = usingCommand;
            valid = true;
        }

        public FlowMeterState(FlowConfigStruct config, FlowCommands usingCommand)
        {
            this.configStruct = config;
            this.usingCommand = usingCommand;
            valid = true;
        }



        public FlowMeterState(DateTime date)
        {
            this.dateStruct = new FlowDateStruct();
            this.dateStruct.ConvertedDateTime = date;
            valid = true;
        }

        public FlowMeterState()
        {
            valid = false;
        }

        public FlowDateStruct DateTime => dateStruct;
        public FlowConfigStruct Config => configStruct;



        public bool Valid => valid;
        public FlowCommands UsingCommand => usingCommand;
    }
}
