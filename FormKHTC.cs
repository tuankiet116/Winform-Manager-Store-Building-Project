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
    public partial class FormKHTC : Form
    {
        public FormKHTC()
        {
            InitializeComponent();
        }
        
        private void FormKHTC_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3228N62; Initial Catalog = QLVatTu; Integrated Security = SSPI;");

            SqlCommand cmd = new SqlCommand("select HoaDon.MaKH,KhachHang.TenKH,  KhachHang.SDT, KhachHang.DiaChi, Sum(HoaDon.ThanhTien) as ThanhToan" +
                " from HoaDon, KhachHang" +
                " where KhachHang.MaKH = HoaDon.MaKH" +
                " group by HoaDon.MaKH, KhachHang.TenKH, KhachHang.SDT, KhachHang.DiaChi" +
                " order by ThanhToan DESC ", conn);
            try
            {
                conn.Open();
                DataTable tb = new DataTable();
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
    }
}
