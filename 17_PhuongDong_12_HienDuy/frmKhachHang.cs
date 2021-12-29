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
    public partial class frmKhachHang : Form
    {
        DataSet ds = new DataSet();
        DataSet dsMaphong = new DataSet();
        public frmKhachHang()
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
            txtHoTen.Text = d.Tables[0].Rows[vt]["HoTen"].ToString();           
            txtDiaChi.Text = d.Tables[0].Rows[vt]["DiaChi"].ToString();
            txtSoCMND.Text = d.Tables[0].Rows[vt]["CMND"].ToString();
            txtSoDienThoai.Text = d.Tables[0].Rows[vt]["SDT"].ToString();
            txtSoDienThoai.Text = d.Tables[0].Rows[vt]["SDT"].ToString();
            dtpNgayDen.Value =(DateTime)d.Tables[0].Rows[vt]["NgayDen"];
            txtMaKH.Text = d.Tables[0].Rows[vt]["MaKH"].ToString();
            string MaPhong = d.Tables[0].Rows[vt]["MaPH"].ToString();
            switch(MaPhong.ToLower())
            {
                case "ph01":
                    cboMaPhong.SelectedIndex = 0;
                    break;
                case "ph02":
                    cboMaPhong.SelectedIndex = 1;
                    break;
                case "ph03":
                    cboMaPhong.SelectedIndex = 2;
                    break;
                case "ph04":
                    cboMaPhong.SelectedIndex = 3;
                    break;
                case "ph05":
                    cboMaPhong.SelectedIndex = 4;
                    break;
                case "ph06":
                    cboMaPhong.SelectedIndex = 5;
                    break;
                case "ph07":
                    cboMaPhong.SelectedIndex = 6;
                    break;
            }

            string GioiTinh = d.Tables[0].Rows[vt]["GioiTinh"].ToString();
            if (GioiTinh.ToLower() == "nam")
                cbmGioiTinh.SelectedIndex = 0;
            else
                cbmGioiTinh.SelectedIndex = 1;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        void Xuly_Textbox(Boolean t)
        {
            txtMaKH.ReadOnly = t;
            txtHoTen.ReadOnly = t;
            txtSoCMND.ReadOnly = t;
            txtSoDienThoai.ReadOnly = t;
            txtDiaChi.ReadOnly = t;
        }

        void Xuly_Chucnang(Boolean t)
        {
            btnThem.Enabled = t;
            btnSua.Enabled = t;
            btnHuy.Enabled = !t;
            btnXoa.Enabled = t;
            btnLuu.Enabled = !t;
        }
        int flag = 0;
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            Xuly_Textbox(true);
            Xuly_Chucnang(true);
            HienThiDuLieu("select * from KhachHang", dgvDanhSachKhachHang);
            dsMaphong = c.LayDuLieu("select Phong.MaPhong,LoaiPhong from Phong inner join LoaiPhong on Phong.MaLoai = LoaiPhong.MaPhong");
            HienThiComboBox(ref dsMaphong, "MaPhong", "MaPhong",cboMaPhong);
            HienThiTextBox(ds, 0);
        }
        void clearTextBox()
        {
            txtDiaChi.Clear();
            txtHoTen.Clear();
            txtSoCMND.Clear();
            txtSoDienThoai.Clear();
            cbmGioiTinh.SelectedIndex = 0;
            cboMaPhong.SelectedIndex = 0;
        }
        string phatSinhMa (DataSet d, string kytu)
        {
            string ma = "";
            int sodong = d.Tables[0].Rows.Count;
            sodong++;
            if (sodong < 10)
                ma = kytu + "0" + sodong.ToString();
            else
                ma = kytu + sodong.ToString();
            return ma;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            txtMaKH.Text = phatSinhMa(ds, "K").ToString();
            Xuly_Textbox(false);
            Xuly_Chucnang(false);
            txtMaKH.ReadOnly = true;
            clearTextBox();
            flag = 1;
        }
        void HienThiComboBox(ref DataSet ds , string ten, string ma, ComboBox c)
        {
            c.DataSource = ds.Tables[0];
            c.DisplayMember = ten;
            c.ValueMember = ma;
            c.SelectedIndex = 0;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            Xuly_Textbox(true);
            Xuly_Chucnang(true);
            string sql = "";
            if (flag == 1)
            {
                sql = "insert into KhachHang values ('" + txtMaKH.Text +"','" +cboMaPhong.SelectedValue+"',N'"+ txtHoTen.Text + "','"+ dtpNgaySinh.Text +"','" + txtSoDienThoai.Text +"', '" + txtSoCMND.Text+"',N'" +txtDiaChi.Text+"',N'"+ cbmGioiTinh.Items[cbmGioiTinh.SelectedIndex] +"','" + dtpNgayDen.Text+"')";

            }
            else if (flag == 2)
            {
                sql = "update KhachHang set MaKH = '" + txtMaKH.Text + "', MaPH = '" + cboMaPhong.SelectedValue + "', HoTen = N'" + txtHoTen.Text + "', NgaySinh = '"+dtpNgaySinh.Text+"', SDT = '" + txtSoDienThoai.Text + "', CMND = '" + txtSoCMND.Text + "',DiaChi= N'" + txtDiaChi.Text + "', GioiTinh = N'" + cbmGioiTinh.Items[cbmGioiTinh.SelectedIndex] + "', NgayDen = '" + dtpNgayDen.Text + "' where MaKH = '" + txtMaKH.Text + "'";

            }
            else
            {
                sql = "delete from KhachHang where MaKH = '" + txtMaKH.Text + "'";

            }
            if (c.CapNhatDuLieu(sql) != 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                frmKhachHang_Load(sender, e);
            }


            flag = 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Xuly_Textbox(false);
            Xuly_Chucnang(false);
            txtMaKH.ReadOnly = true;
            flag = 2;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Xuly_Textbox(true);
            Xuly_Chucnang(true);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDanhSachKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = dgvDanhSachKhachHang.CurrentCell.RowIndex;
            HienThiTextBox(ds, vt);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xuly_Chucnang(false);
            flag = 3;
        }

        private void txtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        private void txtSoCMND_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSoCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
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
