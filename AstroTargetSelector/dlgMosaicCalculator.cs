using System;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.IO;
using ApplicationTools;
using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue "Mosaic Calculator"
    /// </summary>
    public partial class dlgMosaicCalculator : Form
    {
        #region Constantes & Enums

        /// <summary>
        /// Nombre max de panneaux dans la mosaique
        /// </summary>
        private const int NombreMaxPanneau1D = 12;

        /// <summary>
        /// Rotation totale acceptée avant Warning
        /// </summary>
        private const int WarningLevelRotationGlobale = 25;

        /// <summary>
        /// Size du paneau de la mosaique
        /// </summary>
        private const int SizePanel = 400;

        /// <summary>
        /// Size du paneau de la mosaique
        /// </summary>
        private const int RectBorderWidth = 3;

        /// <summary>
        /// Liste des mode d'export des résultats
        /// </summary>
        public enum ModeExport
        {
            /// <summary>
            /// Export .txt
            /// </summary>
            TXT,

            /// <summary>
            /// Export .csv
            /// </summary>
            CSV
        }

        #endregion

        #region Propriété

        /// <summary>
        /// FOV par défaut
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string MosaicFOV
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.MosaicFOV))
                {
                    Properties.Settings.Default.MosaicFOV = "0.5";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"MosaicFOV non présent dans les Settings. Positionnement de 0.5 par défaut", GetType().Name);
                }
                return Properties.Settings.Default.MosaicFOV;
            }
            set
            {
                Properties.Settings.Default.MosaicFOV = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Temps par panneau par défaut
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string TempsPanneau
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.TempsPanneau))
                {
                    Properties.Settings.Default.TempsPanneau = "20";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"TempsPanneau non présent dans les Settings. Positionnement de 20 par défaut", GetType().Name);
                }
                return Properties.Settings.Default.TempsPanneau;
            }
            set
            {
                Properties.Settings.Default.TempsPanneau = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Pct de chevauchement par défaut
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string PctChevauchement
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.PctChevauchement))
                {
                    Properties.Settings.Default.PctChevauchement = "25";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"PctChevauchement non présent dans les Settings. Positionnement de 25 par défaut", GetType().Name);
                }
                return Properties.Settings.Default.PctChevauchement;
            }
            set
            {
                Properties.Settings.Default.PctChevauchement = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Type d'export des résultats
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public ModeExport TypeExport
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.TypeExport))
                {
                    Properties.Settings.Default.TypeExport = "TXT";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"TypeExport non présent dans les Settings. Positionnement de TXT par défaut", GetType().Name);
                }
                return Properties.Settings.Default.TypeExport == "CSV" ? ModeExport.CSV : ModeExport.TXT;
            }
            set
            {
                Properties.Settings.Default.TypeExport = value == ModeExport.CSV ? "CSV" : "TXT";
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Ajoute les liens unistellar lors de l'export des résultats
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public bool ExportUnistellarLinks
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.ExportUnistellarLinks))
                {
                    Properties.Settings.Default.ExportUnistellarLinks = "0";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"ExportUnistellarLinks non présent dans les Settings. Positionnement de 0 par défaut", GetType().Name);
                }
                return Properties.Settings.Default.ExportUnistellarLinks == "1";
            }
            set
            {
                Properties.Settings.Default.ExportUnistellarLinks = value ? "1" : "0";
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="target"></param>
        public dlgMosaicCalculator(IAppObjFactory factory, IObjTarget target)
        {
            InitializeComponent();

            if (factory == null || target == null)
                throw new ArgumentNullException();
            this.factory = factory;
            this.target = target;

            // Initialisation objets
            listeResultRect = new BindingList<Tuple<string, string, string>>();
            
            // Positionne les libellés et le mode Jour/Nuit
            LoadLibelles();
            SetAffichage();

            // Event de Draw du Panel Mosaic
            panelMosaic.Paint += new PaintEventHandler(panelMosaic_Paint);

            // Trace
            factory.GetLog().Log($"Ouverture de la boîte de dialogue", GetType().Name);
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Initialisation de la boîte de dialogue
        /// </summary>
        private void InitialisationDialog()
        {
            try
            {
                // Initialisation, Clear des controle et chargement
                InitialisationListeResultRect();
                ClearAll();
                SetToolTip();
                ChargeFormulaire();

                // Calcul de la mosaïque
                CalculMosaique();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        /// <summary>
        /// Flush le formulaire
        /// </summary>
        private void ClearAll()
        {
            // Trace
            factory.GetLog().Log($"Clear du formulaire", GetType().Name);

            chargementFormulaire = true;

            // Objet céleste
            textBoxNomObjet.Text = string.Empty;
            textBoxDenominations.Text = string.Empty;
            textBoxRA.Text = string.Empty;
            textBoxDEC.Text = string.Empty;
            textBoxGrandeurL.Text = string.Empty;
            textBoxGrandeurH.Text = string.Empty;

            // Paramètres
            textBoxDate.Text = factory.GetAppInputs().Inputs.DateHeureObservation.ToString("d");
            textBoxHeure.Text = factory.GetAppInputs().Inputs.DateHeureObservation.ToString("t");
            textBoxFOV.Text = string.Empty;
            textBoxTempsPanneau.Text = string.Empty;
            textBoxPctChevauchement.Text = string.Empty;

            // Export résultat
            radioButtonExportText.Checked = true;
            radioButtonExportCsv.Checked = false;
            checkBoxExportUnistellarLinks.Checked = false;

            // Clear de la liste des rectangles et des résultats
            listeRect.Clear();
            listeResultRect.Clear();
            textBoxResultNbPanneau.Text = string.Empty;
            textBoxResultTempsGlobal.Text = string.Empty;
            textBoxResultTempsPanneau.Text = string.Empty;
            textBoxResultTotalWidth.Text = string.Empty;
            textBoxResultRotationGlobale.Text = string.Empty;
            
            // PictureBox Erreur
            pictureBoxErreurStellarium.Visible = false;
            pictureBoxErreurCdC.Visible = false;
            pictureBoxErreurFOV.Visible = false;
            pictureBoxErreurTempsPanneau.Visible = false;
            pictureBoxErreurPctChevauchement.Visible = false;
            pictureBoxErreurNombrePanneau.Visible = false;
            pictureBoxWarningRotationGlobale.Visible = false;
            buttonStellarium.Enabled = false;
            buttonCartesDuCiel.Enabled = false;

            chargementFormulaire = false;
        }

        /// <summary>
        /// Permet le chargement du formulaire depuis les données stockées
        /// </summary>
        private void ChargeFormulaire()
        {
            // Trace
            factory.GetLog().Log($"Chargement du formulaire", GetType().Name);

            chargementFormulaire = true;

            // Objet céleste
            textBoxNomObjet.Text = target.Nom;
            textBoxDenominations.Text = target.Description;
            ra = target.RA;
            dec = target.DEC;
            textBoxRA.Text = ra.FormatedString;
            textBoxDEC.Text = dec.FormatedString;
            //textBoxGrandeurL.Text = target.GrandeurWidth.FormatedString;
            //textBoxGrandeurH.Text = target.GrandeurHeight.FormatedString;
            widthMosaique = target.GrandeurWidth.Coordonnee > target.GrandeurHeight.Coordonnee ? target.GrandeurWidth : target.GrandeurHeight;
            textBoxGrandeurL.Text = widthMosaique.FormatedString;

            // Paramètres
            textBoxDate.Text = factory.GetAppInputs().Inputs.DateHeureObservation.ToString("d");
            textBoxHeure.Text = factory.GetAppInputs().Inputs.DateHeureObservation.ToString("t");
            textBoxFOV.Text = MosaicFOV;
            textBoxTempsPanneau.Text = TempsPanneau;
            textBoxPctChevauchement.Text = PctChevauchement;

            // PictureBox Rank
            pictureBoxRank1.Visible = target.Rank == 1 || target.Rank == 0;
            pictureBoxRank2.Visible = target.Rank == 2;
            pictureBoxRank3.Visible = target.Rank == 3;
            pictureBoxRank4.Visible = target.Rank == 4;
            pictureBoxRank5.Visible = target.Rank == 5;

            // Export résultat
            radioButtonExportText.Checked = TypeExport == ModeExport.TXT;
            radioButtonExportCsv.Checked = TypeExport == ModeExport.CSV;
            checkBoxExportUnistellarLinks.Checked = ExportUnistellarLinks;

            chargementFormulaire = false;
        }

        /// <summary>
        /// Permet l'initialisation de la liste des Key/Value Fits
        /// </summary>
        private void InitialisationListeResultRect()
        {
            try
            {
                dataGridViewResultRect.DataSource = listeResultRect;
                dataGridViewResultRect.ColumnHeadersVisible = true;
                dataGridViewResultRect.RowHeadersVisible = false;
                dataGridViewResultRect.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewResultRect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewResultRect.Columns[0].HeaderText = string.Empty;
                dataGridViewResultRect.Columns[1].HeaderText = "RA";
                dataGridViewResultRect.Columns[2].HeaderText = "DEC";
                //dataGridViewResultRect.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dataGridViewResultRect.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                listeResultRect.RaiseListChangedEvents = true;
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Initialisation des infos-bulles
        /// </summary>
        private void SetToolTip()
        {
            toolTipStellarium.SetToolTip(buttonStellarium, Resources.AfficherLePanneauSelectionneDansStellarium);
            toolTipErreurStellarium.SetToolTip(pictureBoxErreurStellarium, ApplicationTools.Properties.Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommande);
            toolTipCartesDuCiel.SetToolTip(buttonCartesDuCiel, Resources.AfficherLePanneauSelectionneDansCartesDuCiel);
            toolTipErreurCdC.SetToolTip(pictureBoxErreurCdC, ApplicationTools.Properties.Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommande);

            toolTipInfosFOV.ToolTipTitle = Resources.InformationsSurLeFOV;
            string toolTipInfosFOVValue = Resources.VoiciLaFormuleVousPermettantDeCalculerVotreChampDeVision
                                + Environment.NewLine + Resources.FOVFormule
                                + Environment.NewLine + Resources.LLargeurDeVotreCapteur
                                + Environment.NewLine + Resources.FLongueurFocale;
            toolTipInfosFOV.SetToolTip(pictureBoxInfosFOV, toolTipInfosFOVValue);

            toolTipInfosRADECModify.SetToolTip(buttonRADECModify, Resources.ModifierLesCoordonneesDuCentreDeLaMosaique);
            toolTipInfosWidthModify.SetToolTip(buttonWidthModify, Resources.ModifierLaTailleDeLaMosaique);
            toolTipInfosRADECRestore.SetToolTip(buttonRADECRestore, Resources.RestaurerLesValeursParDefaut);
            toolTipInfosRADECRestore.SetToolTip(buttonWidthRestore, Resources.RestaurerLesValeursParDefaut);

            toolTipInfosSelectionPanneau.ToolTipTitle = Resources.SelectionDUnElémentDansLaListe;
            string toolTipInfosSelectionPanneauValue = Resources.SelectionnezUnPanneauDansLaListeAfinDeLAfficherDansLaMosaique
                                + Environment.NewLine + Resources.LesBoutonsStellariumEtCartesDuCielVousPermettentDePositionnerCesLogicielsSurLesCoordonneesDuPanneauSelectionne;
            toolTipInfosSelectionPanneau.SetToolTip(pictureBoxInfosSelectionPanneau, toolTipInfosSelectionPanneauValue);

            string toolTipInfosErreurFOVValue = Resources.FormatIncorrect
                    + Environment.NewLine + Resources.VeuillezSpecifierUneValeurDecicmalePositive;
            toolTipErreurFOV.SetToolTip(pictureBoxErreurFOV, toolTipInfosErreurFOVValue);

            string toolTipInfosErreurTempsPanneauValue = Resources.ValeurIncorrecte
                    + Environment.NewLine + Resources.VeuillezSpecifierUnNombreEntierComprisEntre1Et240;
            toolTipErreurTempsPanneau.SetToolTip(pictureBoxErreurTempsPanneau, toolTipInfosErreurTempsPanneauValue);

            string toolTipInfosErreurPctChevauchement = Resources.ValeurIncorrecte
                    + Environment.NewLine + Resources.VeuillezSpecifierUnNombreEntierComprisEntre1Et50;
            toolTipErreurPctChevauchement.SetToolTip(pictureBoxErreurPctChevauchement, toolTipInfosErreurPctChevauchement);

            string toolTipInfosErreurNombrePanneau = Resources.LeCalculDeLaMosaiqueDonneUnNombreDePanneauxTropImportant;
            toolTipErreurNombrePanneau.SetToolTip(pictureBoxErreurNombrePanneau, toolTipInfosErreurNombrePanneau);

            string toolTipInfosWarningRotationGlobale = Resources.AttentionLaRotationGlobaleSurLaDureeTotaleDeLaMosaiqueSembleImportante
                    + Environment.NewLine + Resources.NousVousConseillonsDePlanifierVotreMosaiqueEnPlusieursSoireesEnPrivilegiantLesMemesPlagesHoraires;
            toolTipWarningRotationGlobale.SetToolTip(pictureBoxWarningRotationGlobale, toolTipInfosWarningRotationGlobale);
        }

        /// <summary>
        /// Positionne l'affichage en mode Jour / Nuit
        /// </summary>
        private void SetAffichage()
        {
            bool nuit = factory.GetAppInputs().Inputs.ModeNuit;
            // Fenêtre
            BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // GroupBox
            groupBoxObjet.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            groupBoxResultat.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            groupBoxExportResult.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // Boutons et Contrôles
            btCancel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            buttonExportResult.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            buttonRADECModify.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            buttonRADECRestore.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            buttonWidthModify.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            buttonWidthRestore.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            if (!nuit)
            {
                btCancel.UseVisualStyleBackColor = true;
                buttonExportResult.UseVisualStyleBackColor = true;
                buttonRADECModify.UseVisualStyleBackColor = true;
                buttonRADECRestore.UseVisualStyleBackColor = true;
                buttonWidthModify.UseVisualStyleBackColor = true;
                buttonWidthRestore.UseVisualStyleBackColor = true;
            }

            // Textbox Objet et paramètres
            textBoxNomObjet.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxNomObjet.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxDenominations.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxDenominations.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxRA.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxRA.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxDEC.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxDEC.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxGrandeurL.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxGrandeurL.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxGrandeurH.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxGrandeurH.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxDate.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxDate.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxHeure.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxHeure.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxFOV.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxFOV.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxTempsPanneau.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxTempsPanneau.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxPctChevauchement.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxPctChevauchement.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // Textbox Result
            textBoxResultNbPanneau.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxResultNbPanneau.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxResultTempsGlobal.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxResultTempsGlobal.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxResultTempsPanneau.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxResultTempsPanneau.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxResultTotalWidth.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxResultTotalWidth.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxResultRotationGlobale.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxResultRotationGlobale.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            //dataGridViewResultRect.BackgroundColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            //dataGridViewResultRect.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            dataGridViewResultRect.DefaultCellStyle.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;

            // Textbox Export
            //radioButtonExportText.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            //radioButtonExportText.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            //radioButtonExportCsv.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            //radioButtonExportCsv.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            //checkBoxExportUnistellarLinks.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            //checkBoxExportUnistellarLinks.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // ToolTips
            toolTipStellarium.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipStellarium.OwnerDraw = nuit;
            toolTipErreurStellarium.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipErreurStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipErreurStellarium.OwnerDraw = nuit;
            toolTipCartesDuCiel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipCartesDuCiel.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipCartesDuCiel.OwnerDraw = nuit;
            toolTipErreurCdC.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipErreurCdC.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipErreurCdC.OwnerDraw = nuit;
            toolTipInfosFOV.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfosFOV.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfosFOV.OwnerDraw = nuit;
            toolTipErreurFOV.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipErreurFOV.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipErreurFOV.OwnerDraw = nuit;
            toolTipInfosSelectionPanneau.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfosSelectionPanneau.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfosSelectionPanneau.OwnerDraw = nuit;
            toolTipErreurTempsPanneau.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipErreurTempsPanneau.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipErreurTempsPanneau.OwnerDraw = nuit;
            toolTipErreurPctChevauchement.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipErreurPctChevauchement.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipErreurPctChevauchement.OwnerDraw = nuit;
            toolTipErreurNombrePanneau.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipErreurNombrePanneau.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipErreurNombrePanneau.OwnerDraw = nuit;
            toolTipWarningRotationGlobale.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipWarningRotationGlobale.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipWarningRotationGlobale.OwnerDraw = nuit;
            toolTipInfosRADECModify.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfosRADECModify.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfosRADECModify.OwnerDraw = nuit;
            toolTipInfosRADECRestore.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfosRADECRestore.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfosRADECRestore.OwnerDraw = nuit;
            toolTipInfosWidthModify.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfosWidthModify.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfosWidthModify.OwnerDraw = nuit;
        }

        /// <summary>
        /// Charge des libellés statiques
        /// </summary>
        private void LoadLibelles()
        {
            // Titre
            this.Text = Resources.MosaicCalculator;

            // groupBoxObjet
            groupBoxObjet.Text = Resources.ObjetCelesteEtParametresDeLaMosaique;
            labelInfoNomObjet.Text = Resources.Nom;
            labelInfoDenominations.Text = Resources.Description;
            labelRA.Text = Resources.RA;
            labelDEC.Text = Resources.DEC;
            labelWidth.Text = Resources.LargeurDeLaMosaique;
            labelHeight.Text = Resources.GrandeurH;
            labelDate.Text = Resources.Date;
            labelHeure.Text = Resources.Heure;
            labelFOV.Text = Resources.FOV;
            labelTempsPose.Text = Resources.TempsDePoseParPanneau;
            labelMinutes.Text = Resources.Min;
            labelTauxChevauchement.Text = Resources.NiveauDeChevauchementDesImages;

            // groupBoxResultat
            groupBoxResultat.Text = Resources.ResultatsDuCalculDeLaMosaique;
            labelResultNbPanneau.Text = Resources.NombreDePanneaux;
            labelResultTempsGlobal.Text = Resources.TempsDePoseTotalPourLaMosaique;
            labelTempsPanneau.Text = Resources.TempsDePoseParPanneau;
            labelResultTotalWidth.Text = Resources.EstimationDeLaLargeurTotaleDeLaMosaique;
            labelResultRotationGlobale.Text = Resources.EstimationDeLaRotationGlobaleDeLObjetSurLaSession;

            // groupBoxExportResult
            groupBoxExportResult.Text = Resources.ExporterLesResultats;
            radioButtonExportText.Text = $"{Resources.Format} .txt";
            radioButtonExportCsv.Text = $"{Resources.Format} .csv";
            checkBoxExportUnistellarLinks.Text = Resources.InclureLesLiensPourTelescopesUnistellar;
            buttonExportResult.Text = Resources.Exporter;

            // Boutons
            btCancel.Text = Resources.Fermer;
        }

        /// <summary>
        /// Lance le calcul de la mosaïque en fonction des inputs
        /// </summary>
        private void CalculMosaique()
        {
            try
            {
                factory.GetLog().Log("DEBUT : Calcul mosaique");

                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;
                
                // On fige l'état des zones de saisies pour éviter le lancement de traitements parallèles
                textBoxFOV.Enabled = false;
                textBoxTempsPanneau.Enabled = false;
                textBoxPctChevauchement.Enabled = false;

                // Clear de la liste des rectangles et des résultats
                listeRect.Clear();
                listeResultRect.Clear();
                textBoxResultNbPanneau.Text = string.Empty;
                textBoxResultTempsGlobal.Text = string.Empty;
                textBoxResultTempsPanneau.Text = string.Empty;
                textBoxResultTotalWidth.Text = string.Empty;
                textBoxResultRotationGlobale.Text = string.Empty;
                pictureBoxErreurStellarium.Visible = false;
                pictureBoxErreurCdC.Visible = false;
                pictureBoxErreurFOV.Visible = false;
                pictureBoxErreurTempsPanneau.Visible = false;
                pictureBoxErreurPctChevauchement.Visible = false;
                pictureBoxErreurNombrePanneau.Visible = false;
                pictureBoxWarningRotationGlobale.Visible = false;
                buttonStellarium.Enabled = false;
                buttonCartesDuCiel.Enabled = false;
                groupBoxExportResult.Enabled = false;

                // TODO : Vérif des inputs
                double fov = 1;
                if (string.IsNullOrEmpty(textBoxFOV.Text)
                    || !double.TryParse(textBoxFOV.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out fov)
                    || fov <= 0
                    || fov > 360)
                {
                    pictureBoxErreurFOV.Visible = true;
                    throw new Exception("FOV au mauvais format");
                }
                int tempsPanneau = 1;
                if (string.IsNullOrEmpty(textBoxTempsPanneau.Text)
                    || !int.TryParse(textBoxTempsPanneau.Text, out tempsPanneau)
                    || tempsPanneau <= 0
                    || tempsPanneau > 240)
                {
                    pictureBoxErreurTempsPanneau.Visible = true;
                    throw new Exception("Temps de pose par panneau au mauvais format");
                }
                double pctChevauchement = 10;
                if (string.IsNullOrEmpty(textBoxPctChevauchement.Text)
                    || !double.TryParse(textBoxPctChevauchement.Text, out pctChevauchement)
                    || pctChevauchement <= 0
                    || pctChevauchement > 50)
                {
                    pictureBoxErreurPctChevauchement.Visible = true;
                    throw new Exception("Temps de pose par panneau au mauvais format");
                }

                Application.DoEvents();
                chargementListeResult = true;

                // Taille utilsée = max de Width ou Height
                //double tailleMaxImage = target.GrandeurWidth.Coordonnee > target.GrandeurHeight.Coordonnee ?
                //                            target.GrandeurWidth.Coordonnee : target.GrandeurHeight.Coordonnee;
                double tailleMaxImage = widthMosaique.Coordonnee;

                // On recherche le nombre minimum de panneau pour la cible
                int nbPanneau1D = 0;
                double largeurCalculee = 0;
                do
                {
                    nbPanneau1D++;
                    largeurCalculee = (fov * nbPanneau1D) - ((nbPanneau1D - 1) * (fov * pctChevauchement / 100));

                } while (largeurCalculee < tailleMaxImage && nbPanneau1D < NombreMaxPanneau1D);
                tailleMaxImage = largeurCalculee;

                // Si trop de panneaux dans la mosaique, on sort enb erreur
                if (nbPanneau1D >= NombreMaxPanneau1D)
                {
                    pictureBoxErreurNombrePanneau.Visible = true;
                    throw new Exception("Trop de panneaux dans la mosaïque");
                }

                // Les paramètres sont valides, on les stock en settings
                MosaicFOV = textBoxFOV.Text;
                TempsPanneau = textBoxTempsPanneau.Text;
                PctChevauchement = textBoxPctChevauchement.Text;

                // Par sureté, au minimum 1 panneau
                if (nbPanneau1D == 0)
                    nbPanneau1D = 1;
                int nbPanneau2D = nbPanneau1D * nbPanneau1D;

                int largeurRectangleUnitaire = (int)(fov * SizePanel / tailleMaxImage);
                Coordinate raTopLeft = factory.GetCoordinate(ra.Coordonnee - (tailleMaxImage * 24 / 360 / 2), CoordinatesType.RA);
                Coordinate decTopLeft = factory.GetCoordinate(dec.Coordonnee - (tailleMaxImage / 2), CoordinatesType.DEC);

                // Ajout des Rectangle à la liste
                if (nbPanneau1D == 1)
                {
                    int x = 0;
                    int y = 0;
                    double raValue = ra.Coordonnee;
                    double decValue = dec.Coordonnee;
                    IObjMosaicRect rect = factory.GetObjMosaicRect();
                    rect.TopLeft = new Point(x, y);
                    rect.Dimensions = new Size(largeurRectangleUnitaire, largeurRectangleUnitaire);
                    rect.Index = "1";
                    rect.Text = $"{Resources.Panneau} 1";
                    rect.BorderColor = Color.Yellow;
                    rect.TextColor = Color.Yellow;
                    rect.RA = factory.GetCoordinate(raValue, CoordinatesType.RA);
                    rect.DEC = factory.GetCoordinate(decValue, CoordinatesType.DEC);
                    listeRect.Add(rect);
                    listeResultRect.Add(new Tuple<string, string, string>(rect.Text, rect.RA.FormatedString, rect.DEC.FormatedString));
                }
                else
                {
                    int indexRect = 1;
                    // Parcours de la 1D du tableau de Rectangle
                    for (int i = 0; i < nbPanneau1D; i++)
                    {
                        // Parcours de la 2D du tableau de Rectangle
                        for (int j = 0; j < nbPanneau1D; j++)
                        {
                            int x = 0;
                            int y = 0;
                            double raValue = 0;
                            double decValue = 0;
                            x = (int)(j * (SizePanel - largeurRectangleUnitaire) / (nbPanneau1D - 1));
                            y = (int)(i * (SizePanel - largeurRectangleUnitaire) / (nbPanneau1D - 1));
                            raValue = raTopLeft.Coordonnee + (((j * (tailleMaxImage - fov) / (nbPanneau1D - 1)) + (fov / 2)) * 24 / 360);
                            decValue = decTopLeft.Coordonnee + (i * (tailleMaxImage - fov) / (nbPanneau1D - 1)) + (fov / 2);
                            IObjMosaicRect rect = factory.GetObjMosaicRect();
                            rect.TopLeft = new Point(x, y);
                            rect.Dimensions = new Size(largeurRectangleUnitaire, largeurRectangleUnitaire);
                            rect.Index = indexRect.ToString();
                            rect.Text = $"{Resources.Panneau} {rect.Index}";
                            rect.BorderColor = indexRect % 2 == 0 ? Color.GreenYellow : Color.Yellow;
                            rect.TextColor = indexRect % 2 == 0 ? Color.GreenYellow : Color.Yellow;
                            rect.RA = factory.GetCoordinate(raValue, CoordinatesType.RA);
                            rect.DEC = factory.GetCoordinate(decValue, CoordinatesType.DEC);
                            listeRect.Add(rect);
                            listeResultRect.Add(new Tuple<string, string, string>(rect.Text, rect.RA.FormatedString, rect.DEC.FormatedString));
                            indexRect++;
                        }
                    }
                }

                textBoxResultNbPanneau.Text = nbPanneau2D.ToString();
                TimeSpan tempsTotal = new TimeSpan(0, nbPanneau2D * tempsPanneau, 0);
                textBoxResultTempsGlobal.Text = tempsTotal.ToStandardFormatString();
                TimeSpan spanTempsPanneau = new TimeSpan(0, tempsPanneau, 0);
                textBoxResultTempsPanneau.Text = spanTempsPanneau.ToStandardFormatString();
                textBoxResultTotalWidth.Text = factory.GetCoordinate(tailleMaxImage, CoordinatesType.Degree).FormatedString;

                DateTime dateHeureDebut = factory.GetAppInputs().Inputs.DateHeureObservation;
                DateTime dateHeureFin = dateHeureDebut.AddMinutes(nbPanneau2D * tempsPanneau);
                IObjSliceSpan sliceSpan = factory.GetObjSliceSpan(target, dateHeureDebut, dateHeureFin);
                textBoxResultRotationGlobale.Text = sliceSpan.RotationAngulaireGlobaleFormated;
                if (sliceSpan.RotationAngulaireGlobale > WarningLevelRotationGlobale)
                    pictureBoxWarningRotationGlobale.Visible = true;

                buttonStellarium.Enabled = listeResultRect.Count > 0;
                buttonCartesDuCiel.Enabled = listeResultRect.Count > 0;
                groupBoxExportResult.Enabled = listeResultRect.Count > 0;

                factory.GetLog().Log("FIN : Calcul mosaique");
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                //MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                //                , Application.ProductName
                //                , MessageBoxButtons.OK
                //                , MessageBoxIcon.Error);
            }
            finally
            {
                chargementListeResult = false;

                // Positionnement du Curseur
                Cursor = Cursors.Default;
                
                // On repositionne l'état des zones de saisies
                textBoxFOV.Enabled = true;
                textBoxTempsPanneau.Enabled = true;
                textBoxPctChevauchement.Enabled = true;
            }
        }

        /// <summary>
        /// Evénenement permettant le Draw du Panel Mosaic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMosaic_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                factory.GetLog().Log("DEBUT : Redraw du panel");
                foreach (IObjMosaicRect rectEnCours in listeRect)
                {
                    // Rectangle
                    Pen penRect = new Pen(rectEnCours.BorderColor, RectBorderWidth);
                    penRect.Alignment = PenAlignment.Inset;
                    Rectangle rectDraw = Rectangle.Round(new Rectangle(rectEnCours.TopLeft, rectEnCours.Dimensions));
                    e.Graphics.DrawRectangle(penRect, rectDraw);

                    // Text
                    Brush brushText = rectEnCours.TextColor == Color.Yellow ? Brushes.Yellow : Brushes.GreenYellow;
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(rectEnCours.Index,
                                            new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                                            brushText, rectDraw, stringFormat);
                }

                // On redessine le rect sélectionné pour qu'il apparaisse au dessus
                if (!string.IsNullOrEmpty(selectedRect))
                {
                    IObjMosaicRect selRect = listeRect.Where(mr => mr.Text == selectedRect).FirstOrDefault();
                    if (selRect != null)
                    {
                        // Rectangle
                        Rectangle rectDraw = Rectangle.Round(new Rectangle(selRect.TopLeft, selRect.Dimensions));
                        Brush brushText = new SolidBrush(Color.FromArgb(200, 255, 69, 0));
                        e.Graphics.FillRectangle(brushText, rectDraw);

                        // Text
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        e.Graphics.DrawString(selRect.Index,
                            new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                            Brushes.White, rectDraw, stringFormat);
                    }
                }
                factory.GetLog().Log("FIN : Redraw du panel");
            }
            catch (Exception err)
            {
                factory.GetLog().LogException(err, GetType().Name);
                e.Graphics.Clear(Color.Gray);
            }
        }

        /// <summary>
        /// Lance la commande de sélection dans Stellarium pour le Rect Sélectionné
        /// </summary>
        public void StellariumFocusTo()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                buttonStellarium.Enabled = false;
                pictureBoxErreurStellarium.Visible = false;

                if (dataGridViewResultRect.SelectedRows == null || dataGridViewResultRect.SelectedRows.Count == 0)
                    throw new ApplicationTools.WarningException($"Aucun Rectangle sélectionné dans la liste");

                // On récupère le Rect sélectionné
                DataGridViewRow row = dataGridViewResultRect.SelectedRows[0];
                if (row == null || row.Cells.Count == 0) 
                    throw new ApplicationTools.WarningException(ApplicationTools.Properties.Resources.UneErreurEstSurvenue);
                string textSelRect = row.Cells[0].Value as string;
                IObjMosaicRect selRect = listeRect.Where(mr => mr.Text == textSelRect).FirstOrDefault();
                if (selRect == null) 
                    throw new ApplicationTools.WarningException(ApplicationTools.Properties.Resources.UneErreurEstSurvenue);

                // On récupère le fov
                double fov = 1;
                if (string.IsNullOrEmpty(textBoxFOV.Text)
                    || !double.TryParse(textBoxFOV.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out fov)
                    || fov == 0)
                    throw new ApplicationTools.WarningException("FOV au mauvais format");

                // Lancement de la tâche de fond si aucune autre action en cours de traitement
                List<object> tabArgs = new List<object>();
                tabArgs.Add(selRect.Index);
                if (!backgroundWorkerStellarium.IsBusy)
                    backgroundWorkerStellarium.RunWorkerAsync(tabArgs);
                else
                    factory.GetLog().Log($"backgroundWorkerStellarium BUSY", GetType().Name, null, TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Retour au process principal après {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (ApplicationTools.WarningException err)
            {
                // Sur WarningException, le background n'a pas été lancé, donc on remet le bouton Enable
                BeginInvoke(new Action(() => buttonStellarium.Enabled = true), null);
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Traitement asynchrone de la commande de sélection dans Stellarium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerStellarium_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Status Text
                //SetActionStatusText($"{ApplicationTools.Properties.Resources.Stellarium} : {Resources.EnvoiDeLaCommande}");

                // Récup des paramètres
                List<object> tabArgs = e.Argument as List<object>;
                if (tabArgs != null && tabArgs.Count > 0)
                {
                    string indexRect = tabArgs[0] as string;
                    IObjMosaicRect selRect = listeRect.Where(mr => mr.Index == indexRect).FirstOrDefault();
                    if (selRect == null)
                        throw new ApplicationTools.WarningException(ApplicationTools.Properties.Resources.UneErreurEstSurvenue);

                    // On récupère le fov
                    double fov = 1;
                    if (string.IsNullOrEmpty(textBoxFOV.Text)
                        || !double.TryParse(textBoxFOV.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out fov)
                        || fov == 0)
                        throw new ApplicationTools.WarningException("FOV au mauvais format");

                    // On lance la commande
                    factory.GetAppStellarium().FocusTo(selRect.RA,
                                                        selRect.DEC,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation,
                                                        fov);

                    // Trace
                    factory.GetLog().Log($"Exécution de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }

                // On flush le texte de la Status Text
                //SetDefaultStatusText(true);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                // On positionne le message d'erreur retour dans la Status Text
                BeginInvoke(new Action(() => pictureBoxErreurStellarium.Visible = true), null);
                //SetActionStatusText($"{ApplicationTools.Properties.Resources.Stellarium} : {err.Message}");
            }
            finally
            {
                // Dans tous les cas, on remet le bouton Stellarium Enable
                BeginInvoke(new Action(() => buttonStellarium.Enabled = true), null);
            }
        }

        /// <summary>
        /// Lance la commande de sélection dans CdC pour le Rect Sélectionné
        /// </summary>
        public void CdCFocusTo()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                buttonCartesDuCiel.Enabled = false;
                pictureBoxErreurCdC.Visible = false;

                if (dataGridViewResultRect.SelectedRows == null || dataGridViewResultRect.SelectedRows.Count == 0)
                    throw new ApplicationTools.WarningException($"Aucun Rectangle sélectionné dans la liste");

                // On récupère le Rect sélectionné
                DataGridViewRow row = dataGridViewResultRect.SelectedRows[0];
                if (row == null || row.Cells.Count == 0)
                    throw new ApplicationTools.WarningException(ApplicationTools.Properties.Resources.UneErreurEstSurvenue);
                string textSelRect = row.Cells[0].Value as string;
                IObjMosaicRect selRect = listeRect.Where(mr => mr.Text == textSelRect).FirstOrDefault();
                if (selRect == null)
                    throw new ApplicationTools.WarningException(ApplicationTools.Properties.Resources.UneErreurEstSurvenue);

                // On récupère le fov
                double fov = 1;
                if (string.IsNullOrEmpty(textBoxFOV.Text)
                    || !double.TryParse(textBoxFOV.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out fov)
                    || fov == 0)
                    throw new ApplicationTools.WarningException("FOV au mauvais format");

                // Lancement de la tâche de fond si aucune autre action en cours de traitement
                List<object> tabArgs = new List<object>();
                tabArgs.Add(selRect.Index);
                if (!backgroundWorkerCartesDuCiel.IsBusy)
                    backgroundWorkerCartesDuCiel.RunWorkerAsync(tabArgs);
                else
                    factory.GetLog().Log($"backgroundWorkerCartesDuCiel BUSY", GetType().Name, null, TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Retour au process principal après {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (ApplicationTools.WarningException err)
            {
                // Sur WarningException, le background n'a pas été lancé, donc on remet le bouton Enable
                BeginInvoke(new Action(() => buttonCartesDuCiel.Enabled = true), null);
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Traitement asynchrone de la commande de sélection dans Cartes du Ciel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerCartesDuCiel_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Status Text
                //SetActionStatusText($"{ApplicationTools.Properties.Resources.CartesDuCiel} : {Resources.EnvoiDeLaCommande}");

                // Récup des paramètres
                List<object> tabArgs = e.Argument as List<object>;
                if (tabArgs != null && tabArgs.Count > 0)
                {
                    string indexRect = tabArgs[0] as string;
                    IObjMosaicRect selRect = listeRect.Where(mr => mr.Index == indexRect).FirstOrDefault();
                    if (selRect == null)
                        throw new ApplicationTools.WarningException(ApplicationTools.Properties.Resources.UneErreurEstSurvenue);

                    // On récupère le fov
                    double fov = 1;
                    if (string.IsNullOrEmpty(textBoxFOV.Text)
                        || !double.TryParse(textBoxFOV.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out fov)
                        || fov == 0)
                        throw new ApplicationTools.WarningException("FOV au mauvais format");

                    // On lance la commande
                    factory.GetAppCartesDuCiel().FocusTo(selRect.RA,
                                                        selRect.DEC,
                                                        factory.GetAppInputs().Inputs.DateHeureObservation,
                                                        fov);

                    // Trace
                    factory.GetLog().Log($"Exécution de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }

                // On flush le texte de la Status Text
                //SetDefaultStatusText(false, true);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                // On positionne le message d'erreur retour dans la Status Text
                BeginInvoke(new Action(() => pictureBoxErreurCdC.Visible = true), null);
                //SetActionStatusText($"{ApplicationTools.Properties.Resources.CartesDuCiel} : {err.Message}");
            }
            finally
            {
                // Dans tous les cas, on remet le bouton Enable
                BeginInvoke(new Action(() => buttonCartesDuCiel.Enabled = true), null);
            }
        }

        /// <summary>
        /// Permet d'exporter les résultats de la mosaïque
        /// </summary>
        private void ExportResults()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de l'export des résultats", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;

                // On vérifie s'il y a des éléments à exporter
                if (listeRect.Count == 0)
                    throw new Exception("Export impossible : aucun panneau dans la mosaïque");

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                buttonExportResult.Enabled = false;

                // Création nom de fichier en fonction du type souhaité
                string exportFileName = $"MosaicExport_{target.Nom.Replace(" ", "").Replace(".", "").Replace(",", "").Replace(";", "")}";
                if (radioButtonExportCsv.Checked)
                    exportFileName += ".csv";
                else
                    exportFileName += ".txt";
                string exportFullPathName = Path.Combine(factory.GetAppContext().UserAppDataPath, exportFileName);

                // Caractère de séparation en fonction du type de fichier
                string charSep = radioButtonExportCsv.Checked ? "\t" : "\t\t\t";

                // On ajoute la ligne d'en-tête
                string valueEnTete = Resources.NomPanneau;
                valueEnTete += $"{charSep}{Resources.RA}";
                valueEnTete += $"{charSep}{Resources.RA}";
                valueEnTete += $"{charSep}{Resources.DEC}";
                valueEnTete += $"{charSep}{Resources.DEC}";
                if (checkBoxExportUnistellarLinks.Checked)
                    valueEnTete += $"{charSep}{Resources.LiensUnistellar}";
                using (StreamWriter fichierLog = new StreamWriter(exportFullPathName))
                {
                    // Ecriture de l'en-tête
                    fichierLog.WriteLine(valueEnTete);

                    // On parcours les panneau de la mosaïque pour ajout dans le fichier
                    foreach (IObjMosaicRect panneau in listeRect)
                    {
                        // Dans l'URL, RA est sur 360° ... va comprendre Charles
                        Coordinate fauxRA = factory.GetCoordinate(panneau.RA.Coordonnee * 15, CoordinatesType.Degree);
                        string valueRect = panneau.Text;
                        valueRect += $"{charSep}{panneau.RA.Coordonnee.ToString(CultureInfo.InvariantCulture)}";
                        valueRect += $"{charSep}{panneau.RA.FormatedString}";
                        valueRect += $"{charSep}{panneau.DEC.Coordonnee.ToString(CultureInfo.InvariantCulture)}";
                        valueRect += $"{charSep}{panneau.DEC.FormatedString}";
                        if (checkBoxExportUnistellarLinks.Checked)
                        {
                            //string lien = $"{charSep}=HYPERLINK(\"unistellar://science/transit?ra={panneau.RA.Coordonnee.ToString(CultureInfo.InvariantCulture)}"
                            //                + $"&dec={panneau.DEC.Coordonnee.ToString(CultureInfo.InvariantCulture)}\", \"test\")";
                            string lien = $"{charSep}unistellar://science/transit?ra={fauxRA.Coordonnee.ToString(CultureInfo.InvariantCulture)}"
                                            + $"&dec={panneau.DEC.Coordonnee.ToString(CultureInfo.InvariantCulture)}";
                            valueRect += lien;
                            //if (radioButtonExportCsv.Checked)
                            //{
                            //    string lienAdditionnel = $"{charSep}=HYPERLINK(\"unistellar://science/transit?ra={fauxRA.Coordonnee.ToString(CultureInfo.InvariantCulture)}"
                            //                    + $"&dec={panneau.DEC.Coordonnee.ToString(CultureInfo.InvariantCulture)}\", \"{Resources.Lien}\")";
                            //    valueRect += lienAdditionnel;
                            //}
                        }
                        fichierLog.WriteLine(valueRect);
                    }
                    fichierLog.Close();
                }

                // Sauvegarde des settings
                TypeExport = radioButtonExportCsv.Checked ? ModeExport.CSV : ModeExport.TXT;
                ExportUnistellarLinks = checkBoxExportUnistellarLinks.Checked;

                // Ouverture du fichier d'export
                Process.Start(exportFullPathName);

                // Trace
                factory.GetLog().Log($"Fin de l'export des résultats en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
            finally
            {
                buttonExportResult.Enabled = true;

                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Permet la modification des coordonnées du point central de la mosaïque
        /// </summary>
        private void EditRADEC()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de l'édition des RA/DEC", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                dlgMosaicEditRADEC dlgEdit = new dlgMosaicEditRADEC(factory);
                dlgEdit.RA = ra;
                dlgEdit.DEC = dec;
                if (dlgEdit.ShowDialog() == DialogResult.OK)
                {
                    ra = dlgEdit.RA;
                    dec = dlgEdit.DEC;
                    textBoxRA.Text = ra.FormatedString;
                    textBoxDEC.Text = dec.FormatedString;

                    // Calcul et Refresh de la mosaïque
                    CalculMosaique();
                    panelMosaic.Refresh();
                    textBoxFOV.Focus();
                }

                // Trace
                factory.GetLog().Log($"Fin de l'édition des RA/DEC en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// restauration des coordonnées du point central de la mosaïque
        /// </summary>
        private void RestoreRADEC()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la restauration des RA/DEC", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                ra = target.RA;
                dec = target.DEC;
                textBoxRA.Text = ra.FormatedString;
                textBoxDEC.Text = dec.FormatedString;

                // Calcul et Refresh de la mosaïque
                CalculMosaique();
                panelMosaic.Refresh();
                textBoxFOV.Focus();

                // Trace
                factory.GetLog().Log($"Fin de la restauration des RA/DEC en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permet la modification de la largeur de la mosaïque
        /// </summary>
        private void EditLargeur()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de l'édition de la largeur de la mosaïque", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                dlgMosaicEditWidth dlgEdit = new dlgMosaicEditWidth(factory);
                dlgEdit.WidthMosaique = widthMosaique;
                if (dlgEdit.ShowDialog() == DialogResult.OK)
                {
                    widthMosaique = dlgEdit.WidthMosaique;
                    textBoxGrandeurL.Text = widthMosaique.FormatedString;

                    // Calcul et Refresh de la mosaïque
                    CalculMosaique();
                    panelMosaic.Refresh();
                    textBoxFOV.Focus();
                }

                // Trace
                factory.GetLog().Log($"Fin de l'édition de la largeur de la mosaïque en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restauration de la largeur de la mosaïque
        /// </summary>
        private void RestoreLargeur()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la restauration de la largeur de la mosaïque", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                widthMosaique = target.GrandeurWidth.Coordonnee > target.GrandeurHeight.Coordonnee ? target.GrandeurWidth : target.GrandeurHeight;
                textBoxGrandeurL.Text = widthMosaique.FormatedString;

                // Calcul et Refresh de la mosaïque
                CalculMosaique();
                panelMosaic.Refresh();
                textBoxFOV.Focus();

                // Trace
                factory.GetLog().Log($"Fin de la restauration de la largeur de la mosaïque en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Objet sur lequel sera effectué le calcul de la mosaïque
        /// </summary>
        private readonly IObjTarget target = null;

        /// <summary>
        /// Liste des rectangle du Panel
        /// </summary>
        private List<IObjMosaicRect> listeRect = new List<IObjMosaicRect>();

        /// <summary>
        /// Liste servant à l'affichage des résultats des rectangle de la mosaique
        /// </summary>
        private BindingList<Tuple<string, string, string>> listeResultRect = null;

        /// <summary>
        /// Index du rectangle actuellement sélectionné
        /// </summary>
        private string selectedRect = string.Empty;

        /// <summary>
        /// Flag interne permettant de savoir si l'on charge le formulaire
        /// </summary>
        private bool chargementFormulaire = false;

        /// <summary>
        /// Flag interne permettant de savoir si la liste est en cours de chargement
        /// </summary>
        private bool chargementListeResult = false;

        /// <summary>
        /// Coordonnées ra du point central de la mosaïque
        /// </summary>
        private Coordinate ra = null;

        /// <summary>
        /// Coordonnées dec du point central de la mosaïque
        /// </summary>
        private Coordinate dec = null;

        /// <summary>
        /// Largeur de la mosaïque
        /// </summary>
        private Coordinate widthMosaique = null;

        #endregion

        private void dlgMosaicCalculator_Load(object sender, EventArgs e)
        {
            InitialisationDialog();
        }

        private void dataGridViewResultRect_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                selectedRect = string.Empty;
                if (dataGridViewResultRect.SelectedRows != null && dataGridViewResultRect.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridViewResultRect.SelectedRows[0];
                    if (row != null)
                    {
                        selectedRect = row.Cells[0].Value as string;
                        if (!chargementListeResult)
                            panelMosaic.Refresh();
                    }
                }
            }
            catch (Exception err)
            {
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        private void buttonStellarium_Click(object sender, EventArgs e)
        {
            StellariumFocusTo();
        }

        private void buttonCartesDuCiel_Click(object sender, EventArgs e)
        {
            CdCFocusTo();
        }

        private void textBoxFOV_TextChanged(object sender, EventArgs e)
        {
            if (!chargementFormulaire)
            {
                // Calcul et Refresh de la mosaïque
                CalculMosaique();
                panelMosaic.Refresh();
                textBoxFOV.Focus();
            }
        }

        private void textBoxTempsPanneau_TextChanged(object sender, EventArgs e)
        {
            if (!chargementFormulaire)
            {
                // Calcul et Refresh de la mosaïque
                CalculMosaique();
                panelMosaic.Refresh();
                textBoxTempsPanneau.Focus();
            }
        }

        private void textBoxPctChevauchement_TextChanged(object sender, EventArgs e)
        {
            if (!chargementFormulaire)
            {
                // Calcul et Refresh de la mosaïque
                CalculMosaique();
                panelMosaic.Refresh();
                textBoxPctChevauchement.Focus();
            }
        }

        private void buttonExportResult_Click(object sender, EventArgs e)
        {
            ExportResults();
        }

        private void toolTipStellarium_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipErreurNombrePanneau_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipErreurPctChevauchement_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipErreurTempsPanneau_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipInfosSelectionPanneau_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Background et Border
            e.DrawBackground();
            e.DrawBorder();
            // Icon
            Rectangle rectangleIcon = new Rectangle(4, 4, 16, 16);
            e.Graphics.DrawIcon(SystemIcons.Information, rectangleIcon);
            // Titre
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleTitre = new Rectangle(20, 0, e.Bounds.Width, 16);
                using (Font fontTitre = new Font(e.Font, FontStyle.Bold))
                {
                    e.Graphics.DrawString(toolTipInfosSelectionPanneau.ToolTipTitle, fontTitre, brush, rectangleTitre);
                }
                // Text
                Rectangle rectangleText = new Rectangle(18, 14, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipCartesDuCiel_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipErreurStellarium_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipErreurCdC_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipInfosFOV_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Background et Border
            e.DrawBackground();
            e.DrawBorder();
            // Icon
            Rectangle rectangleIcon = new Rectangle(4, 4, 16, 16);
            e.Graphics.DrawIcon(SystemIcons.Information, rectangleIcon);
            // Titre
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleTitre = new Rectangle(20, 0, e.Bounds.Width, 16);
                using (Font fontTitre = new Font(e.Font, FontStyle.Bold))
                {
                    e.Graphics.DrawString(toolTipInfosFOV.ToolTipTitle, fontTitre, brush, rectangleTitre);
                }
                // Text
                Rectangle rectangleText = new Rectangle(18, 14, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipErreurFOV_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipWarningRotationGlobale_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 30, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void buttonRADECModify_Click(object sender, EventArgs e)
        {
            EditRADEC();
        }

        private void buttonRADECRestore_Click(object sender, EventArgs e)
        {
            RestoreRADEC();
        }

        private void buttonWidthModify_Click(object sender, EventArgs e)
        {
            EditLargeur();
        }

        private void buttonWidthRestore_Click(object sender, EventArgs e)
        {
            RestoreLargeur();
        }

        private void toolTipInfosRADECModify_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipInfosWidthModify_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipInfosRADECRestore_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }
    }
}
