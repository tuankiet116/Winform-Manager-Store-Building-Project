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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 2)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            button1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 5)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");
            SqlCommand cmd = new SqlCommand("Select * from NguoiDung where TaiKhoan = @TK and MatKhau = @MK", conn);
            cmd.Parameters.AddWithValue("@TK", textBox1.Text);
            cmd.Parameters.AddWithValue("@MK", textBox2.Text);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DialogResult = DialogResult.OK;
                    conn.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể đăng nhập! Vui lòng kiểm tra lại !");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
