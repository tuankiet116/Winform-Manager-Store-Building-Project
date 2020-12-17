using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace ManagerStoreBuilding
{
    public partial class FormPN : Form
    {
        public FormPN()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");

        private bool IsNumber(string s)
        {
            foreach (Char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        private int loadThanhTien()
        {
            int thanhtien = 0;
            SqlCommand cmd = new SqlCommand("Select ThanhTien from PhieuNhap where MaPN = @MaPN", conn);
            cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                thanhtien = (int)dr.GetInt32(dr.GetOrdinal("ThanhTien"));
            }
            conn.Close();

            return thanhtien;
        }
        private void updateVatTu(string MaVT, string SoLuong, int i)
        {
            SqlCommand select = new SqlCommand("Select SoLuong from VatTu where MaVT = @MaVT", conn);
            SqlCommand cmd = new SqlCommand("update VatTu set SoLuong = @SoLuong where MaVT = @MaVT", conn);

            select.Parameters.AddWithValue("@MaVT", MaVT);
            int SL = 0;
            conn.Open();
            SqlDataReader dr = select.ExecuteReader();
            if (dr.Read())
            {
                //i=1 là cộng, i khác 1 là trừ
                SL = (int)dr.GetInt32(dr.GetOrdinal("SoLuong"));
                if (i == 1)
                {
                    SL = SL + int.Parse(SoLuong);
                }
                else
                {
                    SL = SL - int.Parse(SoLuong);
                }
            }

            conn.Close();

            cmd.Parameters.AddWithValue("@MaVT", MaVT);
            cmd.Parameters.AddWithValue("@SoLuong", SL);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds, "VatTu");
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        private void FormPN_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from PhieuNhap", conn);
            DataTable tb = new DataTable();
            try
            {
                conn.Open();
                tb.Load(cmd.ExecuteReader());
                dataGridView2.DataSource = tb;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxMaPN.TextLength < 3)
            {
                MessageBox.Show("Text field could not empty");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into PhieuNhap(MaPN, NgayNhap, ThanhTien) values(@MaPN, @NgayNhap, 0)", conn);
                cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd.Parameters.AddWithValue("@NgayNhap", dateTimePicker1.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable tb = new DataTable();

                if (textBoxMaPN.TextLength > 2)
                {
                    try
                    {
                        conn.Open();
                        da.Fill(ds, "PhieuNhap");
                        cmd = new SqlCommand("select * from PhieuNhap", conn);
                        tb.Load(cmd.ExecuteReader());
                        dataGridView2.DataSource = tb;
                        conn.Close();
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show("Check the values");
                        MessageBox.Show(ee.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập lại!");
                }
            }
            
        }
        private void deleteDetail()
        {
            SqlCommand cmd = new SqlCommand("Select * from CTPhieuNhap where MaPN = @MaPN", conn);

            //string[] arrayMaVT = new string[100];
            //string[] arraySL = new string[100];
            cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                conn.Close();
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from CTPhieuNhap where MaPN = @MaPN", conn);
                    cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DataTable tb = new DataTable();
                    da.Fill(ds, "CTPhieuNhap");
                    cmd = new SqlCommand("Select * from CTPhieuNhap", conn);
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
            else
            {
                conn.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxMaPN.TextLength < 3)
            {
                MessageBox.Show("Vui lòng nhập lại!");
            }
            else
            {
                DialogResult rs = MessageBox.Show("Nó sẽ xóa tất cả các chi tiết thuộc id này! Bạn có muốn tiếp tục?", "Warning", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete PhieuNhap where MaPN = @MaPN", conn);
                    cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DataTable tb = new DataTable();
                    deleteDetail();
                    if (textBoxMaPN.TextLength > 3)
                    {
                        try
                        {
                            conn.Open();
                            da.Fill(ds, "PhieuNhap");
                            cmd = new SqlCommand("select * from PhieuNhap", conn);
                            tb.Load(cmd.ExecuteReader());
                            dataGridView2.DataSource = tb;
                            conn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Vui lòng nhập lại");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập lại!");
                    }
                }
            }
            
        }

        
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBoxMaPN.TextLength < 3 || textBoxMaVT.TextLength<3)
            {
                MessageBox.Show("Vui lòng nhập lại!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from CTPhieuNhap where MaPN = @MaPN and MaVT = @MaVT ", conn);
                SqlCommand cmd1 = new SqlCommand("select ThanhTien from CTPhieuNhap where MaPN = @MaPN and MaVT = @MaVT", conn);
                cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);

                cmd1.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd1.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);

                int tongtien = loadThanhTien();

                int thanhtien = 0;

                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    thanhtien = (int)dr.GetInt32(dr.GetOrdinal("ThanhTien"));

                }
                conn.Close();
                tongtien = tongtien - thanhtien;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                try
                {
                    conn.Open();
                    da.Fill(ds, "CTPhieuNhap");

                    cmd1 = new SqlCommand("update PhieuNhap set ThanhTien = @ThanhTien where MaPN = @MaPN", conn);
                    cmd1.Parameters.AddWithValue("@ThanhTien", tongtien);
                    cmd1.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);

                    da = new SqlDataAdapter(cmd1);
                    da.Fill(ds, "PhieuNhap");

                    cmd = new SqlCommand("Select * from CTPhieuNhap where MaPN = @MaPN", conn);
                    cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;

                    cmd1 = new SqlCommand("Select * from PhieuNhap", conn);
                    DataTable tb1 = new DataTable();
                    tb1.Load(cmd1.ExecuteReader());
                    dataGridView2.DataSource = tb1;
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                updateVatTu(textBoxMaVT.Text.ToString(), textBoxSoLuong.Text.ToString(), 0);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxMaPN.TextLength < 3)
            {
                MessageBox.Show("Vui lòng nhập lại");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select * from CTPhieuNhap where MaPN = @MaPN", conn);
                cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                DataTable tb = new DataTable();
                try
                {
                    conn.Open();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBoxMaPN.TextLength < 3 || textBoxMaVT.TextLength < 3)
            {
                MessageBox.Show("Vui lòng nhập lại");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into CTPhieuNhap(MaPN, MaVT, SoLuong, DonGia, ThanhTien) values (@MaPN, @MaVT, @SoLuong, @DonGia, @ThanhTien)", conn);
                int thanhtien = int.Parse(textBoxDonGia.Text) * int.Parse(textBoxSoLuong.Text);


                cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);
                cmd.Parameters.AddWithValue("@SoLuong", textBoxSoLuong.Text);
                cmd.Parameters.AddWithValue("@DonGia", textBoxDonGia.Text);
                cmd.Parameters.AddWithValue("@ThanhTien", thanhtien);

                int tongtien = loadThanhTien();
                tongtien += thanhtien;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();


                try
                {
                    conn.Open();
                    da.Fill(ds, "CTPhieuNhap");
                    cmd = new SqlCommand("update PhieuNhap set ThanhTien = @ThanhTien where MaPN  = @MaPN", conn);
                    cmd.Parameters.AddWithValue("@ThanhTien", tongtien);
                    cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);

                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "PhieuNhap");
                    cmd = new SqlCommand("Select * from CTPhieuNhap where MaPN = @MaPN", conn);
                    cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);

                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;

                    cmd = new SqlCommand("Select * from PhieuNhap", conn);
                    DataTable tb1 = new DataTable();
                    tb1.Load(cmd.ExecuteReader());
                    dataGridView2.DataSource = tb1;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
                }
                updateVatTu(textBoxMaVT.Text.ToString(), textBoxSoLuong.Text.ToString(), 1);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBoxMaPN.TextLength < 3 || textBoxMaVT.TextLength < 3)
            {
                MessageBox.Show("Vui lòng nhập lại");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update CTPhieuNhap set MaVT = @MaVT, SoLuong = @SoLuong, DonGia = @DonGia, ThanhTien = @ThanhTien where  MaPN = @MaPN and MaVT = @MaVT", conn);
                SqlCommand cmd1 = new SqlCommand("update PhieuNhap set ThanhTien = @TT where MaPN = @MaPN", conn);
                SqlCommand cmd2 = new SqlCommand("select ThanhTien, SoLuong from CTPhieuNhap where MaPN = @MaPN and MaVT = @MaVT", conn);
                int thanhtien = int.Parse(textBoxDonGia.Text) * int.Parse(textBoxSoLuong.Text);
                cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);
                cmd.Parameters.AddWithValue("@SoLuong", textBoxSoLuong.Text);
                cmd.Parameters.AddWithValue("@DonGia", textBoxDonGia.Text);
                cmd.Parameters.AddWithValue("@ThanhTien", thanhtien.ToString());
                cmd1.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd2.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                cmd2.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);
                int tongtien = loadThanhTien();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                conn.Open();
                int thanhtiencu = 0;
                int SL = 0;
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    thanhtiencu = (int)dr.GetInt32(dr.GetOrdinal("ThanhTien"));
                    SL = (int)dr.GetInt32(dr.GetOrdinal("SoLuong"));
                }
                tongtien = tongtien - thanhtiencu + thanhtien;

                cmd1.Parameters.AddWithValue("@TT", tongtien);
                conn.Close();

                try
                {
                    conn.Open();

                    da.Fill(ds, "CTPhieuNhap");

                    da = new SqlDataAdapter(cmd1);
                    da.Fill(ds, "PhieuNhap");

                    cmd = new SqlCommand("Select * from CTPhieuNhap where MaPN = @MaPN", conn);
                    cmd1 = new SqlCommand("Select  * from PhieuNhap ", conn);
                    cmd.Parameters.AddWithValue("@MaPN", textBoxMaPN.Text);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;

                    DataTable tb1 = new DataTable();
                    tb1.Load(cmd1.ExecuteReader());
                    dataGridView2.DataSource = tb1;

                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                SL = SL - int.Parse(textBoxSoLuong.Text);
                updateVatTu(textBoxMaVT.Text, SL.ToString(), 0);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaPN.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBoxMaVT.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxDonGia.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxSoLuong.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBoxThanhTienCT.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaPN.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView2.Rows[e.RowIndex].Cells[1].Value;
            textBoxThanhTienPN.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string s = textBoxTimKiem.Text;
            if(s.Contains('/') || s.Contains('-'))
            {
                SqlCommand dmd = new SqlCommand("select * from PhieuNhap where Cast(NgayNhap as date) = @T", conn);
                dmd.Parameters.AddWithValue("@T", textBoxTimKiem.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(dmd.ExecuteReader());
                    SqlDataReader dr = dmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView2.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại trong bảng PhieuNhap");
                    }
                    
                }
                catch
                {
                    MessageBox.Show("Kí tự không hợp lệ!");
                }
                finally
                {
                    conn.Close();
                }
            }
            if (IsNumber(s))
            {
                SqlCommand smd = new SqlCommand("Select * from PhieuNhap where MaPN = @T or ThanhTien = @T", conn);
                SqlCommand smd1 = new SqlCommand("Select * from CTPhieuNhap where MaPN = @T or DonGia = @T  or ThanhTien = @T or SoLuong = @T or MaVT = @T", conn);
                smd.Parameters.AddWithValue("@T", textBoxTimKiem.Text);

                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(smd.ExecuteReader());
                    SqlDataReader dr = smd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView2.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại trong bảng PhieuNhap");
                    }
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                smd1.Parameters.AddWithValue("@T", textBoxTimKiem.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(smd1.ExecuteReader());
                    SqlDataReader dr = smd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView1.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại trong bảng PhieuNhap");
                    }

                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select * from PhieuNhap where MaPN = @T", conn);
                SqlCommand cmd1 = new SqlCommand("Select * from CTPhieuNhap where MaPN = @T or MaVT = @T", conn);

                cmd.Parameters.AddWithValue("@T", textBoxTimKiem.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView2.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại trong bảng PhieuNhap");
                    }
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                cmd1.Parameters.AddWithValue("@T", textBoxTimKiem.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(cmd1.ExecuteReader());
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView1.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại trong bảng PhieuNhap");
                    }

                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                
            }
        }

        private void textBoxSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
















