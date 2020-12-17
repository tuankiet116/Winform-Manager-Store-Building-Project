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

namespace ManagerStoreBuilding
{
    public partial class FormHD : Form
    {
        public FormHD()
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
            SqlCommand cmd = new SqlCommand("Select ThanhTien from HoaDon where MaHD = @MaHD", conn);
            cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    thanhtien = (int)dr.GetInt32(dr.GetOrdinal("ThanhTien"));
                }
                conn.Close();
            }
            catch
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

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
                MessageBox.Show("Đã Cập Nhật Bảng Vật Tư!");
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
        private void deleteDetail()
        {
            SqlCommand cmd = new SqlCommand("Select * from CTHoaDon where MaHD = @MaHD", conn);
            cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                conn.Close();
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from CTHoaDon where MaHD = @MaHD", conn);
                    cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DataTable tb = new DataTable();
                    da.Fill(ds, "CTHoaDon");
                    cmd = new SqlCommand("Select * from CTHoaDon", conn);
                    tb.Load(cmd.ExecuteReader());
                    dataGridViewCT.DataSource = tb;
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
        private void FormHD_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from HoaDon", conn);
            DataTable tb = new DataTable();
            try
            {
                conn.Open();
                tb.Load(cmd.ExecuteReader());
                dataGridViewHD.DataSource = tb;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi Kết Nối!");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into HoaDon(MaHD, NgayTao, ThanhTien, MaKH) values(@MaHD, @NgayTao, 0, @MaKH)", conn);
            cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
            cmd.Parameters.AddWithValue("@NgayTao", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@MaKH", textBoxMaKH.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable tb = new DataTable();

            if (textBoxMaHD.TextLength > 2 || textBoxMaKH.TextLength > 2 )
            {
                try
                {
                    conn.Open();
                    da.Fill(ds, "HoaDon");
                    cmd = new SqlCommand("select * from HoaDon", conn);
                    tb.Load(cmd.ExecuteReader());
                    dataGridViewHD.DataSource = tb;
                    conn.Close();
                }
                catch (Exception ee)
                {
                    //MessageBox.Show("Check the values");
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập !!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxMaHD.TextLength < 2)
            {
                MessageBox.Show("Vui lòng nhập!!");
            }
            else
            {
                DialogResult rs = MessageBox.Show("Nó sẽ xóa tất cả các chi tiết thuộc id này! Bạn có muốn tiếp tục ? ", "Warning", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete HoaDon where MaHD = @MaHD", conn);
                    cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DataTable tb = new DataTable();
                    deleteDetail();
                    if (textBoxMaHD.TextLength > 3)
                    {
                        try
                        {
                            conn.Open();
                            da.Fill(ds, "HoaDon");
                            cmd = new SqlCommand("select * from HoaDon", conn);
                            tb.Load(cmd.ExecuteReader());
                            dataGridViewHD.DataSource = tb;
                            conn.Close();
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
                        MessageBox.Show("Vui lòng nhập!!");
                    }
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxMaHD.TextLength < 3)
            {
                MessageBox.Show("Vui lòng nhập!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select * from CTHoaDon where MaHD = @MaHD", conn);
                cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                DataTable tb = new DataTable();
                try
                {
                    conn.Open();
                    tb.Load(cmd.ExecuteReader());
                    dataGridViewCT.DataSource = tb;
                    conn.Close();
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
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBoxMaHD.TextLength<3 || textBoxSoLuong.TextLength < 1 || textBoxMaVT.TextLength<2)
            {
                MessageBox.Show("Vui lòng nhập!!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update CTHoaDon set MaHD = @MaHD, SoLuong = @SoLuong, DonGia = @DonGia, ThanhTien = @ThanhTien where  MaHD = @MaHD and MaVT = @MaVT", conn);
                SqlCommand cmd1 = new SqlCommand("update HoaDon set ThanhTien = @TT where MaHD = @MaHD", conn);
                SqlCommand cmd2 = new SqlCommand("select ThanhTien, SoLuong from CTHoaDon where MaHD = @MaHD and MaVT = @MaVT", conn);
                int thanhtien = int.Parse(textBoxDonGia.Text) * int.Parse(textBoxSoLuong.Text);
                cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                cmd.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);
                cmd.Parameters.AddWithValue("@SoLuong", textBoxSoLuong.Text);
                cmd.Parameters.AddWithValue("@DonGia", textBoxDonGia.Text);
                cmd.Parameters.AddWithValue("@ThanhTien", thanhtien.ToString());
                cmd1.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                cmd2.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                cmd2.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);
                int tongtien = loadThanhTien();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                int thanhtiencu = 0;
                int SL = 0;
                try
                {
                    conn.Open();
                    
                    SqlDataReader dr = cmd2.ExecuteReader();
                    if (dr.Read())
                    {
                        thanhtiencu = (int)dr.GetInt32(dr.GetOrdinal("ThanhTien"));
                        SL = (int)dr.GetInt32(dr.GetOrdinal("SoLuong"));
                    }
                    tongtien = tongtien - thanhtiencu + thanhtien;
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
                }

                cmd1.Parameters.AddWithValue("@TT", tongtien);

                try
                {
                    conn.Open();

                    da.Fill(ds, "CTHoaDon");

                    da = new SqlDataAdapter(cmd1);
                    da.Fill(ds, "HoaDon");

                    cmd = new SqlCommand("Select * from CTHoaDon where MaHD = @MaHD", conn);
                    cmd1 = new SqlCommand("Select  * from HoaDon ", conn);
                    cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridViewCT.DataSource = tb;

                    DataTable tb1 = new DataTable();
                    tb1.Load(cmd1.ExecuteReader());
                    dataGridViewHD.DataSource = tb1;

                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
                }
                SL = SL - int.Parse(textBoxSoLuong.Text);
                updateVatTu(textBoxMaVT.Text, SL.ToString(), 1);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBoxMaHD.TextLength < 3 || textBoxMaVT.TextLength < 2)
            {
                MessageBox.Show("Vui lòng nhập!!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from CTHoaDon where MaHD = @MaHD and MaVT = @MaVT ", conn);
                SqlCommand cmd1 = new SqlCommand("select ThanhTien from CTHoaDon where MaHD = @MaHD and MaVT = @MaVT", conn);
                cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                cmd.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);

                cmd1.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                cmd1.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);

                int tongtien = loadThanhTien();

                int thanhtien = 0;

                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        thanhtien = (int)dr.GetInt32(dr.GetOrdinal("ThanhTien"));
                    }
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
                }
                
                tongtien = tongtien - thanhtien;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                try
                {
                    conn.Open();
                    da.Fill(ds, "CTHoaDon");

                    cmd1 = new SqlCommand("update HoaDon set ThanhTien = @ThanhTien where MaHD = @MaHD", conn);
                    cmd1.Parameters.AddWithValue("@ThanhTien", tongtien);
                    cmd1.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);

                    da = new SqlDataAdapter(cmd1);
                    da.Fill(ds, "HoaDon");

                    cmd = new SqlCommand("Select * from CTHoaDon where MaHD = @MaHD", conn);
                    cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridViewCT.DataSource = tb;

                    cmd1 = new SqlCommand("Select * from HoaDon", conn);
                    DataTable tb1 = new DataTable();
                    tb1.Load(cmd1.ExecuteReader());
                    dataGridViewCT.DataSource = tb1;
                    conn.Close();
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

        private void button7_Click(object sender, EventArgs e)
        {
            if(textBoxMaHD.TextLength>2 && textBoxMaVT.TextLength>2 && textBoxSoLuong.TextLength > 0)
            {
                SqlCommand cmd = new SqlCommand("insert into CTHoaDon(MaHD, MaVT, SoLuong, DonGia, ThanhTien) values (@MaHD, @MaVT, @SoLuong, @DonGia, @ThanhTien)", conn);
                int thanhtien = int.Parse(textBoxDonGia.Text) * int.Parse(textBoxSoLuong.Text);


                cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);
                cmd.Parameters.AddWithValue("@MaVT", textBoxMaVT.Text);
                cmd.Parameters.AddWithValue("@SoLuong", textBoxSoLuong.Text);
                cmd.Parameters.AddWithValue("@DonGia", textBoxDonGia.Text);
                cmd.Parameters.AddWithValue("@ThanhTien", thanhtien);

                int tongtien = loadThanhTien();
                tongtien += thanhtien;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                bool check = false;
                try
                {
                    conn.Open();
                    da.Fill(ds, "CTHoaDon");
                    cmd = new SqlCommand("update HoaDon set ThanhTien = @ThanhTien where MaHD  = @MaHD", conn);
                    cmd.Parameters.AddWithValue("@ThanhTien", tongtien);
                    cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);

                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "HoaDon");
                    cmd = new SqlCommand("Select * from CTHoaDon where MaHD = @MaHD", conn);
                    cmd.Parameters.AddWithValue("@MaHD", textBoxMaHD.Text);

                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridViewCT.DataSource = tb;

                    cmd = new SqlCommand("Select * from HoaDon", conn);
                    DataTable tb1 = new DataTable();
                    tb1.Load(cmd.ExecuteReader());
                    dataGridViewHD.DataSource = tb1;
                    check = true;
                }
                catch
                {
                    MessageBox.Show("Vui Lòng Kiểm Tra Lại Thông TIn");
                }
                finally
                {
                    conn.Close();
                }
                if(check == true)
                {
                    updateVatTu(textBoxMaVT.Text.ToString(), textBoxSoLuong.Text.ToString(), 0);
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng nhập!!");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = textBoxTimKiem.Text;
            if (s.Contains('/') || s.Contains('-'))
            {
                SqlCommand dmd = new SqlCommand("select * from HoaDon where Cast(NgayTao as date) = @T", conn);
                dmd.Parameters.AddWithValue("@T", textBoxTimKiem.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(dmd.ExecuteReader());
                    SqlDataReader dr = dmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridViewHD.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy trong bảng HoaDon");
                    }

                }
                catch
                {
                    MessageBox.Show("Kí tự không hợp lệ");
                }
                finally
                {
                    conn.Close();
                }
            }
            if (IsNumber(s))
            {
                SqlCommand smd = new SqlCommand("Select * from HoaDon where MaHD = @T or ThanhTien = @T or MaKH = @T", conn);
                SqlCommand smd1 = new SqlCommand("Select * from CTHoaDon where MaHD = @T or DonGia = @T  or ThanhTien = @T or SoLuong = @T or MaVT = @T", conn);
                smd.Parameters.AddWithValue("@T", textBoxTimKiem.Text);

                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(smd.ExecuteReader());
                    SqlDataReader dr = smd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridViewHD.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy trong bảng HoaDon");
                    }
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
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
                        dataGridViewCT.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy trong bảng HoaDon");
                    }

                    conn.Close();
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
                SqlCommand cmd = new SqlCommand("Select * from HoaDon where MaHD = @T or MaKH = @T", conn);
                SqlCommand cmd1 = new SqlCommand("Select * from CTHoaDon where MaHD = @T or MaVT = @T", conn);

                cmd.Parameters.AddWithValue("@T", textBoxTimKiem.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridViewHD.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy trong bảng HoaDon");
                    }
                    conn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    conn.Close();
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
                        dataGridViewCT.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy trong bảng HoaDon");
                    }

                    conn.Close();
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
        }

        private void dataGridViewHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaHD.Text = dataGridViewHD.Rows[e.RowIndex].Cells[0].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridViewHD.Rows[e.RowIndex].Cells[1].Value;
            textBoxThanhTienHD.Text = dataGridViewHD.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxMaKH.Text = dataGridViewHD.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dataGridViewCT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaHD.Text = dataGridViewCT.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBoxMaVT.Text = dataGridViewCT.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxDonGia.Text = dataGridViewCT.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxSoLuong.Text = dataGridViewCT.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBoxThanhTienCT.Text = dataGridViewCT.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void textBoxMaVT_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select DonGia from VatTu where MaVT = @P", conn);
            cmd.Parameters.AddWithValue("@P", textBoxMaVT.Text);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr.HasRows)
                    {
                        textBoxDonGia.Text = dr.GetInt32(dr.GetOrdinal("DonGia")).ToString();
                    }
                }
            }
            finally
            {
                conn.Close();
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

        
    }
}
