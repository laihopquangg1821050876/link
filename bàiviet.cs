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
    public partial class bàiviet : Form
    {
        public bàiviet()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            Settings.Default.txtTukhoabaiviet =  txtTukhoabaiviet.Text;
            Settings.Default.txtSoluongbaiviet = Convert.ToInt32(txtSoluongbaiviet.Text);
            Settings.Default.txtComent = txtComent.Text;
            Settings.Default.Save();

            this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {

            this.Hide();
        }
        private void txtTukhoabaiviet_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoluongbaiviet_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtComent_TextChanged(object sender, EventArgs e)
        {

        }

        private void bàiviet_Load(object sender, EventArgs e)
        {
            txtTukhoabaiviet.Text = Settings.Default.txtTukhoabaiviet;
            txtSoluongbaiviet.Text = Settings.Default.txtSoluongbaiviet.ToString();
            txtComent.Text = Settings.Default.txtComent;

            
        }
    }
}
