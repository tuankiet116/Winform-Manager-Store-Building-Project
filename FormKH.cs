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
    public partial class FormKH : Form
    {
        public FormKH()
        {
            InitializeComponent();
            LoadData();
        }
        bool finished = false;
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");
        private void LoadData()
        {
            SqlCommand com = new SqlCommand("Select * from KhachHang", conn);
            DataTable tb = new DataTable();
            try
            {
                conn.Open();
                tb.Load(com.ExecuteReader());
                comboBox1.DisplayMember = "MaKH";
                comboBox1.ValueMember = "MaKH";
                comboBox1.DataSource = tb;
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối!");
            }
            finally
            {
                conn.Close();
                finished = true;
            }

        }
        private void FormKH_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from KhachHang", conn);
            DataTable tb = new DataTable(); 
            try
            {
                conn.Open();
                tb.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = tb;
                comboBox1.Text = " ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            finished = false;
            if (textBox1.TextLength < 2 || textBox2.TextLength < 2 || textBox3.TextLength < 2 || textBox4.TextLength < 9)
            {
                MessageBox.Show("Vui lòng nhập!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into KhachHang(MaKH, TenKH, DiaChi, SDT) values (@MaKH, @TenKH, @DC, @SDT) ", conn);
                cmd.Parameters.AddWithValue("@MaKH", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenKH", textBox2.Text);
                cmd.Parameters.AddWithValue("@DC", textBox3.Text);
                cmd.Parameters.AddWithValue("@SDT", textBox4.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable tb = new DataTable();
                try
                {
                    conn.Open();
                    da.Fill(ds, "KhachHang");
                    cmd = new SqlCommand("select * from KhachHang", conn);
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    
                    MessageBox.Show("Thêm thành công");
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối!");
                }
                finally
                {
                    conn.Close();
                    LoadData();
                    finished = true;
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            finished = false;
            if (textBox1.TextLength < 2 || textBox2.TextLength < 2 || textBox3.TextLength < 2 || textBox4.TextLength < 9)
            {
                MessageBox.Show("Vui lòng nhập!");
            }
            else
            {

                SqlCommand cmd = new SqlCommand("update KhachHang set MaKH = @MaKH, TenKH = @TenKH, SDT= @SDT, DiaChi = @DC where MaKH = @MaKH_Original ", conn);
                cmd.Parameters.AddWithValue("@MaKH", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenKH", textBox2.Text);
                cmd.Parameters.AddWithValue("@DC", textBox3.Text);
                cmd.Parameters.AddWithValue("@SDT", textBox4.Text);
                cmd.Parameters.AddWithValue("@MaKH_Original", comboBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable tb = new DataTable();
                try
                {
                    conn.Open();
                    da.Fill(ds, "KhachHang");
                    cmd = new SqlCommand("select * from KhachHang", conn);
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    
                    MessageBox.Show("Sửa thành công!");
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    //MessageBox.Show("Check the text field! Something went wrong!");
                }
                finally
                {
                    conn.Close();
                    LoadData();
                    finished = true;
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            finished = false;
            if (comboBox1.Text == null)
            {
                MessageBox.Show("Please Choose Member!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from KhachHang where MaKH = @MaKH", conn);
                cmd.Parameters.AddWithValue("@MaKH", comboBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    da.Fill(ds, "KhachHang");
                    cmd = new SqlCommand("select * from KhachHang", conn);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    MessageBox.Show("Xóa thành công!");
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối!");
                }
                finally
                {
                    conn.Close();
                    LoadData();
                    finished = true;
                }
            }
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            finished = false;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            finished = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from KhachHang where MaKH = @P or TenKH = @P or SDT = @P or DiaChi = @P", conn);
            cmd.Parameters.AddWithValue("@P", textBox5.Text);
            try
            {
                conn.Open();
                DataTable tb = new DataTable();
                tb.Load(cmd.ExecuteReader());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dataGridView1.DataSource = tb;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Không tồn tại");
                    conn.Close();
                }

            }
            catch
            {
                MessageBox.Show("Không thể kết nối");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(finished == true)
            {
                SqlCommand cmd = new SqlCommand("Select * from KhachHang where MaKH = @MaKH", conn);
                cmd.Parameters.AddWithValue("@MaKH", comboBox1.Text);
                DataTable tb = new DataTable();
                try
                {
                    conn.Open();
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
            
        }
    }
}
