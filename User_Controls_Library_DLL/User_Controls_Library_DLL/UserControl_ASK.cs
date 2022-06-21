using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LKDS_Type;

namespace DeviceManagerLKDS
{

    public partial class UserControl_ASK : Parent
    {
        public UserControl_ASK()
        {
            InitializeComponent();

        }
        public override EnumHelper.CAN_Devices type => EnumHelper.CAN_Devices.ASK;
        public override void SetData(byte[] array, ushort address)
        {

            if (array.Length>=32)
            {
                EnumHelper.CAN_Devices type = (EnumHelper.CAN_Devices)array[1];
                EnumHelper.Device_Status status = (EnumHelper.Device_Status)array[0];
                if(type == EnumHelper.CAN_Devices.ASK)
                {
                    device_status_tb.Text = status.GetNameOfEnum();
                    device_name_tb.Text = type.GetNameOfEnum();
                    device_address_tb.Text = Convert.ToString(address);

                    in_1_pb_sk.BackColor = ((array[3] & 0x01) != 0) ? Color.Green : Color.White;
                    in_2_pb_sk.BackColor = ((array[3] & 0x02) != 0) ? Color.Green : Color.White;
                    in_3_pb_sk.BackColor = ((array[3] & 0x04) != 0) ? Color.Green : Color.White;
                    in_4_pb_sk.BackColor = ((array[3] & 0x08) != 0) ? Color.Green : Color.White;
                    in_5_pb_sk.BackColor = ((array[3] & 0x10) != 0) ? Color.Green : Color.White;
                    in_6_pb_sk.BackColor = ((array[3] & 0x20) != 0) ? Color.Green : Color.White;
                    in_7_pb_sk.BackColor = ((array[3] & 0x40) != 0) ? Color.Green : Color.White;
                    in_8_pb_sk.BackColor = ((array[3] & 0x80) != 0) ? Color.Green : Color.White;
                    in_9_pb_sk.BackColor = ((array[2] & 0x01) != 0) ? Color.Green : Color.White;
                    in_10_pb_sk.BackColor = ((array[2] & 0x02) != 0) ? Color.Green : Color.White;
                    in_11_pb_sk.BackColor = ((array[2] & 0x04) != 0) ? Color.Green : Color.White;
                    in_12_pb_sk.BackColor = ((array[2] & 0x08) != 0) ? Color.Green : Color.White;
                    in_13_pb_sk.BackColor = ((array[2] & 0x10) != 0) ? Color.Green : Color.White;
                    in_14_pb_sk.BackColor = ((array[2] & 0x20) != 0) ? Color.Green : Color.White;
                    in_15_pb_sk.BackColor = ((array[2] & 0x40) != 0) ? Color.Green : Color.White;
                    in_16_pb_sk.BackColor = ((array[2] & 0x80) != 0) ? Color.Green : Color.White;

                    in_1_pb_ts.BackColor = ((array[5] & 0x01) != 0) ? Color.Green : Color.White;
                    in_2_pb_ts.BackColor = ((array[5] & 0x02) != 0) ? Color.Green : Color.White;
                    in_3_pb_ts.BackColor = ((array[5] & 0x04) != 0) ? Color.Green : Color.White;
                    in_4_pb_ts.BackColor = ((array[5] & 0x08) != 0) ? Color.Green : Color.White;
                    in_5_pb_ts.BackColor = ((array[5] & 0x10) != 0) ? Color.Green : Color.White;
                    in_6_pb_ts.BackColor = ((array[5] & 0x20) != 0) ? Color.Green : Color.White;
                    in_7_pb_ts.BackColor = ((array[5] & 0x40) != 0) ? Color.Green : Color.White;
                    in_8_pb_ts.BackColor = ((array[5] & 0x80) != 0) ? Color.Green : Color.White;
                    in_9_pb_ts.BackColor = ((array[4] & 0x01) != 0) ? Color.Green : Color.White;
                    in_10_pb_ts.BackColor = ((array[4] & 0x02) != 0) ? Color.Green : Color.White;
                    in_11_pb_ts.BackColor = ((array[4] & 0x04) != 0) ? Color.Green : Color.White;
                    in_12_pb_ts.BackColor = ((array[4] & 0x08) != 0) ? Color.Green : Color.White;
                    in_13_pb_ts.BackColor = ((array[4] & 0x10) != 0) ? Color.Green : Color.White;
                    in_14_pb_ts.BackColor = ((array[4] & 0x20) != 0) ? Color.Green : Color.White;
                    in_15_pb_ts.BackColor = ((array[4] & 0x40) != 0) ? Color.Green : Color.White;
                    in_16_pb_ts.BackColor = ((array[4] & 0x80) != 0) ? Color.Green : Color.White;
                    software_version_tb.Text = $"{array[8]}.{array[9]}.{array[10]}";
                }
            }
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x00);
        }

        private void reset1_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x01);
        }
        private void reset2_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x02);
        }

        private void reset3_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x03);
        }

        private void reset4_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x04);
        }

        private void reset5_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x05);
        }

        private void reset6_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x06);
        }

        private void reset7_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x07);
        }

        private void reset8_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x08);
        }

        private void reset9_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x09);
        }

        private void reset10_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x10);
        }

        private void reset11_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x11);
        }

        private void reset12_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x12);
        }

        private void reset13_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x13);
        }

        private void reset14_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x14);
        }

        private void reset15_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x15);
        }

        private void reset16_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterTriggerOff, device_address_tb.Text, 0x16);
        }
    }
}
