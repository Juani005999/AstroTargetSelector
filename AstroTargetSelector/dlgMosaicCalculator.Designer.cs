namespace AstroTargetSelector
{
    partial class dlgMosaicCalculator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgMosaicCalculator));
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBoxObjet = new System.Windows.Forms.GroupBox();
            this.buttonWidthRestore = new System.Windows.Forms.Button();
            this.buttonWidthModify = new System.Windows.Forms.Button();
            this.buttonRADECRestore = new System.Windows.Forms.Button();
            this.buttonRADECModify = new System.Windows.Forms.Button();
            this.pictureBoxErreurPctChevauchement = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurTempsPanneau = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurFOV = new System.Windows.Forms.PictureBox();
            this.pictureBoxInfosFOV = new System.Windows.Forms.PictureBox();
            this.pictureBoxRank5 = new System.Windows.Forms.PictureBox();
            this.textBoxPctChevauchement = new System.Windows.Forms.TextBox();
            this.textBoxGrandeurL = new System.Windows.Forms.TextBox();
            this.labelTauxChevauchement = new System.Windows.Forms.Label();
            this.labelHeure = new System.Windows.Forms.Label();
            this.textBoxTempsPanneau = new System.Windows.Forms.TextBox();
            this.labelTempsPose = new System.Windows.Forms.Label();
            this.textBoxHeure = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxFOV = new System.Windows.Forms.TextBox();
            this.labelFOV = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelDEC = new System.Windows.Forms.Label();
            this.labelRA = new System.Windows.Forms.Label();
            this.textBoxRA = new System.Windows.Forms.TextBox();
            this.textBoxDEC = new System.Windows.Forms.TextBox();
            this.textBoxDenominations = new System.Windows.Forms.TextBox();
            this.labelInfoDenominations = new System.Windows.Forms.Label();
            this.textBoxNomObjet = new System.Windows.Forms.TextBox();
            this.labelInfoNomObjet = new System.Windows.Forms.Label();
            this.pictureBoxRank1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxRank2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxRank3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxRank4 = new System.Windows.Forms.PictureBox();
            this.textBoxGrandeurH = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelPct = new System.Windows.Forms.Label();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.labelDegrees = new System.Windows.Forms.Label();
            this.dataGridViewResultRect = new System.Windows.Forms.DataGridView();
            this.groupBoxResultat = new System.Windows.Forms.GroupBox();
            this.pictureBoxInfosASCOM = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurASCOMMoveTo = new System.Windows.Forms.PictureBox();
            this.buttonASCOMMoveTo = new System.Windows.Forms.Button();
            this.checkBoxPanneauSup = new System.Windows.Forms.CheckBox();
            this.pictureBoxInfosSelectionPanneau = new System.Windows.Forms.PictureBox();
            this.pictureBoxWarningRotationGlobale = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurNombrePanneau = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurCdC = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurStellarium = new System.Windows.Forms.PictureBox();
            this.buttonCartesDuCiel = new System.Windows.Forms.Button();
            this.buttonStellarium = new System.Windows.Forms.Button();
            this.textBoxResultTotalWidth = new System.Windows.Forms.TextBox();
            this.labelResultTotalWidth = new System.Windows.Forms.Label();
            this.textBoxResultTempsPanneau = new System.Windows.Forms.TextBox();
            this.labelTempsPanneau = new System.Windows.Forms.Label();
            this.textBoxResultRotationGlobale = new System.Windows.Forms.TextBox();
            this.labelResultRotationGlobale = new System.Windows.Forms.Label();
            this.textBoxResultTempsGlobal = new System.Windows.Forms.TextBox();
            this.labelResultTempsGlobal = new System.Windows.Forms.Label();
            this.textBoxResultNbPanneau = new System.Windows.Forms.TextBox();
            this.labelResultNbPanneau = new System.Windows.Forms.Label();
            this.panelMosaic = new System.Windows.Forms.Panel();
            this.groupBoxExportResult = new System.Windows.Forms.GroupBox();
            this.buttonExportResult = new System.Windows.Forms.Button();
            this.checkBoxExportUnistellarLinks = new System.Windows.Forms.CheckBox();
            this.radioButtonExportCsv = new System.Windows.Forms.RadioButton();
            this.radioButtonExportText = new System.Windows.Forms.RadioButton();
            this.toolTipStellarium = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCartesDuCiel = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorkerStellarium = new System.ComponentModel.BackgroundWorker();
            this.toolTipErreurStellarium = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorkerCartesDuCiel = new System.ComponentModel.BackgroundWorker();
            this.toolTipErreurCdC = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosFOV = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurFOV = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurTempsPanneau = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurPctChevauchement = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurNombrePanneau = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipWarningRotationGlobale = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosSelectionPanneau = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosRADECModify = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosWidthModify = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosRADECRestore = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxASCOM = new System.Windows.Forms.GroupBox();
            this.panelSlew = new System.Windows.Forms.Panel();
            this.labelSlew = new System.Windows.Forms.Label();
            this.pictureBoxErreurASCOM = new System.Windows.Forms.PictureBox();
            this.comboBoxASCOMNom = new System.Windows.Forms.ComboBox();
            this.buttonASCOMStop = new System.Windows.Forms.Button();
            this.buttonASCOMStandby = new System.Windows.Forms.Button();
            this.textBoxASCOMAlt = new System.Windows.Forms.TextBox();
            this.textBoxASCOMAz = new System.Windows.Forms.TextBox();
            this.textBoxASCOMRA = new System.Windows.Forms.TextBox();
            this.textBoxASCOMDEC = new System.Windows.Forms.TextBox();
            this.labelASCOMAz = new System.Windows.Forms.Label();
            this.labelASCOMAlt = new System.Windows.Forms.Label();
            this.labelASCOMDEC = new System.Windows.Forms.Label();
            this.labelASCOMRA = new System.Windows.Forms.Label();
            this.backgroundWorkerTelescope = new System.ComponentModel.BackgroundWorker();
            this.toolTipErreurASCOM = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosASCOM = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurASCOMMoveTo = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipASCOMMoveTo = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipASCOMStandBy = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipASCOMStopSlew = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCartesDuCielGlobal = new System.Windows.Forms.Button();
            this.buttonStellariumGlobal = new System.Windows.Forms.Button();
            this.pictureBoxErreurCdCGlobal = new System.Windows.Forms.PictureBox();
            this.pictureBoxErreurStellariumGlobal = new System.Windows.Forms.PictureBox();
            this.toolTipStellariumGlobal = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCartesDuCielGlobal = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurStellariumGlobal = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipErreurCdCGlobal = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxObjet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurPctChevauchement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurTempsPanneau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurFOV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosFOV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResultRect)).BeginInit();
            this.groupBoxResultat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosASCOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurASCOMMoveTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosSelectionPanneau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningRotationGlobale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurNombrePanneau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurCdC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurStellarium)).BeginInit();
            this.groupBoxExportResult.SuspendLayout();
            this.groupBoxASCOM.SuspendLayout();
            this.panelSlew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurASCOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurCdCGlobal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurStellariumGlobal)).BeginInit();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(819, 598);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Fermer";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxObjet
            // 
            this.groupBoxObjet.Controls.Add(this.pictureBoxErreurCdCGlobal);
            this.groupBoxObjet.Controls.Add(this.pictureBoxErreurStellariumGlobal);
            this.groupBoxObjet.Controls.Add(this.buttonCartesDuCielGlobal);
            this.groupBoxObjet.Controls.Add(this.buttonStellariumGlobal);
            this.groupBoxObjet.Controls.Add(this.buttonWidthRestore);
            this.groupBoxObjet.Controls.Add(this.buttonWidthModify);
            this.groupBoxObjet.Controls.Add(this.buttonRADECRestore);
            this.groupBoxObjet.Controls.Add(this.buttonRADECModify);
            this.groupBoxObjet.Controls.Add(this.pictureBoxErreurPctChevauchement);
            this.groupBoxObjet.Controls.Add(this.pictureBoxErreurTempsPanneau);
            this.groupBoxObjet.Controls.Add(this.pictureBoxErreurFOV);
            this.groupBoxObjet.Controls.Add(this.pictureBoxInfosFOV);
            this.groupBoxObjet.Controls.Add(this.pictureBoxRank5);
            this.groupBoxObjet.Controls.Add(this.textBoxPctChevauchement);
            this.groupBoxObjet.Controls.Add(this.textBoxGrandeurL);
            this.groupBoxObjet.Controls.Add(this.labelTauxChevauchement);
            this.groupBoxObjet.Controls.Add(this.labelHeure);
            this.groupBoxObjet.Controls.Add(this.textBoxTempsPanneau);
            this.groupBoxObjet.Controls.Add(this.labelTempsPose);
            this.groupBoxObjet.Controls.Add(this.textBoxHeure);
            this.groupBoxObjet.Controls.Add(this.labelWidth);
            this.groupBoxObjet.Controls.Add(this.textBoxFOV);
            this.groupBoxObjet.Controls.Add(this.labelFOV);
            this.groupBoxObjet.Controls.Add(this.textBoxDate);
            this.groupBoxObjet.Controls.Add(this.labelDate);
            this.groupBoxObjet.Controls.Add(this.labelDEC);
            this.groupBoxObjet.Controls.Add(this.labelRA);
            this.groupBoxObjet.Controls.Add(this.textBoxRA);
            this.groupBoxObjet.Controls.Add(this.textBoxDEC);
            this.groupBoxObjet.Controls.Add(this.textBoxDenominations);
            this.groupBoxObjet.Controls.Add(this.labelInfoDenominations);
            this.groupBoxObjet.Controls.Add(this.textBoxNomObjet);
            this.groupBoxObjet.Controls.Add(this.labelInfoNomObjet);
            this.groupBoxObjet.Controls.Add(this.pictureBoxRank1);
            this.groupBoxObjet.Controls.Add(this.pictureBoxRank2);
            this.groupBoxObjet.Controls.Add(this.pictureBoxRank3);
            this.groupBoxObjet.Controls.Add(this.pictureBoxRank4);
            this.groupBoxObjet.Controls.Add(this.textBoxGrandeurH);
            this.groupBoxObjet.Controls.Add(this.labelHeight);
            this.groupBoxObjet.Controls.Add(this.labelPct);
            this.groupBoxObjet.Controls.Add(this.labelMinutes);
            this.groupBoxObjet.Controls.Add(this.labelDegrees);
            this.groupBoxObjet.Location = new System.Drawing.Point(12, 12);
            this.groupBoxObjet.Name = "groupBoxObjet";
            this.groupBoxObjet.Size = new System.Drawing.Size(662, 155);
            this.groupBoxObjet.TabIndex = 0;
            this.groupBoxObjet.TabStop = false;
            this.groupBoxObjet.Text = "Objet céleste et paramètres de la mosaïque";
            // 
            // buttonWidthRestore
            // 
            this.buttonWidthRestore.Image = global::AstroTargetSelector.Properties.Resources.delete16;
            this.buttonWidthRestore.Location = new System.Drawing.Point(294, 121);
            this.buttonWidthRestore.Name = "buttonWidthRestore";
            this.buttonWidthRestore.Size = new System.Drawing.Size(28, 28);
            this.buttonWidthRestore.TabIndex = 8;
            this.buttonWidthRestore.UseVisualStyleBackColor = true;
            this.buttonWidthRestore.Click += new System.EventHandler(this.buttonWidthRestore_Click);
            // 
            // buttonWidthModify
            // 
            this.buttonWidthModify.Image = global::AstroTargetSelector.Properties.Resources.EditNom16;
            this.buttonWidthModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonWidthModify.Location = new System.Drawing.Point(263, 121);
            this.buttonWidthModify.Name = "buttonWidthModify";
            this.buttonWidthModify.Size = new System.Drawing.Size(28, 28);
            this.buttonWidthModify.TabIndex = 7;
            this.buttonWidthModify.UseVisualStyleBackColor = true;
            this.buttonWidthModify.Click += new System.EventHandler(this.buttonWidthModify_Click);
            // 
            // buttonRADECRestore
            // 
            this.buttonRADECRestore.Image = global::AstroTargetSelector.Properties.Resources.delete16;
            this.buttonRADECRestore.Location = new System.Drawing.Point(294, 69);
            this.buttonRADECRestore.Name = "buttonRADECRestore";
            this.buttonRADECRestore.Size = new System.Drawing.Size(28, 28);
            this.buttonRADECRestore.TabIndex = 6;
            this.buttonRADECRestore.UseVisualStyleBackColor = true;
            this.buttonRADECRestore.Click += new System.EventHandler(this.buttonRADECRestore_Click);
            // 
            // buttonRADECModify
            // 
            this.buttonRADECModify.Image = global::AstroTargetSelector.Properties.Resources.EditNom16;
            this.buttonRADECModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonRADECModify.Location = new System.Drawing.Point(263, 69);
            this.buttonRADECModify.Name = "buttonRADECModify";
            this.buttonRADECModify.Size = new System.Drawing.Size(28, 28);
            this.buttonRADECModify.TabIndex = 5;
            this.buttonRADECModify.UseVisualStyleBackColor = true;
            this.buttonRADECModify.Click += new System.EventHandler(this.buttonRADECModify_Click);
            // 
            // pictureBoxErreurPctChevauchement
            // 
            this.pictureBoxErreurPctChevauchement.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurPctChevauchement.Location = new System.Drawing.Point(640, 126);
            this.pictureBoxErreurPctChevauchement.Name = "pictureBoxErreurPctChevauchement";
            this.pictureBoxErreurPctChevauchement.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurPctChevauchement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurPctChevauchement.TabIndex = 76;
            this.pictureBoxErreurPctChevauchement.TabStop = false;
            // 
            // pictureBoxErreurTempsPanneau
            // 
            this.pictureBoxErreurTempsPanneau.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurTempsPanneau.Location = new System.Drawing.Point(640, 100);
            this.pictureBoxErreurTempsPanneau.Name = "pictureBoxErreurTempsPanneau";
            this.pictureBoxErreurTempsPanneau.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurTempsPanneau.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurTempsPanneau.TabIndex = 75;
            this.pictureBoxErreurTempsPanneau.TabStop = false;
            // 
            // pictureBoxErreurFOV
            // 
            this.pictureBoxErreurFOV.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurFOV.Location = new System.Drawing.Point(640, 74);
            this.pictureBoxErreurFOV.Name = "pictureBoxErreurFOV";
            this.pictureBoxErreurFOV.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurFOV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurFOV.TabIndex = 74;
            this.pictureBoxErreurFOV.TabStop = false;
            // 
            // pictureBoxInfosFOV
            // 
            this.pictureBoxInfosFOV.Image = global::AstroTargetSelector.Properties.Resources.ico16783;
            this.pictureBoxInfosFOV.Location = new System.Drawing.Point(468, 71);
            this.pictureBoxInfosFOV.Name = "pictureBoxInfosFOV";
            this.pictureBoxInfosFOV.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfosFOV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfosFOV.TabIndex = 73;
            this.pictureBoxInfosFOV.TabStop = false;
            // 
            // pictureBoxRank5
            // 
            this.pictureBoxRank5.Image = global::AstroTargetSelector.Properties.Resources._5;
            this.pictureBoxRank5.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxRank5.Name = "pictureBoxRank5";
            this.pictureBoxRank5.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRank5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRank5.TabIndex = 42;
            this.pictureBoxRank5.TabStop = false;
            // 
            // textBoxPctChevauchement
            // 
            this.textBoxPctChevauchement.Location = new System.Drawing.Point(545, 123);
            this.textBoxPctChevauchement.Name = "textBoxPctChevauchement";
            this.textBoxPctChevauchement.Size = new System.Drawing.Size(64, 20);
            this.textBoxPctChevauchement.TabIndex = 2;
            this.textBoxPctChevauchement.TextChanged += new System.EventHandler(this.textBoxPctChevauchement_TextChanged);
            // 
            // textBoxGrandeurL
            // 
            this.textBoxGrandeurL.Location = new System.Drawing.Point(151, 126);
            this.textBoxGrandeurL.Name = "textBoxGrandeurL";
            this.textBoxGrandeurL.ReadOnly = true;
            this.textBoxGrandeurL.Size = new System.Drawing.Size(106, 20);
            this.textBoxGrandeurL.TabIndex = 32;
            this.textBoxGrandeurL.TabStop = false;
            // 
            // labelTauxChevauchement
            // 
            this.labelTauxChevauchement.Location = new System.Drawing.Point(327, 126);
            this.labelTauxChevauchement.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTauxChevauchement.Name = "labelTauxChevauchement";
            this.labelTauxChevauchement.Size = new System.Drawing.Size(213, 17);
            this.labelTauxChevauchement.TabIndex = 32;
            this.labelTauxChevauchement.Text = "Niveau de chevauchement des images";
            this.labelTauxChevauchement.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelHeure
            // 
            this.labelHeure.Location = new System.Drawing.Point(474, 48);
            this.labelHeure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelHeure.Name = "labelHeure";
            this.labelHeure.Size = new System.Drawing.Size(66, 13);
            this.labelHeure.TabIndex = 31;
            this.labelHeure.Text = "Heure";
            this.labelHeure.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxTempsPanneau
            // 
            this.textBoxTempsPanneau.Location = new System.Drawing.Point(545, 97);
            this.textBoxTempsPanneau.Name = "textBoxTempsPanneau";
            this.textBoxTempsPanneau.Size = new System.Drawing.Size(64, 20);
            this.textBoxTempsPanneau.TabIndex = 1;
            this.textBoxTempsPanneau.TextChanged += new System.EventHandler(this.textBoxTempsPanneau_TextChanged);
            // 
            // labelTempsPose
            // 
            this.labelTempsPose.Location = new System.Drawing.Point(357, 100);
            this.labelTempsPose.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTempsPose.Name = "labelTempsPose";
            this.labelTempsPose.Size = new System.Drawing.Size(183, 17);
            this.labelTempsPose.TabIndex = 19;
            this.labelTempsPose.Text = "Temps de pose par panneau";
            this.labelTempsPose.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxHeure
            // 
            this.textBoxHeure.Location = new System.Drawing.Point(545, 45);
            this.textBoxHeure.Name = "textBoxHeure";
            this.textBoxHeure.ReadOnly = true;
            this.textBoxHeure.Size = new System.Drawing.Size(111, 20);
            this.textBoxHeure.TabIndex = 30;
            this.textBoxHeure.TabStop = false;
            // 
            // labelWidth
            // 
            this.labelWidth.Location = new System.Drawing.Point(18, 129);
            this.labelWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(128, 13);
            this.labelWidth.TabIndex = 30;
            this.labelWidth.Text = "Largeur de la mosaïque";
            this.labelWidth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxFOV
            // 
            this.textBoxFOV.Location = new System.Drawing.Point(545, 71);
            this.textBoxFOV.Name = "textBoxFOV";
            this.textBoxFOV.Size = new System.Drawing.Size(64, 20);
            this.textBoxFOV.TabIndex = 0;
            this.textBoxFOV.TextChanged += new System.EventHandler(this.textBoxFOV_TextChanged);
            // 
            // labelFOV
            // 
            this.labelFOV.Location = new System.Drawing.Point(493, 74);
            this.labelFOV.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFOV.Name = "labelFOV";
            this.labelFOV.Size = new System.Drawing.Size(47, 19);
            this.labelFOV.TabIndex = 16;
            this.labelFOV.Text = "FOV";
            this.labelFOV.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(545, 19);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(111, 20);
            this.textBoxDate.TabIndex = 23;
            this.textBoxDate.TabStop = false;
            // 
            // labelDate
            // 
            this.labelDate.Location = new System.Drawing.Point(463, 22);
            this.labelDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(77, 19);
            this.labelDate.TabIndex = 22;
            this.labelDate.Text = "Date";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDEC
            // 
            this.labelDEC.Location = new System.Drawing.Point(88, 103);
            this.labelDEC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDEC.Name = "labelDEC";
            this.labelDEC.Size = new System.Drawing.Size(58, 13);
            this.labelDEC.TabIndex = 29;
            this.labelDEC.Text = "DEC";
            this.labelDEC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelRA
            // 
            this.labelRA.Location = new System.Drawing.Point(88, 77);
            this.labelRA.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRA.Name = "labelRA";
            this.labelRA.Size = new System.Drawing.Size(58, 13);
            this.labelRA.TabIndex = 28;
            this.labelRA.Text = "RA";
            this.labelRA.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxRA
            // 
            this.textBoxRA.Location = new System.Drawing.Point(151, 74);
            this.textBoxRA.Name = "textBoxRA";
            this.textBoxRA.ReadOnly = true;
            this.textBoxRA.Size = new System.Drawing.Size(106, 20);
            this.textBoxRA.TabIndex = 26;
            this.textBoxRA.TabStop = false;
            // 
            // textBoxDEC
            // 
            this.textBoxDEC.Location = new System.Drawing.Point(151, 100);
            this.textBoxDEC.Name = "textBoxDEC";
            this.textBoxDEC.ReadOnly = true;
            this.textBoxDEC.Size = new System.Drawing.Size(106, 20);
            this.textBoxDEC.TabIndex = 27;
            this.textBoxDEC.TabStop = false;
            // 
            // textBoxDenominations
            // 
            this.textBoxDenominations.Location = new System.Drawing.Point(151, 45);
            this.textBoxDenominations.Name = "textBoxDenominations";
            this.textBoxDenominations.ReadOnly = true;
            this.textBoxDenominations.Size = new System.Drawing.Size(171, 20);
            this.textBoxDenominations.TabIndex = 17;
            this.textBoxDenominations.TabStop = false;
            // 
            // labelInfoDenominations
            // 
            this.labelInfoDenominations.Location = new System.Drawing.Point(85, 48);
            this.labelInfoDenominations.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfoDenominations.Name = "labelInfoDenominations";
            this.labelInfoDenominations.Size = new System.Drawing.Size(61, 19);
            this.labelInfoDenominations.TabIndex = 16;
            this.labelInfoDenominations.Text = "Description";
            this.labelInfoDenominations.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxNomObjet
            // 
            this.textBoxNomObjet.Location = new System.Drawing.Point(151, 19);
            this.textBoxNomObjet.Name = "textBoxNomObjet";
            this.textBoxNomObjet.ReadOnly = true;
            this.textBoxNomObjet.Size = new System.Drawing.Size(171, 20);
            this.textBoxNomObjet.TabIndex = 15;
            this.textBoxNomObjet.TabStop = false;
            // 
            // labelInfoNomObjet
            // 
            this.labelInfoNomObjet.Location = new System.Drawing.Point(85, 22);
            this.labelInfoNomObjet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfoNomObjet.Name = "labelInfoNomObjet";
            this.labelInfoNomObjet.Size = new System.Drawing.Size(61, 19);
            this.labelInfoNomObjet.TabIndex = 14;
            this.labelInfoNomObjet.Text = "Nom";
            this.labelInfoNomObjet.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBoxRank1
            // 
            this.pictureBoxRank1.Image = global::AstroTargetSelector.Properties.Resources._1;
            this.pictureBoxRank1.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxRank1.Name = "pictureBoxRank1";
            this.pictureBoxRank1.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRank1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRank1.TabIndex = 46;
            this.pictureBoxRank1.TabStop = false;
            // 
            // pictureBoxRank2
            // 
            this.pictureBoxRank2.Image = global::AstroTargetSelector.Properties.Resources._2;
            this.pictureBoxRank2.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxRank2.Name = "pictureBoxRank2";
            this.pictureBoxRank2.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRank2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRank2.TabIndex = 45;
            this.pictureBoxRank2.TabStop = false;
            // 
            // pictureBoxRank3
            // 
            this.pictureBoxRank3.Image = global::AstroTargetSelector.Properties.Resources._3;
            this.pictureBoxRank3.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxRank3.Name = "pictureBoxRank3";
            this.pictureBoxRank3.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRank3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRank3.TabIndex = 44;
            this.pictureBoxRank3.TabStop = false;
            // 
            // pictureBoxRank4
            // 
            this.pictureBoxRank4.Image = global::AstroTargetSelector.Properties.Resources._4;
            this.pictureBoxRank4.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxRank4.Name = "pictureBoxRank4";
            this.pictureBoxRank4.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRank4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRank4.TabIndex = 43;
            this.pictureBoxRank4.TabStop = false;
            // 
            // textBoxGrandeurH
            // 
            this.textBoxGrandeurH.Location = new System.Drawing.Point(151, 126);
            this.textBoxGrandeurH.Name = "textBoxGrandeurH";
            this.textBoxGrandeurH.ReadOnly = true;
            this.textBoxGrandeurH.Size = new System.Drawing.Size(106, 20);
            this.textBoxGrandeurH.TabIndex = 33;
            this.textBoxGrandeurH.TabStop = false;
            this.textBoxGrandeurH.Visible = false;
            // 
            // labelHeight
            // 
            this.labelHeight.Location = new System.Drawing.Point(62, 129);
            this.labelHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(84, 13);
            this.labelHeight.TabIndex = 31;
            this.labelHeight.Text = "Grandeur (H)";
            this.labelHeight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelHeight.Visible = false;
            // 
            // labelPct
            // 
            this.labelPct.AutoSize = true;
            this.labelPct.Location = new System.Drawing.Point(612, 126);
            this.labelPct.Name = "labelPct";
            this.labelPct.Size = new System.Drawing.Size(15, 13);
            this.labelPct.TabIndex = 34;
            this.labelPct.Text = "%";
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Location = new System.Drawing.Point(612, 100);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(26, 13);
            this.labelMinutes.TabIndex = 21;
            this.labelMinutes.Text = "min.";
            // 
            // labelDegrees
            // 
            this.labelDegrees.AutoSize = true;
            this.labelDegrees.Location = new System.Drawing.Point(612, 74);
            this.labelDegrees.Name = "labelDegrees";
            this.labelDegrees.Size = new System.Drawing.Size(11, 13);
            this.labelDegrees.TabIndex = 18;
            this.labelDegrees.Text = "°";
            // 
            // dataGridViewResultRect
            // 
            this.dataGridViewResultRect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResultRect.Location = new System.Drawing.Point(164, 177);
            this.dataGridViewResultRect.MultiSelect = false;
            this.dataGridViewResultRect.Name = "dataGridViewResultRect";
            this.dataGridViewResultRect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResultRect.Size = new System.Drawing.Size(244, 155);
            this.dataGridViewResultRect.TabIndex = 1;
            this.dataGridViewResultRect.SelectionChanged += new System.EventHandler(this.dataGridViewResultRect_SelectionChanged);
            // 
            // groupBoxResultat
            // 
            this.groupBoxResultat.Controls.Add(this.pictureBoxInfosASCOM);
            this.groupBoxResultat.Controls.Add(this.pictureBoxErreurASCOMMoveTo);
            this.groupBoxResultat.Controls.Add(this.buttonASCOMMoveTo);
            this.groupBoxResultat.Controls.Add(this.checkBoxPanneauSup);
            this.groupBoxResultat.Controls.Add(this.pictureBoxInfosSelectionPanneau);
            this.groupBoxResultat.Controls.Add(this.pictureBoxWarningRotationGlobale);
            this.groupBoxResultat.Controls.Add(this.pictureBoxErreurNombrePanneau);
            this.groupBoxResultat.Controls.Add(this.pictureBoxErreurCdC);
            this.groupBoxResultat.Controls.Add(this.pictureBoxErreurStellarium);
            this.groupBoxResultat.Controls.Add(this.buttonCartesDuCiel);
            this.groupBoxResultat.Controls.Add(this.buttonStellarium);
            this.groupBoxResultat.Controls.Add(this.textBoxResultTotalWidth);
            this.groupBoxResultat.Controls.Add(this.labelResultTotalWidth);
            this.groupBoxResultat.Controls.Add(this.textBoxResultTempsPanneau);
            this.groupBoxResultat.Controls.Add(this.labelTempsPanneau);
            this.groupBoxResultat.Controls.Add(this.textBoxResultRotationGlobale);
            this.groupBoxResultat.Controls.Add(this.labelResultRotationGlobale);
            this.groupBoxResultat.Controls.Add(this.textBoxResultTempsGlobal);
            this.groupBoxResultat.Controls.Add(this.labelResultTempsGlobal);
            this.groupBoxResultat.Controls.Add(this.textBoxResultNbPanneau);
            this.groupBoxResultat.Controls.Add(this.labelResultNbPanneau);
            this.groupBoxResultat.Controls.Add(this.dataGridViewResultRect);
            this.groupBoxResultat.Location = new System.Drawing.Point(12, 173);
            this.groupBoxResultat.Name = "groupBoxResultat";
            this.groupBoxResultat.Size = new System.Drawing.Size(475, 338);
            this.groupBoxResultat.TabIndex = 2;
            this.groupBoxResultat.TabStop = false;
            this.groupBoxResultat.Text = "Résultats du calcul de la mosaïque";
            // 
            // pictureBoxInfosASCOM
            // 
            this.pictureBoxInfosASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfosASCOM.Image = global::AstroTargetSelector.Properties.Resources.ico16783;
            this.pictureBoxInfosASCOM.Location = new System.Drawing.Point(420, 274);
            this.pictureBoxInfosASCOM.Name = "pictureBoxInfosASCOM";
            this.pictureBoxInfosASCOM.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfosASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfosASCOM.TabIndex = 86;
            this.pictureBoxInfosASCOM.TabStop = false;
            // 
            // pictureBoxErreurASCOMMoveTo
            // 
            this.pictureBoxErreurASCOMMoveTo.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurASCOMMoveTo.Location = new System.Drawing.Point(452, 308);
            this.pictureBoxErreurASCOMMoveTo.Name = "pictureBoxErreurASCOMMoveTo";
            this.pictureBoxErreurASCOMMoveTo.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurASCOMMoveTo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurASCOMMoveTo.TabIndex = 85;
            this.pictureBoxErreurASCOMMoveTo.TabStop = false;
            // 
            // buttonASCOMMoveTo
            // 
            this.buttonASCOMMoveTo.Image = global::AstroTargetSelector.Properties.Resources.accuracy_50;
            this.buttonASCOMMoveTo.Location = new System.Drawing.Point(414, 300);
            this.buttonASCOMMoveTo.Name = "buttonASCOMMoveTo";
            this.buttonASCOMMoveTo.Size = new System.Drawing.Size(32, 32);
            this.buttonASCOMMoveTo.TabIndex = 4;
            this.buttonASCOMMoveTo.UseVisualStyleBackColor = true;
            this.buttonASCOMMoveTo.Click += new System.EventHandler(this.buttonASCOMMoveTo_Click);
            // 
            // checkBoxPanneauSup
            // 
            this.checkBoxPanneauSup.AutoSize = true;
            this.checkBoxPanneauSup.Location = new System.Drawing.Point(65, 23);
            this.checkBoxPanneauSup.Name = "checkBoxPanneauSup";
            this.checkBoxPanneauSup.Size = new System.Drawing.Size(343, 17);
            this.checkBoxPanneauSup.TabIndex = 0;
            this.checkBoxPanneauSup.Text = "Ajouter un panneau central supplémentaire (mosaïque 4 panneaux)";
            this.checkBoxPanneauSup.UseVisualStyleBackColor = true;
            this.checkBoxPanneauSup.CheckedChanged += new System.EventHandler(this.checkBoxPanneauSup_CheckedChanged);
            // 
            // pictureBoxInfosSelectionPanneau
            // 
            this.pictureBoxInfosSelectionPanneau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfosSelectionPanneau.Image = global::AstroTargetSelector.Properties.Resources.ico16783;
            this.pictureBoxInfosSelectionPanneau.Location = new System.Drawing.Point(138, 177);
            this.pictureBoxInfosSelectionPanneau.Name = "pictureBoxInfosSelectionPanneau";
            this.pictureBoxInfosSelectionPanneau.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfosSelectionPanneau.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfosSelectionPanneau.TabIndex = 77;
            this.pictureBoxInfosSelectionPanneau.TabStop = false;
            // 
            // pictureBoxWarningRotationGlobale
            // 
            this.pictureBoxWarningRotationGlobale.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxWarningRotationGlobale.Location = new System.Drawing.Point(421, 153);
            this.pictureBoxWarningRotationGlobale.Name = "pictureBoxWarningRotationGlobale";
            this.pictureBoxWarningRotationGlobale.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxWarningRotationGlobale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxWarningRotationGlobale.TabIndex = 76;
            this.pictureBoxWarningRotationGlobale.TabStop = false;
            // 
            // pictureBoxErreurNombrePanneau
            // 
            this.pictureBoxErreurNombrePanneau.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurNombrePanneau.Location = new System.Drawing.Point(421, 49);
            this.pictureBoxErreurNombrePanneau.Name = "pictureBoxErreurNombrePanneau";
            this.pictureBoxErreurNombrePanneau.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurNombrePanneau.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurNombrePanneau.TabIndex = 75;
            this.pictureBoxErreurNombrePanneau.TabStop = false;
            // 
            // pictureBoxErreurCdC
            // 
            this.pictureBoxErreurCdC.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurCdC.Location = new System.Drawing.Point(452, 222);
            this.pictureBoxErreurCdC.Name = "pictureBoxErreurCdC";
            this.pictureBoxErreurCdC.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurCdC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurCdC.TabIndex = 53;
            this.pictureBoxErreurCdC.TabStop = false;
            // 
            // pictureBoxErreurStellarium
            // 
            this.pictureBoxErreurStellarium.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurStellarium.Location = new System.Drawing.Point(452, 184);
            this.pictureBoxErreurStellarium.Name = "pictureBoxErreurStellarium";
            this.pictureBoxErreurStellarium.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurStellarium.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurStellarium.TabIndex = 52;
            this.pictureBoxErreurStellarium.TabStop = false;
            // 
            // buttonCartesDuCiel
            // 
            this.buttonCartesDuCiel.Image = global::AstroTargetSelector.Properties.Resources.cartesduciel;
            this.buttonCartesDuCiel.Location = new System.Drawing.Point(414, 215);
            this.buttonCartesDuCiel.Name = "buttonCartesDuCiel";
            this.buttonCartesDuCiel.Size = new System.Drawing.Size(32, 32);
            this.buttonCartesDuCiel.TabIndex = 3;
            this.buttonCartesDuCiel.UseVisualStyleBackColor = true;
            this.buttonCartesDuCiel.Click += new System.EventHandler(this.buttonCartesDuCiel_Click);
            // 
            // buttonStellarium
            // 
            this.buttonStellarium.Image = global::AstroTargetSelector.Properties.Resources.stellarium;
            this.buttonStellarium.Location = new System.Drawing.Point(414, 177);
            this.buttonStellarium.Name = "buttonStellarium";
            this.buttonStellarium.Size = new System.Drawing.Size(32, 32);
            this.buttonStellarium.TabIndex = 2;
            this.buttonStellarium.UseVisualStyleBackColor = true;
            this.buttonStellarium.Click += new System.EventHandler(this.buttonStellarium_Click);
            // 
            // textBoxResultTotalWidth
            // 
            this.textBoxResultTotalWidth.Location = new System.Drawing.Point(328, 125);
            this.textBoxResultTotalWidth.Name = "textBoxResultTotalWidth";
            this.textBoxResultTotalWidth.ReadOnly = true;
            this.textBoxResultTotalWidth.Size = new System.Drawing.Size(80, 20);
            this.textBoxResultTotalWidth.TabIndex = 49;
            this.textBoxResultTotalWidth.TabStop = false;
            // 
            // labelResultTotalWidth
            // 
            this.labelResultTotalWidth.Location = new System.Drawing.Point(39, 128);
            this.labelResultTotalWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelResultTotalWidth.Name = "labelResultTotalWidth";
            this.labelResultTotalWidth.Size = new System.Drawing.Size(284, 19);
            this.labelResultTotalWidth.TabIndex = 48;
            this.labelResultTotalWidth.Text = "Estimation de la largeur totale de la mosaïque";
            this.labelResultTotalWidth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxResultTempsPanneau
            // 
            this.textBoxResultTempsPanneau.Location = new System.Drawing.Point(328, 99);
            this.textBoxResultTempsPanneau.Name = "textBoxResultTempsPanneau";
            this.textBoxResultTempsPanneau.ReadOnly = true;
            this.textBoxResultTempsPanneau.Size = new System.Drawing.Size(80, 20);
            this.textBoxResultTempsPanneau.TabIndex = 47;
            this.textBoxResultTempsPanneau.TabStop = false;
            // 
            // labelTempsPanneau
            // 
            this.labelTempsPanneau.Location = new System.Drawing.Point(71, 102);
            this.labelTempsPanneau.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTempsPanneau.Name = "labelTempsPanneau";
            this.labelTempsPanneau.Size = new System.Drawing.Size(252, 19);
            this.labelTempsPanneau.TabIndex = 46;
            this.labelTempsPanneau.Text = "Temps de pose par panneau";
            this.labelTempsPanneau.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxResultRotationGlobale
            // 
            this.textBoxResultRotationGlobale.Location = new System.Drawing.Point(328, 151);
            this.textBoxResultRotationGlobale.Name = "textBoxResultRotationGlobale";
            this.textBoxResultRotationGlobale.ReadOnly = true;
            this.textBoxResultRotationGlobale.Size = new System.Drawing.Size(80, 20);
            this.textBoxResultRotationGlobale.TabIndex = 24;
            this.textBoxResultRotationGlobale.TabStop = false;
            // 
            // labelResultRotationGlobale
            // 
            this.labelResultRotationGlobale.Location = new System.Drawing.Point(39, 154);
            this.labelResultRotationGlobale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelResultRotationGlobale.Name = "labelResultRotationGlobale";
            this.labelResultRotationGlobale.Size = new System.Drawing.Size(284, 19);
            this.labelResultRotationGlobale.TabIndex = 23;
            this.labelResultRotationGlobale.Text = "Estimation de la rotation globale de l\'objet sur la session";
            this.labelResultRotationGlobale.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxResultTempsGlobal
            // 
            this.textBoxResultTempsGlobal.Location = new System.Drawing.Point(328, 73);
            this.textBoxResultTempsGlobal.Name = "textBoxResultTempsGlobal";
            this.textBoxResultTempsGlobal.ReadOnly = true;
            this.textBoxResultTempsGlobal.Size = new System.Drawing.Size(80, 20);
            this.textBoxResultTempsGlobal.TabIndex = 22;
            this.textBoxResultTempsGlobal.TabStop = false;
            // 
            // labelResultTempsGlobal
            // 
            this.labelResultTempsGlobal.Location = new System.Drawing.Point(74, 76);
            this.labelResultTempsGlobal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelResultTempsGlobal.Name = "labelResultTempsGlobal";
            this.labelResultTempsGlobal.Size = new System.Drawing.Size(249, 19);
            this.labelResultTempsGlobal.TabIndex = 21;
            this.labelResultTempsGlobal.Text = "Temps de pose total pour la mosaïque";
            this.labelResultTempsGlobal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxResultNbPanneau
            // 
            this.textBoxResultNbPanneau.Location = new System.Drawing.Point(328, 47);
            this.textBoxResultNbPanneau.Name = "textBoxResultNbPanneau";
            this.textBoxResultNbPanneau.ReadOnly = true;
            this.textBoxResultNbPanneau.Size = new System.Drawing.Size(80, 20);
            this.textBoxResultNbPanneau.TabIndex = 19;
            this.textBoxResultNbPanneau.TabStop = false;
            // 
            // labelResultNbPanneau
            // 
            this.labelResultNbPanneau.Location = new System.Drawing.Point(138, 50);
            this.labelResultNbPanneau.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelResultNbPanneau.Name = "labelResultNbPanneau";
            this.labelResultNbPanneau.Size = new System.Drawing.Size(185, 19);
            this.labelResultNbPanneau.TabIndex = 18;
            this.labelResultNbPanneau.Text = "Nombre de panneaux";
            this.labelResultNbPanneau.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelMosaic
            // 
            this.panelMosaic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMosaic.Location = new System.Drawing.Point(494, 181);
            this.panelMosaic.Name = "panelMosaic";
            this.panelMosaic.Size = new System.Drawing.Size(400, 400);
            this.panelMosaic.TabIndex = 48;
            // 
            // groupBoxExportResult
            // 
            this.groupBoxExportResult.Controls.Add(this.buttonExportResult);
            this.groupBoxExportResult.Controls.Add(this.checkBoxExportUnistellarLinks);
            this.groupBoxExportResult.Controls.Add(this.radioButtonExportCsv);
            this.groupBoxExportResult.Controls.Add(this.radioButtonExportText);
            this.groupBoxExportResult.Location = new System.Drawing.Point(12, 517);
            this.groupBoxExportResult.Name = "groupBoxExportResult";
            this.groupBoxExportResult.Size = new System.Drawing.Size(475, 66);
            this.groupBoxExportResult.TabIndex = 3;
            this.groupBoxExportResult.TabStop = false;
            this.groupBoxExportResult.Text = "Exporter les résultats";
            // 
            // buttonExportResult
            // 
            this.buttonExportResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportResult.Location = new System.Drawing.Point(394, 37);
            this.buttonExportResult.Name = "buttonExportResult";
            this.buttonExportResult.Size = new System.Drawing.Size(75, 23);
            this.buttonExportResult.TabIndex = 3;
            this.buttonExportResult.Text = "Exporter";
            this.buttonExportResult.UseVisualStyleBackColor = true;
            this.buttonExportResult.Click += new System.EventHandler(this.buttonExportResult_Click);
            // 
            // checkBoxExportUnistellarLinks
            // 
            this.checkBoxExportUnistellarLinks.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxExportUnistellarLinks.Location = new System.Drawing.Point(164, 21);
            this.checkBoxExportUnistellarLinks.Name = "checkBoxExportUnistellarLinks";
            this.checkBoxExportUnistellarLinks.Size = new System.Drawing.Size(248, 19);
            this.checkBoxExportUnistellarLinks.TabIndex = 2;
            this.checkBoxExportUnistellarLinks.Text = "Inclure les liens pour téléscopes Unistellar";
            this.checkBoxExportUnistellarLinks.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxExportUnistellarLinks.UseVisualStyleBackColor = true;
            // 
            // radioButtonExportCsv
            // 
            this.radioButtonExportCsv.AutoSize = true;
            this.radioButtonExportCsv.Location = new System.Drawing.Point(7, 43);
            this.radioButtonExportCsv.Name = "radioButtonExportCsv";
            this.radioButtonExportCsv.Size = new System.Drawing.Size(80, 17);
            this.radioButtonExportCsv.TabIndex = 1;
            this.radioButtonExportCsv.Text = "Format .csv";
            this.radioButtonExportCsv.UseVisualStyleBackColor = true;
            // 
            // radioButtonExportText
            // 
            this.radioButtonExportText.AutoSize = true;
            this.radioButtonExportText.Checked = true;
            this.radioButtonExportText.Location = new System.Drawing.Point(7, 20);
            this.radioButtonExportText.Name = "radioButtonExportText";
            this.radioButtonExportText.Size = new System.Drawing.Size(74, 17);
            this.radioButtonExportText.TabIndex = 0;
            this.radioButtonExportText.TabStop = true;
            this.radioButtonExportText.Text = "Format .txt";
            this.radioButtonExportText.UseVisualStyleBackColor = true;
            // 
            // toolTipStellarium
            // 
            this.toolTipStellarium.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipStellarium_Draw);
            // 
            // toolTipCartesDuCiel
            // 
            this.toolTipCartesDuCiel.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipCartesDuCiel_Draw);
            // 
            // backgroundWorkerStellarium
            // 
            this.backgroundWorkerStellarium.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerStellarium_DoWork);
            // 
            // toolTipErreurStellarium
            // 
            this.toolTipErreurStellarium.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurStellarium_Draw);
            // 
            // backgroundWorkerCartesDuCiel
            // 
            this.backgroundWorkerCartesDuCiel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCartesDuCiel_DoWork);
            // 
            // toolTipErreurCdC
            // 
            this.toolTipErreurCdC.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurCdC_Draw);
            // 
            // toolTipInfosFOV
            // 
            this.toolTipInfosFOV.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfosFOV.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipInfosFOV_Draw);
            // 
            // toolTipErreurFOV
            // 
            this.toolTipErreurFOV.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurFOV_Draw);
            // 
            // toolTipErreurTempsPanneau
            // 
            this.toolTipErreurTempsPanneau.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurTempsPanneau_Draw);
            // 
            // toolTipErreurPctChevauchement
            // 
            this.toolTipErreurPctChevauchement.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurPctChevauchement_Draw);
            // 
            // toolTipErreurNombrePanneau
            // 
            this.toolTipErreurNombrePanneau.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurNombrePanneau_Draw);
            // 
            // toolTipWarningRotationGlobale
            // 
            this.toolTipWarningRotationGlobale.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipWarningRotationGlobale_Draw);
            // 
            // toolTipInfosSelectionPanneau
            // 
            this.toolTipInfosSelectionPanneau.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfosSelectionPanneau.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipInfosSelectionPanneau_Draw);
            // 
            // toolTipInfosRADECModify
            // 
            this.toolTipInfosRADECModify.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipInfosRADECModify_Draw);
            // 
            // toolTipInfosWidthModify
            // 
            this.toolTipInfosWidthModify.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipInfosWidthModify_Draw);
            // 
            // toolTipInfosRADECRestore
            // 
            this.toolTipInfosRADECRestore.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipInfosRADECRestore_Draw);
            // 
            // groupBoxASCOM
            // 
            this.groupBoxASCOM.Controls.Add(this.panelSlew);
            this.groupBoxASCOM.Controls.Add(this.pictureBoxErreurASCOM);
            this.groupBoxASCOM.Controls.Add(this.comboBoxASCOMNom);
            this.groupBoxASCOM.Controls.Add(this.buttonASCOMStop);
            this.groupBoxASCOM.Controls.Add(this.buttonASCOMStandby);
            this.groupBoxASCOM.Controls.Add(this.textBoxASCOMAlt);
            this.groupBoxASCOM.Controls.Add(this.textBoxASCOMAz);
            this.groupBoxASCOM.Controls.Add(this.textBoxASCOMRA);
            this.groupBoxASCOM.Controls.Add(this.textBoxASCOMDEC);
            this.groupBoxASCOM.Controls.Add(this.labelASCOMAz);
            this.groupBoxASCOM.Controls.Add(this.labelASCOMAlt);
            this.groupBoxASCOM.Controls.Add(this.labelASCOMDEC);
            this.groupBoxASCOM.Controls.Add(this.labelASCOMRA);
            this.groupBoxASCOM.Location = new System.Drawing.Point(680, 12);
            this.groupBoxASCOM.Name = "groupBoxASCOM";
            this.groupBoxASCOM.Size = new System.Drawing.Size(214, 155);
            this.groupBoxASCOM.TabIndex = 1;
            this.groupBoxASCOM.TabStop = false;
            this.groupBoxASCOM.Text = "Télescope ASCOM";
            // 
            // panelSlew
            // 
            this.panelSlew.BackColor = System.Drawing.Color.Black;
            this.panelSlew.Controls.Add(this.labelSlew);
            this.panelSlew.Location = new System.Drawing.Point(6, 77);
            this.panelSlew.Name = "panelSlew";
            this.panelSlew.Size = new System.Drawing.Size(44, 17);
            this.panelSlew.TabIndex = 87;
            // 
            // labelSlew
            // 
            this.labelSlew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSlew.ForeColor = System.Drawing.Color.Yellow;
            this.labelSlew.Location = new System.Drawing.Point(0, 0);
            this.labelSlew.Name = "labelSlew";
            this.labelSlew.Size = new System.Drawing.Size(44, 17);
            this.labelSlew.TabIndex = 0;
            this.labelSlew.Text = "Slew";
            this.labelSlew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxErreurASCOM
            // 
            this.pictureBoxErreurASCOM.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurASCOM.Location = new System.Drawing.Point(12, 45);
            this.pictureBoxErreurASCOM.Name = "pictureBoxErreurASCOM";
            this.pictureBoxErreurASCOM.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurASCOM.TabIndex = 86;
            this.pictureBoxErreurASCOM.TabStop = false;
            // 
            // comboBoxASCOMNom
            // 
            this.comboBoxASCOMNom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxASCOMNom.DropDownWidth = 200;
            this.comboBoxASCOMNom.FormattingEnabled = true;
            this.comboBoxASCOMNom.Location = new System.Drawing.Point(40, 20);
            this.comboBoxASCOMNom.Name = "comboBoxASCOMNom";
            this.comboBoxASCOMNom.Size = new System.Drawing.Size(168, 21);
            this.comboBoxASCOMNom.TabIndex = 2;
            this.comboBoxASCOMNom.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxASCOMNom_DrawItem);
            // 
            // buttonASCOMStop
            // 
            this.buttonASCOMStop.Image = global::AstroTargetSelector.Properties.Resources.icons8_stop_sign_color_32;
            this.buttonASCOMStop.Location = new System.Drawing.Point(6, 102);
            this.buttonASCOMStop.Name = "buttonASCOMStop";
            this.buttonASCOMStop.Size = new System.Drawing.Size(44, 44);
            this.buttonASCOMStop.TabIndex = 3;
            this.buttonASCOMStop.UseVisualStyleBackColor = true;
            this.buttonASCOMStop.Click += new System.EventHandler(this.buttonASCOMStop_Click);
            // 
            // buttonASCOMStandby
            // 
            this.buttonASCOMStandby.Image = global::AstroTargetSelector.Properties.Resources.shutdown_24;
            this.buttonASCOMStandby.Location = new System.Drawing.Point(6, 17);
            this.buttonASCOMStandby.Name = "buttonASCOMStandby";
            this.buttonASCOMStandby.Size = new System.Drawing.Size(28, 28);
            this.buttonASCOMStandby.TabIndex = 1;
            this.buttonASCOMStandby.UseVisualStyleBackColor = true;
            this.buttonASCOMStandby.Click += new System.EventHandler(this.buttonASCOMStandby_Click);
            // 
            // textBoxASCOMAlt
            // 
            this.textBoxASCOMAlt.Location = new System.Drawing.Point(102, 100);
            this.textBoxASCOMAlt.Name = "textBoxASCOMAlt";
            this.textBoxASCOMAlt.ReadOnly = true;
            this.textBoxASCOMAlt.Size = new System.Drawing.Size(106, 20);
            this.textBoxASCOMAlt.TabIndex = 34;
            this.textBoxASCOMAlt.TabStop = false;
            // 
            // textBoxASCOMAz
            // 
            this.textBoxASCOMAz.Location = new System.Drawing.Point(102, 126);
            this.textBoxASCOMAz.Name = "textBoxASCOMAz";
            this.textBoxASCOMAz.ReadOnly = true;
            this.textBoxASCOMAz.Size = new System.Drawing.Size(106, 20);
            this.textBoxASCOMAz.TabIndex = 35;
            this.textBoxASCOMAz.TabStop = false;
            // 
            // textBoxASCOMRA
            // 
            this.textBoxASCOMRA.Location = new System.Drawing.Point(102, 48);
            this.textBoxASCOMRA.Name = "textBoxASCOMRA";
            this.textBoxASCOMRA.ReadOnly = true;
            this.textBoxASCOMRA.Size = new System.Drawing.Size(106, 20);
            this.textBoxASCOMRA.TabIndex = 30;
            this.textBoxASCOMRA.TabStop = false;
            // 
            // textBoxASCOMDEC
            // 
            this.textBoxASCOMDEC.Location = new System.Drawing.Point(102, 74);
            this.textBoxASCOMDEC.Name = "textBoxASCOMDEC";
            this.textBoxASCOMDEC.ReadOnly = true;
            this.textBoxASCOMDEC.Size = new System.Drawing.Size(106, 20);
            this.textBoxASCOMDEC.TabIndex = 31;
            this.textBoxASCOMDEC.TabStop = false;
            // 
            // labelASCOMAz
            // 
            this.labelASCOMAz.Location = new System.Drawing.Point(60, 129);
            this.labelASCOMAz.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelASCOMAz.Name = "labelASCOMAz";
            this.labelASCOMAz.Size = new System.Drawing.Size(37, 14);
            this.labelASCOMAz.TabIndex = 37;
            this.labelASCOMAz.Text = "Az.";
            this.labelASCOMAz.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelASCOMAlt
            // 
            this.labelASCOMAlt.Location = new System.Drawing.Point(60, 103);
            this.labelASCOMAlt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelASCOMAlt.Name = "labelASCOMAlt";
            this.labelASCOMAlt.Size = new System.Drawing.Size(37, 14);
            this.labelASCOMAlt.TabIndex = 36;
            this.labelASCOMAlt.Text = "Alt.";
            this.labelASCOMAlt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelASCOMDEC
            // 
            this.labelASCOMDEC.Location = new System.Drawing.Point(60, 77);
            this.labelASCOMDEC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelASCOMDEC.Name = "labelASCOMDEC";
            this.labelASCOMDEC.Size = new System.Drawing.Size(37, 14);
            this.labelASCOMDEC.TabIndex = 33;
            this.labelASCOMDEC.Text = "DEC";
            this.labelASCOMDEC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelASCOMRA
            // 
            this.labelASCOMRA.Location = new System.Drawing.Point(60, 51);
            this.labelASCOMRA.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelASCOMRA.Name = "labelASCOMRA";
            this.labelASCOMRA.Size = new System.Drawing.Size(37, 14);
            this.labelASCOMRA.TabIndex = 32;
            this.labelASCOMRA.Text = "RA";
            this.labelASCOMRA.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // backgroundWorkerTelescope
            // 
            this.backgroundWorkerTelescope.WorkerSupportsCancellation = true;
            this.backgroundWorkerTelescope.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerTelescope_DoWork);
            // 
            // toolTipErreurASCOM
            // 
            this.toolTipErreurASCOM.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurASCOM_Draw);
            // 
            // toolTipInfosASCOM
            // 
            this.toolTipInfosASCOM.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfosASCOM.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipInfosASCOM_Draw);
            // 
            // toolTipErreurASCOMMoveTo
            // 
            this.toolTipErreurASCOMMoveTo.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurASCOMMoveTo_Draw);
            // 
            // toolTipASCOMMoveTo
            // 
            this.toolTipASCOMMoveTo.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipASCOMMoveTo_Draw);
            // 
            // toolTipASCOMStandBy
            // 
            this.toolTipASCOMStandBy.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipASCOMStandBy_Draw);
            // 
            // toolTipASCOMStopSlew
            // 
            this.toolTipASCOMStopSlew.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipASCOMStopSlew_Draw);
            // 
            // buttonCartesDuCielGlobal
            // 
            this.buttonCartesDuCielGlobal.Image = global::AstroTargetSelector.Properties.Resources.cartesduciel;
            this.buttonCartesDuCielGlobal.Location = new System.Drawing.Point(44, 71);
            this.buttonCartesDuCielGlobal.Name = "buttonCartesDuCielGlobal";
            this.buttonCartesDuCielGlobal.Size = new System.Drawing.Size(32, 32);
            this.buttonCartesDuCielGlobal.TabIndex = 4;
            this.buttonCartesDuCielGlobal.UseVisualStyleBackColor = true;
            this.buttonCartesDuCielGlobal.Click += new System.EventHandler(this.buttonCartesDuCielGlobal_Click);
            // 
            // buttonStellariumGlobal
            // 
            this.buttonStellariumGlobal.Image = global::AstroTargetSelector.Properties.Resources.stellarium;
            this.buttonStellariumGlobal.Location = new System.Drawing.Point(6, 71);
            this.buttonStellariumGlobal.Name = "buttonStellariumGlobal";
            this.buttonStellariumGlobal.Size = new System.Drawing.Size(32, 32);
            this.buttonStellariumGlobal.TabIndex = 3;
            this.buttonStellariumGlobal.UseVisualStyleBackColor = true;
            this.buttonStellariumGlobal.Click += new System.EventHandler(this.buttonStellariumGlobal_Click);
            // 
            // pictureBoxErreurCdCGlobal
            // 
            this.pictureBoxErreurCdCGlobal.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurCdCGlobal.Location = new System.Drawing.Point(52, 107);
            this.pictureBoxErreurCdCGlobal.Name = "pictureBoxErreurCdCGlobal";
            this.pictureBoxErreurCdCGlobal.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurCdCGlobal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurCdCGlobal.TabIndex = 86;
            this.pictureBoxErreurCdCGlobal.TabStop = false;
            // 
            // pictureBoxErreurStellariumGlobal
            // 
            this.pictureBoxErreurStellariumGlobal.Image = global::AstroTargetSelector.Properties.Resources.Warning;
            this.pictureBoxErreurStellariumGlobal.Location = new System.Drawing.Point(14, 107);
            this.pictureBoxErreurStellariumGlobal.Name = "pictureBoxErreurStellariumGlobal";
            this.pictureBoxErreurStellariumGlobal.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxErreurStellariumGlobal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErreurStellariumGlobal.TabIndex = 85;
            this.pictureBoxErreurStellariumGlobal.TabStop = false;
            // 
            // toolTipStellariumGlobal
            // 
            this.toolTipStellariumGlobal.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipStellariumGlobal_Draw);
            // 
            // toolTipCartesDuCielGlobal
            // 
            this.toolTipCartesDuCielGlobal.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipCartesDuCielGlobal_Draw);
            // 
            // toolTipErreurStellariumGlobal
            // 
            this.toolTipErreurStellariumGlobal.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurStellariumGlobal_Draw);
            // 
            // toolTipErreurCdCGlobal
            // 
            this.toolTipErreurCdCGlobal.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipErreurCdCGlobal_Draw);
            // 
            // dlgMosaicCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(906, 633);
            this.Controls.Add(this.groupBoxASCOM);
            this.Controls.Add(this.groupBoxExportResult);
            this.Controls.Add(this.panelMosaic);
            this.Controls.Add(this.groupBoxResultat);
            this.Controls.Add(this.groupBoxObjet);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgMosaicCalculator";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ATS : Mosaic Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgMosaicCalculator_FormClosing);
            this.Load += new System.EventHandler(this.dlgMosaicCalculator_Load);
            this.groupBoxObjet.ResumeLayout(false);
            this.groupBoxObjet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurPctChevauchement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurTempsPanneau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurFOV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosFOV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRank4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResultRect)).EndInit();
            this.groupBoxResultat.ResumeLayout(false);
            this.groupBoxResultat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosASCOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurASCOMMoveTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosSelectionPanneau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningRotationGlobale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurNombrePanneau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurCdC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurStellarium)).EndInit();
            this.groupBoxExportResult.ResumeLayout(false);
            this.groupBoxExportResult.PerformLayout();
            this.groupBoxASCOM.ResumeLayout(false);
            this.groupBoxASCOM.PerformLayout();
            this.panelSlew.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurASCOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurCdCGlobal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErreurStellariumGlobal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBoxObjet;
        private System.Windows.Forms.DataGridView dataGridViewResultRect;
        private System.Windows.Forms.TextBox textBoxNomObjet;
        private System.Windows.Forms.Label labelInfoNomObjet;
        private System.Windows.Forms.TextBox textBoxDenominations;
        private System.Windows.Forms.Label labelInfoDenominations;
        private System.Windows.Forms.Label labelDEC;
        private System.Windows.Forms.Label labelRA;
        private System.Windows.Forms.TextBox textBoxRA;
        private System.Windows.Forms.TextBox textBoxDEC;
        private System.Windows.Forms.TextBox textBoxGrandeurL;
        private System.Windows.Forms.TextBox textBoxGrandeurH;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxFOV;
        private System.Windows.Forms.Label labelFOV;
        private System.Windows.Forms.Label labelDegrees;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.TextBox textBoxTempsPanneau;
        private System.Windows.Forms.Label labelTempsPose;
        private System.Windows.Forms.GroupBox groupBoxResultat;
        private System.Windows.Forms.TextBox textBoxResultRotationGlobale;
        private System.Windows.Forms.Label labelResultRotationGlobale;
        private System.Windows.Forms.TextBox textBoxResultTempsGlobal;
        private System.Windows.Forms.Label labelResultTempsGlobal;
        private System.Windows.Forms.TextBox textBoxResultNbPanneau;
        private System.Windows.Forms.Label labelResultNbPanneau;
        private System.Windows.Forms.PictureBox pictureBoxRank5;
        private System.Windows.Forms.Label labelHeure;
        private System.Windows.Forms.TextBox textBoxHeure;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.TextBox textBoxResultTempsPanneau;
        private System.Windows.Forms.Label labelTempsPanneau;
        private System.Windows.Forms.Panel panelMosaic;
        private System.Windows.Forms.Label labelPct;
        private System.Windows.Forms.TextBox textBoxPctChevauchement;
        private System.Windows.Forms.Label labelTauxChevauchement;
        private System.Windows.Forms.TextBox textBoxResultTotalWidth;
        private System.Windows.Forms.Label labelResultTotalWidth;
        private System.Windows.Forms.Button buttonStellarium;
        private System.Windows.Forms.Button buttonCartesDuCiel;
        private System.Windows.Forms.GroupBox groupBoxExportResult;
        private System.Windows.Forms.RadioButton radioButtonExportCsv;
        private System.Windows.Forms.RadioButton radioButtonExportText;
        private System.Windows.Forms.Button buttonExportResult;
        private System.Windows.Forms.CheckBox checkBoxExportUnistellarLinks;
        private System.Windows.Forms.PictureBox pictureBoxRank4;
        private System.Windows.Forms.PictureBox pictureBoxRank3;
        private System.Windows.Forms.PictureBox pictureBoxRank2;
        private System.Windows.Forms.PictureBox pictureBoxRank1;
        private System.Windows.Forms.ToolTip toolTipStellarium;
        private System.Windows.Forms.ToolTip toolTipCartesDuCiel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStellarium;
        private System.Windows.Forms.PictureBox pictureBoxErreurStellarium;
        private System.Windows.Forms.ToolTip toolTipErreurStellarium;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCartesDuCiel;
        private System.Windows.Forms.PictureBox pictureBoxErreurCdC;
        private System.Windows.Forms.ToolTip toolTipErreurCdC;
        private System.Windows.Forms.PictureBox pictureBoxInfosFOV;
        private System.Windows.Forms.ToolTip toolTipInfosFOV;
        private System.Windows.Forms.PictureBox pictureBoxErreurFOV;
        private System.Windows.Forms.ToolTip toolTipErreurFOV;
        private System.Windows.Forms.PictureBox pictureBoxErreurTempsPanneau;
        private System.Windows.Forms.ToolTip toolTipErreurTempsPanneau;
        private System.Windows.Forms.PictureBox pictureBoxErreurPctChevauchement;
        private System.Windows.Forms.ToolTip toolTipErreurPctChevauchement;
        private System.Windows.Forms.PictureBox pictureBoxErreurNombrePanneau;
        private System.Windows.Forms.ToolTip toolTipErreurNombrePanneau;
        private System.Windows.Forms.PictureBox pictureBoxWarningRotationGlobale;
        private System.Windows.Forms.ToolTip toolTipWarningRotationGlobale;
        private System.Windows.Forms.PictureBox pictureBoxInfosSelectionPanneau;
        private System.Windows.Forms.ToolTip toolTipInfosSelectionPanneau;
        private System.Windows.Forms.ToolTip toolTipInfosRADECModify;
        private System.Windows.Forms.ToolTip toolTipInfosWidthModify;
        private System.Windows.Forms.Button buttonRADECModify;
        private System.Windows.Forms.Button buttonRADECRestore;
        private System.Windows.Forms.Button buttonWidthRestore;
        private System.Windows.Forms.Button buttonWidthModify;
        private System.Windows.Forms.ToolTip toolTipInfosRADECRestore;
        private System.Windows.Forms.CheckBox checkBoxPanneauSup;
        private System.Windows.Forms.GroupBox groupBoxASCOM;
        private System.Windows.Forms.TextBox textBoxASCOMAlt;
        private System.Windows.Forms.TextBox textBoxASCOMAz;
        private System.Windows.Forms.TextBox textBoxASCOMRA;
        private System.Windows.Forms.TextBox textBoxASCOMDEC;
        private System.Windows.Forms.Label labelASCOMAz;
        private System.Windows.Forms.Label labelASCOMAlt;
        private System.Windows.Forms.Label labelASCOMDEC;
        private System.Windows.Forms.Label labelASCOMRA;
        private System.Windows.Forms.PictureBox pictureBoxErreurASCOMMoveTo;
        private System.Windows.Forms.Button buttonASCOMMoveTo;
        private System.Windows.Forms.Button buttonASCOMStandby;
        private System.Windows.Forms.Button buttonASCOMStop;
        private System.Windows.Forms.ComboBox comboBoxASCOMNom;
        private System.Windows.Forms.PictureBox pictureBoxErreurASCOM;
        private System.ComponentModel.BackgroundWorker backgroundWorkerTelescope;
        private System.Windows.Forms.ToolTip toolTipErreurASCOM;
        private System.Windows.Forms.Panel panelSlew;
        private System.Windows.Forms.Label labelSlew;
        private System.Windows.Forms.PictureBox pictureBoxInfosASCOM;
        private System.Windows.Forms.ToolTip toolTipInfosASCOM;
        private System.Windows.Forms.ToolTip toolTipErreurASCOMMoveTo;
        private System.Windows.Forms.ToolTip toolTipASCOMMoveTo;
        private System.Windows.Forms.ToolTip toolTipASCOMStandBy;
        private System.Windows.Forms.ToolTip toolTipASCOMStopSlew;
        private System.Windows.Forms.Button buttonCartesDuCielGlobal;
        private System.Windows.Forms.Button buttonStellariumGlobal;
        private System.Windows.Forms.PictureBox pictureBoxErreurCdCGlobal;
        private System.Windows.Forms.PictureBox pictureBoxErreurStellariumGlobal;
        private System.Windows.Forms.ToolTip toolTipStellariumGlobal;
        private System.Windows.Forms.ToolTip toolTipCartesDuCielGlobal;
        private System.Windows.Forms.ToolTip toolTipErreurStellariumGlobal;
        private System.Windows.Forms.ToolTip toolTipErreurCdCGlobal;
    }
}