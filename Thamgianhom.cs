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
    public partial class Thamgianhom : Form
    {
        public Thamgianhom()
        {
            InitializeComponent();
        }

        private void Saves_Click(object sender, EventArgs e)
        {
            Settings.Default.soluongnhom = Convert.ToInt32(Slnhom.Text);
            Settings.Default.tukhoanhom = txtTukhoanhom.Text;
            Settings.Default.answer = txttraloi.Text;
            
            Settings.Default.Save();
            this.Close();
        }

        private void Closes_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Thamgianhom_Load(object sender, EventArgs e)
        {
            txtTukhoanhom.Text = Settings.Default.tukhoanhom;
            Slnhom.Text = Settings.Default.soluongnhom.ToString();
            txttraloi.Text = Settings.Default.answer;
           
        }
    }
}
