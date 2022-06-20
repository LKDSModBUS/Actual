using LKDS_Type;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiftControlLibraryDLL
{
    public partial class LiftControl : Parent
    {
        public LiftControl()
        {
            InitializeComponent();
        }
        int maxlvl = 0;
        EnumHelper.Union64 Order = new EnumHelper.Union64();
        EnumHelper.Union64 Up = new EnumHelper.Union64();
        EnumHelper.Union64 Down = new EnumHelper.Union64();
        public override void SetData(byte[] array, ushort address)
        {
            if (array.Length > 230)
            {
                if(array[0] == 4)
                {
                    EnumHelper.Union16 val = new EnumHelper.Union16();
                    val.Byte0 = array[2];
                    val.Byte1 = array[3];

                    EnumHelper.LBClassType type = (EnumHelper.LBClassType)val.Value;
                    lb_type.Text = type.GetNameOfEnum();

                    EnumHelper.Stage type1 = (EnumHelper.Stage)array[14];
                    call_source.Text = type1.GetNameOfEnum();

                    EnumHelper.WorkMode type2 = (EnumHelper.WorkMode)array[40];
                    working_mode.Text = type2.GetNameOfEnum();

                    EnumHelper.StageNum type4 = (EnumHelper.StageNum)array[34];
                    floor.Text = type4.GetNameOfEnum();

                    EnumHelper.Doors type3 = (EnumHelper.Doors)array[35];
                    door_status.Text = type3.GetNameOfEnum();

                    lb_appver.Text = $"{array[4]}.{array[5]}.{array[6]}";

                    restart.Text = $"код {array[7].ToString("X2")} ({array[8]})";

                    EnumHelper.Union64 val6 = new EnumHelper.Union64();
                    val6.Byte0 = array[64];
                    val6.Byte1 = array[65];
                    val6.Byte2 = array[66];
                    val6.Byte3 = array[67];
                    val6.Byte4 = array[68];
                    val6.Byte5 = array[69];

                    lift_status.Items.Clear();
                    foreach (ulong tmmp in System.Enum.GetValues(typeof(EnumHelper.LiftStatus)))
                    {
                        EnumHelper.LiftStatus cur = (EnumHelper.LiftStatus)tmmp;
                        if (EnumHelper.isStateFlag(cur, val6.UValue))
                            lift_status.Items.Add(cur.GetNameOfEnum());
                    }
                    if (EnumHelper.isStateFlag(EnumHelper.LiftStatus.calldispather, val6.UValue))
                        if (!EnumHelper.isStateFlag(EnumHelper.LiftStatus.calldispatcherfrom, val6.UValue))
                            lift_status.Items.Add("Вызов диспетчера из кабины");
                                        
                    EnumHelper.Union32 val1 = new EnumHelper.Union32();
                    val1.Byte0 = array[192];
                    val1.Byte1 = array[193];
                    val1.Byte2 = array[194];
                    val1.Byte3 = array[195];
                    main_drive_inclusions.Text = val1.Value.ToString();

                    EnumHelper.Union32 val2 = new EnumHelper.Union32();
                    val2.Byte0 = array[196];
                    val2.Byte1 = array[197];
                    val2.Byte2 = array[198];
                    val2.Byte3 = array[199];
                    main_drive_work_time.Text = $"{val2.Value.ToString()} сек.";

                    EnumHelper.Union32 val3 = new EnumHelper.Union32();
                    val3.Byte0 = array[200];
                    val3.Byte1 = array[201];
                    val3.Byte2 = array[202];
                    val3.Byte3 = array[203];
                    door_drive_inclusions.Text = val3.Value.ToString();

                    EnumHelper.Union32 val4 = new EnumHelper.Union32();
                    val4.Byte0 = array[204];
                    val4.Byte1 = array[205];
                    val4.Byte2 = array[206];
                    val4.Byte3 = array[207];
                    door_drive_work_time.Text = $"{val4.Value.ToString()} сек.";

                    if (array[40] == 16)
                    {
                        emergency_stop.Image = Properties.Resources.alarm;
                    }
                  
                    switch (array[37])
                    {
                        case 0:
                            {
                                no_move.Checked = true;
                                break;
                            }
                        case 1:
                            {
                                up.Image = Properties.Resources.up2;
                                down.Image = Properties.Resources.down2;
                                break;
                            }
                        case 2:
                            {
                                up.Image = Properties.Resources.up2;
                                down.Image = Properties.Resources.down1;
                                break;
                            }
                        case 3:
                            {
                                up.Image = Properties.Resources.up1;
                                down.Image = Properties.Resources.down2;
                                break;
                            }
                        case 4:
                            {
                                up.Image = Properties.Resources.up1;
                                down.Image = Properties.Resources.down1;
                                break;
                            }
                    }
               
                    switch (type3)
                    {
                        case  EnumHelper.Doors.undefined:
                            {
                                lift.Image = Properties.Resources.where;
                                break;
                            }
                        case EnumHelper.Doors.open:
                            {
                                lift.Image = Properties.Resources.closing;
                                break;
                            }
                        case EnumHelper.Doors.all_open:
                            {
                                lift.Image = Properties.Resources.open;
                                break;
                            }
                        case EnumHelper.Doors.close:
                            {
                                lift.Image = Properties.Resources.opening;
                                break;
                            }
                        case EnumHelper.Doors.all_close:
                            {
                                lift.Image = Properties.Resources.close;
                                break;
                            }
                        case EnumHelper.Doors.underclosed:
                            {
                                lift.Image = Properties.Resources.not_full_open;
                                break;
                            }
                        case EnumHelper.Doors.doors_lock:
                            {
                                lift.Image = Properties.Resources._lock;
                                break;
                            }
                        case EnumHelper.Doors.absence:
                            {
                                lift.Image = Properties.Resources.no;
                                break;
                            }
                    }

                    EnumHelper.Union64 Ordernew = new EnumHelper.Union64();

                    Ordernew.Byte0 = array[128];
                    Ordernew.Byte1 = array[129];
                    Ordernew.Byte2 = array[130];
                    Ordernew.Byte3 = array[131];
                    Ordernew.Byte4 = array[132];
                    Ordernew.Byte5 = array[133];
                    Ordernew.Byte6 = array[134];
                    Ordernew.Byte7 = array[135];

                    EnumHelper.Union64 Upnew = new EnumHelper.Union64();

                    Upnew.Byte0 = array[136];
                    Upnew.Byte1 = array[137];
                    Upnew.Byte2 = array[138];
                    Upnew.Byte3 = array[139];
                    Upnew.Byte4 = array[140];
                    Upnew.Byte5 = array[141];
                    Upnew.Byte6 = array[142];
                    Upnew.Byte7 = array[143];

                    EnumHelper.Union64 Downnew = new EnumHelper.Union64();

                    Downnew.Byte0 = array[144];
                    Downnew.Byte1 = array[145];
                    Downnew.Byte2 = array[146];
                    Downnew.Byte3 = array[147];
                    Downnew.Byte4 = array[148];
                    Downnew.Byte5 = array[149];
                    Downnew.Byte6 = array[150];
                    Downnew.Byte7 = array[151];


                    if ((maxlvl != array[33]) || (Ordernew.Value != Order.Value)
                        || (Upnew.Value != Up.Value) || (Downnew.Value != Down.Value))
                    {
                        maxlvl = array[33];
                        Order.Value = Ordernew.Value;
                        Up.Value = Upnew.Value;
                        Down.Value = Downnew.Value;
                        CallsOrders.Items.Clear();

                        for (byte i = (byte)maxlvl; i > 0; i--)
                        {

                            string str = $"{i} этаж";


                            if (Order.isBitSet(i))
                                str += " П";
                            if (Up.isBitSet(i))
                                str += " В Вверх";
                            if (Down.isBitSet(i))
                                str += " В Вниз";

                            CallsOrders.Items.Add(str);
                        }
                    }

                    #region [ggs]
                    dispatcher.Checked = ((array[13] & 0x20) != 0);
                    fire_subdivision.Checked = ((array[13] & 0x80) != 0);

                    if (((array[13] & 0x40) == 1))
                        ggs_on.Checked = true;
                    else
                        ggs_off.Checked = true;

                    if (((array[13] & 0x01) == 0))
                        test_ggs_no_data.Checked = true;
                    else
                        test_ggs_yes.Checked = true;

                    if (((array[13] & 0x02) == 1))
                        test_result_yes.Checked = true;
                    else
                        test_result_ggs_err.Checked = true;
                    #endregion
                     
                    #region [battary]
                    if (((array[15] & 0x01) == 1))
                        batt_support_yes.Checked = true;
                    else
                        batt_support_no.Checked = true;

                    if (((array[15] & 0x02) == 1))
                        change_mode_support_no.Checked = true;
                    else
                        change_mode_support_yes.Checked = true;

                    if (((array[15] & 0x80) == 1))
                        power_device_backup.Checked = true;
                    else
                        power_device_basic.Checked = true;

                    batery_err.Checked = ((array[15] & 0x04) != 0);
                    test_batery_ok.Checked = ((array[15] & 0x08) != 0);
                    test_batery_go.Checked = ((array[15] & 0x10) != 0);
                    battery_charge.Checked = ((array[15] & 0x20) != 0);
                    no_battery.Checked = ((array[15] & 0x40) != 0);
                    #endregion

                    #region [passangers]
                    passangers.Checked = (array[41] >= 10 && array[41] <= 100);

                    if (array[41] >= 0 && array[41] <= 100)
                    {
                        precent.Text = $"{array[41]}%";
                    }
                    if (array[41] == 255)
                    {
                        precent.Text = $"???  :')";
                    }

                    if (array[41] >= 0 && array[41] <= 100)
                    {
                        progressBar1.Value = array[41];
                        progressBar1.ValueColor = Color.Green;
                    }
                    if (array[41] == 100)
                    {
                        progressBar1.Value = 100;
                        progressBar1.ValueColor = Color.Yellow;
                    }
                    if (array[41] > 100)
                    {
                        progressBar1.Value = 100;
                        progressBar1.ValueColor = Color.Red;
                    }
                    if (array[41] == 255)
                    {
                        progressBar1.Value = 0;
                    }
                    #endregion

                    #region [battary_precent]
                    if (array[16] >= 0 && array[16] <= 100)
                    {
                        battary_precent.Text = $"{array[16]}%";
                    }
                    if (array[16] == 255)
                    {
                        battary_precent.Text = $"???  :')";
                    }

                    if (array[16] >= 0 && array[16] <= 20)
                    {
                        progressBar2.Value = array[16];
                        progressBar2.ValueColor = Color.Red;
                    }
                    if (array[16] >= 21 && array[16] <= 50)
                    {
                        progressBar2.Value = array[16];
                        progressBar2.ValueColor = Color.Yellow;
                    }
                    if (array[16] >= 51 && array[16] <= 99)
                    {
                        progressBar2.Value = array[16];
                        progressBar2.ValueColor = Color.Green;
                    }
                    if (array[16] >= 100)
                    {
                        progressBar2.Value = 100;
                        progressBar2.ValueColor = Color.Green;
                    }
                    if (array[16] == 255)
                    {
                        progressBar2.Value = 0;
                    }
                    #endregion
                }
            }
        }

        private void LiftControl_Load(object sender, EventArgs e)
        {

        }


    }
}
