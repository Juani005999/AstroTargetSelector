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
    /// Boîte de Dialogue pour l'edition du point central de la mosaïque
    /// </summary>
    public partial class dlgMosaicEditRADEC : Form
    {
        #region Propriété

        /// <summary>
        /// RA du point central
        /// </summary>
        public Coordinate RA { get; set; }

        /// <summary>
        /// DEC du point central
        /// </summary>
        public Coordinate DEC { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="factory"></param>
        public dlgMosaicEditRADEC(IAppObjFactory factory)
        {
            InitializeComponent();

            this.factory = factory;

            // Initialisation objets
            RA = factory.GetCoordinate(0, CoordinatesType.RA);
            DEC = factory.GetCoordinate(0, CoordinatesType.DEC);

            // Positionne les libellés et le mode Jour/Nuit
            LoadLibelles();
            SetAffichage();

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
                ClearAll();
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

            // RA / DEC
            textBoxRAHour.Text = string.Empty;
            textBoxRAMinute.Text = string.Empty;
            textBoxRASeconde.Text = string.Empty;
            textBoxDECDegre.Text = string.Empty;
            textBoxDECMinute.Text = string.Empty;
            textBoxDECSeconde.Text = string.Empty;
        }

        /// <summary>
        /// Permet le chargement du formulaire depuis les données stockées
        /// </summary>
        private void ChargeFormulaire()
        {
            // Trace
            factory.GetLog().Log($"Chargement du formulaire", GetType().Name);

            // RA / DEC
            textBoxRAHour.Text = RA.Hours.ToString();
            textBoxRAMinute.Text = RA.Minutes.ToString("00", CultureInfo.InvariantCulture);
            textBoxRASeconde.Text = RA.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
            textBoxDECDegre.Text = (DEC.Degrees * (DEC.Coordonnee > 0 ? 1 : -1)).ToString();
            textBoxDECMinute.Text = DEC.Minutes.ToString("00", CultureInfo.InvariantCulture);
            textBoxDECSeconde.Text = DEC.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
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

            // Boutons et Contrôles
            btCancel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            btOK.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            if (!nuit)
            {
                btCancel.UseVisualStyleBackColor = true;
                btOK.UseVisualStyleBackColor = true;
            }

            // Textbox RA
            textBoxRAHour.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxRAHour.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxRAMinute.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxRAMinute.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxRASeconde.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxRASeconde.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // Textbox DEC
            textBoxDECDegre.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxDECDegre.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxDECMinute.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxDECMinute.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxDECSeconde.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxDECSeconde.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
        }

        /// <summary>
        /// Charge des libellés statiques
        /// </summary>
        private void LoadLibelles()
        {
            // Titre
            this.Text = Resources.CentreDeLaMosaique;

            // Boutons
            btCancel.Text = ApplicationTools.Properties.Resources.Annuler;
        }

        /// <summary>
        /// Lance l'enregistrement
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Enregistrement des RA/DEC", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On vérifie d'abord la validité de tous les champs
                int raHour;
                if (string.IsNullOrEmpty(textBoxRAHour.Text)
                    || !int.TryParse(textBoxRAHour.Text, out raHour)
                    || raHour < 0
                    || raHour > 24)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampRAIncorrect);
                }
                int raMinute;
                if (string.IsNullOrEmpty(textBoxRAMinute.Text)
                    || !int.TryParse(textBoxRAMinute.Text, out raMinute)
                    || raMinute < 0
                    || raMinute > 60)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampRAIncorrect);
                }
                double raSeconde;
                if (string.IsNullOrEmpty(textBoxRASeconde.Text)
                    || !double.TryParse(textBoxRASeconde.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out raSeconde)
                    || raSeconde < 0
                    || raSeconde > 60)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampRAIncorrect);
                }

                int decDegree;
                if (string.IsNullOrEmpty(textBoxDECDegre.Text)
                    || !int.TryParse(textBoxDECDegre.Text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decDegree)
                    || decDegree < -180
                    || decDegree > 180)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampDECIncorrect);
                }
                int decMinute;
                if (string.IsNullOrEmpty(textBoxDECMinute.Text)
                    || !int.TryParse(textBoxDECMinute.Text, out decMinute)
                    || decMinute < 0
                    || decMinute > 60)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampDECIncorrect);
                }
                double decSeconde;
                if (string.IsNullOrEmpty(textBoxDECSeconde.Text)
                    || !double.TryParse(textBoxDECSeconde.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decSeconde)
                    || decSeconde < 0
                    || decSeconde > 60)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampDECIncorrect);
                }

                // RA
                string raString = $"{textBoxRAHour.Text}h{textBoxRAMinute.Text}'{textBoxRASeconde.Text}\""; 
                Coordinate raSaisi = factory.GetCoordinate(0, CoordinatesType.RA);
                if (!Coordinate.TryParseFromFormatedString(raString, ref raSaisi))
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampRAIncorrect);
                }

                // DEC
                string decString = $"{textBoxDECDegre.Text}h{textBoxDECMinute.Text}'{textBoxDECSeconde.Text}\"";
                Coordinate decSaisi = factory.GetCoordinate(0, CoordinatesType.DEC);
                if (!Coordinate.TryParseFromFormatedString(decString, ref decSaisi))
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampDECIncorrect);
                }

                RA = raSaisi;
                DEC = decSaisi;

                // Trace
                factory.GetLog().Log($"Enregistrement des Settings effectué avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

                // Fermeture de la Dialogue
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ApplicationTools.WarningException err)
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

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        #endregion

        private void dlgMosaicEditRADEC_Load(object sender, EventArgs e)
        {
            InitialisationDialog();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
