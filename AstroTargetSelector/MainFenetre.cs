using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using AstroTargetSelector.Properties;
using AstroTargetSelectorBusiness;
using System.Drawing;
using System.Globalization;

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
        /// Permet le chargement des Inputs
        /// </summary>
        private void LoadInputs()
        {
            // Date et Heure de l'observation
            dateTimePickerDateObservation.Value = factory.GetAppInputs().Inputs.DateHeureObservation;
            dateTimePickerHeureObservation.Value = factory.GetAppInputs().Inputs.DateHeureObservation;

            // Libellé Coordonnées de l'observation
            lblLieuObservation.Text = factory.GetAppInputs().LieuObservation;

            // ToolTip Paramètres de l'observation
            toolTipInfoParametre.SetToolTip(labelInputsPlusInfos, factory.GetAppInputs().ToolTipInfosTexte);
        }

        /// <summary>
        ///  Permet la mise à jour de la liste avec la collection Targets
        /// </summary>
        private void RechargeListeTarget()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log("Chargement de la liste des targets", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewTarget.Items.Clear();
                foreach (ObjTarget target in factory.GetAppTarget().Targets.ListeObjTarget)
                {
                    listViewTarget.Items.Add(new ListViewItem(new[] { target.Rank.ToString(),
                                                target.Scoring.ToString(),
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

                // Trace
                factory.GetLog().Log($"Chargement de la liste des {listViewTarget.Items.Count} targets effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
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
        /// Mise à jour du panneau d'informations de l'objet céleste en cours
        /// </summary>
        private void UpdateViewPanelInfo()
        {
            try
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
                    if (target != null && !string.IsNullOrEmpty(target.Nom))
                    {
                        // Infos sur l'objet
                        textBoxInfoPanelNom.Text = target.Nom;
                        textBoxInfoPanelType.Text = target.Type;
                        textBoxInfoPanelDescription.Text = target.Description;
                        textBoxInfoPanelLongueur.Text = target.GrandeurWidth.ToString();
                        textBoxInfoPanelHauteur.Text = target.GrandeurHeight.ToString();
                        textBoxInfoPanelMagnitude.Text = target.Magnitude.ToString();
                        textBoxInfoPanelRA.Text = target.RA.ToString();
                        textBoxInfoPanelDEC.Text = target.DEC.ToString();

                        // Graphique
                        chartSliceListe.Series.Clear();
                        chartSliceListe.Series.Add(target.Nom);
                        chartSliceListe.Series[target.Nom].ChartType = SeriesChartType.Column;
                        chartSliceListe.Series[target.Nom].XValueType = ChartValueType.DateTime;
                        chartSliceListe.Series[target.Nom].CustomProperties = "LabelStyle=Bottom, DrawingStyle=LightToDark";
                        chartSliceListe.Series[target.Nom].IsVisibleInLegend = false;
                        foreach (ObjSliceTarget slice in target.Slices)
                        {
                            chartSliceListe.Series[target.Nom].Points.Add(new DataPoint()
                            {
                                XValue = slice.DateHeure.ToOADate(),
                                YValues = new double[] { Convert.ToDouble(slice.TempsPoseCalcule) },
                                Color = slice.CouleurPointGraphique,
                                IsValueShownAsLabel = true,
                                LabelFormat = "0 s"
                            });
                        }
                        chartSliceListe.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                        chartSliceListe.ChartAreas[0].AxisX.Interval = 30;
                        chartSliceListe.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
                        chartSliceListe.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                        chartSliceListe.ChartAreas[0].RecalculateAxesScale();

                        // Trace
                        factory.GetLog().Log($"Chargement du Panneau d'informations pour l'objet {target.Nom} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                    }
                    else
                        splitContainerSecondaire.Panel2Collapsed = true;
                }
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

        private void dateTimePickerHeureObservation_ValueChanged(object sender, EventArgs e)
        {
            factory.GetAppInputs().Inputs.DateHeureObservation = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                                                factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                                                factory.GetAppInputs().Inputs.DateHeureObservation.Day,
                                                                                dateTimePickerHeureObservation.Value.Hour,
                                                                                dateTimePickerHeureObservation.Value.Minute,
                                                                                0);
            RechargeListeTarget();
            RechargeListeFiltreType();
            UpdateViewPanelInfo();
        }
    }
}
