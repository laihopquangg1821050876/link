namespace m.fb
{
    partial class checkidbaiviet
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
            this.Idbaiviet = new System.Windows.Forms.Label();
            this.txtIdbaiviet = new System.Windows.Forms.TextBox();
            this.Idnhom = new System.Windows.Forms.Label();
            this.txtIdnhom = new System.Windows.Forms.TextBox();
            this.saves = new System.Windows.Forms.Button();
            this.dong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Idbaiviet
            // 
            this.Idbaiviet.AutoSize = true;
            this.Idbaiviet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Idbaiviet.Location = new System.Drawing.Point(12, 21);
            this.Idbaiviet.Name = "Idbaiviet";
            this.Idbaiviet.Size = new System.Drawing.Size(80, 19);
            this.Idbaiviet.TabIndex = 0;
            this.Idbaiviet.Text = "ID bài viết";
            // 
            // txtIdbaiviet
            // 
            this.txtIdbaiviet.Location = new System.Drawing.Point(115, 22);
            this.txtIdbaiviet.Multiline = true;
            this.txtIdbaiviet.Name = "txtIdbaiviet";
            this.txtIdbaiviet.Size = new System.Drawing.Size(138, 145);
            this.txtIdbaiviet.TabIndex = 1;
            // 
            // Idnhom
            // 
            this.Idnhom.AutoSize = true;
            this.Idnhom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Idnhom.Location = new System.Drawing.Point(12, 221);
            this.Idnhom.Name = "Idnhom";
            this.Idnhom.Size = new System.Drawing.Size(67, 19);
            this.Idnhom.TabIndex = 2;
            this.Idnhom.Text = "ID nhóm";
            // 
            // txtIdnhom
            // 
            this.txtIdnhom.Location = new System.Drawing.Point(115, 222);
            this.txtIdnhom.Multiline = true;
            this.txtIdnhom.Name = "txtIdnhom";
            this.txtIdnhom.Size = new System.Drawing.Size(138, 157);
            this.txtIdnhom.TabIndex = 3;
            // 
            // saves
            // 
            this.saves.BackColor = System.Drawing.Color.Red;
            this.saves.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saves.Location = new System.Drawing.Point(16, 173);
            this.saves.Name = "saves";
            this.saves.Size = new System.Drawing.Size(108, 33);
            this.saves.TabIndex = 4;
            this.saves.Text = "Lưu";
            this.saves.UseVisualStyleBackColor = false;
            this.saves.Click += new System.EventHandler(this.saves_Click);
            // 
            // dong
            // 
            this.dong.BackColor = System.Drawing.Color.Blue;
            this.dong.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dong.Location = new System.Drawing.Point(142, 173);
            this.dong.Name = "dong";
            this.dong.Size = new System.Drawing.Size(111, 33);
            this.dong.TabIndex = 4;
            this.dong.Text = "Đóng";
            this.dong.UseVisualStyleBackColor = false;
            this.dong.Click += new System.EventHandler(this.dong_Click);
            // 
            // checkidbaiviet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(263, 392);
            this.Controls.Add(this.dong);
            this.Controls.Add(this.saves);
            this.Controls.Add(this.txtIdnhom);
            this.Controls.Add(this.Idnhom);
            this.Controls.Add(this.txtIdbaiviet);
            this.Controls.Add(this.Idbaiviet);
            this.Name = "checkidbaiviet";
            this.Text = "Check id bai viet";
            this.Load += new System.EventHandler(this.checkidbaiviet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Idbaiviet;
        private System.Windows.Forms.TextBox txtIdbaiviet;
        private System.Windows.Forms.Label Idnhom;
        private System.Windows.Forms.TextBox txtIdnhom;
        private System.Windows.Forms.Button saves;
        private System.Windows.Forms.Button dong;
    }
}