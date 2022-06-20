using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LKDS_Type
{
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }


        public void UpdateTabControl(TabPage page)
        {
            //mainTabControl.TabPages.Add(page);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
