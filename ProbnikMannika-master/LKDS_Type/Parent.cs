
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
        public bool IfItIsLift;
        public static string PortName;
        DataReader dr = new DataReader(PortName);
        public virtual void SetData(byte[] array, ushort address)
        {
            
        }

        public delegate void OnCmdDelegate(CmdTypes current_enum, short num);
        public byte adress;
        virtual public CAN_Devices type => CAN_Devices.LB;


        public OnCmdDelegate OnCmd = null;//SendCmd(CmdTypes.AdapterOff, 0);


        public void SendCmd(EnumHelper.CmdTypes current_enum, short num)
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
                dr.Send(QueryLiftWrite);
            } else
            {
                byte[] QueryAdapterWrite = new byte[]
                {
                                        0x01, // Адрес устройства
                                        0x06, // Адерс команды
                                        0x12, // адрес устройства + 0F
                                        0x2F, // адрес устройства + 0F
                                        (byte)current_enum, // Команда из Enum CMD
                                        (byte)num, // Статично
                                        0,
                                        0
                };
                dr.Send(QueryAdapterWrite);
            }
        }
    }
}
