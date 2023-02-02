using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue "Nouvelle version disponible"
    /// </summary>
    partial class dlgNewVersion : Form
    {
        #region Propriétés

        /// <summary>
        /// Nom de l'Assembly
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="version"></param>
        /// <param name="nom"></param>
        /// <param name="description"></param>
        /// <param name="url"></param>
        public dlgNewVersion(IAppObjFactory factory, string version, string nom, string description, string url)
        {
            InitializeComponent();
            this.factory = factory;
            this.url = url;

            // Positionnement des libellés
            this.Text = $"{AssemblyTitle} : {Resources.NouvelleVersionDisponible}";
            this.labelNouvelleVersion.Text = Resources.NouvelleVersionDisponible;
            this.labelVersion.Text = $"{Resources.Version} {version}";
            this.labelNom.Text = nom;
            this.linkLabelUpdate.Text = AssemblyTitle;
            this.textBoxDescription.Text = description.Replace("\\n", Environment.NewLine);
            this.btOK.Text = ApplicationTools.Properties.Resources.OK;

            // Positionne le mode Jour/Nuit
            SetAffichage();
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

            // Boutons et Contrôles
            btOK.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxDescription.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxDescription.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            if (!nuit)
            {
                btOK.UseVisualStyleBackColor = true;
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// URL de la mise à jour
        /// </summary>
        private string url = string.Empty;

        #endregion

        private void linkLabelUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
            {
                // On ouvre URL.
                System.Diagnostics.Process.Start(url);

                // On quitte l'application afin de na pas perturber la mise à jour
                Application.Exit();
            }
        }
    }
}
