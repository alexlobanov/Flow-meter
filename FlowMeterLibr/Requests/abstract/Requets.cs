using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMeterLibr.Structs;
using FlowMeterLibr.TO;

namespace FlowMeterLibr.Requests
{
    public class Requets
    {
        private FlowIdParkage idParkage;
        private FlowCommands commands;
        private FlowStatusRequest status;
        private byte[] data;

        public Requets(FlowIdParkage idParkage, FlowCommands commands,
            FlowStatusRequest status, byte[] data)
        {
            this.idParkage = idParkage;
            this.commands = commands;
            this.status = status;
            this.data = data;
        }
        public Requets(FlowIdParkage idParkage, FlowCommands commands,
            FlowStatusRequest status)
        {
            this.idParkage = idParkage;
            this.commands = commands;
            this.status = status;
            this.data = null;
        }

        public byte[] getDataToSend()
        {
            byte[] sendData = new byte[64];
            sendData[0] = (byte)idParkage;
            sendData[1] = (byte)commands;
            sendData[2] = (byte)status;

            if (data == null)
            {
                byte[] tempdate = new byte[3];
                tempdate[0] = sendData[0];
                tempdate[1] = sendData[1];
                tempdate[2] = sendData[2];
                return tempdate;
            }
            for (int i = 3; i <= 64; i++)
            {
                sendData[i] = data[i-3];
            }
            return sendData;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Requets: IdParkage: " + idParkage + " command: " +
                       commands + " status: " + status + "Byte date: ");
            foreach (var b in data)
            {
                str.Append(b + " ");
            }
            return str.ToString();
        }
    }
}
