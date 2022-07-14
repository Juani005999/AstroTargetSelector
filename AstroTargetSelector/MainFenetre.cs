using System;
using System.Windows.Forms;
using System.Diagnostics;
using AstroTargetSelector.Properties;
using AstroTargetSelectorBusiness;
using ApplicationTools;

namespace AstroTargetSelector
{
    /// <summary>
    /// Fenêtre principale de l'application
    /// </summary>
    public partial class MainFenetre : Form
    {
        #region Enums
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MainFenetre()
        {
            InitializeComponent();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Initialisation du Formulaire principal
        /// </summary>
        private void InitialisationFormulaire()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Initialisation de la Fabrique d'objet globale à l'application
                factory = new AppFactory();

                // Trace
                factory.Log("Fonction InitialisationFormulaire DEBUT", GetType().Name);
                factory.Log($"ProductName : {factory.GetAppContext().ProductName}", GetType().Name);
                factory.Log($"Version : {factory.GetAppContext().ProductVersion}", GetType().Name);
                factory.Log($"Application file : {factory.GetAppContext().ExecutablePath}", GetType().Name);
                factory.Log($"Répertoire UserAppDataPath : {factory.GetAppContext().UserAppDataPath}", GetType().Name);
                factory.Log($"Répertoire StartupPath : {factory.GetAppContext().StartupPath}", GetType().Name);

                // Trace
                factory.Log("Fonction InitialisationFormulaire FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.Log(Resources.UneErreurEstSurvenue + err.Message, GetType().Name, null, AppToolLog.TypeLog.Error);
                MessageBox.Show(Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                // Sur erreur dans l'initialisation de la fenêtre principale, on quitte l'appli
                Application.Exit();
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objets
        /// </summary>
        private AppFactory factory = null;

        #endregion

        #region Evénements

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Initilisation du Formulaire
            InitialisationFormulaire();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ouvrirLeFichierDeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(factory.GetLog().FullPathName);
        }

        #endregion
    }
}
