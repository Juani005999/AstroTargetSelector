using ApplicationTools;
using AstroTargetSelectorBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de dialogue d'attente pour traitement long
    /// </summary>
    public partial class WaitDialog : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public WaitDialog(IAppObjFactory factory, Action worker, string libelleAction)
        {
            InitializeComponent();

            // Valorisation des membres internes
            this.factory = factory;
            this.worker = worker;

            // Vérif des paramètres
            if (worker == null || string.IsNullOrEmpty(libelleAction))
                throw new ArgumentNullException();

            // Mode Jour / Nuit
            SetAffichage();

            // Update des contrôles
            textBoxWorker.Text = libelleAction;
        }

        #endregion

        #region Méthodes


        /// <summary>
        /// Positionne l'affichage en mode Jour / Nuit
        /// </summary>
        private void SetAffichage()
        {
            bool nuit = factory.GetAppInputs().Inputs.ModeNuit;

            // Fenêtre
            BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // Contrôles
            textBoxWorker.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxWorker.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objets
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Background worker d'exécution de l'action
        /// </summary>
        private readonly Action worker = null;

        #endregion

        private void WaitDialog_Load(object sender, EventArgs e)
        {
            // Après exécution de la tâche passée à la consruction, on Close
            Task.Factory.StartNew(worker)
                        .ContinueWith(t => factory.GetLog().Log($"FIN waitDownloadDlg worker", GetType().Name))
                        .ContinueWith(t => Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void WaitDialog_Shown(object sender, EventArgs e)
        {
            textBoxWorker.SelectionLength = 0;
            progressBarWorker.Focus();
        }
    }
}
