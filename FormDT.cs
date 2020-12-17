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
    public partial class FormDT : Form
    {
        public FormDT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");
            SqlCommand cmd = new SqlCommand("select * " +
                " from HoaDon" +
                " where NgayTao between @DayStart and @DayEnd", conn);
            cmd.Parameters.AddWithValue("@DayStart", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@DayEnd", dateTimePicker2.Value);

            try
            {
                conn.Open();
                DataTable tb = new DataTable();
                tb.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = tb;
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
            SqlCommand cmd1 = new SqlCommand("select Sum(ThanhTien) as DoanhThu " +
                " from HoaDon" +
                " where NgayTao between @DayStart and @DayEnd", conn);
            cmd1.Parameters.AddWithValue("@DayStart", dateTimePicker1.Value);
            cmd1.Parameters.AddWithValue("@DayEnd", dateTimePicker2.Value);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    int ThanhTien = (int)dr.GetInt32(dr.GetOrdinal("DoanhThu"));
                    textBox1.Text = ThanhTien.ToString();
                }
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
