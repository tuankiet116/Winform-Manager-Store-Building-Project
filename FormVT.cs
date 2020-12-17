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
    public partial class FormVT : Form
    {
        public FormVT()
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 2 || textBox2.TextLength < 2 || textBox3.TextLength < 2 || textBox5.TextLength < 2)
            {
                MessageBox.Show("Vui lòng nhập lại!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into VatTu(MaVT,TenVT,MaNCC, DonGia, SoLuong) values(@MaVT, @TenVT, @MaNCC, @DonGia, 0)", conn);
                cmd.Parameters.AddWithValue("@MaVT", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenVT", textBox2.Text);
                cmd.Parameters.AddWithValue("@MaNCC", textBox3.Text);
                cmd.Parameters.AddWithValue("@DonGia", textBox5.Text);

                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "VatTu");
                    cmd = new SqlCommand("select * from VatTu", conn);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    MessageBox.Show("Thêm thành công!");
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối!");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 2 || textBox2.TextLength < 2 || textBox3.TextLength < 2 || textBox5.TextLength < 2)
            {
                MessageBox.Show("Vui lòng nhập lại!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update VatTu set MaVT = @MaVT,TenVT = @TenVT,MaNCC= @MaNCC, DonGia = @DonGia, SoLuong = @SoLuong", conn);
                cmd.Parameters.AddWithValue("@MaVT", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenVT", textBox2.Text);
                cmd.Parameters.AddWithValue("@MaNCC", textBox3.Text);
                cmd.Parameters.AddWithValue("@SoLuong", textBox4.Text);
                cmd.Parameters.AddWithValue("@DonGia", textBox5.Text);

                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "VatTu");
                    cmd = new SqlCommand("select * from VatTu", conn);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    MessageBox.Show("Sửa thành công!");
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối!");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 2 || textBox2.TextLength < 2 || textBox3.TextLength < 2 || textBox5.TextLength < 2)
            {
                MessageBox.Show("Vui lòng nhập lại!!");
            }
            else
            {
                DialogResult rs = MessageBox.Show("Nó sẽ xóa tất cả các chi tiết và mọi thứ trong HoaDon và PhieuNhap với MaVT như id này, bạn sẽ mất dữ liệu của mình. Bạn có muốn tiếp tục?", "Warning", MessageBoxButtons.YesNo);

                if(rs == DialogResult.Yes)
                {
                    DataSet ds = new DataSet();

                    SqlCommand cmd = new SqlCommand("delete VatTu where MaVT = @MaVT", conn);
                    cmd.Parameters.AddWithValue("@MaVT", textBox1.Text);

                    SqlCommand cmd1 = new SqlCommand("delete from CTHoaDon where MaVT = @MaVT ", conn);
                    cmd1.Parameters.AddWithValue("@MaVT", textBox1.Text);

                    SqlCommand cmd2 = new SqlCommand("delete from CTPhieuNhap where MaVT = @MaVT ", conn);
                    cmd2.Parameters.AddWithValue("@MaVT", textBox1.Text);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        da.Fill(ds, "CTHoaDon");

                        da = new SqlDataAdapter(cmd2);
                        da.Fill(ds, "CTPhieuNhap");

                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds, "VatTu");
                        cmd = new SqlCommand("select * from VatTu", conn);
                        DataTable tb = new DataTable();
                        tb.Load(cmd.ExecuteReader());
                        dataGridView1.DataSource = tb;
                        MessageBox.Show("Xóa thành công!");
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = textBox6.Text;
            if (IsNumber(s))
            {
                SqlCommand smd = new SqlCommand("Select * from VatTu where MaVT = @T or TenVT = @T or MaNCC = @T or DonGia = @T or SoLuong = @T", conn);
                smd.Parameters.AddWithValue("@T", textBox6.Text);

                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(smd.ExecuteReader());
                    SqlDataReader dr = smd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView1.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Not Found");
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
                SqlCommand cmd = new SqlCommand("Select * from VatTu where MaVT = @T or TenVT = @T or MaNCC = @T", conn);

                cmd.Parameters.AddWithValue("@T", textBox6.Text);
                try
                {
                    conn.Open();
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dataGridView1.DataSource = tb;
                    }
                    else
                    {
                        MessageBox.Show("Not Found");
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
        

        private void FormVT_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from VatTu ", conn);
            DataTable tb = new DataTable();
            try
            {
                conn.Open();
                tb.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = tb; 
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
