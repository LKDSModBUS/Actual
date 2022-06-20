using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LKDS_Type;

namespace DeviceManagerLKDS
{
    public partial class UserControl_APU : Parent
    {
        public UserControl_APU()
        {
            InitializeComponent();
        }
        public override EnumHelper.CAN_Devices type => EnumHelper.CAN_Devices.APU;
        public override void SetData(byte[] array, ushort address)
        {

            if (array.Length >= 32)
            {
                EnumHelper.CAN_Devices type = (EnumHelper.CAN_Devices)array[1];
                EnumHelper.Device_Status status = (EnumHelper.Device_Status)array[0];
                if (type == EnumHelper.CAN_Devices.APU)
                {
                    device_status_tb.Text = status.GetNameOfEnum();
                    device_name_tb.Text = type.GetNameOfEnum();
                    device_address_lb.Text = Convert.ToString(address);

                    in_1_pb_sk.BackColor = ((array[3] & 0x01) != 0) ? Color.Green : Color.White;
                    in_2_pb_sk.BackColor = ((array[3] & 0x02) != 0) ? Color.Green : Color.White;
                    in_3_pb_sk.BackColor = ((array[3] & 0x04) != 0) ? Color.Green : Color.White;
                    in_4_pb_sk.BackColor = ((array[3] & 0x08) != 0) ? Color.Green : Color.White;
                    in_5_pb_sk.BackColor = ((array[3] & 0x10) != 0) ? Color.Green : Color.White;
                    in_6_pb_sk.BackColor = ((array[3] & 0x020) != 0) ? Color.Green : Color.White;
                    in_7_pb_sk.BackColor = ((array[3] & 0x40) != 0) ? Color.Green : Color.White; 

                    in_1_pb_ts.BackColor = ((array[5] & 0x01) != 0) ? Color.Green : Color.White;
                    in_2_pb_ts.BackColor = ((array[5] & 0x02) != 0) ? Color.Green : Color.White;
                    in_3_pb_ts.BackColor = ((array[5] & 0x04) != 0) ? Color.Green : Color.White;
                    in_4_pb_ts.BackColor = ((array[5] & 0x08) != 0) ? Color.Green : Color.White;
                    in_5_pb_ts.BackColor = ((array[5] & 0x010) != 0) ? Color.Green : Color.White;
                    in_6_pb_ts.BackColor = ((array[5] & 0x020) != 0) ? Color.Green : Color.White;
                    in_7_pb_ts.BackColor = ((array[5] & 0x040) != 0) ? Color.Green : Color.White;

                    out_pb1.BackColor = ((array[7] & 0x01) != 0) ? Color.Green : Color.White;
                    out_pb2.BackColor = ((array[7] & 0x02) != 0) ? Color.Green : Color.White;
                    out_pb3.BackColor = ((array[7] & 0x04) != 0) ? Color.Green : Color.White;

                    software_version_tb.Text = $"{array[8]}.{array[9]}.{array[10]}";
                }
            }
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x00);
        }

        private void reset1_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x01);
        }

        private void reset2_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x02);
        }

        private void reset3_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x03);
        }

        private void reset4_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x04);
        }

        private void reset5_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x05);
        }

        private void reset6_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x06);
        }

        private void reset7_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x07);
        }

        private void vkl_btn1_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vykl_btn1_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vkl_btn2_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vykl_btn2_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vkl_btn3_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vykl_btn3_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }
    }
}
