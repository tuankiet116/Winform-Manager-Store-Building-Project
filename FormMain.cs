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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormLogin fl = new FormLogin();
            DialogResult rs = fl.ShowDialog();
            if (rs == DialogResult.OK)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }
        private Form activeForm = null;
        private void openWorkForm(Form workForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = workForm;
            workForm.TopLevel = false;
            workForm.FormBorderStyle = FormBorderStyle.None;
            workForm.Dock = DockStyle.Fill;
            panelWorkMain.Controls.Add(workForm);
            panelWorkMain.Tag = workForm;
            workForm.BringToFront();
            workForm.Show();
        }
        private void MenuToolTripNCC_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormNCC());
        }

        private void MenuToolTripVT_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormVT());
        }

        private void MenuToolTripKH_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormKH());
        }
        private void MenuToolTripNK_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormPN());
        }
    

        private void MenuToolTripXK_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormHD());
        }

        private void MenuToolTripKHTC_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormKHTC());
        }

        private void DT_Click(object sender, EventArgs e)
        {
            openWorkForm(new FormDT());
        }


        private void MenuToolTripDX_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin f = new FormLogin();
            DialogResult rs = f.ShowDialog();
            if (rs == DialogResult.OK)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void MenuToolTripDK_Click(object sender, EventArgs e)
        {
            Form f = new FormDK();
            f.ShowDialog();
        }
    }
}
