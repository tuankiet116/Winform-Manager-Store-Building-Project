namespace ManagerStoreBuilding
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripDK = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripDX = new System.Windows.Forms.ToolStripMenuItem();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripNCC = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripVT = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripKH = new System.Windows.Forms.ToolStripMenuItem();
            this.nhậpXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripNK = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripXK = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoThốngKêToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolTripKHTC = new System.Windows.Forms.ToolStripMenuItem();
            this.DT = new System.Windows.Forms.ToolStripMenuItem();
            this.panelWorkMain = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.danhMụcToolStripMenuItem,
            this.nhậpXuấtToolStripMenuItem,
            this.báoCáoThốngKêToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(887, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolTripDK,
            this.MenuToolTripDX});
            this.hệThốngToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hệThốngToolStripMenuItem.Image = global::ManagerStoreBuilding.Properties.Resources.user_icon;
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(90, 21);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            // 
            // MenuToolTripDK
            // 
            this.MenuToolTripDK.Image = global::ManagerStoreBuilding.Properties.Resources.Admin_icon;
            this.MenuToolTripDK.Name = "MenuToolTripDK";
            this.MenuToolTripDK.Size = new System.Drawing.Size(137, 22);
            this.MenuToolTripDK.Text = "Đăng Ký";
            this.MenuToolTripDK.Click += new System.EventHandler(this.MenuToolTripDK_Click);
            // 
            // MenuToolTripDX
            // 
            this.MenuToolTripDX.Image = global::ManagerStoreBuilding.Properties.Resources.Extras_Shutdown_icon;
            this.MenuToolTripDX.Name = "MenuToolTripDX";
            this.MenuToolTripDX.Size = new System.Drawing.Size(137, 22);
            this.MenuToolTripDX.Text = "Đăng Xuất";
            this.MenuToolTripDX.Click += new System.EventHandler(this.MenuToolTripDX_Click);
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolTripNCC,
            this.MenuToolTripVT,
            this.MenuToolTripKH});
            this.danhMụcToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.danhMụcToolStripMenuItem.Image = global::ManagerStoreBuilding.Properties.Resources.list_icon;
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(94, 21);
            this.danhMụcToolStripMenuItem.Text = "Danh mục";
            // 
            // MenuToolTripNCC
            // 
            this.MenuToolTripNCC.Image = global::ManagerStoreBuilding.Properties.Resources.Admin_icon;
            this.MenuToolTripNCC.Name = "MenuToolTripNCC";
            this.MenuToolTripNCC.Size = new System.Drawing.Size(180, 22);
            this.MenuToolTripNCC.Text = "Nhà Cung Cấp";
            this.MenuToolTripNCC.Click += new System.EventHandler(this.MenuToolTripNCC_Click);
            // 
            // MenuToolTripVT
            // 
            this.MenuToolTripVT.Image = global::ManagerStoreBuilding.Properties.Resources.StrappedBox_icon;
            this.MenuToolTripVT.Name = "MenuToolTripVT";
            this.MenuToolTripVT.Size = new System.Drawing.Size(180, 22);
            this.MenuToolTripVT.Text = "Vật tư";
            this.MenuToolTripVT.Click += new System.EventHandler(this.MenuToolTripVT_Click);
            // 
            // MenuToolTripKH
            // 
            this.MenuToolTripKH.Image = global::ManagerStoreBuilding.Properties.Resources.Supervisor_icon;
            this.MenuToolTripKH.Name = "MenuToolTripKH";
            this.MenuToolTripKH.Size = new System.Drawing.Size(180, 22);
            this.MenuToolTripKH.Text = "Khách Hàng";
            this.MenuToolTripKH.Click += new System.EventHandler(this.MenuToolTripKH_Click);
            // 
            // nhậpXuấtToolStripMenuItem
            // 
            this.nhậpXuấtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolTripNK,
            this.MenuToolTripXK});
            this.nhậpXuấtToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nhậpXuấtToolStripMenuItem.Image = global::ManagerStoreBuilding.Properties.Resources.download_database_icon;
            this.nhậpXuấtToolStripMenuItem.Name = "nhậpXuấtToolStripMenuItem";
            this.nhậpXuấtToolStripMenuItem.Size = new System.Drawing.Size(99, 21);
            this.nhậpXuấtToolStripMenuItem.Text = "Nhập-Xuất";
            // 
            // MenuToolTripNK
            // 
            this.MenuToolTripNK.Image = global::ManagerStoreBuilding.Properties.Resources.Downloads_Black_Folder_icon;
            this.MenuToolTripNK.Name = "MenuToolTripNK";
            this.MenuToolTripNK.Size = new System.Drawing.Size(133, 22);
            this.MenuToolTripNK.Text = "Nhập kho";
            this.MenuToolTripNK.Click += new System.EventHandler(this.MenuToolTripNK_Click);
            // 
            // MenuToolTripXK
            // 
            this.MenuToolTripXK.Image = global::ManagerStoreBuilding.Properties.Resources.box_out_icon;
            this.MenuToolTripXK.Name = "MenuToolTripXK";
            this.MenuToolTripXK.Size = new System.Drawing.Size(133, 22);
            this.MenuToolTripXK.Text = "Xuất kho";
            this.MenuToolTripXK.Click += new System.EventHandler(this.MenuToolTripXK_Click);
            // 
            // báoCáoThốngKêToolStripMenuItem
            // 
            this.báoCáoThốngKêToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolTripKHTC,
            this.DT});
            this.báoCáoThốngKêToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.báoCáoThốngKêToolStripMenuItem.Image = global::ManagerStoreBuilding.Properties.Resources.Document_Line_Chart_icon1;
            this.báoCáoThốngKêToolStripMenuItem.Name = "báoCáoThốngKêToolStripMenuItem";
            this.báoCáoThốngKêToolStripMenuItem.Size = new System.Drawing.Size(150, 21);
            this.báoCáoThốngKêToolStripMenuItem.Text = "Báo cáo - Thống kê";
            // 
            // MenuToolTripKHTC
            // 
            this.MenuToolTripKHTC.Image = global::ManagerStoreBuilding.Properties.Resources.Document_Line_Chart_icon1;
            this.MenuToolTripKHTC.Name = "MenuToolTripKHTC";
            this.MenuToolTripKHTC.Size = new System.Drawing.Size(199, 22);
            this.MenuToolTripKHTC.Text = "Khách Hàng Tích Cực";
            this.MenuToolTripKHTC.Click += new System.EventHandler(this.MenuToolTripKHTC_Click);
            // 
            // DT
            // 
            this.DT.Image = global::ManagerStoreBuilding.Properties.Resources.Money_icon;
            this.DT.Name = "DT";
            this.DT.Size = new System.Drawing.Size(199, 22);
            this.DT.Text = "Doanh Thu";
            this.DT.Click += new System.EventHandler(this.DT_Click);
            // 
            // panelWorkMain
            // 
            this.panelWorkMain.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelWorkMain.BackgroundImage = global::ManagerStoreBuilding.Properties.Resources._9f5d6b94496517_5e808d30f29c2;
            this.panelWorkMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelWorkMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkMain.Location = new System.Drawing.Point(0, 25);
            this.panelWorkMain.Name = "panelWorkMain";
            this.panelWorkMain.Size = new System.Drawing.Size(887, 656);
            this.panelWorkMain.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 681);
            this.Controls.Add(this.panelWorkMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "mains";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripDX;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripNCC;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripVT;
        private System.Windows.Forms.ToolStripMenuItem nhậpXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripNK;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripXK;
        private System.Windows.Forms.ToolStripMenuItem báoCáoThốngKêToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripDK;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripKH;
        private System.Windows.Forms.ToolStripMenuItem MenuToolTripKHTC;
        private System.Windows.Forms.ToolStripMenuItem DT;
        private System.Windows.Forms.Panel panelWorkMain;
    }
}