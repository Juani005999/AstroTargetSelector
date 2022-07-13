using System;
using System.Windows.Forms;
using System.Diagnostics;
using AstroTargetSelector.Properties;
using AstroTargetSelectorBusiness;

namespace AstroTargetSelector
{
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
                factory = new AppObjFactory();

                // Trace
                factory.GetLog().LogInfos("Fonction InitialisationFormulaire DEBUT", GetType().Name);
                factory.GetLog().LogInfos($"ProductName : {factory.GetAppContext().ProductName}", GetType().Name);
                factory.GetLog().LogInfos("Version : " + factory.GetAppContext().ProductVersion, GetType().Name);
                factory.GetLog().LogInfos("Application file : " + factory.GetAppContext().ExecutablePath, GetType().Name);
                factory.GetLog().LogInfos("Répertoire UserAppDataPath : " + factory.GetAppContext().UserAppDataPath, GetType().Name);
                factory.GetLog().LogInfos("Répertoire StartupPath : " + factory.GetAppContext().StartupPath, GetType().Name);

                // Trace
                factory.GetLog().LogInfos("Fonction InitialisationFormulaire FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogError(Resources.UneErreurEstSurvenue + err.Message, GetType().Name);
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
        /// Instance de la fabrique d'objets métier
        /// </summary>
        private AppObjFactory factory = null;

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
