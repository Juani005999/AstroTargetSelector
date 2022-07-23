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
                factory = new AppObjFactory();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire DEBUT", GetType().Name);
                factory.GetLog().Log($"ProductName : {factory.GetAppContext().ProductName}", GetType().Name);
                factory.GetLog().Log($"Version : {factory.GetAppContext().ProductVersion}", GetType().Name);
                factory.GetLog().Log($"Application file : {factory.GetAppContext().ExecutablePath}", GetType().Name);
                factory.GetLog().Log($"Répertoire UserAppDataPath : {factory.GetAppContext().UserAppDataPath}", GetType().Name);
                factory.GetLog().Log($"Répertoire StartupPath : {factory.GetAppContext().StartupPath}", GetType().Name);

                // Chargement des Inputs
                LoadInputs();

                // Initialisation et rechargement de la ListeView
                InitialisationListeTarget();
                RechargeListeTarget();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                // Sur erreur dans l'initialisation de la fenêtre principale, on quitte l'appli
                Application.Exit();
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des Targets
        /// </summary>
        private void InitialisationListeTarget()
        {
            try
            {
                // Type de vue
                listViewTarget.View = View.Details;

                // Adjout des colonnes
                listViewTarget.Columns.Add("Rank", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Scoring", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Nom", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Type", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Description", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("RA", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("DEC", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Magnitude", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Largeur", -2, HorizontalAlignment.Left);
                listViewTarget.Columns.Add("Hauteur", -2, HorizontalAlignment.Left);

                //// Filtre par défaut
                //if (!string.IsNullOrEmpty(Properties.Settings.Default.SortColumn)
                //    && !string.IsNullOrEmpty(Properties.Settings.Default.SortOrder))
                //{
                //    lvwColumnSorter.SortColumn = int.Parse(Properties.Settings.Default.SortColumn);
                //    if (Properties.Settings.Default.SortOrder == "Descending")
                //        lvwColumnSorter.Order = SortOrder.Descending;
                //    else
                //        lvwColumnSorter.Order = SortOrder.Ascending;
                //    listViewMain.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
                //    listViewMain.Sort();
                //}

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Targets OK", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Permet la mise à jour de la liste avec la collection Targets
        /// </summary>
        private void RechargeListeTarget()
        {
            // Trace
            factory.GetLog().Log("Chargement de la liste des targets", GetType().Name);

            listViewTarget.Items.Clear();
            foreach(ObjTarget target in factory.GetAppTarget().Targets.ListeObjTarget)
            {
                listViewTarget.Items.Add(new ListViewItem(new[] { "1",
                                                "2",
                                                target.Nom,
                                                target.Type,
                                                target.Description,
                                                target.RA.ToString(),
                                                target.DEC.ToString(),
                                                target.Magnitude.ToString(),
                                                target.GrandeurWidth.ToString(),
                                                target.GrandeurHeight.ToString()}));
            }

            // AutoFit des colonnes
            listViewTarget.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewTarget.FullRowSelect = true;
        }

        private void LoadInputs()
        {
            // Date et Heure de l'observation
            dateTimePickerDateObservation.Value = factory.GetAppInputs().Inputs.DateHeureObservation;
            dateTimePickerHeureObservation.Value = factory.GetAppInputs().Inputs.DateHeureObservation;

            // Libellé Coordonnées de l'observation
            lblLieuObservation.Text = factory.GetAppInputs().LieuObservation;
        }

        private void UpdateViewPanelInfo()
        {
            splitContainerSecondaire.Panel2Collapsed = listViewTarget.SelectedItems == null || listViewTarget.SelectedItems.Count == 0;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objets
        /// </summary>
        private AppObjFactory factory = null;

        #endregion

        #region Evénements

        /// <summary>
        /// Evénement de chargement de la fenêtre
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Initilisation du Formulaire
            InitialisationFormulaire();

            splitContainerSecondaire.Panel2Collapsed = true;
            //chartSliceListe.Series[0].IsValueShownAsLabel = true;
            //chartSliceListe.Series[0].IsVisibleInLegend = false;
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

        private void listViewTarget_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateViewPanelInfo();
        }
    }
}
