
namespace AstroTargetSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFenetre));
            this.menuStripGlobal = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirLeFichierDeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripGlobal = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelDateObs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelNomTarget = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewTarget = new System.Windows.Forms.ListView();
            this.toolBarPrincipale = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStripGlobal.SuspendLayout();
            this.statusStripGlobal.SuspendLayout();
            this.toolBarPrincipale.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripGlobal
            // 
            this.menuStripGlobal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.outilsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStripGlobal.Location = new System.Drawing.Point(0, 0);
            this.menuStripGlobal.Name = "menuStripGlobal";
            this.menuStripGlobal.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStripGlobal.Size = new System.Drawing.Size(711, 28);
            this.menuStripGlobal.TabIndex = 0;
            this.menuStripGlobal.Text = "menuStripGlobal";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirLeFichierDeLogToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.fichierToolStripMenuItem.Text = "&Fichier";
            // 
            // ouvrirLeFichierDeLogToolStripMenuItem
            // 
            this.ouvrirLeFichierDeLogToolStripMenuItem.Name = "ouvrirLeFichierDeLogToolStripMenuItem";
            this.ouvrirLeFichierDeLogToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.ouvrirLeFichierDeLogToolStripMenuItem.Text = "Ouvrir le fichier de log";
            this.ouvrirLeFichierDeLogToolStripMenuItem.Click += new System.EventHandler(this.ouvrirLeFichierDeLogToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.quitterToolStripMenuItem.Text = "&Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // outilsToolStripMenuItem
            // 
            this.outilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.outilsToolStripMenuItem.Name = "outilsToolStripMenuItem";
            this.outilsToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.outilsToolStripMenuItem.Text = "&Outils";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(30, 24);
            this.toolStripMenuItem1.Text = "&?";
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.aProposToolStripMenuItem.Text = "&A propos";
            // 
            // statusStripGlobal
            // 
            this.statusStripGlobal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDateObs,
            this.toolStripStatusLabelNomTarget});
            this.statusStripGlobal.Location = new System.Drawing.Point(0, 334);
            this.statusStripGlobal.Name = "statusStripGlobal";
            this.statusStripGlobal.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStripGlobal.Size = new System.Drawing.Size(711, 26);
            this.statusStripGlobal.TabIndex = 1;
            this.statusStripGlobal.Text = "statusStripGlobal";
            // 
            // toolStripStatusLabelDateObs
            // 
            this.toolStripStatusLabelDateObs.Name = "toolStripStatusLabelDateObs";
            this.toolStripStatusLabelDateObs.Size = new System.Drawing.Size(169, 20);
            this.toolStripStatusLabelDateObs.Text = "Date/Heure observation";
            // 
            // toolStripStatusLabelNomTarget
            // 
            this.toolStripStatusLabelNomTarget.Name = "toolStripStatusLabelNomTarget";
            this.toolStripStatusLabelNomTarget.Size = new System.Drawing.Size(160, 20);
            this.toolStripStatusLabelNomTarget.Text = "Nom objet sélectionné";
            // 
            // listViewTarget
            // 
            this.listViewTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTarget.FullRowSelect = true;
            this.listViewTarget.HideSelection = false;
            this.listViewTarget.Location = new System.Drawing.Point(12, 58);
            this.listViewTarget.MultiSelect = false;
            this.listViewTarget.Name = "listViewTarget";
            this.listViewTarget.Size = new System.Drawing.Size(687, 273);
            this.listViewTarget.TabIndex = 2;
            this.listViewTarget.UseCompatibleStateImageBehavior = false;
            this.listViewTarget.View = System.Windows.Forms.View.Details;
            // 
            // toolBarPrincipale
            // 
            this.toolBarPrincipale.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolBarPrincipale.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolBarPrincipale.Location = new System.Drawing.Point(0, 28);
            this.toolBarPrincipale.Name = "toolBarPrincipale";
            this.toolBarPrincipale.Size = new System.Drawing.Size(711, 47);
            this.toolBarPrincipale.TabIndex = 3;
            this.toolBarPrincipale.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(123, 44);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // MainFenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.toolBarPrincipale);
            this.Controls.Add(this.listViewTarget);
            this.Controls.Add(this.statusStripGlobal);
            this.Controls.Add(this.menuStripGlobal);
            this.MainMenuStrip = this.menuStripGlobal;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainFenetre";
            this.Text = "Astro Target Selector";
            this.menuStripGlobal.ResumeLayout(false);
            this.menuStripGlobal.PerformLayout();
            this.statusStripGlobal.ResumeLayout(false);
            this.statusStripGlobal.PerformLayout();
            this.toolBarPrincipale.ResumeLayout(false);
            this.toolBarPrincipale.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem ouvrirLeFichierDeLogToolStripMenuItem;
        private System.Windows.Forms.ListView listViewTarget;
        private System.Windows.Forms.ToolStrip toolBarPrincipale;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}