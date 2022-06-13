namespace m.fb
{
    partial class bàiviet
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
            this.tukhoabaiviet = new System.Windows.Forms.Label();
            this.txtTukhoabaiviet = new System.Windows.Forms.TextBox();
            this.Soluogbaiviet = new System.Windows.Forms.Label();
            this.txtSoluongbaiviet = new System.Windows.Forms.TextBox();
            this.Coment = new System.Windows.Forms.Label();
            this.txtComent = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tukhoabaiviet
            // 
            this.tukhoabaiviet.AutoSize = true;
            this.tukhoabaiviet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tukhoabaiviet.Location = new System.Drawing.Point(8, 9);
            this.tukhoabaiviet.Name = "tukhoabaiviet";
            this.tukhoabaiviet.Size = new System.Drawing.Size(119, 19);
            this.tukhoabaiviet.TabIndex = 0;
            this.tukhoabaiviet.Text = "Từ khóa bài viết";
            this.tukhoabaiviet.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtTukhoabaiviet
            // 
            this.txtTukhoabaiviet.Location = new System.Drawing.Point(133, 9);
            this.txtTukhoabaiviet.Name = "txtTukhoabaiviet";
            this.txtTukhoabaiviet.Size = new System.Drawing.Size(100, 20);
            this.txtTukhoabaiviet.TabIndex = 1;
            this.txtTukhoabaiviet.TextChanged += new System.EventHandler(this.txtTukhoabaiviet_TextChanged);
            // 
            // Soluogbaiviet
            // 
            this.Soluogbaiviet.AutoSize = true;
            this.Soluogbaiviet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Soluogbaiviet.Location = new System.Drawing.Point(8, 38);
            this.Soluogbaiviet.Name = "Soluogbaiviet";
            this.Soluogbaiviet.Size = new System.Drawing.Size(73, 19);
            this.Soluogbaiviet.TabIndex = 2;
            this.Soluogbaiviet.Text = "Số lượng ";
            // 
            // txtSoluongbaiviet
            // 
            this.txtSoluongbaiviet.Location = new System.Drawing.Point(133, 37);
            this.txtSoluongbaiviet.Name = "txtSoluongbaiviet";
            this.txtSoluongbaiviet.Size = new System.Drawing.Size(71, 20);
            this.txtSoluongbaiviet.TabIndex = 3;
            this.txtSoluongbaiviet.TextChanged += new System.EventHandler(this.txtSoluongbaiviet_TextChanged);
            // 
            // Coment
            // 
            this.Coment.AutoSize = true;
            this.Coment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Coment.Location = new System.Drawing.Point(8, 68);
            this.Coment.Name = "Coment";
            this.Coment.Size = new System.Drawing.Size(73, 19);
            this.Coment.TabIndex = 4;
            this.Coment.Text = "Comment";
            // 
            // txtComent
            // 
            this.txtComent.Location = new System.Drawing.Point(133, 67);
            this.txtComent.Name = "txtComent";
            this.txtComent.Size = new System.Drawing.Size(100, 20);
            this.txtComent.TabIndex = 5;
            this.txtComent.TextChanged += new System.EventHandler(this.txtComent_TextChanged);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Save.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save.Location = new System.Drawing.Point(12, 93);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(105, 31);
            this.Save.TabIndex = 6;
            this.Save.Text = "Lưu";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.Red;
            this.Close.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(133, 93);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(101, 31);
            this.Close.TabIndex = 7;
            this.Close.Text = "Đóng ";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // bàiviet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(242, 129);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.txtComent);
            this.Controls.Add(this.Coment);
            this.Controls.Add(this.txtSoluongbaiviet);
            this.Controls.Add(this.Soluogbaiviet);
            this.Controls.Add(this.txtTukhoabaiviet);
            this.Controls.Add(this.tukhoabaiviet);
            this.Name = "bàiviet";
            this.Text = "bàiviet";
            this.Load += new System.EventHandler(this.bàiviet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tukhoabaiviet;
        private System.Windows.Forms.TextBox txtTukhoabaiviet;
        private System.Windows.Forms.Label Soluogbaiviet;
        private System.Windows.Forms.TextBox txtSoluongbaiviet;
        private System.Windows.Forms.Label Coment;
        private System.Windows.Forms.TextBox txtComent;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Close;
    }
}