using ApplicationTools;
using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue pour l'edition de la largeur de la mosaïque
    /// </summary>
    public partial class dlgMosaicEditWidth : Form
    {
        #region Propriété

        /// <summary>
        /// Largeur de la mosaïque
        /// </summary>
        public Coordinate WidthMosaique { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="factory"></param>
        public dlgMosaicEditWidth(IAppObjFactory factory)
        {
            InitializeComponent();

            this.factory = factory;

            // Initialisation objets
            WidthMosaique = factory.GetCoordinate(0, CoordinatesType.Degree);

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

            // Width
            textBoxWidthDegre.Text = string.Empty;
            textBoxWidthMinute.Text = string.Empty;
            textBoxWidthSeconde.Text = string.Empty;
        }

        /// <summary>
        /// Permet le chargement du formulaire depuis les données stockées
        /// </summary>
        private void ChargeFormulaire()
        {
            // Trace
            factory.GetLog().Log($"Chargement du formulaire", GetType().Name);

            // Width
            textBoxWidthDegre.Text = WidthMosaique.Degrees.ToString(); ;
            textBoxWidthMinute.Text = WidthMosaique.Minutes.ToString("00", CultureInfo.InvariantCulture);
            textBoxWidthSeconde.Text = WidthMosaique.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
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

            // Textbox
            textBoxWidthDegre.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxWidthDegre.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxWidthMinute.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxWidthMinute.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxWidthSeconde.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxWidthSeconde.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
        }

        /// <summary>
        /// Charge des libellés statiques
        /// </summary>
        private void LoadLibelles()
        {
            // Titre
            this.Text = Resources.LargeurDeLaMosaique;

            // Label
            labelWidth.Text = Resources.Largeur;

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
                int widthDegree;
                if (string.IsNullOrEmpty(textBoxWidthDegre.Text)
                    || !int.TryParse(textBoxWidthDegre.Text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out widthDegree)
                    || widthDegree < 0
                    || widthDegree > 180)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampLargeurIncorrect);
                }
                int widthMinute;
                if (string.IsNullOrEmpty(textBoxWidthMinute.Text)
                    || !int.TryParse(textBoxWidthMinute.Text, out widthMinute)
                    || widthMinute < 0
                    || widthMinute > 60)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampLargeurIncorrect);
                }
                double widthSeconde;
                if (string.IsNullOrEmpty(textBoxWidthSeconde.Text)
                    || !double.TryParse(textBoxWidthSeconde.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out widthSeconde)
                    || widthSeconde < 0
                    || widthSeconde > 60)
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampLargeurIncorrect);
                }

                string widthString = $"{textBoxWidthDegre.Text}°{textBoxWidthMinute.Text}'{textBoxWidthSeconde.Text}\"";
                Coordinate widthSaisi = factory.GetCoordinate(0, CoordinatesType.Degree);
                if (!Coordinate.TryParseFromFormatedString(widthString, ref widthSaisi))
                {
                    throw new ApplicationTools.WarningException(Resources.FormatDuChampLargeurIncorrect);
                }

                WidthMosaique = widthSaisi;

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

        private void dlgMosaicEditWidth_Load(object sender, EventArgs e)
        {
            InitialisationDialog();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
