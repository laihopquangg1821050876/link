﻿using System;
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

            this.Close();
        }

        private void Closes_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}