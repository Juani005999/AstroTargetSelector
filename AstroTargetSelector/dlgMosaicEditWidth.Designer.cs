namespace AstroTargetSelector
{
    partial class dlgMosaicEditWidth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgMosaicEditWidth));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.labelWidthSeconde = new System.Windows.Forms.Label();
            this.textBoxWidthSeconde = new System.Windows.Forms.TextBox();
            this.labelWidthMinute = new System.Windows.Forms.Label();
            this.textBoxWidthMinute = new System.Windows.Forms.TextBox();
            this.textBoxWidthDegre = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelWidthDegree = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(97, 48);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(178, 48);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // labelWidthSeconde
            // 
            this.labelWidthSeconde.AutoSize = true;
            this.labelWidthSeconde.Location = new System.Drawing.Point(252, 13);
            this.labelWidthSeconde.Name = "labelWidthSeconde";
            this.labelWidthSeconde.Size = new System.Drawing.Size(12, 13);
            this.labelWidthSeconde.TabIndex = 37;
            this.labelWidthSeconde.Text = "\"";
            // 
            // textBoxWidthSeconde
            // 
            this.textBoxWidthSeconde.Location = new System.Drawing.Point(196, 12);
            this.textBoxWidthSeconde.Name = "textBoxWidthSeconde";
            this.textBoxWidthSeconde.Size = new System.Drawing.Size(55, 20);
            this.textBoxWidthSeconde.TabIndex = 3;
            // 
            // labelWidthMinute
            // 
            this.labelWidthMinute.AutoSize = true;
            this.labelWidthMinute.Location = new System.Drawing.Point(181, 13);
            this.labelWidthMinute.Name = "labelWidthMinute";
            this.labelWidthMinute.Size = new System.Drawing.Size(9, 13);
            this.labelWidthMinute.TabIndex = 36;
            this.labelWidthMinute.Text = "\'";
            // 
            // textBoxWidthMinute
            // 
            this.textBoxWidthMinute.Location = new System.Drawing.Point(145, 12);
            this.textBoxWidthMinute.Name = "textBoxWidthMinute";
            this.textBoxWidthMinute.Size = new System.Drawing.Size(33, 20);
            this.textBoxWidthMinute.TabIndex = 1;
            // 
            // textBoxWidthDegre
            // 
            this.textBoxWidthDegre.Location = new System.Drawing.Point(94, 12);
            this.textBoxWidthDegre.Name = "textBoxWidthDegre";
            this.textBoxWidthDegre.Size = new System.Drawing.Size(33, 20);
            this.textBoxWidthDegre.TabIndex = 0;
            // 
            // labelWidth
            // 
            this.labelWidth.Location = new System.Drawing.Point(19, 15);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(69, 17);
            this.labelWidth.TabIndex = 34;
            this.labelWidth.Text = "Largeur";
            this.labelWidth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelWidthDegree
            // 
            this.labelWidthDegree.AutoSize = true;
            this.labelWidthDegree.Location = new System.Drawing.Point(128, 12);
            this.labelWidthDegree.Name = "labelWidthDegree";
            this.labelWidthDegree.Size = new System.Drawing.Size(11, 13);
            this.labelWidthDegree.TabIndex = 35;
            this.labelWidthDegree.Text = "°";
            // 
            // dlgMosaicEditWidth
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(270, 82);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.labelWidthSeconde);
            this.Controls.Add(this.textBoxWidthSeconde);
            this.Controls.Add(this.labelWidthMinute);
            this.Controls.Add(this.textBoxWidthMinute);
            this.Controls.Add(this.textBoxWidthDegre);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelWidthDegree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgMosaicEditWidth";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Largeur de la mosaïque";
            this.Load += new System.EventHandler(this.dlgMosaicEditWidth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label labelWidthSeconde;
        private System.Windows.Forms.TextBox textBoxWidthSeconde;
        private System.Windows.Forms.Label labelWidthMinute;
        private System.Windows.Forms.TextBox textBoxWidthMinute;
        private System.Windows.Forms.TextBox textBoxWidthDegre;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelWidthDegree;
    }
}