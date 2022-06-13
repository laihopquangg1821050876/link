namespace m.fb
{
    partial class Ketban
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
            this.Tukhoa = new System.Windows.Forms.Label();
            this.txtTukhoa = new System.Windows.Forms.TextBox();
            this.Soluong = new System.Windows.Forms.Label();
            this.Number = new System.Windows.Forms.TextBox();
            this.Saves = new System.Windows.Forms.Button();
            this.Dong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tukhoa
            // 
            this.Tukhoa.AutoSize = true;
            this.Tukhoa.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Tukhoa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tukhoa.Location = new System.Drawing.Point(21, 21);
            this.Tukhoa.Name = "Tukhoa";
            this.Tukhoa.Size = new System.Drawing.Size(70, 19);
            this.Tukhoa.TabIndex = 0;
            this.Tukhoa.Text = "Từ khóa ";
            // 
            // txtTukhoa
            // 
            this.txtTukhoa.Location = new System.Drawing.Point(111, 22);
            this.txtTukhoa.Name = "txtTukhoa";
            this.txtTukhoa.Size = new System.Drawing.Size(98, 20);
            this.txtTukhoa.TabIndex = 1;
            this.txtTukhoa.TextChanged += new System.EventHandler(this.txtTukhoa_TextChanged);
            // 
            // Soluong
            // 
            this.Soluong.AutoSize = true;
            this.Soluong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Soluong.Location = new System.Drawing.Point(21, 49);
            this.Soluong.Name = "Soluong";
            this.Soluong.Size = new System.Drawing.Size(73, 19);
            this.Soluong.TabIndex = 2;
            this.Soluong.Text = "Số lượng ";
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(111, 50);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(40, 20);
            this.Number.TabIndex = 3;
            this.Number.TextChanged += new System.EventHandler(this.Number_TextChanged);
            // 
            // Saves
            // 
            this.Saves.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Saves.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Saves.Location = new System.Drawing.Point(12, 86);
            this.Saves.Name = "Saves";
            this.Saves.Size = new System.Drawing.Size(82, 34);
            this.Saves.TabIndex = 4;
            this.Saves.Text = "Lưu";
            this.Saves.UseVisualStyleBackColor = false;
            this.Saves.Click += new System.EventHandler(this.button1_Click);
            // 
            // Dong
            // 
            this.Dong.BackColor = System.Drawing.Color.Red;
            this.Dong.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dong.Location = new System.Drawing.Point(129, 86);
            this.Dong.Name = "Dong";
            this.Dong.Size = new System.Drawing.Size(80, 32);
            this.Dong.TabIndex = 5;
            this.Dong.Text = "Đóng";
            this.Dong.UseVisualStyleBackColor = false;
            this.Dong.Click += new System.EventHandler(this.Dong_Click);
            // 
            // Ketban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(221, 141);
            this.Controls.Add(this.Dong);
            this.Controls.Add(this.Saves);
            this.Controls.Add(this.Number);
            this.Controls.Add(this.Soluong);
            this.Controls.Add(this.txtTukhoa);
            this.Controls.Add(this.Tukhoa);
            this.Name = "Ketban";
            this.Text = "Ketban";
            this.Load += new System.EventHandler(this.Ketban_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Tukhoa;
        private System.Windows.Forms.TextBox txtTukhoa;
        private System.Windows.Forms.Label Soluong;
        private System.Windows.Forms.TextBox Number;
        private System.Windows.Forms.Button Saves;
        private System.Windows.Forms.Button Dong;
    }
}