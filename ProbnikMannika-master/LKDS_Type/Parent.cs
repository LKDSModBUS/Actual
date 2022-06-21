
using DeviceManagerLKDS.Classes;
using DeviceManagerLKDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LKDS_Type.EnumHelper;

namespace LKDS_Type
{
    public class Parent : UserControl
    {
        public static bool IfItIsLift;
        public virtual void SetData(byte[] array, ushort address)
        {
            
        }

        public delegate void OnCmdDelegate(CmdTypes current_enum, string adress, short num);
        public byte adress;
        virtual public CAN_Devices type => CAN_Devices.LB;


        public OnCmdDelegate OnCmd = SendCmd;


        public static void SendCmd(EnumHelper.CmdTypes current_enum, string adress, short num)
        {
            if (IfItIsLift)
            {
                byte[] QueryLiftWrite = new byte[]
                                        {
                                        0x01, // Адрес устройства
                                        0x06, // Адерс команды
                                        0x01, // Статично для лифта
                                        0x11, // Статично для лифта
                                        (byte)current_enum, // Команда из Enum CMD
                                        (byte)num, // Статично
                                        0,
                                        0
                                        };
                DataReader.dr.Send(QueryLiftWrite);
            } else
            {

                Union16 var1 = new Union16();

                var1.Value = (short)(0x100F + 0x0010 * Math.Abs((Convert.ToInt32(adress))));

                byte[] QueryAdapterWrite = new byte[]
                {
                                        0x01, // Адрес устройства
                                        0x06, // Адерс команды
                                        var1.Byte1, // адрес устройства + 0F
                                        var1.Byte0, // адрес устройства + 0F
                                        (byte)current_enum, // Команда из Enum CMD
                                        (byte)num, // Статично
                                        0,
                                        0
                };
                DataReader.dr.Send(QueryAdapterWrite);
            }
        }
    }
}
