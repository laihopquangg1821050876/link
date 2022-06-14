namespace m.fb
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.login = new System.Windows.Forms.Button();
            this.ketban = new System.Windows.Forms.Button();
            this.Joingroup = new System.Windows.Forms.Button();
            this.baiviet = new System.Windows.Forms.Button();
            this.ttbanbe = new System.Windows.Forms.Button();
            this.ttnewfeed = new System.Windows.Forms.Button();
            this.CheckId = new System.Windows.Forms.Button();
            this.dgvDatalst = new System.Windows.Forms.DataGridView();
            this.cChose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cUid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c2Fa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bỏChọnTấtẢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteUidPass2FaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUidPass2FaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Soluog = new System.Windows.Forms.Label();
            this.Soluongg = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatalst)).BeginInit();
            this.ctms.SuspendLayout();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.SystemColors.Highlight;
            this.login.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login.Location = new System.Drawing.Point(12, 309);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(114, 36);
            this.login.TabIndex = 2;
            this.login.Text = " Login";
            this.login.UseVisualStyleBackColor = false;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // ketban
            // 
            this.ketban.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ketban.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ketban.Location = new System.Drawing.Point(12, 11);
            this.ketban.Name = "ketban";
            this.ketban.Size = new System.Drawing.Size(150, 34);
            this.ketban.TabIndex = 3;
            this.ketban.Text = "Kết bạn ";
            this.ketban.UseVisualStyleBackColor = false;
            this.ketban.Click += new System.EventHandler(this.ketban_Click);
            // 
            // Joingroup
            // 
            this.Joingroup.BackColor = System.Drawing.Color.Fuchsia;
            this.Joingroup.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Joingroup.Location = new System.Drawing.Point(356, 51);
            this.Joingroup.Name = "Joingroup";
            this.Joingroup.Size = new System.Drawing.Size(150, 33);
            this.Joingroup.TabIndex = 4;
            this.Joingroup.Text = "Tham gia nhóm";
            this.Joingroup.UseVisualStyleBackColor = false;
            this.Joingroup.Click += new System.EventHandler(this.Joingroup_Click);
            // 
            // baiviet
            // 
            this.baiviet.BackColor = System.Drawing.Color.Aqua;
            this.baiviet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baiviet.Location = new System.Drawing.Point(12, 51);
            this.baiviet.Name = "baiviet";
            this.baiviet.Size = new System.Drawing.Size(150, 33);
            this.baiviet.TabIndex = 5;
            this.baiviet.Text = "Bài Viết";
            this.baiviet.UseVisualStyleBackColor = false;
            this.baiviet.Click += new System.EventHandler(this.button1_Click);
            // 
            // ttbanbe
            // 
            this.ttbanbe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ttbanbe.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ttbanbe.Location = new System.Drawing.Point(188, 51);
            this.ttbanbe.Name = "ttbanbe";
            this.ttbanbe.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ttbanbe.Size = new System.Drawing.Size(150, 33);
            this.ttbanbe.TabIndex = 6;
            this.ttbanbe.Text = "Tương tác bạn bè";
            this.ttbanbe.UseVisualStyleBackColor = false;
            this.ttbanbe.Click += new System.EventHandler(this.ttbanbe_Click);
            // 
            // ttnewfeed
            // 
            this.ttnewfeed.BackColor = System.Drawing.Color.Lime;
            this.ttnewfeed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ttnewfeed.Location = new System.Drawing.Point(356, 12);
            this.ttnewfeed.Name = "ttnewfeed";
            this.ttnewfeed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ttnewfeed.Size = new System.Drawing.Size(150, 33);
            this.ttnewfeed.TabIndex = 7;
            this.ttnewfeed.Text = "Tương tác newfeed";
            this.ttnewfeed.UseVisualStyleBackColor = false;
            this.ttnewfeed.Click += new System.EventHandler(this.ttnewfeed_Click);
            // 
            // CheckId
            // 
            this.CheckId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.CheckId.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckId.Location = new System.Drawing.Point(188, 11);
            this.CheckId.Name = "CheckId";
            this.CheckId.Size = new System.Drawing.Size(150, 34);
            this.CheckId.TabIndex = 8;
            this.CheckId.Text = "Check ID baiviet";
            this.CheckId.UseVisualStyleBackColor = false;
            this.CheckId.Click += new System.EventHandler(this.thongbao_Click);
            // 
            // dgvDatalst
            // 
            this.dgvDatalst.AllowUserToAddRows = false;
            this.dgvDatalst.AllowUserToDeleteRows = false;
            this.dgvDatalst.AllowUserToResizeColumns = false;
            this.dgvDatalst.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatalst.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatalst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatalst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChose,
            this.cUid,
            this.cPass,
            this.c2Fa,
            this.cStatus});
            this.dgvDatalst.ContextMenuStrip = this.ctms;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDatalst.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDatalst.Location = new System.Drawing.Point(12, 90);
            this.dgvDatalst.Name = "dgvDatalst";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatalst.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDatalst.RowHeadersVisible = false;
            this.dgvDatalst.Size = new System.Drawing.Size(494, 204);
            this.dgvDatalst.TabIndex = 9;
            // 
            // cChose
            // 
            this.cChose.HeaderText = "Chọn";
            this.cChose.Name = "cChose";
            this.cChose.Width = 40;
            // 
            // cUid
            // 
            this.cUid.HeaderText = "Uid";
            this.cUid.Name = "cUid";
            // 
            // cPass
            // 
            this.cPass.HeaderText = "Pass";
            this.cPass.Name = "cPass";
            // 
            // c2Fa
            // 
            this.c2Fa.HeaderText = "2Fa";
            this.c2Fa.Name = "c2Fa";
            // 
            // cStatus
            // 
            this.cStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cStatus.HeaderText = "Trạng thái";
            this.cStatus.Name = "cStatus";
            // 
            // ctms
            // 
            this.ctms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chọnTấtCảToolStripMenuItem,
            this.bỏChọnTấtẢToolStripMenuItem,
            this.pasteUidPass2FaToolStripMenuItem,
            this.copyUidPass2FaToolStripMenuItem});
            this.ctms.Name = "ctms";
            this.ctms.Size = new System.Drawing.Size(171, 92);
            this.ctms.Opening += new System.ComponentModel.CancelEventHandler(this.ctms_Opening);
            // 
            // chọnTấtCảToolStripMenuItem
            // 
            this.chọnTấtCảToolStripMenuItem.Name = "chọnTấtCảToolStripMenuItem";
            this.chọnTấtCảToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.chọnTấtCảToolStripMenuItem.Text = "Chọn tất cả";
            this.chọnTấtCảToolStripMenuItem.Click += new System.EventHandler(this.chọnTấtCảToolStripMenuItem_Click);
            // 
            // bỏChọnTấtẢToolStripMenuItem
            // 
            this.bỏChọnTấtẢToolStripMenuItem.Name = "bỏChọnTấtẢToolStripMenuItem";
            this.bỏChọnTấtẢToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bỏChọnTấtẢToolStripMenuItem.Text = "Bỏ chọn tất cả";
            this.bỏChọnTấtẢToolStripMenuItem.Click += new System.EventHandler(this.bỏChọnTấtẢToolStripMenuItem_Click);
            // 
            // pasteUidPass2FaToolStripMenuItem
            // 
            this.pasteUidPass2FaToolStripMenuItem.Name = "pasteUidPass2FaToolStripMenuItem";
            this.pasteUidPass2FaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.pasteUidPass2FaToolStripMenuItem.Text = "Paste Uid|Pass|2Fa";
            this.pasteUidPass2FaToolStripMenuItem.Click += new System.EventHandler(this.pasteUidPass2FaToolStripMenuItem_Click);
            // 
            // copyUidPass2FaToolStripMenuItem
            // 
            this.copyUidPass2FaToolStripMenuItem.Name = "copyUidPass2FaToolStripMenuItem";
            this.copyUidPass2FaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.copyUidPass2FaToolStripMenuItem.Text = "Copy Uid|Pass|2Fa";
            this.copyUidPass2FaToolStripMenuItem.Click += new System.EventHandler(this.copyUidPass2FaToolStripMenuItem_Click);
            // 
            // Soluog
            // 
            this.Soluog.AutoSize = true;
            this.Soluog.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Soluog.Location = new System.Drawing.Point(350, 316);
            this.Soluog.Name = "Soluog";
            this.Soluog.Size = new System.Drawing.Size(86, 22);
            this.Soluog.TabIndex = 10;
            this.Soluog.Text = "Số luồng ";
            // 
            // Soluongg
            // 
            this.Soluongg.Location = new System.Drawing.Point(442, 316);
            this.Soluongg.Name = "Soluongg";
            this.Soluongg.Size = new System.Drawing.Size(32, 20);
            this.Soluongg.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(511, 368);
            this.Controls.Add(this.Soluongg);
            this.Controls.Add(this.Soluog);
            this.Controls.Add(this.dgvDatalst);
            this.Controls.Add(this.CheckId);
            this.Controls.Add(this.ttnewfeed);
            this.Controls.Add(this.ttbanbe);
            this.Controls.Add(this.baiviet);
            this.Controls.Add(this.Joingroup);
            this.Controls.Add(this.ketban);
            this.Controls.Add(this.login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Fb_Interactive";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatalst)).EndInit();
            this.ctms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.Button ketban;
        private System.Windows.Forms.Button Joingroup;
        private System.Windows.Forms.Button baiviet;
        private System.Windows.Forms.Button ttbanbe;
        private System.Windows.Forms.Button ttnewfeed;
        private System.Windows.Forms.Button CheckId;
        private System.Windows.Forms.DataGridView dgvDatalst;
        private System.Windows.Forms.ContextMenuStrip ctms;
        private System.Windows.Forms.ToolStripMenuItem chọnTấtCảToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bỏChọnTấtẢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteUidPass2FaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyUidPass2FaToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cChose;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUid;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn c2Fa;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStatus;
        private System.Windows.Forms.Label Soluog;
        private System.Windows.Forms.TextBox Soluongg;
    }
}

