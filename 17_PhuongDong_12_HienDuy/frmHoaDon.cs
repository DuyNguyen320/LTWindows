﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _17_PhuongDong_12_HienDuy
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon(frmGiaoDienChinh frm)
        {
            InitializeComponent();
            frm1 = frm;
            this.CenterToScreen();
        }
        frmGiaoDienChinh frm1;
        private void HoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm1.Show();
        }
    }
}
