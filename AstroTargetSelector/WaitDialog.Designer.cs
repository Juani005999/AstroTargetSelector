namespace AstroTargetSelector
{
    partial class WaitDialog
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
            this.textBoxWorker = new System.Windows.Forms.TextBox();
            this.progressBarWorker = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // textBoxWorker
            // 
            this.textBoxWorker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWorker.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxWorker.Location = new System.Drawing.Point(17, 22);
            this.textBoxWorker.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxWorker.Multiline = true;
            this.textBoxWorker.Name = "textBoxWorker";
            this.textBoxWorker.ReadOnly = true;
            this.textBoxWorker.Size = new System.Drawing.Size(439, 41);
            this.textBoxWorker.TabIndex = 5;
            // 
            // progressBarWorker
            // 
            this.progressBarWorker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarWorker.Location = new System.Drawing.Point(16, 64);
            this.progressBarWorker.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarWorker.MarqueeAnimationSpeed = 25;
            this.progressBarWorker.Name = "progressBarWorker";
            this.progressBarWorker.Size = new System.Drawing.Size(440, 28);
            this.progressBarWorker.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarWorker.TabIndex = 4;
            // 
            // WaitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 114);
            this.Controls.Add(this.textBoxWorker);
            this.Controls.Add(this.progressBarWorker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WaitDialog";
            this.Load += new System.EventHandler(this.WaitDialog_Load);
            this.Shown += new System.EventHandler(this.WaitDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWorker;
        private System.Windows.Forms.ProgressBar progressBarWorker;
    }
}