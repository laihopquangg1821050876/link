namespace m.fb
{
    partial class Thamgianhom
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
            this.tukhoanhom = new System.Windows.Forms.Label();
            this.txtTukhoanhom = new System.Windows.Forms.TextBox();
            this.soluongnhom = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.answer = new System.Windows.Forms.Label();
            this.txttraloi = new System.Windows.Forms.TextBox();
            this.Saves = new System.Windows.Forms.Button();
            this.Closes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tukhoanhom
            // 
            this.tukhoanhom.AutoSize = true;
            this.tukhoanhom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tukhoanhom.Location = new System.Drawing.Point(26, 18);
            this.tukhoanhom.Name = "tukhoanhom";
            this.tukhoanhom.Size = new System.Drawing.Size(70, 19);
            this.tukhoanhom.TabIndex = 0;
            this.tukhoanhom.Text = "Từ khóa ";
            // 
            // txtTukhoanhom
            // 
            this.txtTukhoanhom.Location = new System.Drawing.Point(120, 18);
            this.txtTukhoanhom.Name = "txtTukhoanhom";
            this.txtTukhoanhom.Size = new System.Drawing.Size(87, 20);
            this.txtTukhoanhom.TabIndex = 1;
            // 
            // soluongnhom
            // 
            this.soluongnhom.AutoSize = true;
            this.soluongnhom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soluongnhom.Location = new System.Drawing.Point(27, 50);
            this.soluongnhom.Name = "soluongnhom";
            this.soluongnhom.Size = new System.Drawing.Size(69, 19);
            this.soluongnhom.TabIndex = 2;
            this.soluongnhom.Text = "Số lượng";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 20);
            this.textBox1.TabIndex = 3;
            // 
            // answer
            // 
            this.answer.AutoSize = true;
            this.answer.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answer.Location = new System.Drawing.Point(26, 86);
            this.answer.Name = "answer";
            this.answer.Size = new System.Drawing.Size(53, 19);
            this.answer.TabIndex = 4;
            this.answer.Text = "Trả lời";
            // 
            // txttraloi
            // 
            this.txttraloi.Location = new System.Drawing.Point(120, 87);
            this.txttraloi.Name = "txttraloi";
            this.txttraloi.Size = new System.Drawing.Size(87, 20);
            this.txttraloi.TabIndex = 5;
            // 
            // Saves
            // 
            this.Saves.BackColor = System.Drawing.Color.Cyan;
            this.Saves.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Saves.Location = new System.Drawing.Point(12, 124);
            this.Saves.Name = "Saves";
            this.Saves.Size = new System.Drawing.Size(93, 37);
            this.Saves.TabIndex = 6;
            this.Saves.Text = "Lưu";
            this.Saves.UseVisualStyleBackColor = false;
            this.Saves.Click += new System.EventHandler(this.Saves_Click);
            // 
            // Closes
            // 
            this.Closes.BackColor = System.Drawing.Color.Red;
            this.Closes.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Closes.Location = new System.Drawing.Point(133, 124);
            this.Closes.Name = "Closes";
            this.Closes.Size = new System.Drawing.Size(93, 37);
            this.Closes.TabIndex = 7;
            this.Closes.Text = "Đóng";
            this.Closes.UseVisualStyleBackColor = false;
            this.Closes.Click += new System.EventHandler(this.Closes_Click);
            // 
            // Thamgianhom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(238, 174);
            this.Controls.Add(this.Closes);
            this.Controls.Add(this.Saves);
            this.Controls.Add(this.txttraloi);
            this.Controls.Add(this.answer);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.soluongnhom);
            this.Controls.Add(this.txtTukhoanhom);
            this.Controls.Add(this.tukhoanhom);
            this.Name = "Thamgianhom";
            this.Text = "Thamgianhom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tukhoanhom;
        private System.Windows.Forms.TextBox txtTukhoanhom;
        private System.Windows.Forms.Label soluongnhom;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label answer;
        private System.Windows.Forms.TextBox txttraloi;
        private System.Windows.Forms.Button Saves;
        private System.Windows.Forms.Button Closes;
    }
}