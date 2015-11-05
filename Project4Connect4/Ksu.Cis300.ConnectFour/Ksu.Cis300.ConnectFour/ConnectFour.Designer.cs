namespace Ksu.Cis300.ConnectFour
{
    partial class ConnectFour
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
            this.uxButton1 = new System.Windows.Forms.Button();
            this.uxButton2 = new System.Windows.Forms.Button();
            this.uxButton3 = new System.Windows.Forms.Button();
            this.uxButton4 = new System.Windows.Forms.Button();
            this.uxButton5 = new System.Windows.Forms.Button();
            this.uxFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uxButton1
            // 
            this.uxButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxButton1.Location = new System.Drawing.Point(12, 12);
            this.uxButton1.Name = "uxButton1";
            this.uxButton1.Size = new System.Drawing.Size(67, 27);
            this.uxButton1.TabIndex = 0;
            this.uxButton1.Text = "V";
            this.uxButton1.UseVisualStyleBackColor = true;
            this.uxButton1.Click += new System.EventHandler(this.uxButton1_Click);
            // 
            // uxButton2
            // 
            this.uxButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxButton2.Location = new System.Drawing.Point(85, 12);
            this.uxButton2.Name = "uxButton2";
            this.uxButton2.Size = new System.Drawing.Size(67, 27);
            this.uxButton2.TabIndex = 1;
            this.uxButton2.Text = "V";
            this.uxButton2.UseVisualStyleBackColor = true;
            this.uxButton2.Click += new System.EventHandler(this.uxButton2_Click);
            // 
            // uxButton3
            // 
            this.uxButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxButton3.Location = new System.Drawing.Point(158, 12);
            this.uxButton3.Name = "uxButton3";
            this.uxButton3.Size = new System.Drawing.Size(67, 27);
            this.uxButton3.TabIndex = 2;
            this.uxButton3.Text = "V";
            this.uxButton3.UseVisualStyleBackColor = true;
            this.uxButton3.Click += new System.EventHandler(this.uxButton3_Click);
            // 
            // uxButton4
            // 
            this.uxButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxButton4.Location = new System.Drawing.Point(231, 12);
            this.uxButton4.Name = "uxButton4";
            this.uxButton4.Size = new System.Drawing.Size(67, 27);
            this.uxButton4.TabIndex = 3;
            this.uxButton4.Text = "V";
            this.uxButton4.UseVisualStyleBackColor = true;
            this.uxButton4.Click += new System.EventHandler(this.uxButton4_Click);
            // 
            // uxButton5
            // 
            this.uxButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxButton5.Location = new System.Drawing.Point(304, 12);
            this.uxButton5.Name = "uxButton5";
            this.uxButton5.Size = new System.Drawing.Size(67, 27);
            this.uxButton5.TabIndex = 4;
            this.uxButton5.Text = "V";
            this.uxButton5.UseVisualStyleBackColor = true;
            this.uxButton5.Click += new System.EventHandler(this.uxButton5_Click);
            // 
            // uxFlowLayoutPanel
            // 
            this.uxFlowLayoutPanel.Location = new System.Drawing.Point(8, 45);
            this.uxFlowLayoutPanel.Name = "uxFlowLayoutPanel";
            this.uxFlowLayoutPanel.Size = new System.Drawing.Size(372, 314);
            this.uxFlowLayoutPanel.TabIndex = 5;
            // 
            // uxTextBox
            // 
            this.uxTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTextBox.Location = new System.Drawing.Point(13, 365);
            this.uxTextBox.Name = "uxTextBox";
            this.uxTextBox.ReadOnly = true;
            this.uxTextBox.Size = new System.Drawing.Size(359, 29);
            this.uxTextBox.TabIndex = 6;
            // 
            // ConnectFour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 398);
            this.Controls.Add(this.uxTextBox);
            this.Controls.Add(this.uxFlowLayoutPanel);
            this.Controls.Add(this.uxButton5);
            this.Controls.Add(this.uxButton4);
            this.Controls.Add(this.uxButton3);
            this.Controls.Add(this.uxButton2);
            this.Controls.Add(this.uxButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConnectFour";
            this.Text = "Connect 4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxButton1;
        private System.Windows.Forms.Button uxButton2;
        private System.Windows.Forms.Button uxButton3;
        private System.Windows.Forms.Button uxButton4;
        private System.Windows.Forms.Button uxButton5;
        private System.Windows.Forms.FlowLayoutPanel uxFlowLayoutPanel;
        private System.Windows.Forms.TextBox uxTextBox;
    }
}

