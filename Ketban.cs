using m.fb.Properties;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace m.fb
{
    public partial class Ketban : Form
    {
        public Ketban()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }
        ChromeDriver drv; Thread Th;

        private void txtTukhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dong_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.txtTukhoa = txtTukhoa.Text;
            Settings.Default.Number = Convert.ToInt32(Number.Text);

            Settings.Default.Save();

            this.Close();
        }

        private void Number_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ketban_Load(object sender, EventArgs e)
        {
            txtTukhoa.Text = Settings.Default.txtTukhoa;
            Number.Text = Settings.Default.Number.ToString();
        }
    }
}
