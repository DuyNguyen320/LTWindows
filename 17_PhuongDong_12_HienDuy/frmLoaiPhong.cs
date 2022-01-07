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
    public partial class frmLoaiPhong : Form
    {
        DataSet ds = new DataSet();
        DataSet dsLoaiPhong = new DataSet();
        public frmLoaiPhong()
        {
            InitializeComponent();
        }

        clsQuanLyKhachSan c = new clsQuanLyKhachSan();

        void HienThiDuLieu(String sql, DataGridView dgs)
        {
            ds = c.LayDuLieu(sql);
            dgs.DataSource = ds.Tables[0];
        }
        void HienThiTextBox(DataSet d, int vt)
        {
            txtMaPH.Text = d.Tables[0].Rows[vt]["MaPhong"].ToString();
            txtLoaiPhong.Text = d.Tables[0].Rows[vt]["LoaiPhong"].ToString();
            

        }
        void Xuly_Textbox(Boolean t)
        {
            txtMaPH.ReadOnly = t;
            txtLoaiPhong.ReadOnly = t;
        }
        void clearTextBox()
        {
            txtMaPH.Clear();
            txtLoaiPhong.Clear();           
        }
        void Xuly_Chucnang(Boolean t)
        {
            btnThem.Enabled = t;
            btnSua.Enabled = t;
            btnHuy.Enabled = !t;
            btnXoa.Enabled = t;
            btnLuu.Enabled = !t;
        }
        string Phatsinhma(DataSet d, string kytudb)
        {
            string maps = "";
            int sodong = ds.Tables[0].Rows.Count;
            sodong = sodong + 1;
            if (sodong < 10)
                maps = kytudb + "0" + sodong.ToString();
            else
                maps = kytudb + sodong.ToString();
            return maps;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Xuly_Textbox(false);
            Xuly_Chucnang(false);
            txtMaPH.ReadOnly = true;
            clearTextBox();
            txtMaPH.Text=Phatsinhma(ds, "LP");
            flag = 1;
            txtLoaiPhong.Clear();
        }
        int flag = 0;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            Xuly_Textbox(true);
            Xuly_Chucnang(true);
            string sql = "";
            if (flag == 1)
            {
                sql = "insert into LoaiPhong values('" + txtMaPH.Text + "',N'" + txtLoaiPhong.Text + "')";

            }
            else if (flag == 2)
            {
                sql = "update LoaiPhong set MaPhong = '" + txtMaPH.Text + "', LoaiPhong = N'" + txtLoaiPhong.Text + "' where MaPhong = '" + txtMaPH.Text + "'";

            }
            else
            {
                sql = "delete from LoaiPhong where MaPhong = '" + txtMaPH.Text + "'";

            }
            if (c.CapNhatDuLieu(sql) != 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                frmLoaiPhong_Load(sender, e);
            }


            flag = 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Xuly_Textbox(true);
            Xuly_Chucnang(false);
            txtMaPH.ReadOnly = false;
            flag = 2;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            Xuly_Textbox(true);
            Xuly_Chucnang(true);
        }
        void HienThiComboBox(DataSet ds, string ten, string ma, ComboBox c)
        {
            c.DataSource = ds.Tables[0];
            c.DisplayMember = ten;
            c.ValueMember = ma;
            c.SelectedIndex = 0;
        }
        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            Xuly_Textbox(true);
            Xuly_Chucnang(true);
            HienThiDuLieu("select * from LoaiPhong", dgvDanhSach);
            dsLoaiPhong = c.LayDuLieu("select * from LoaiPhong");
            HienThiTextBox(ds, 0);

        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = dgvDanhSach.CurrentCell.RowIndex;
            HienThiTextBox(ds, vt);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xuly_Chucnang(false);
            flag = 3;
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtGiaPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void frmLoaiPhong_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
