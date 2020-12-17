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
    public partial class FormNCC : Form
    {
        public FormNCC()
        {
            InitializeComponent();
            LoadData();
        }

        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");
        bool finished = false;
        private void LoadData()
        {
            finished = false;
            SqlCommand com = new SqlCommand("Select MaNCC, TenNCC from NCC", conn);
            try
            {
                conn.Open();
                DataTable tb = new DataTable();
                tb.Load(com.ExecuteReader());
                comboBox1.DisplayMember = "MaNCC";
                comboBox1.ValueMember = "TenNCC";
                comboBox1.DataSource = tb;
                
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                //MessageBox.Show("Invalid Connection");
            }
            finally
            {
                conn.Close();
                finished = true;
            }
            
        }
        private void FormNCC_Load(object sender, EventArgs e)
        {
            finished = false;
            SqlCommand cmd = new SqlCommand("Select * from NCC", conn);
            DataTable tb = new DataTable(); 
            try
            {
                conn.Open();
                tb.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = tb;
            }
            catch
            {
                this.Close();
                MessageBox.Show("Lỗi kết nối1");
            }
            finally
            {
                conn.Close();
                finished = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            finished = false;
            if (textBoxMNCC.TextLength<2 || textBoxTNCC.TextLength<2 || textBoxDC.TextLength<2 || textBoxSDT.TextLength < 9)
            {
                MessageBox.Show("Vui lòng nhập!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into NCC(MaNCC,TenNCC, SDT, DiaChi) values(@MaNCC, @TenNCC, @SDT, @DiaChi)", conn);
                cmd.Parameters.AddWithValue("@MaNCC", textBoxMNCC.Text);
                cmd.Parameters.AddWithValue("@TenNCC", textBoxTNCC.Text);
                cmd.Parameters.AddWithValue("@SDT", textBoxSDT.Text);
                cmd.Parameters.AddWithValue("@DiaChi", textBoxDC.Text);

                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "NCC");
                    cmd = new SqlCommand("select * from NCC", conn);
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
                    finished = true;
                    LoadData();
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            finished = false;
            if (textBoxMNCC.TextLength < 2 || textBoxTNCC.TextLength < 2 || textBoxDC.TextLength < 2 || textBoxSDT.TextLength < 9)
            {
                MessageBox.Show("Vui lòng nhập lại!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update NCC set MaNCC = @MaNCC, TenNCC = @TenNCC, SDT= @SDT, DiaChi = @DiaChi where MaNCC = @MaNCC_Original", conn);
                cmd.Parameters.AddWithValue("@MaNCC", textBoxMNCC.Text);
                cmd.Parameters.AddWithValue("@TenNCC", textBoxTNCC.Text);
                cmd.Parameters.AddWithValue("@SDT", textBoxSDT.Text);
                cmd.Parameters.AddWithValue("@DiaChi", textBoxDC.Text);
                cmd.Parameters.AddWithValue("@MaNCC_Original", comboBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    da.Fill(ds, "NCC");
                    cmd = new SqlCommand("select * from NCC", conn);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    MessageBox.Show("Sửa thành công!");
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
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
                SqlCommand cmd = new SqlCommand("delete from NCC where MaNCC = @MaNCC", conn);
                cmd.Parameters.AddWithValue("@MaNCC", comboBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    da.Fill(ds, "NCC");
                    cmd = new SqlCommand("select * from NCC", conn);
                    DataTable tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = tb;
                    MessageBox.Show("Xóa thành công!");

                    conn.Close();
                }
                catch
                {
                    MessageBox.Show("Check the text field! Something went wrong!");
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
            textBoxMNCC.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBoxTNCC.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxSDT.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxDC.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.Text = textBoxMNCC.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            finished = true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select * from NCC where MaNCC = @P or TenNCC = @P or SDT = @P or DiaChi = @P", conn);
            cmd.Parameters.AddWithValue("@P", textBox1.Text);
            try
            {   conn.Open();
                DataTable tb = new DataTable();
                tb.Load(cmd.ExecuteReader());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows){
                    dataGridView1.DataSource = tb;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Không tồn tại!");
                    conn.Close();
                }
                
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void textBoxSDT_KeyPress(object sender, KeyPressEventArgs e)
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
            if (finished == true)
            {
                SqlCommand cmd = new SqlCommand("Select * from NCC where MaNCC = @MaNCC", conn);
                cmd.Parameters.AddWithValue("@MaNCC", comboBox1.Text);
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
