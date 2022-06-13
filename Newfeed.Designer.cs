namespace m.fb
{
    partial class mNewfeed
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
            this.sobaituongtacnewfeed = new System.Windows.Forms.Label();
            this.txtsobaituongtacnewfeed = new System.Windows.Forms.TextBox();
            this.Commentnewfeed = new System.Windows.Forms.Label();
            this.txtCommentnewfeed = new System.Windows.Forms.TextBox();
            this.Saves = new System.Windows.Forms.Button();
            this.dong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sobaituongtacnewfeed
            // 
            this.sobaituongtacnewfeed.AutoSize = true;
            this.sobaituongtacnewfeed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sobaituongtacnewfeed.Location = new System.Drawing.Point(13, 23);
            this.sobaituongtacnewfeed.Name = "sobaituongtacnewfeed";
            this.sobaituongtacnewfeed.Size = new System.Drawing.Size(69, 19);
            this.sobaituongtacnewfeed.TabIndex = 0;
            this.sobaituongtacnewfeed.Text = "Số lượng";
            this.sobaituongtacnewfeed.Click += new System.EventHandler(this.sobaituongtacnewfeed_Click);
            // 
            // txtsobaituongtacnewfeed
            // 
            this.txtsobaituongtacnewfeed.Location = new System.Drawing.Point(100, 22);
            this.txtsobaituongtacnewfeed.Name = "txtsobaituongtacnewfeed";
            this.txtsobaituongtacnewfeed.Size = new System.Drawing.Size(33, 20);
            this.txtsobaituongtacnewfeed.TabIndex = 1;
            this.txtsobaituongtacnewfeed.TextChanged += new System.EventHandler(this.txtsobaituongtacnewfeed_TextChanged);
            // 
            // Commentnewfeed
            // 
            this.Commentnewfeed.AutoSize = true;
            this.Commentnewfeed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Commentnewfeed.Location = new System.Drawing.Point(11, 49);
            this.Commentnewfeed.Name = "Commentnewfeed";
            this.Commentnewfeed.Size = new System.Drawing.Size(73, 19);
            this.Commentnewfeed.TabIndex = 2;
            this.Commentnewfeed.Text = "Comment";
            // 
            // txtCommentnewfeed
            // 
            this.txtCommentnewfeed.Location = new System.Drawing.Point(100, 48);
            this.txtCommentnewfeed.Name = "txtCommentnewfeed";
            this.txtCommentnewfeed.Size = new System.Drawing.Size(100, 20);
            this.txtCommentnewfeed.TabIndex = 3;
            // 
            // Saves
            // 
            this.Saves.BackColor = System.Drawing.Color.Red;
            this.Saves.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Saves.Location = new System.Drawing.Point(15, 91);
            this.Saves.Name = "Saves";
            this.Saves.Size = new System.Drawing.Size(67, 39);
            this.Saves.TabIndex = 4;
            this.Saves.Text = "Lưu";
            this.Saves.UseVisualStyleBackColor = false;
            this.Saves.Click += new System.EventHandler(this.Saves_Click);
            // 
            // dong
            // 
            this.dong.BackColor = System.Drawing.Color.Blue;
            this.dong.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dong.Location = new System.Drawing.Point(133, 91);
            this.dong.Name = "dong";
            this.dong.Size = new System.Drawing.Size(67, 39);
            this.dong.TabIndex = 5;
            this.dong.Text = "Đóng";
            this.dong.UseVisualStyleBackColor = false;
            this.dong.Click += new System.EventHandler(this.dong_Click);
            // 
            // mNewfeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(217, 147);
            this.Controls.Add(this.dong);
            this.Controls.Add(this.Saves);
            this.Controls.Add(this.txtCommentnewfeed);
            this.Controls.Add(this.Commentnewfeed);
            this.Controls.Add(this.txtsobaituongtacnewfeed);
            this.Controls.Add(this.sobaituongtacnewfeed);
            this.Name = "mNewfeed";
            this.Text = "Newfeed";
            this.Load += new System.EventHandler(this.Newfeed_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sobaituongtacnewfeed;
        private System.Windows.Forms.TextBox txtsobaituongtacnewfeed;
        private System.Windows.Forms.Label Commentnewfeed;
        private System.Windows.Forms.TextBox txtCommentnewfeed;
        private System.Windows.Forms.Button Saves;
        private System.Windows.Forms.Button dong;
    }
}