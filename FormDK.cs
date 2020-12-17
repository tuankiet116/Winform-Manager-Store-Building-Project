using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStoreBuilding
{
    public partial class FormDK : Form
    {
        public FormDK()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.TextLength <3 || textBox2.TextLength<5 || textBox3.TextLength < 5)
            {
                MessageBox.Show("Mật khẩu quá ngắn!!");
            }
            else
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");
                    SqlCommand cmd = new SqlCommand("insert into NguoiDung(TaiKhoan, MatKhau) values (@TK, @MK)", conn);
                    cmd.Parameters.AddWithValue("@TK", textBox1.Text);
                    cmd.Parameters.AddWithValue("@MK", textBox2.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    try
                    {
                        conn.Open();
                        da.Fill(ds, "NguoiDung");
                        MessageBox.Show("Đăng Ký Thành Công");
                        
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
                else
                {
                    MessageBox.Show("Mật khẩu không trùng khớp!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new FormLogin();
            f.ShowDialog();
        }

     
    }
}
