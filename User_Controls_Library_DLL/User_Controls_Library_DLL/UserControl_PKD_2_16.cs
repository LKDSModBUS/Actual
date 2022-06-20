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
    public partial class UserControl_PKD_2_16 : Parent
    {
        public UserControl_PKD_2_16()
        {
            InitializeComponent();
        }
        public override EnumHelper.CAN_Devices type => EnumHelper.CAN_Devices.PKD216CR;
        public override void SetData(byte[] array, ushort address)
        {

            //string str =dlajskldj.GetNameOfEnum();

            if (array.Length >= 32)
            {
                EnumHelper.CAN_Devices type = (EnumHelper.CAN_Devices)array[1];
                EnumHelper.Device_Status status = (EnumHelper.Device_Status)array[0];
                EnumHelper.Key_Format format = (EnumHelper.Key_Format)array[15];
                if (type == EnumHelper.CAN_Devices.ATU)
                {
                    device_status_tb.Text = status.GetNameOfEnum();
                    device_name_tb.Text = type.GetNameOfEnum();
                    key_format_tb.Text = format.GetNameOfEnum();
                    device_address_tb.Text = Convert.ToString(address);

                    in_1_pb_sk.BackColor = ((array[3] & 0x01) != 0) ? Color.Green : Color.White;
                    in_2_pb_sk.BackColor = ((array[3] & 0x02) != 0) ? Color.Green : Color.White;
                    in_3_pb_sk.BackColor = ((array[3] & 0x04) != 0) ? Color.Green : Color.White;
                    in_4_pb_sk.BackColor = ((array[3] & 0x08) != 0) ? Color.Green : Color.White;
                    in_5_pb_sk.BackColor = ((array[3] & 0x10) != 0) ? Color.Green : Color.White;
                    in_6_pb_sk.BackColor = ((array[3] & 0x20) != 0) ? Color.Green : Color.White;
               

                    in_1_pb_ts.BackColor = ((array[5] & 0x01) != 0) ? Color.Green : Color.White;
                    in_2_pb_ts.BackColor = ((array[5] & 0x02) != 0) ? Color.Green : Color.White;
                    in_3_pb_ts.BackColor = ((array[5] & 0x04) != 0) ? Color.Green : Color.White;
                    in_4_pb_ts.BackColor = ((array[5] & 0x08) != 0) ? Color.Green : Color.White;
                    in_5_pb_ts.BackColor = ((array[5] & 0x10) != 0) ? Color.Green : Color.White;
                    in_6_pb_ts.BackColor = ((array[5] & 0x20) != 0) ? Color.Green : Color.White;

                    out_pb1.BackColor = ((array[7] & 0x01) != 0) ? Color.Green : Color.White;
                    out_pb2.BackColor = ((array[7] & 0x02) != 0) ? Color.Green : Color.White;
                    out_pb3.BackColor = ((array[7] & 0x04) != 0) ? Color.Green : Color.White;
                    out_pb4.BackColor = ((array[7] & 0x08) != 0) ? Color.Green : Color.White;
                    out_pb5.BackColor = ((array[7] & 0x10) != 0) ? Color.Green : Color.White;
                    out_pb6.BackColor = ((array[7] & 0x20) != 0) ? Color.Green : Color.White;
                    out_pb7.BackColor = ((array[7] & 0x40) != 0) ? Color.Green : Color.White;
                    out_pb8.BackColor = ((array[7] & 0x80) != 0) ? Color.Green : Color.White;
                    out_pb9.BackColor = ((array[6] & 0x01) != 0) ? Color.Green : Color.White;
                    out_pb10.BackColor = ((array[6] & 0x02) != 0) ? Color.Green : Color.White;
                    out_pb11.BackColor = ((array[6] & 0x04) != 0) ? Color.Green : Color.White;
                    out_pb12.BackColor = ((array[6] & 0x08) != 0) ? Color.Green : Color.White;
                    out_pb13.BackColor = ((array[6] & 0x10) != 0) ? Color.Green : Color.White;
                    out_pb14.BackColor = ((array[6] & 0x20) != 0) ? Color.Green : Color.White;
                    out_pb15.BackColor = ((array[6] & 0x40) != 0) ? Color.Green : Color.White;
                    out_pb16.BackColor = ((array[6] & 0x80) != 0) ? Color.Green : Color.White;

                    software_version_tb.Text = $"{array[8]}.{array[9]}.{array[10]}";
                    key_number_tb.Text = $"{array[17].ToString("X2")} {array[18].ToString("X2")} {array[19].ToString("X2")} {array[20].ToString("X2")} {array[21].ToString("X2")} {array[22].ToString("X2")} {array[23].ToString("X2")} {array[24].ToString("X2")} {array[25].ToString("X2")} {array[26].ToString("X2")}";
                }
            }
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

        private void reset_btn_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterReset, 0x00);
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

        private void vkl_btn3_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn4_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn5_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn6_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn7_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn8_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vykl_btn2_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn3_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn4_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn5_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn6_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn7_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn8_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vkl_btn16_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn15_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn14_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn13_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn12_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn11_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn10_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vkl_btn9_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOn, 1);
        }

        private void vykl_btn16_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn15_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn14_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn13_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn12_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn11_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn10_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }

        private void vykl_btn9_Click(object sender, EventArgs e)
        {
            OnCmd?.Invoke(EnumHelper.CmdTypes.AdapterOff, 1);
        }
    }
}
