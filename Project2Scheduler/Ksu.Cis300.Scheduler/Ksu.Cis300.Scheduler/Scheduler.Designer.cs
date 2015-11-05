namespace Ksu.Cis300.Scheduler
{
    partial class Scheduler
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
            this.uxInput = new System.Windows.Forms.Button();
            this.uxTextBox = new System.Windows.Forms.TextBox();
            this.uxLabel = new System.Windows.Forms.Label();
            this.uxNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.uxComputeSchedule = new System.Windows.Forms.Button();
            this.uxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxSaveDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.uxNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // uxInput
            // 
            this.uxInput.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxInput.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxInput.Location = new System.Drawing.Point(12, 12);
            this.uxInput.Name = "uxInput";
            this.uxInput.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.uxInput.Size = new System.Drawing.Size(158, 29);
            this.uxInput.TabIndex = 0;
            this.uxInput.Text = "Input File";
            this.uxInput.UseVisualStyleBackColor = true;
            this.uxInput.Click += new System.EventHandler(this.uxInput_Click);
            // 
            // uxTextBox
            // 
            this.uxTextBox.Location = new System.Drawing.Point(176, 12);
            this.uxTextBox.Name = "uxTextBox";
            this.uxTextBox.ReadOnly = true;
            this.uxTextBox.Size = new System.Drawing.Size(259, 20);
            this.uxTextBox.TabIndex = 1;
            // 
            // uxLabel
            // 
            this.uxLabel.AutoSize = true;
            this.uxLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLabel.Location = new System.Drawing.Point(12, 49);
            this.uxLabel.Name = "uxLabel";
            this.uxLabel.Size = new System.Drawing.Size(158, 22);
            this.uxLabel.TabIndex = 2;
            this.uxLabel.Text = "Schedule Length:";
            // 
            // uxNumericUpDown
            // 
            this.uxNumericUpDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxNumericUpDown.Location = new System.Drawing.Point(176, 45);
            this.uxNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.uxNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxNumericUpDown.Name = "uxNumericUpDown";
            this.uxNumericUpDown.Size = new System.Drawing.Size(120, 26);
            this.uxNumericUpDown.TabIndex = 3;
            this.uxNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uxComputeSchedule
            // 
            this.uxComputeSchedule.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxComputeSchedule.Enabled = false;
            this.uxComputeSchedule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxComputeSchedule.Location = new System.Drawing.Point(302, 42);
            this.uxComputeSchedule.Name = "uxComputeSchedule";
            this.uxComputeSchedule.Size = new System.Drawing.Size(133, 29);
            this.uxComputeSchedule.TabIndex = 4;
            this.uxComputeSchedule.Text = "Compute Schedule";
            this.uxComputeSchedule.UseVisualStyleBackColor = true;
            this.uxComputeSchedule.Click += new System.EventHandler(this.uxComputeSchedule_Click);
            // 
            // uxOpenFileDialog
            // 
            this.uxOpenFileDialog.Filter = "CSV files (*.csv)|*.csv";
            this.uxOpenFileDialog.Title = "Select Input File";
            // 
            // uxSaveDialog
            // 
            this.uxSaveDialog.Filter = "CSV files (*.csv)|*.csv";
            // 
            // Scheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 98);
            this.Controls.Add(this.uxComputeSchedule);
            this.Controls.Add(this.uxNumericUpDown);
            this.Controls.Add(this.uxLabel);
            this.Controls.Add(this.uxTextBox);
            this.Controls.Add(this.uxInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Scheduler";
            this.Text = "Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.uxNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxInput;
        private System.Windows.Forms.TextBox uxTextBox;
        private System.Windows.Forms.Label uxLabel;
        private System.Windows.Forms.NumericUpDown uxNumericUpDown;
        private System.Windows.Forms.Button uxComputeSchedule;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog uxSaveDialog;
    }
}

