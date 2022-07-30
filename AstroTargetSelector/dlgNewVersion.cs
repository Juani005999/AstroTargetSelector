using AstroTargetSelectorBusiness;
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
        public dlgNewVersion(AppObjFactory factory, string version, string nom, string description, string url)
        {
            InitializeComponent();
            this.factory = factory;
            this.url = url;

            // Positionnement des libellés
            this.Text = $"{AssemblyTitle} : Nouvelle version disponible";
            this.labelNouvelleVersion.Text = $"Nouvelle version disponible";
            this.labelVersion.Text = $"Version {version}";
            this.labelNom.Text = nom;
            this.linkLabelUpdate.Text = AssemblyTitle;
            this.textBoxDescription.Text = description.Replace("\\n", Environment.NewLine);
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

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

                // On ferme la boîte de dialogue
                Close();
            }
        }
    }
}
