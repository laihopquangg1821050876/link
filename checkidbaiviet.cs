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
    public partial class checkidbaiviet : Form
    {
        public checkidbaiviet()
        {
            InitializeComponent();
        }

        private void dong_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void saves_Click(object sender, EventArgs e)
        {
            Settings.Default.txtIdbaiviet = Convert.ToInt32(txtIdbaiviet.Text);
            Settings.Default.tukhoanhom = Convert.ToInt32(txtIdnhom.Text);

            Settings.Default.Save();
            this.Close();
        }

        private void checkidbaiviet_Load(object sender, EventArgs e)
        {
            txtIdbaiviet.Text = Settings.Default.txtIdbaiviet.ToString();
            txtIdnhom.Text = Settings.Default.tukhoanhom.ToString();
        }
    }
}
