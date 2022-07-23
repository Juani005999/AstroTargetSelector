using System;
using System.Windows.Forms;
using System.Diagnostics;
using AstroTargetSelector.Properties;
using AstroTargetSelectorBusiness;

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

                // Initialisation de la ListeView
                InitialisationListeTarget();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
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

        /// <summary>
        ///  Permet la mise à jour de la liste avec la collection Targets
        /// </summary>
        private void RechargeListeTarget()
        {
            try
            {
                // Trace
                factory.GetLog().Log("Chargement de la liste des targets", GetType().Name);

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewTarget.Items.Clear();
                foreach (ObjTarget target in factory.GetAppTarget().Targets.ListeObjTarget)
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
        /// Permet le rechargement de la liste des filtres sur type en fonction du contenu de la liste des targets
        /// </summary>
        private void RechargeListeFiltreType()
        {
            try
            {
                // Trace
                factory.GetLog().Log("Chargement de la liste des filtres sur Type", GetType().Name);

                // CLear de la liste
                comboBoxFiltreType.Items.Clear();

                // Rechargement depuis la liste chargée
                foreach (string typeEnCours in factory.GetAppTarget().ListeType)
                {
                    comboBoxFiltreType.Items.Add(typeEnCours);
                }

                // Positionnement de "Tous" par défaut
                comboBoxFiltreType.SelectedIndex = 0;
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
        /// Permet le chargement des Inputs
        /// </summary>
        private void LoadInputs()
        {
            // Date et Heure de l'observation
            dateTimePickerDateObservation.Value = factory.GetAppInputs().Inputs.DateHeureObservation;
            dateTimePickerHeureObservation.Value = factory.GetAppInputs().Inputs.DateHeureObservation;

            // Libellé Coordonnées de l'observation
            lblLieuObservation.Text = factory.GetAppInputs().LieuObservation;

            // ToolTip
            toolTipInfoParametre.SetToolTip(labelInputsPlusInfos, factory.GetAppInputs().ToolTipInfosTexte);
        }

        /// <summary>
        /// Mise à jour du panneau d'informations de l'objet céleste en cours
        /// </summary>
        private void UpdateViewPanelInfo()
        {
            // Trace et Chrono
            factory.GetLog().Log($"Rechargement du Panneau d'informations sur l'objet céleste sélectionné", GetType().Name);
            Stopwatch debutFonction = new Stopwatch();
            debutFonction.Start();
            
            // On masque le panneau si aucun élément sélectionné dans la liste
            if (listViewTarget.SelectedItems == null || listViewTarget.SelectedItems.Count == 0)
                splitContainerSecondaire.Panel2Collapsed = true;
            else
            {
                splitContainerSecondaire.Panel2Collapsed = false;

                // Affichage des informations de l'objet céleste
                ObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[2].Text);
                if (target != null)
                {
                    textBoxInfoPanelNom.Text = target.Nom;

                    // Trace
                    factory.GetLog().Log($"Chargement du Panneau d'informations pour l'objet {target.Nom}  en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
            }
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

            // Rechargement de la ListeView et de la liste des filtres sur Type
            RechargeListeTarget();
            RechargeListeFiltreType();

            // Update du Panel Info
            UpdateViewPanelInfo();

            //// Par défaut, la panneau
            //splitContainerSecondaire.Panel2Collapsed = true;
            ////chartSliceListe.Series[0].IsValueShownAsLabel = true;
            ////chartSliceListe.Series[0].IsVisibleInLegend = false;
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
