using DeviceManagerLKDS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Timers;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static LKDS_Type.EnumHelper;
using LKDS_Type;
using LiftControlLibraryDLL;

namespace DeviceManagerLKDS
{
    public partial class Form1 : Form
    {
        public Stream st = null;
        TimeSpan time_span = new TimeSpan(0, 24, 0, 0);
        public static int log_number = 1;
        public static string log_path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        #region Искусственные переменные

        // StreamWriter logWriter = new StreamWriter("C:\\DeviceManagerLKDS\\DeviceManagerLKDS\\DeviceManagerLKDS\\Logs\\Log.txt"); // ПУТЬ
        int[] connectedDevices = new int[64];


        public static string filename = $"\\Log{log_number}_{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}_{DateTime.Now.Hour}-{DateTime.Now.Minute}.txt";

        public static FileStream fs = File.Create(log_path + filename);

        StreamWriter sw = new StreamWriter(fs);

        Parent[] cnctDev = new Parent[256];

        byte[] query = new byte[]
                                   {
                                        0x01,
                                        0x04,
                                        0x1F,
                                        0xF0,
                                        0x00,
                                        0x10,
                                        0,
                                        0
                                   };
        byte[] query2 = new byte[]
                                   {
                                        0x01,
                                        0x04,
                                        0x00,
                                        0x00,
                                        0x00,
                                        0x73,
                                        0,
                                        0
                                   };
        byte[] query3 = new byte[]
                                   {
                                        0x01,
                                        0x04,
                                        0x12,
                                        0x00, // НУЖНО 0x00
                                        0x00,
                                        0x01,
                                        0,
                                        0
                                   };
        byte[] clone = new byte[34];

        LiftControl LB_UC = new LiftControl();
        UserControl_APU APU_UC = new UserControl_APU();
        UserControl_ASK ASK_UC = new UserControl_ASK();
        UserControl_PKD_2_2 PKD22_UC = new UserControl_PKD_2_2();
        UserControl_PKD_2_16 PKD216_UC = new UserControl_PKD_2_16();
        UserControl1_ARVcs ARV_UC = new UserControl1_ARVcs();
        UserControl1_ATU ATU_UC = new UserControl1_ATU();

        #endregion

        #region Конструктор формы и её методы
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataReader.dr?.Disconnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getCOMports();
        }

        #endregion

        #region Клик-события

        private void button1_Click(object sender, EventArgs e)
        {
            string title = "Page " + (mainTabControl.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            mainTabControl.TabPages.Add(myTabPage);
        }
        bool iscnt = false;
        private void bConnectPort_Click(object sender, EventArgs e)
        {
            if (cbConnectedPorts.Items.Count != 0)
            {
                if (iscnt)
                {
                    try
                    {
                        DataReader.dr.Disconnect();
                        rtbLog.Text += $"\nСоединение с портом {cbConnectedPorts.SelectedItem} разорвано";
                        timer1.Stop();
                        btnConnect.Text = "Открыть соединение";
                        while (mainTabControl.TabPages.Count > 1)
                            mainTabControl.TabPages.RemoveAt(1);
                        clone = new byte[34];
                        DataReader.dr = null;
                    }
                    catch
                    {
                    }
                    iscnt = false;
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < cbConnectedPorts.Items.Count; i++)
                        {
                            if (i == cbConnectedPorts.SelectedIndex)
                            {

                                //

                                if (Directory.Exists(log_path))
                                {

                                        sw.AutoFlush = true;
                                        sw.WriteLine($"Log Begin {DateTime.Now}");
                                }

                                    sw.WriteLine("Addicted!!!!!!!!");

                                DataReader.dr = new DataReader(cbConnectedPorts.SelectedItem.ToString());
                                btnConnect.Text = "Закрыть соединение";
                                timer1.Start();
                                mainTabControl.SelectedIndex = 0;
                            }
                        }
                        rtbLog.Text += DataReader.log_conStatus;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    iscnt = true;
                }
                rtbLog.Text += $"\n\n[{DateTime.Now}-{DateTime.Now.Millisecond}] Выбранна вкладка: {mainTabControl.TabPages[mainTabControl.SelectedIndex].Text}";
            }

        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            rtbLog.Text = "";
        }

        #endregion

        #region Функции и процедуры

        int index = 0;

        void AskLiftState()
        {
            byte[] array = new byte[]
            {
                0x01,
                0x04,
                0x00, 
                0x00,
                0x00,
                0x73,
                0,
                0

            };

            SendQuery(array);

            ((Parent)(mainTabControl.SelectedTab.Controls[0])).SetData(DataReader.dr.setOfBytes, Convert.ToUInt16(mainTabControl.SelectedTab.Name));
        }

        void AskDevState()
        {
            Union16 var1 = new Union16();

            var1.Value =(short)( 0x1000 + 0x0010 * Math.Abs((Convert.ToInt32(mainTabControl.SelectedTab.Name))));


            byte[] array = new byte[]
            {
                 0x01,
                 0x04,
                 var1.Byte1, // НУЖНО 0x00
                 var1.Byte0,
                 0x00,
                 0x10,
                 0,
                 0

            };

            SendQuery(array);

            ((Parent)(mainTabControl.SelectedTab.Controls[0])).SetData(DataReader.dr.setOfBytes, Convert.ToUInt16(mainTabControl.SelectedTab.Name));
        }

        bool SendQuery(byte[] query)
        {
            DataReader.dr.Send(query);

            bool isdata = DataReader.dr.OnDataEvent.WaitOne(1000);

            Console.WriteLine($"Get {isdata}; Len {DataReader.dr.setOfBytes.Length}");
            rtbLog.Text += DataReader.log_input + DataReader.log_output;
            DataReader.dr.outputBytes = "";
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();

            return isdata && DataReader.dr.setOfBytes.Length != 0;

        }

        #endregion

        #region Таймер
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            index++;

            if (!SendQuery(query))
            {
                timer1.Start();
                return;
            }
            try
            {
                // CRC bit
                if (clone[clone.Length - 1] != DataReader.dr.setOfBytes[DataReader.dr.setOfBytes.Length - 1] || clone[clone.Length - 2] != DataReader.dr.setOfBytes[DataReader.dr.setOfBytes.Length - 2])
                {
                    Array.Copy(DataReader.dr.setOfBytes, clone, DataReader.dr.setOfBytes.Length);
                    List<byte> bits = new List<byte>();
                    for (int i = 0; i <= 255; i++)
                    {
                        int b = DataReader.dr.setOfBytes[(int)(i / 8)];
                        if (((b & (1 << (i % 8))) != 0))
                            bits.Add((byte)i);
                    }

                    mainTabControl.TabPages.Clear();
                    // run for device
                    for (int j = 0; j < bits.Count; j++)
                    {
                        // Query get type
                        Union16 var1 = new Union16();
                        var1.Value = (short)(0x1000 + 0x0010 * bits[j]);
                        byte[] array = new byte[]
                        {
                                0x01,
                                        0x04,
                                        var1.Byte1, // НУЖНО 0x00
                                        var1.Byte0,
                                        0x00,
                                        0x01,
                                        0,
                                        0

                        };

                        while (!SendQuery(array));

                        if (DataReader.dr.setOfBytes[0] != 0xFF)
                        {
                            Parent CD = cnctDev[bits[j]];
                            if (CD == null)
                            {

                                // create tabs
                                CAN_Devices CAND = (CAN_Devices)(DataReader.dr.setOfBytes[1]);

                                switch (CAND)
                                {
                                    case CAN_Devices.LB:
                                        {
                                            CD = new LiftControl();
                                            // CD.IfItIsLift = true;

                                            break;
                                        }
                                    case CAN_Devices.APU:
                                        {
                                            CD = new UserControl_APU();
                                            break;
                                        }
                                    case CAN_Devices.ASK:
                                        {
                                            CD = new UserControl_ASK();
                                            break;
                                        }
                                    case CAN_Devices.PKD216CR:
                                        {
                                            CD = new UserControl_PKD_2_16();
                                            break;
                                        }
                                    case CAN_Devices.PKD22CR:
                                        {
                                            CD = new UserControl_PKD_2_2();
                                            break;
                                        }
                                    case CAN_Devices.ARV:
                                        {
                                            CD = new UserControl1_ARVcs();
                                            break;
                                        }
                                    case CAN_Devices.ATU:
                                        {
                                            CD = new UserControl1_ATU();
                                            break;
                                        }
                                    default:
                                        {
                                            //CD = new Parent();
                                            continue; ;
                                        }
                                } 
                                CD.adress = Convert.ToByte(bits[j]);
                                // CD.IfItIsLift = false;

                                cnctDev[bits[j]] = CD;
                                TabPage newPage = new TabPage(CD.type.GetNameOfEnum());
                                newPage.Name = CD.adress.ToString();
                                newPage.Controls.Add(CD);
                                mainTabControl.TabPages.Add(newPage);
                            }
                            else
                            {
                                TabPage ExistPage = new TabPage(CD.type.GetNameOfEnum());
                                ExistPage.Name = CD.adress.ToString();
                                ExistPage.Controls.Add(CD);
                                mainTabControl.TabPages.Add(ExistPage);
                                continue;
                            }
                            //CD.SetData(dr.setOfBytes, CD.adress);
                        }
                    }
                    rtbLog.Text += "\n\n----------------\nПодключенные устройства:\n";
                    rtbLog.Text += "\n----------------\n";
                    timer1.Start();
                    return;
                }
                
            }
            catch 
            { 
            }
            if (mainTabControl.SelectedIndex != -1)
            {
                if (index % 3 == 0)
                {
                    if (mainTabControl.SelectedIndex != 0)
                    {
                        AskDevState();
                    }
                }

                if (index % 5 == 0)
                {
                    if(mainTabControl.SelectedIndex == 0)
                    {
                        AskLiftState();
                    }
                }
            }
            Update();
            timer1.Start();
        }
        #endregion

        #region Проверка подключенных COM-портов

        private struct DEV_BROADCAST_HDR
        {
            //отключаем предупреждения компилятора для ошибки 0649
        #pragma warning disable 0649
            internal UInt32 dbch_size;
            internal UInt32 dbch_devicetype;
            internal UInt32 dbch_reserved;
            //включаем предупреждения компилятора для ошибки 0649
        #pragma warning restore 0649
        };
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0219)
            {
                DEV_BROADCAST_HDR dbh;
                switch ((int)m.WParam)
                {
                    case 0x8000:
                        dbh = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_HDR));
                        if (dbh.dbch_devicetype == 0x00000003)
                        {
                            getCOMports();
                        }
                        break;
                    case 0x8004:
                        dbh = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_HDR));
                        if (dbh.dbch_devicetype == 0x00000003)
                        {

                            cbConnectedPorts.Text = "";
                            getCOMports();
                        }
                        break;
                }
            }
        }
        public void getCOMports()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                cbConnectedPorts.Items.Clear();
                cbConnectedPorts.Items.AddRange(ports);
                cbConnectedPorts.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // MessageBox.Show("Нет доступных соединений");
            }
        }

        #endregion

        #region Прочее

        #endregion


        public void button4_Click(object sender, EventArgs e)
        {
            Process.Start(log_path);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void mainUserControl_Load(object sender, EventArgs e)
        {

        }

      
    }
}
