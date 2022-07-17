
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
            this.splitContainerGlobal = new System.Windows.Forms.SplitContainer();
            this.splitContainerSecondaire = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.menuStripGlobal.SuspendLayout();
            this.statusStripGlobal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGlobal)).BeginInit();
            this.splitContainerGlobal.Panel2.SuspendLayout();
            this.splitContainerGlobal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSecondaire)).BeginInit();
            this.splitContainerSecondaire.Panel1.SuspendLayout();
            this.splitContainerSecondaire.SuspendLayout();
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
            this.menuStripGlobal.Size = new System.Drawing.Size(1135, 28);
            this.menuStripGlobal.TabIndex = 0;
            this.menuStripGlobal.Text = "menuStripGlobal";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirLeFichierDeLogToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(66, 26);
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
            this.outilsToolStripMenuItem.Size = new System.Drawing.Size(61, 26);
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
            this.toolStripMenuItem1.Size = new System.Drawing.Size(30, 26);
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
            this.statusStripGlobal.Location = new System.Drawing.Point(0, 660);
            this.statusStripGlobal.Name = "statusStripGlobal";
            this.statusStripGlobal.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStripGlobal.Size = new System.Drawing.Size(1135, 26);
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
            // splitContainerGlobal
            // 
            this.splitContainerGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerGlobal.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerGlobal.IsSplitterFixed = true;
            this.splitContainerGlobal.Location = new System.Drawing.Point(12, 33);
            this.splitContainerGlobal.Name = "splitContainerGlobal";
            this.splitContainerGlobal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerGlobal.Panel2
            // 
            this.splitContainerGlobal.Panel2.Controls.Add(this.splitContainerSecondaire);
            this.splitContainerGlobal.Size = new System.Drawing.Size(1111, 624);
            this.splitContainerGlobal.SplitterDistance = 80;
            this.splitContainerGlobal.TabIndex = 0;
            // 
            // splitContainerSecondaire
            // 
            this.splitContainerSecondaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSecondaire.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerSecondaire.IsSplitterFixed = true;
            this.splitContainerSecondaire.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSecondaire.Name = "splitContainerSecondaire";
            this.splitContainerSecondaire.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSecondaire.Panel1
            // 
            this.splitContainerSecondaire.Panel1.Controls.Add(this.listView1);
            this.splitContainerSecondaire.Size = new System.Drawing.Size(1111, 540);
            this.splitContainerSecondaire.SplitterDistance = 236;
            this.splitContainerSecondaire.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1111, 236);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // MainFenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 686);
            this.Controls.Add(this.splitContainerGlobal);
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
            this.splitContainerGlobal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGlobal)).EndInit();
            this.splitContainerGlobal.ResumeLayout(false);
            this.splitContainerSecondaire.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSecondaire)).EndInit();
            this.splitContainerSecondaire.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainerGlobal;
        private System.Windows.Forms.SplitContainer splitContainerSecondaire;
        private System.Windows.Forms.ListView listView1;
    }
}