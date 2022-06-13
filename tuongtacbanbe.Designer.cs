namespace m.fb
{
    partial class tuongtacbanbe
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
            this.Dangbai = new System.Windows.Forms.Label();
            this.txtdangbaibanbe = new System.Windows.Forms.TextBox();
            this.cmt = new System.Windows.Forms.Label();
            this.txtcmt = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.sobaicmt = new System.Windows.Forms.Label();
            this.soluongcmt = new System.Windows.Forms.TextBox();
            this.sobbtt = new System.Windows.Forms.Label();
            this.txtbb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Dangbai
            // 
            this.Dangbai.AutoSize = true;
            this.Dangbai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dangbai.Location = new System.Drawing.Point(12, 41);
            this.Dangbai.Name = "Dangbai";
            this.Dangbai.Size = new System.Drawing.Size(73, 19);
            this.Dangbai.TabIndex = 0;
            this.Dangbai.Text = "Đăng bài ";
            // 
            // txtdangbaibanbe
            // 
            this.txtdangbaibanbe.Location = new System.Drawing.Point(124, 42);
            this.txtdangbaibanbe.Name = "txtdangbaibanbe";
            this.txtdangbaibanbe.Size = new System.Drawing.Size(184, 20);
            this.txtdangbaibanbe.TabIndex = 1;
            // 
            // cmt
            // 
            this.cmt.AutoSize = true;
            this.cmt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmt.Location = new System.Drawing.Point(12, 74);
            this.cmt.Name = "cmt";
            this.cmt.Size = new System.Drawing.Size(68, 19);
            this.cmt.TabIndex = 2;
            this.cmt.Text = "Commen";
            // 
            // txtcmt
            // 
            this.txtcmt.Location = new System.Drawing.Point(124, 73);
            this.txtcmt.Name = "txtcmt";
            this.txtcmt.Size = new System.Drawing.Size(184, 20);
            this.txtcmt.TabIndex = 3;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.Blue;
            this.save.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save.Location = new System.Drawing.Point(16, 149);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(124, 36);
            this.save.TabIndex = 4;
            this.save.Text = "Lưu";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.Red;
            this.Close.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(185, 149);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(123, 36);
            this.Close.TabIndex = 5;
            this.Close.Text = "Đóng";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // sobaicmt
            // 
            this.sobaicmt.AutoSize = true;
            this.sobaicmt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sobaicmt.Location = new System.Drawing.Point(12, 108);
            this.sobaicmt.Name = "sobaicmt";
            this.sobaicmt.Size = new System.Drawing.Size(78, 19);
            this.sobaicmt.TabIndex = 8;
            this.sobaicmt.Text = "Số bài cmt";
            // 
            // soluongcmt
            // 
            this.soluongcmt.Location = new System.Drawing.Point(124, 107);
            this.soluongcmt.Name = "soluongcmt";
            this.soluongcmt.Size = new System.Drawing.Size(54, 20);
            this.soluongcmt.TabIndex = 9;
            this.soluongcmt.TextChanged += new System.EventHandler(this.soluongcmt_TextChanged);
            // 
            // sobbtt
            // 
            this.sobbtt.AutoSize = true;
            this.sobbtt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sobbtt.Location = new System.Drawing.Point(12, 9);
            this.sobbtt.Name = "sobbtt";
            this.sobbtt.Size = new System.Drawing.Size(97, 19);
            this.sobbtt.TabIndex = 10;
            this.sobbtt.Text = "Số lượng bạn";
            // 
            // txtbb
            // 
            this.txtbb.Location = new System.Drawing.Point(124, 10);
            this.txtbb.Name = "txtbb";
            this.txtbb.Size = new System.Drawing.Size(54, 20);
            this.txtbb.TabIndex = 11;
            // 
            // tuongtacbanbe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(326, 253);
            this.Controls.Add(this.txtbb);
            this.Controls.Add(this.sobbtt);
            this.Controls.Add(this.soluongcmt);
            this.Controls.Add(this.sobaicmt);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.txtcmt);
            this.Controls.Add(this.cmt);
            this.Controls.Add(this.txtdangbaibanbe);
            this.Controls.Add(this.Dangbai);
            this.Name = "tuongtacbanbe";
            this.Text = "tuongtacbanbe";
            this.Load += new System.EventHandler(this.tuongtacbanbe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Dangbai;
        private System.Windows.Forms.TextBox txtdangbaibanbe;
        private System.Windows.Forms.Label cmt;
        private System.Windows.Forms.TextBox txtcmt;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Label sobaicmt;
        private System.Windows.Forms.TextBox soluongcmt;
        private System.Windows.Forms.Label sobbtt;
        private System.Windows.Forms.TextBox txtbb;
    }
}