using System;
using System.Windows.Forms;
using TargetSelector.Properties;
using TargetSelectorBusiness;
using ApplicationTools;

namespace TargetSelector
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
                DateTime debutInitialisation = DateTime.Now;

                // Initialisation de la Fabrique d'objet globale à l'application
                factory = new AppObjFactory();

                // Trace
                factory.GetLog().LogInfos("Fonction InitialisationFormulaire DEBUT", "MainFenetre");
                factory.GetLog().LogInfos("Application : " + Application.ExecutablePath, "MainFenetre");
                factory.GetLog().LogInfos("Version : " + Application.ProductVersion, "MainFenetre");
                factory.GetLog().LogInfos("Répertoire UserAppDataPath : " + Application.UserAppDataPath, "MainFenetre");
                factory.GetLog().LogInfos("Répertoire StartupPath : " + Application.StartupPath, "MainFenetre");

                // Trace
                factory.GetLog().LogInfos("Fonction InitialisationFormulaire FIN", "MainFenetre", debutInitialisation.GetElapsed());
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogError(Resources.UneErreurEstSurvenue + err.Message, "MainFenetre");
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

        #endregion
    }
}
