using System;
using FlowMeterLibr.Enums;

namespace FlowMeterLibr.Exceptions
{
    public class FlowMeterException : Exception
    {
        public FlowMeterException()
        {
        }

        public FlowMeterException(string message)
            : base(message)
        {
        }


        public FlowMeterException(FlowMeterErrorCodes errorCodes)
        {
            var stringToDisplay = "";
            switch (errorCodes)
            {
                case FlowMeterErrorCodes.DeviceCrash:
                    stringToDisplay = "[ВНИМАНИЕ] Аппаратная неисправность прибора";
                    break;
                case FlowMeterErrorCodes.LowBattery:
                    stringToDisplay = "[ВНИМАНИЕ] Низкое напряжение батареи.";
                    break;
                case FlowMeterErrorCodes.tubeInAir:
                    stringToDisplay = "[ВНИМАНИЕ] Низкое напряжение батареи.";
                    break;
                case FlowMeterErrorCodes.NoUltrasonicSensors:
                    stringToDisplay =
                        "[ВНИМАНИЕ] Нет ультразвуковых датчиков.";
                    break;
                case FlowMeterErrorCodes.NoInSetupMode:
                    stringToDisplay =
                        "[ВНИМАНИЕ] Нет прав доступа на изменение параметра (Прибор не в режиме настройки)";
                    break;
                case FlowMeterErrorCodes.ErrorInExternalMemory:
                    stringToDisplay = "[Ошибка внешней памяти]";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(errorCodes), errorCodes, null);
            }
            throw new FlowMeterException(stringToDisplay);
        }

        public FlowMeterException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public FlowMeterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FlowMeterException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}