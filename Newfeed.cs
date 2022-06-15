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
    public partial class mNewfeed : Form
    {
        public mNewfeed()
        {
            InitializeComponent();
        }

        private void Saves_Click(object sender, EventArgs e)
        {
            Settings.Default.txtsobaituongtacnewfeed = Convert.ToInt32(txtsobaituongtacnewfeed.Text);
            Settings.Default.txtComment = txtCommentnewfeed.Text;

            Settings.Default.Save();
            this.Close();
        }

        private void dong_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Newfeed_Load(object sender, EventArgs e)
        {
            txtsobaituongtacnewfeed.Text = Settings.Default.txtsobaituongtacnewfeed.ToString();
            txtCommentnewfeed.Text = Settings.Default.txtComment;


        }

        private void txtsobaituongtacnewfeed_TextChanged(object sender, EventArgs e)
        {

        }

        private void sobaituongtacnewfeed_Click(object sender, EventArgs e)
        {

        }

        private void Commentnewfeed_Click(object sender, EventArgs e)
        {

        }
    }
}
