using m.fb.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace m.fb
{
    public partial class tuongtacbanbe : Form
    {
        public tuongtacbanbe()
        {
            InitializeComponent();
        }

        private void tuongtacbanbe_Load(object sender, EventArgs e)
        {
            
            txtbb.Text = Settings.Default.txtbb.ToString();
            txtdangbaibanbe.Text = Settings.Default.txtdangbaibanbe;
            txtcmt.Text = Settings.Default.txtcmt;
            soluongcmt.Text = Settings.Default.soluongcmt.ToString();
        }

        private void save_Click(object sender, EventArgs e)
        {
            
            Settings.Default.txtbb = Convert.ToInt32(txtbb.Text);
            Settings.Default.txtdangbaibanbe = txtdangbaibanbe.Text;
            Settings.Default.txtcmt = txtcmt.Text;
            Settings.Default.soluongcmt = Convert.ToInt32(soluongcmt.Text);

            Settings.Default.Save();

            this.Close(); 
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void soluongcmt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
 

