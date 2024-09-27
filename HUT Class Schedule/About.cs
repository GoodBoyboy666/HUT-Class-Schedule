using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUT_Class_Schedule
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "https://www.goodboyboy.top"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "https://blog.goodboyboy.top"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "https://github.com/GoodBoyboy666/HUT-Class-Schedule"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本程序使用GPL v3进行开源授权。\nThis program is open source licensed under GPL v3.");
            Process.Start(new ProcessStartInfo("explorer.exe", "https://www.gnu.org/licenses/gpl-3.0.html"));
        }
    }
}
