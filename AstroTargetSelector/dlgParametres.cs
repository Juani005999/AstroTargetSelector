using System;
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
            // Longitude
            textBoxLongitudeDegre.Text = string.Empty;
            textBoxLongitudeMinute.Text = string.Empty;
            textBoxLongitudeSeconde.Text = string.Empty;
            // Latitude
            textBoxLatitudeDegre.Text = string.Empty;
            textBoxLatitudeMinute.Text = string.Empty;
            textBoxLatitudeSeconde.Text = string.Empty;
            // Capteur
            textBoxCapteurNom.Text = string.Empty;
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
            // Longitude
            comboBoxLongitudeDirection.Items.Clear();
            comboBoxLongitudeDirection.Items.Add(CoordinatesPosition.E);
            comboBoxLongitudeDirection.Items.Add(CoordinatesPosition.O);

            // Latitude
            comboBoxLatitudeDirection.Items.Clear();
            comboBoxLatitudeDirection.Items.Add(CoordinatesPosition.N);
            comboBoxLatitudeDirection.Items.Add(CoordinatesPosition.S);

            // Capteur
            comboBoxCapteur.Items.Clear();
            foreach(ObjCapteur capteur in factory.GetAppCapteur().Capteurs.ListeObjCapteur)
            {
                comboBoxCapteur.Items.Add(capteur.Nom);
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

        }
    }
}
