using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ApplicationTools;
using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue "Paramètres de l'observation"
    /// </summary>
    public partial class dlgParametres : Form
    {
        #region Propriété
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="factory"></param>
        public dlgParametres(AppObjFactory factory)
        {
            InitializeComponent();
            this.factory = factory;

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
            
            // Longitude
            textBoxLongitudeDegre.Text = string.Empty;
            textBoxLongitudeMinute.Text = string.Empty;
            textBoxLongitudeSeconde.Text = string.Empty;
            // Latitude
            textBoxLatitudeDegre.Text = string.Empty;
            textBoxLatitudeMinute.Text = string.Empty;
            textBoxLatitudeSeconde.Text = string.Empty;
            // Capteur
            textBoxCapteurLargeur.Text = string.Empty;
            // Zones
            ckN.Checked = false;
            ckNE.Checked = false;
            ckE.Checked = false;
            ckSE.Checked = false;
            ckS.Checked = false;
            ckSO.Checked = false;
            ckO.Checked = false;
            ckNO.Checked = false;
            // Bouge Max
            textBoxBougeMax.Text = string.Empty;

            InitCombos();
        }

        /// <summary>
        /// Initialisations des Combos
        /// </summary>
        private void InitCombos()
        {
            // Trace
            factory.GetLog().Log($"Initialisation des Combos", GetType().Name);

            // Longitude
            comboBoxLongitudeDirection.Items.Clear();
            comboBoxLongitudeDirection.Items.Add(CoordinatesDirection.E.ToString());
            comboBoxLongitudeDirection.Items.Add(CoordinatesDirection.O.ToString());

            // Latitude
            comboBoxLatitudeDirection.Items.Clear();
            comboBoxLatitudeDirection.Items.Add(CoordinatesDirection.N.ToString());
            comboBoxLatitudeDirection.Items.Add(CoordinatesDirection.S.ToString());

            // Capteur
            comboBoxCapteur.Items.Clear();
            foreach(ObjCapteur capteur in factory.GetAppCapteur().Capteurs.ListeObjCapteur)
            {
                comboBoxCapteur.Items.Add(capteur.Nom);
            }
        }

        /// <summary>
        /// Permet le chargement du formulaire depuis les données stockées
        /// </summary>
        private void ChargeFormulaire()
        {
            // Trace
            factory.GetLog().Log($"Chargement du formulaire", GetType().Name);

            // Lieu d'observation : Longitude
            textBoxLongitudeDegre.Text = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLongitude.Degrees.ToString("0", CultureInfo.InvariantCulture);
            textBoxLongitudeMinute.Text = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLongitude.Minutes.ToString("0", CultureInfo.InvariantCulture);
            textBoxLongitudeSeconde.Text = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLongitude.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
            comboBoxLongitudeDirection.SelectedItem = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLongitude.Direction;

            // Lieu d'observation : Longitude
            textBoxLatitudeDegre.Text = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLatitude.Degrees.ToString("0", CultureInfo.InvariantCulture);
            textBoxLatitudeMinute.Text = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLatitude.Minutes.ToString("0", CultureInfo.InvariantCulture);
            textBoxLatitudeSeconde.Text = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLatitude.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
            comboBoxLatitudeDirection.SelectedItem = factory.GetAppInputs().Inputs.LieuObservation.CoordonneeLatitude.Direction;

            // Capteur
            comboBoxCapteur.Text = factory.GetAppInputs().Inputs.Capteur.Nom;
            textBoxCapteurLargeur.Text = factory.GetAppInputs().Inputs.Capteur.Largeur.ToString("0", CultureInfo.InvariantCulture);
            toolTipInfoCapteur.SetToolTip(pictureBoxIconInfoToolTip, Resources.InfoCapteur1 + Environment.NewLine + Resources.InfoCapteur2);

            // Zones Exclues
            ckN.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.N.ToString())).ToList().Count > 0;
            ckNE.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.NE.ToString())).ToList().Count > 0;
            ckE.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.E.ToString())).ToList().Count > 0;
            ckSE.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.SE.ToString())).ToList().Count > 0;
            ckS.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.S.ToString())).ToList().Count > 0;
            ckSO.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.SO.ToString())).ToList().Count > 0;
            ckO.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.O.ToString())).ToList().Count > 0;
            ckNO.Checked = factory.GetAppInputs().Inputs.ZonesExclues.Where(z => z.ToString().Equals(CoordinatesDirection.NO.ToString())).ToList().Count > 0;

            // Bougé max
            textBoxBougeMax.Text = factory.GetAppInputs().Inputs.BougeMax.ToString(CultureInfo.InvariantCulture);
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

                // On vérifie la validité du Lieu d'observation
                Coordinates nouveauLieu = factory.GetCoordinates(0, 0);
                if (!Coordinates.TryParse(textBoxLongitudeDegre.Text, textBoxLongitudeMinute.Text, textBoxLongitudeSeconde.Text, comboBoxLongitudeDirection.Text,
                                        textBoxLatitudeDegre.Text, textBoxLatitudeMinute.Text, textBoxLatitudeSeconde.Text, comboBoxLatitudeDirection.Text,
                                        ref nouveauLieu, factory))
                    throw new Exception(Resources.FormatDuLieuDObservationIncorrect);
                // Mise à jour des Settings applicatifs
                factory.GetAppInputs().Inputs.LieuObservation = nouveauLieu;

                // Capteur
                ObjCapteur capteur;
                if (!ObjCapteur.TryParse(comboBoxCapteur.Text, textBoxCapteurLargeur.Text, out capteur, factory))
                    throw new Exception(Resources.FormatDuCapteurIncorrect);
                // Mise à jour des Settings applicatifs
                factory.GetAppInputs().Inputs.Capteur = capteur;

                // Zones Exclues
                List<CoordinatesDirection> zones = new List<CoordinatesDirection>();
                if (ckN.Checked)
                    zones.Add(CoordinatesDirection.N);
                if (ckNE.Checked)
                    zones.Add(CoordinatesDirection.NE);
                if (ckE.Checked)
                    zones.Add(CoordinatesDirection.E);
                if (ckSE.Checked)
                    zones.Add(CoordinatesDirection.SE);
                if (ckS.Checked)
                    zones.Add(CoordinatesDirection.S);
                if (ckSO.Checked)
                    zones.Add(CoordinatesDirection.SO);
                if (ckO.Checked)
                    zones.Add(CoordinatesDirection.O);
                if (ckNO.Checked)
                    zones.Add(CoordinatesDirection.NO);
                // Mise à jour des Settings applicatifs
                factory.GetAppInputs().Inputs.ZonesExclues = zones;

                // Bougé max. : On vérifie le format et la plage de validité
                decimal bougeMax;
                if (decimal.TryParse(textBoxBougeMax.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out bougeMax)
                    && bougeMax > 0
                    && bougeMax < 5)
                    factory.GetAppInputs().Inputs.BougeMax = bougeMax;
                else
                    throw new Exception(Resources.FormatDuChampBougeMaxIncorrect);

                // Trace
                factory.GetLog().Log($"Enregistrement des Settings effectué avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

                // Fermeture de la Dialogue
                DialogResult = DialogResult.OK;
                Close();
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
        /// Met à jour le champ Largeur en fonction du Capteur sélectionné dans la liste
        /// </summary>
        private void UpdateLargeur()
        {
            // On recherche dans la liste des capteur
            if (!string.IsNullOrEmpty(comboBoxCapteur.Text))
            {
                ObjCapteur objEnCours = factory.GetAppCapteur().Capteurs.ListeObjCapteur.Where(t => t.Nom == comboBoxCapteur.Text).FirstOrDefault();
                if (objEnCours != null)
                {
                    textBoxCapteurLargeur.Text = objEnCours.Largeur.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        #endregion

        private void btOK_Click(object sender, System.EventArgs e)
        {
            // Sauvegarde des Settings
            SaveSettings();
        }

        private void dlgParametres_Load(object sender, EventArgs e)
        {
            // Initialisation de la Boîte de dialogue
            InitialisationDialog();
        }

        private void comboBoxCapteur_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Met à jour le champ Largeur
            UpdateLargeur();
        }
    }
}
