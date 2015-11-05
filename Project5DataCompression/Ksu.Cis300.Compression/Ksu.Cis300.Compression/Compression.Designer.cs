namespace Ksu.Cis300.Compression
{
    partial class Compression
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
            this.uxCompressButton = new System.Windows.Forms.Button();
            this.uxOpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.uxDecompressButton = new System.Windows.Forms.Button();
            this.uxSaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.uxOpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.uxSaveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // uxCompressButton
            // 
            this.uxCompressButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCompressButton.Location = new System.Drawing.Point(12, 12);
            this.uxCompressButton.Name = "uxCompressButton";
            this.uxCompressButton.Size = new System.Drawing.Size(260, 45);
            this.uxCompressButton.TabIndex = 0;
            this.uxCompressButton.Text = "Compress a File";
            this.uxCompressButton.UseVisualStyleBackColor = true;
            this.uxCompressButton.Click += new System.EventHandler(this.uxCompressButton_Click);
            // 
            // uxOpenFileDialog1
            // 
            this.uxOpenFileDialog1.Filter = "All Files (*.*)|*.*";
            this.uxOpenFileDialog1.Title = "Compress File";
            // 
            // uxDecompressButton
            // 
            this.uxDecompressButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxDecompressButton.Location = new System.Drawing.Point(12, 63);
            this.uxDecompressButton.Name = "uxDecompressButton";
            this.uxDecompressButton.Size = new System.Drawing.Size(260, 45);
            this.uxDecompressButton.TabIndex = 1;
            this.uxDecompressButton.Text = "Decompress a File";
            this.uxDecompressButton.UseVisualStyleBackColor = true;
            this.uxDecompressButton.Click += new System.EventHandler(this.uxDecompressButton_Click);
            // 
            // uxSaveFileDialog1
            // 
            this.uxSaveFileDialog1.Filter = "Compressed files (*.cmp)|*.cmp|All Files (*.*)|*.*";
            this.uxSaveFileDialog1.SupportMultiDottedExtensions = true;
            this.uxSaveFileDialog1.Title = "Save As";
            // 
            // uxOpenFileDialog2
            // 
            this.uxOpenFileDialog2.Filter = "Compressed files (*.cmp)|*.cmp|All Files (*.*)|*.*";
            this.uxOpenFileDialog2.SupportMultiDottedExtensions = true;
            this.uxOpenFileDialog2.Title = "Decompress File";
            // 
            // uxSaveFileDialog2
            // 
            this.uxSaveFileDialog2.Filter = "All Files (*.*)|*.*";
            this.uxSaveFileDialog2.Title = "Save As";
            // 
            // Compression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 128);
            this.Controls.Add(this.uxDecompressButton);
            this.Controls.Add(this.uxCompressButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Compression";
            this.Text = "File Compressor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxCompressButton;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog1;
        private System.Windows.Forms.Button uxDecompressButton;
        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog1;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog2;
        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog2;
    }
}

