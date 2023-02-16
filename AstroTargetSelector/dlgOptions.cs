using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ApplicationTools;
using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue "Options"
    /// </summary>
    public partial class dlgOptions : Form
    {
        #region Propriété
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="factory"></param>
        public dlgOptions(IAppObjFactory factory)
        {
            InitializeComponent();
            this.factory = factory;

            // Positionne les libellés et le mode Jour/Nuit
            LoadLibelles();
            SetAffichage();

            // Bouton Custom Color Mode Nuit
            colorDialogModeNuit.CustomColors = new int[] { ColorTranslator.ToOle(Color.FromArgb(30, 30, 30)),
                                                            ColorTranslator.ToOle(Color.FromArgb(48, 48, 48)),
                                                            ColorTranslator.ToOle(Color.FromArgb(70, 70, 70)),
                                                            ColorTranslator.ToOle(Color.DarkSlateGray),
                                                            ColorTranslator.ToOle(Color.FromArgb(112, 112, 112)),
                                                            ColorTranslator.ToOle(Color.LightYellow),
                                                            ColorTranslator.ToOle(Color.OrangeRed),
                                                            ColorTranslator.ToOle(Color.Chocolate),
                                                            ColorTranslator.ToOle(Color.Coral),
                                                            ColorTranslator.ToOle(Color.Aquamarine),
                                                            ColorTranslator.ToOle(Color.CornflowerBlue),
                                                            ColorTranslator.ToOle(Color.CadetBlue),
                                                            ColorTranslator.ToOle(Color.DarkGreen),
                                                            ColorTranslator.ToOle(Color.DarkOliveGreen),
                                                            ColorTranslator.ToOle(Color.IndianRed),
                                                            ColorTranslator.ToOle(Color.Maroon)};

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
                // Clear du formulaire
                ClearAll();

                // Chargement du formulaire
                ChargeFormulaire();
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

            // Stellarium & Cartes du Ciel
            textBoxHostStellarium.Text = string.Empty;
            textBoxPortStellarium.Text = string.Empty;
            textBoxHostCartesDuCiel.Text = string.Empty;
        }

        /// <summary>
        /// Permet le chargement du formulaire depuis les données stockées
        /// </summary>
        private void ChargeFormulaire()
        {
            // Trace
            factory.GetLog().Log($"Chargement du formulaire", GetType().Name);

            // Stellarium
            groupBoxStellarium.Enabled = factory.GetAppStellarium().IsInstalled || factory.GetAppCartesDuCiel().IsInstalled;
            textBoxHostStellarium.Enabled = factory.GetAppStellarium().IsInstalled;
            textBoxPortStellarium.Enabled = factory.GetAppStellarium().IsInstalled;
            textBoxHostStellarium.Text = factory.GetAppStellarium().Host;
            textBoxPortStellarium.Text = factory.GetAppStellarium().Port;
            toolTipInfoStellarium.ToolTipTitle = Resources.ParametresDuPluginDeControleQDistanceDeStellarium;
            toolTipInfoStellarium.SetToolTip(pictureBoxIconInfoStellarium,
                    Resources.PositionnezIciLesOnformationsNecessairesALaConnexionAuPluginDeCommandeADistanceDeStellarium
                    + Environment.NewLine
                    + Resources.PourExecuterStellariumDirectementSurCetOrdinateurLaissezLaValeurParDefautLocalhost
                    + Environment.NewLine
                    + Resources.LaValeurDuPortDoitCorrespondreACellePositionneeDansStellariumParDefaut8090);

            // Cartes du Ciel
            textBoxHostCartesDuCiel.Enabled = factory.GetAppCartesDuCiel().IsInstalled;
            textBoxHostCartesDuCiel.Text = factory.GetAppCartesDuCiel().Host;
            toolTipInfoCartesDuCiel.ToolTipTitle = Resources.ParametresDeCartesDuCiel;
            toolTipInfoCartesDuCiel.SetToolTip(pictureBoxIconInfoCartesDuCiel,
                    Resources.PourVousConnecterACartesDuCielSurUnServeurSpecifiezIciLAdresseIPDuServeur
                    + Environment.NewLine
                    + Resources.PourExecuterCartesDuCielDirectementSurCetOrdinateurLaissezLaValeurParDefaut127001);

            // Bouton Custom Color Mode Nuit
            buttonColorFenetreTonsSombre.BackColor = factory.GetAppInputs().BackColor;
            buttonColorFenetreTonsClair.BackColor = factory.GetAppInputs().BackColorLight;
            buttonColorPolicesTonsSombre.BackColor = factory.GetAppInputs().ForeColor;
            buttonColorPolicesTonsClair.BackColor = factory.GetAppInputs().ForeColorLight;
        }

        /// <summary>
        /// Lance l'enregistrement des Settings saisis
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Enregistrement des paramètres", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On vérifie d'abord la validité de tous les champs
                // Stellarium.
                if (string.IsNullOrEmpty(textBoxHostStellarium.Text) || string.IsNullOrEmpty(textBoxPortStellarium.Text))
                    throw new WarningException(Resources.FormatDesChampsPourLePluginStellariumIncorrect);
                // Cartes du Ciel.
                if (string.IsNullOrEmpty(textBoxHostCartesDuCiel.Text))
                    throw new WarningException(Resources.FormatDuChampServeurPourCartesDuCielIncorrect);

                // Si tous les champs valide, mise à jour des Settings applicatifs
                factory.GetAppStellarium().Host = textBoxHostStellarium.Text;
                factory.GetAppStellarium().Port = textBoxPortStellarium.Text;
                factory.GetAppCartesDuCiel().Host = textBoxHostCartesDuCiel.Text;

                // Couleur Mode Nuit
                factory.GetAppInputs().BackColor = buttonColorFenetreTonsSombre.BackColor;
                factory.GetAppInputs().BackColorLight = buttonColorFenetreTonsClair.BackColor;
                factory.GetAppInputs().ForeColor = buttonColorPolicesTonsSombre.BackColor;
                factory.GetAppInputs().ForeColorLight = buttonColorPolicesTonsClair.BackColor;

                // Trace
                factory.GetLog().Log($"Enregistrement des Settings effectué avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

                // Fermeture de la Dialogue
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (WarningException err)
            {
                // Trace de l'erreur et information Warning à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Warning);
                MessageBox.Show(err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Warning);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Fatal);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
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
            groupBoxStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            groupBoxCustomColors.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // Boutons et Contrôles
            btOK.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            btCancel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            buttonRASColor.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            if (!nuit)
            {
                btOK.UseVisualStyleBackColor = true;
                btCancel.UseVisualStyleBackColor = true;
                buttonRASColor.UseVisualStyleBackColor = true;
            }

            // Planetarium
            textBoxHostStellarium.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxHostStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxPortStellarium.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxPortStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxHostCartesDuCiel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxHostCartesDuCiel.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            toolTipInfoStellarium.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfoStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfoStellarium.OwnerDraw = nuit;
            toolTipInfoCartesDuCiel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfoCartesDuCiel.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfoCartesDuCiel.OwnerDraw = nuit;
        }

        /// <summary>
        /// Charge des libellés statiques
        /// </summary>
        private void LoadLibelles()
        {
            // Titre
            this.Text = Resources.Options;
            // Planetarium
            groupBoxStellarium.Text = Resources.ParametresDesPlanetariumsStellariumCartesDuCiel;
            labelServeurStellarium.Text = Resources.Serveur;
            labelPortStellarium.Text = Resources.Port;
            labelServeurCdC.Text = Resources.Serveur;
            // Custom Colors
            groupBoxCustomColors.Text = Resources.PersonnalisationDeLAffichageEnModeNuit;
            labelCouleurFenetre.Text = Resources.CouleursDeFenetre;
            labelCouleurPolices.Text = Resources.CouleursDesPolices;
            labelTonsSombres.Text = Resources.TonsSombres;
            labelTonsClairs.Text = Resources.TonsClairs;
            // Boutons
            btOK.Text = ApplicationTools.Properties.Resources.OK;
            btCancel.Text = ApplicationTools.Properties.Resources.Annuler;
        }

        /// <summary>
        /// Repositionne les couleur par défaut du mode nuit
        /// </summary>
        private void SetDefaultColorNuit()
        {
            try
            {
                int red = int.Parse("30", NumberStyles.HexNumber);
                int green = int.Parse("30", NumberStyles.HexNumber);
                int blue = int.Parse("30", NumberStyles.HexNumber);
                buttonColorFenetreTonsSombre.BackColor = Color.FromArgb(red, green, blue);
                red = int.Parse("70", NumberStyles.HexNumber);
                green = int.Parse("70", NumberStyles.HexNumber);
                blue = int.Parse("70", NumberStyles.HexNumber);
                buttonColorFenetreTonsClair.BackColor = Color.FromArgb(red, green, blue);
                buttonColorPolicesTonsSombre.BackColor = Color.OrangeRed;
                buttonColorPolicesTonsClair.BackColor = Color.LightYellow;
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Fatal);
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

        #endregion

        private void dlgOptions_Load(object sender, EventArgs e)
        {
            // Initialisation de la Boîte de dialogue
            InitialisationDialog();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            // Sauvegarde des Settings
            SaveSettings();
        }

        private void toolTipInfoStellarium_Draw(object sender, DrawToolTipEventArgs e)
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
                    e.Graphics.DrawString(toolTipInfoStellarium.ToolTipTitle, fontTitre, brush, rectangleTitre);
                }
                // Text
                Rectangle rectangleText = new Rectangle(18, 14, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void toolTipInfoCartesDuCiel_Draw(object sender, DrawToolTipEventArgs e)
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
                    e.Graphics.DrawString(toolTipInfoCartesDuCiel.ToolTipTitle, fontTitre, brush, rectangleTitre);
                }
                // Text
                Rectangle rectangleText = new Rectangle(18, 14, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void buttonColorFenetreTonsSombre_Click(object sender, EventArgs e)
        {
            colorDialogModeNuit.Color = factory.GetAppInputs().BackColor;
            if (colorDialogModeNuit.ShowDialog() == DialogResult.OK)
            {
                buttonColorFenetreTonsSombre.BackColor = colorDialogModeNuit.Color;
            }
        }

        private void buttonColorFenetreTonsClair_Click(object sender, EventArgs e)
        {
            colorDialogModeNuit.Color = factory.GetAppInputs().BackColor;
            if (colorDialogModeNuit.ShowDialog() == DialogResult.OK)
            {
                buttonColorFenetreTonsClair.BackColor = colorDialogModeNuit.Color;
            }
        }

        private void buttonColorPolicesTonsSombre_Click(object sender, EventArgs e)
        {
            colorDialogModeNuit.Color = factory.GetAppInputs().ForeColor;
            if (colorDialogModeNuit.ShowDialog() == DialogResult.OK)
            {
                buttonColorPolicesTonsSombre.BackColor = colorDialogModeNuit.Color;
            }
        }

        private void buttonColorPolicesTonsClair_Click(object sender, EventArgs e)
        {
            colorDialogModeNuit.Color = factory.GetAppInputs().ForeColorLight;
            if (colorDialogModeNuit.ShowDialog() == DialogResult.OK)
            {
                buttonColorPolicesTonsClair.BackColor = colorDialogModeNuit.Color;
            }
        }

        private void buttonRASColor_Click(object sender, EventArgs e)
        {
            SetDefaultColorNuit();
        }
    }
}
