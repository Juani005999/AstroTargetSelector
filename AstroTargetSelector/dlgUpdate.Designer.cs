namespace AstroTargetSelector
{
    partial class dlgUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgUpdate));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.mainLabel = new System.Windows.Forms.Label();
            this.pictureBoxActionIconSuccess = new System.Windows.Forms.PictureBox();
            this.pictureBoxActionIconInfo = new System.Windows.Forms.PictureBox();
            this.pictureBoxActionIconError = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActionIconSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActionIconInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActionIconError)).BeginInit();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(232, 83);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(313, 83);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // mainLabel
            // 
            this.mainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainLabel.Location = new System.Drawing.Point(51, 13);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(337, 67);
            this.mainLabel.TabIndex = 8;
            this.mainLabel.Text = "mainLabel";
            // 
            // pictureBoxActionIconSuccess
            // 
            this.pictureBoxActionIconSuccess.Image = global::AstroTargetSelector.Properties.Resources.TaskValidated;
            this.pictureBoxActionIconSuccess.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxActionIconSuccess.Name = "pictureBoxActionIconSuccess";
            this.pictureBoxActionIconSuccess.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxActionIconSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxActionIconSuccess.TabIndex = 11;
            this.pictureBoxActionIconSuccess.TabStop = false;
            // 
            // pictureBoxActionIconInfo
            // 
            this.pictureBoxActionIconInfo.Image = global::AstroTargetSelector.Properties.Resources.UpdateFile;
            this.pictureBoxActionIconInfo.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxActionIconInfo.Name = "pictureBoxActionIconInfo";
            this.pictureBoxActionIconInfo.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxActionIconInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxActionIconInfo.TabIndex = 9;
            this.pictureBoxActionIconInfo.TabStop = false;
            // 
            // pictureBoxActionIconError
            // 
            this.pictureBoxActionIconError.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxActionIconError.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxActionIconError.Name = "pictureBoxActionIconError";
            this.pictureBoxActionIconError.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxActionIconError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxActionIconError.TabIndex = 10;
            this.pictureBoxActionIconError.TabStop = false;
            // 
            // dlgUpdate
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(400, 118);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxActionIconInfo);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.pictureBoxActionIconError);
            this.Controls.Add(this.pictureBoxActionIconSuccess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgUpdate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mise à jour des fichiers de configuration";
            this.Load += new System.EventHandler(this.dlgUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActionIconSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActionIconInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActionIconError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.PictureBox pictureBoxActionIconInfo;
        private System.Windows.Forms.PictureBox pictureBoxActionIconError;
        private System.Windows.Forms.PictureBox pictureBoxActionIconSuccess;
    }
}