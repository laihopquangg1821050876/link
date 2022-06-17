namespace m.fb
{
    partial class DangbaiNF
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
            this.Contentt = new System.Windows.Forms.Label();
            this.Commentt = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txxCommen = new System.Windows.Forms.TextBox();
            this.Saves = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Contentt
            // 
            this.Contentt.AutoSize = true;
            this.Contentt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contentt.Location = new System.Drawing.Point(12, 9);
            this.Contentt.Name = "Contentt";
            this.Contentt.Size = new System.Drawing.Size(62, 19);
            this.Contentt.TabIndex = 0;
            this.Contentt.Text = "Content";
            // 
            // Commentt
            // 
            this.Commentt.AutoSize = true;
            this.Commentt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Commentt.Location = new System.Drawing.Point(13, 69);
            this.Commentt.Name = "Commentt";
            this.Commentt.Size = new System.Drawing.Size(61, 19);
            this.Commentt.TabIndex = 1;
            this.Commentt.Text = "Coment";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(91, 9);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(138, 48);
            this.txtContent.TabIndex = 2;
            // 
            // txxCommen
            // 
            this.txxCommen.Location = new System.Drawing.Point(91, 70);
            this.txxCommen.Multiline = true;
            this.txxCommen.Name = "txxCommen";
            this.txxCommen.Size = new System.Drawing.Size(138, 34);
            this.txxCommen.TabIndex = 3;
            // 
            // Saves
            // 
            this.Saves.BackColor = System.Drawing.Color.Cyan;
            this.Saves.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Saves.Location = new System.Drawing.Point(17, 125);
            this.Saves.Name = "Saves";
            this.Saves.Size = new System.Drawing.Size(96, 39);
            this.Saves.TabIndex = 4;
            this.Saves.Text = "Lưu";
            this.Saves.UseVisualStyleBackColor = false;
            this.Saves.Click += new System.EventHandler(this.button1_Click);
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.Red;
            this.Close.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(133, 125);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(96, 39);
            this.Close.TabIndex = 4;
            this.Close.Text = "Đóng";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.button2_Click);
            // 
            // DangbaiNF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(238, 175);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.Saves);
            this.Controls.Add(this.txxCommen);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.Commentt);
            this.Controls.Add(this.Contentt);
            this.Name = "DangbaiNF";
            this.Text = "Đăng bài ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Contentt;
        private System.Windows.Forms.Label Commentt;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.TextBox txxCommen;
        private System.Windows.Forms.Button Saves;
        private System.Windows.Forms.Button Close;
    }
}