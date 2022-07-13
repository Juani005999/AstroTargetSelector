
namespace TargetSelector
{
    partial class MainFenetre
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
            this.menuStripGlobal = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripGlobal = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelDateObs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelNomTarget = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripGlobal.SuspendLayout();
            this.statusStripGlobal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripGlobal
            // 
            this.menuStripGlobal.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripGlobal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.outilsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStripGlobal.Location = new System.Drawing.Point(0, 0);
            this.menuStripGlobal.Name = "menuStripGlobal";
            this.menuStripGlobal.Size = new System.Drawing.Size(800, 33);
            this.menuStripGlobal.TabIndex = 0;
            this.menuStripGlobal.Text = "menuStripGlobal";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.fichierToolStripMenuItem.Text = "&Fichier";
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.quitterToolStripMenuItem.Text = "&Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // outilsToolStripMenuItem
            // 
            this.outilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.outilsToolStripMenuItem.Name = "outilsToolStripMenuItem";
            this.outilsToolStripMenuItem.Size = new System.Drawing.Size(74, 32);
            this.outilsToolStripMenuItem.Text = "&Outils";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(36, 32);
            this.toolStripMenuItem1.Text = "&?";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.aProposToolStripMenuItem.Text = "&A propos";
            // 
            // statusStripGlobal
            // 
            this.statusStripGlobal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDateObs,
            this.toolStripStatusLabelNomTarget});
            this.statusStripGlobal.Location = new System.Drawing.Point(0, 418);
            this.statusStripGlobal.Name = "statusStripGlobal";
            this.statusStripGlobal.Size = new System.Drawing.Size(800, 32);
            this.statusStripGlobal.TabIndex = 1;
            this.statusStripGlobal.Text = "statusStripGlobal";
            // 
            // toolStripStatusLabelDateObs
            // 
            this.toolStripStatusLabelDateObs.Name = "toolStripStatusLabelDateObs";
            this.toolStripStatusLabelDateObs.Size = new System.Drawing.Size(202, 25);
            this.toolStripStatusLabelDateObs.Text = "Date/Heure observation";
            // 
            // toolStripStatusLabelNomTarget
            // 
            this.toolStripStatusLabelNomTarget.Name = "toolStripStatusLabelNomTarget";
            this.toolStripStatusLabelNomTarget.Size = new System.Drawing.Size(191, 25);
            this.toolStripStatusLabelNomTarget.Text = "Nom objet sélectionné";
            // 
            // MainFenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStripGlobal);
            this.Controls.Add(this.menuStripGlobal);
            this.MainMenuStrip = this.menuStripGlobal;
            this.Name = "MainFenetre";
            this.Text = "Astro Target Selector";
            this.menuStripGlobal.ResumeLayout(false);
            this.menuStripGlobal.PerformLayout();
            this.statusStripGlobal.ResumeLayout(false);
            this.statusStripGlobal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripGlobal;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripGlobal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDateObs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelNomTarget;
    }
}