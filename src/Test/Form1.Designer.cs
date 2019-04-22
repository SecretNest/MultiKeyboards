namespace Test
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.deviceList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.refreshDeviceList = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox_AllKeys = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBox_Code = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1227, 832);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.deviceList);
            this.tabPage1.Controls.Add(this.refreshDeviceList);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1219, 799);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Devices";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // deviceList
            // 
            this.deviceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.deviceList.FullRowSelect = true;
            this.deviceList.GridLines = true;
            this.deviceList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.deviceList.HideSelection = false;
            this.deviceList.Location = new System.Drawing.Point(8, 6);
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(1088, 785);
            this.deviceList.TabIndex = 1;
            this.deviceList.UseCompatibleStateImageBehavior = false;
            this.deviceList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Handle";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 400;
            // 
            // refreshDeviceList
            // 
            this.refreshDeviceList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshDeviceList.Location = new System.Drawing.Point(1102, 6);
            this.refreshDeviceList.Name = "refreshDeviceList";
            this.refreshDeviceList.Size = new System.Drawing.Size(109, 36);
            this.refreshDeviceList.TabIndex = 0;
            this.refreshDeviceList.Text = "Refresh";
            this.refreshDeviceList.UseVisualStyleBackColor = true;
            this.refreshDeviceList.Click += new System.EventHandler(this.RefreshDeviceList_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBox_AllKeys);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1219, 799);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Single Key";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox_AllKeys
            // 
            this.listBox_AllKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_AllKeys.FormattingEnabled = true;
            this.listBox_AllKeys.ItemHeight = 20;
            this.listBox_AllKeys.Location = new System.Drawing.Point(3, 3);
            this.listBox_AllKeys.Name = "listBox_AllKeys";
            this.listBox_AllKeys.Size = new System.Drawing.Size(1213, 793);
            this.listBox_AllKeys.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox_Code);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1095, 683);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Code";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listBox_Code
            // 
            this.listBox_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Code.FormattingEnabled = true;
            this.listBox_Code.ItemHeight = 20;
            this.listBox_Code.Location = new System.Drawing.Point(0, 0);
            this.listBox_Code.Name = "listBox_Code";
            this.listBox_Code.Size = new System.Drawing.Size(1095, 683);
            this.listBox_Code.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 832);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Test";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView deviceList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button refreshDeviceList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox_AllKeys;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listBox_Code;
    }
}

