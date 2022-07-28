using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Net;
using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;
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

        #region Constanstes

        /// <summary>
        /// Index de la colonne Nom dans la liste View
        /// </summary>
        private const int IndexColonneNom = 1;

        #endregion

        #region Propriétés

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string SortColumn
        {
            get
            {
                return Properties.Settings.Default.SortColumn;
            }
            set
            {
                Properties.Settings.Default.SortColumn = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Ordre de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string SortOrder
        {
            get
            {
                return Properties.Settings.Default.SortOrder;
            }
            set
            {
                Properties.Settings.Default.SortOrder = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Determine si la fenêtre principale est à l'état Maximisé
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public bool WindowMaximized
        {
            get
            {
                return Properties.Settings.Default.WindowState == "Maximized";
            }
            set
            {
                Properties.Settings.Default.WindowState = value ? "Maximized" : "NormalState";
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MainFenetre()
        {
            InitializeComponent();

            // Création d'une instance du ListView column sorter et assignation à la liste
            lvwColumnSorter = new ListViewColumnSorter();
            listViewTarget.ListViewItemSorter = lvwColumnSorter;
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

                // Positionnement du flag d'initialisation
                initialisationFormEnCours = true;

                // Repositionne l'état de la fenêtre principale sur la valeur positionnée en settings
                if (WindowMaximized)
                    WindowState = FormWindowState.Maximized;

                // Chargement des Inputs
                LoadInputs();

                // Text par défaut de la Status Bar
                SetDefaultStatusText();

                // Initialisation de la ListeView
                InitialisationListeTarget();

                // Initialisation des Combos
                InitialisationComboMinuteIntervalle();
                InitialisationComboTotalTimeSlice();
                InitialisationComboFiltreRank();
                InitialisationComboFiltreMagnitude();

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
            finally
            {
                // On repositionne en sortie le flag d'initialisation du formulaire
                initialisationFormEnCours = false;
            }
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxMinuteIntervalle
        /// </summary>
        private void InitialisationComboMinuteIntervalle()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxMinuteIntervalle", GetType().Name);

            // CLear de la liste
            comboBoxMinuteIntervalle.Items.Clear();

            // Rechargement depuis la liste chargée
            foreach (KeyValuePair<string,string> intervalleEnCours in factory.GetListeMinuteIntervalle())
            {
                comboBoxMinuteIntervalle.Items.Add(intervalleEnCours.Value);
            }

            // Sélection de l'élément positionné
            comboBoxMinuteIntervalle.SelectedItem = factory.GetAppInputs().Inputs.MinuteIntervalSlice.ToString();

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxMinuteIntervalle.Items.Count} Intervalles et sélection de l'élément : {comboBoxMinuteIntervalle.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxTotalTimeSlice
        /// </summary>
        private void InitialisationComboTotalTimeSlice()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxTotalTimeSlice", GetType().Name);

            // CLear de la liste
            comboBoxTotalTimeSlice.Items.Clear();

            // Rechargement depuis la liste chargée
            foreach (KeyValuePair<string, string> dureeEnCours in factory.GetListeTotalTimeSlice())
            {
                comboBoxTotalTimeSlice.Items.Add(dureeEnCours.Value);
            }

            // Sélection de l'élément positionné
            comboBoxTotalTimeSlice.SelectedItem = factory.GetAppInputs().Inputs.TotalTimeSlice.ToString();

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxTotalTimeSlice.Items.Count} Durées et sélection de l'élément : {comboBoxTotalTimeSlice.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltreRank
        /// </summary>
        private void InitialisationComboFiltreRank()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltreRank", GetType().Name);

            // CLear de la liste
            comboBoxFiltreRank.Items.Clear();

            // Rechargement depuis la liste chargée
            foreach (KeyValuePair<string, string> filtreEnCours in factory.GetListeFiltreRank())
            {
                comboBoxFiltreRank.Items.Add(filtreEnCours.Value);
            }

            // Positionnement de "Tous" par défaut
            comboBoxFiltreRank.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltreRank.Items.Count} Filtre de Rank et sélection de l'élément : {comboBoxFiltreRank.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltreMagnitude
        /// </summary>
        private void InitialisationComboFiltreMagnitude()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltreMagnitude", GetType().Name);

            // CLear de la liste
            comboBoxFiltreMagnitude.Items.Clear();

            // Rechargement depuis la liste chargée
            foreach (KeyValuePair<string, string> filtreEnCours in factory.GetListeFiltreMagnitude())
            {
                comboBoxFiltreMagnitude.Items.Add(filtreEnCours.Value);
            }

            // Positionnement de "Tous" par défaut
            comboBoxFiltreMagnitude.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltreMagnitude.Items.Count} Filtre de Magnitude et sélection de l'élément : {comboBoxFiltreMagnitude.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la liste des Targets
        /// </summary>
        private void InitialisationListeTarget()
        {
            // Type de vue
            listViewTarget.View = View.Details;

            // Adjout des colonnes
            listViewTarget.Columns.Add(Resources.Scoring, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Nom, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Type, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Description, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.RA, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.DEC, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Magnitude, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Largeur, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Hauteur, -2, HorizontalAlignment.Left);

            // Tri par défaut
            if (!string.IsNullOrEmpty(SortColumn) && !string.IsNullOrEmpty(SortOrder))
            {
                lvwColumnSorter.SortColumn = int.Parse(SortColumn);
                if (SortOrder == "Descending")
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                else
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                listViewTarget.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
                listViewTarget.Sort();
            }

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
            toolTipInfoParametre.SetToolTip(pictureBoxIconInfoToolTip, factory.GetAppInputs().ToolTipInfosTexte);
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

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewTarget.BeginUpdate();

                // On récupère la target sélectionnée pour la resélectionnée après actialisation de la liste
                string selectedTarget = string.Empty;
                if (listViewTarget.SelectedItems.Count > 0)
                    selectedTarget = listViewTarget.SelectedItems[0].SubItems[2].Text;

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewTarget.Items.Clear();
                foreach (ObjTarget target in factory.GetAppTarget().ListeObjTarget)
                {
                    ListViewItem item = listViewTarget.Items.Add(new ListViewItem(new[] {
                                                target.Scoring.ToString(),
                                                target.Nom,
                                                target.Type,
                                                target.Description,
                                                target.RA.FormatedString,
                                                target.DEC.FormatedString,
                                                target.Magnitude.ToString("0.00"),
                                                target.GrandeurWidth.FormatedString,
                                                target.GrandeurHeight.FormatedString}));

                    // Image en fonction du Statut
                    item.ImageKey = target.Rank.ToString();
                }

                // AutoFit des colonnes
                listViewTarget.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewTarget.FullRowSelect = true;

                // Si une target était sélectionnée, on la repositionne
                if (!string.IsNullOrEmpty(selectedTarget))
                {
                    ListViewItem itemTrouve = listViewTarget.FindItemWithText(selectedTarget);
                    if (itemTrouve != null)
                    {
                        listViewTarget.Items[itemTrouve.Index].Selected = true;
                    }
                }

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
            finally
            {
                // Réactive la liste
                listViewTarget.EndUpdate();
            }
        }

        /// <summary>
        /// Tri de la liste des Targets
        /// </summary>
        /// <param name="indexColonne">Index de la colonne à trier</param>
        private void TriListTargets(int indexColonne)
        {
            try
            {
                // Trace
                factory.GetLog().Log($"Tri de la liste sur l'index de colonne : {indexColonne}", GetType().Name);

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewTarget.BeginUpdate();

                // Colonne actuellement en cours de tri ? Dans ce cas, on inverse le tri en cours
                if (indexColonne == lvwColumnSorter.SortColumn)
                {
                    if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    else
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                else
                {
                    // Sinon, on positionne le tri sur la colonne en cours en mode Ascendant
                    lvwColumnSorter.SortColumn = indexColonne;
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }

                // On lance le tri de la liste
                listViewTarget.Sort();
                listViewTarget.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);

                // MAJ des settings
                SortColumn = lvwColumnSorter.SortColumn.ToString();
                SortOrder = lvwColumnSorter.Order.ToString();
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
            finally
            {
                // Réactive la liste
                listViewTarget.EndUpdate();
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

                // Trace
                factory.GetLog().Log($"Chargement de {comboBoxFiltreType.Items.Count} Type de Target et sélection de l'élément : {comboBoxFiltreType.SelectedItem}", GetType().Name);
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
                    ObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                    if (target != null && !string.IsNullOrEmpty(target.Nom))
                    {
                        // Infos sur l'objet
                        textBoxInfoPanelNom.Text = target.Nom;
                        textBoxInfoPanelType.Text = target.Type;
                        textBoxInfoPanelDescription.Text = target.Description;
                        textBoxInfoPanelLongueur.Text = target.GrandeurWidth.FormatedString;
                        textBoxInfoPanelHauteur.Text = target.GrandeurHeight.FormatedString;
                        textBoxInfoPanelMagnitude.Text = target.Magnitude.ToString("0.00");
                        textBoxInfoPanelRA.Text = target.RA.FormatedString;
                        textBoxInfoPanelDEC.Text = target.DEC.FormatedString;

                        // Graphique
                        chartSliceListe.Series.Clear();
                        chartSliceListe.Series.Add(target.Nom);
                        chartSliceListe.Series[target.Nom].ChartType = SeriesChartType.Column;
                        chartSliceListe.Series[target.Nom].XValueType = ChartValueType.DateTime;
                        chartSliceListe.Series[target.Nom].CustomProperties = "LabelStyle=Top, DrawingStyle=LightToDark";
                        chartSliceListe.Series[target.Nom].IsVisibleInLegend = false;
                        foreach (ObjSliceTarget slice in target.Slices)
                        {
                            // On positionne une police spécifique pour les Labels des Points
                            Font font = new System.Drawing.Font("Tahoma", 8, FontStyle.Bold);
                            // On stocke la valeur du TempsPoseCalcule afin de n'effectuer le calcul qu'une seule fois
                            double tempsPoseCalcule = Convert.ToDouble(slice.TempsPoseCalcule);
                            // On ajoute le point à la série
                            chartSliceListe.Series[target.Nom].Points.Add(new DataPoint()
                            {
                                XValue = slice.DateHeure.ToOADate(),
                                YValues = new double[] { tempsPoseCalcule },
                                Color = slice.CouleurPointGraphique,
                                IsValueShownAsLabel = true,
                                LabelFormat = "0s",
                                Font = font,
                                ToolTip = $"{slice.DateHeure.ToString("HH")}h{slice.DateHeure.ToString("mm")} : {Math.Floor(tempsPoseCalcule)} s",
                                LabelToolTip = $"{slice.DateHeure.ToString("HH")}h{slice.DateHeure.ToString("mm")} : {Math.Floor(tempsPoseCalcule)} s"
                            });
                        }
                        
                        // ChartArea
                        chartSliceListe.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                        chartSliceListe.ChartAreas[0].AxisX.Interval = 30;
                        chartSliceListe.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
                        chartSliceListe.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                        chartSliceListe.ChartAreas[0].RecalculateAxesScale();

                        // Titre graphique
                        chartSliceListe.Titles.Clear();
                        chartSliceListe.Titles.Add($"Temps de pose (secondes) sans rotation de champs (bougé max. {factory.GetAppInputs().BougeMaxString})");
                        chartSliceListe.Titles[0].TextStyle = TextStyle.Shadow;
                        chartSliceListe.Titles[0].ShadowColor = Color.Gray;

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

        /// <summary>
        /// Permet d'actualiser la liste et le panneau d'information
        /// </summary>
        private void UpdateListeAndPanel()
        {
            if (!initialisationFormEnCours)
            {
                // Rechargement Liste, Panneau d'information
                RechargeListeTarget();
                UpdateViewPanelInfo();

                // Update du texte de la Status Bar
                SetDefaultStatusText();
            }
        }

        /// <summary>
        /// Positionne le texte par défaut dans la Status Bar
        /// </summary>
        private void SetDefaultStatusText()
        {
            // Status Text 1 : Date et Heure de l'observation
            toolStripStatusLabelDateObs.Text = $"{Resources.DateDeLObservation} : {factory.GetAppInputs().Inputs.DateHeureObservation.ToString("d")} - {factory.GetAppInputs().Inputs.DateHeureObservation.ToString("t")}";
            
            // Status Text 2 : Objet céleste sélectionné
            if (listViewTarget.SelectedItems == null || listViewTarget.SelectedItems.Count == 0)
            {
                toolStripStatusLabelNomTarget.Visible = false;
                toolStripStatusLabelNomTarget.Text = string.Empty;
            }
            else
            {
                toolStripStatusLabelNomTarget.Visible = true;
                // Affichage des informations de l'objet céleste
                ObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                if (target != null && !string.IsNullOrEmpty(target.Nom))
                    toolStripStatusLabelNomTarget.Text = $"{Resources.ObjetSelectionne} : {target.Nom} - {target.Description}";
            }
        }

        /// <summary>
        /// Lance le téléchargement et l'installation du fichier de configuration des objets célestes
        /// </summary>
        public void UpdateTargetFile()
        {
            try
            {
                // Ouverture de la boîte de dialogue Update
                dlgUpdate dialogUpdate = new dlgUpdate(factory, dlgUpdate.UpdateDialogMode.Target);
                if (dialogUpdate.ShowDialog() == DialogResult.OK)
                {
                    // Tout d'abord, on force le rechargement de la liste des targets dans l'objet ObjTargetList afin de prendre en considération les zones à exclure
                    factory.GetAppTarget().ForceUpdateListe = true;

                    // Rechargement de la liste et du panneau d'information
                    UpdateListeAndPanel();
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

        /// <summary>
        /// Lance le téléchargement et l'installation du fichier de configuration des capteurs
        /// </summary>
        public void UpdateCapteurFile()
        {
            try
            {
                // Ouverture de la boîte de dialogue Update. Pas besoin de rafraissement de la fenêtre sur modification de la liste des capteurs
                dlgUpdate dialogUpdate = new dlgUpdate(factory, dlgUpdate.UpdateDialogMode.Capteur);
                if (dialogUpdate.ShowDialog() == DialogResult.OK)
                {
                    // On force le rechargement de la liste des capteurs lors du prochain appel
                    factory.GetAppCapteur().ForceUpdateListe = true;
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

        /// <summary>
        /// Ouvre la boîte de dialogue A propos
        /// </summary>
        public void OpenAPropos()
        {
            try
            {
                // Ouverture de la boîte de dialogue APropos
                dlgAPropos dialogAPropos = new dlgAPropos(factory);
                dialogAPropos.ShowDialog();
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
        /// Ouvre la boîte de dialogue des Paramètres de l'observation
        /// </summary>
        public void OpenParametres()
        {
            try
            {
                // Ouverture de la boîte de dialogue Paramètres
                dlgParametres dialogParametres = new dlgParametres(factory);
                if (dialogParametres.ShowDialog() == DialogResult.OK)
                {
                    // Sur modification des paramètres, on actualise l'affichage
                    // Tout d'abord, on force le rechargement de la liste des targets dans l'objet ObjTargetList afin de prendre en considération les zones à exclure
                    factory.GetAppTarget().ForceUpdateListe = true;

                    // Rechargement de la liste et du panneau d'information
                    UpdateListeAndPanel();

                    // On actualise les paramètres d'observation
                    lblLieuObservation.Text = factory.GetAppInputs().LieuObservation;
                    toolTipInfoParametre.SetToolTip(pictureBoxIconInfoToolTip, factory.GetAppInputs().ToolTipInfosTexte);
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

        /// <summary>
        /// Flag permettant de savoir si le formulaire est en cours d'initialisation afin de stopper la transmission des events de modification des contrôles
        /// </summary>
        private bool initialisationFormEnCours = false;

        /// <summary>
        /// IComparer permettant le tri de la listview
        /// </summary>
        private ListViewColumnSorter lvwColumnSorter = null;

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

        #endregion

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listViewTarget_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Update du panneau d'information de l'objet sélectionné
            if (listViewTarget.SelectedItems != null && listViewTarget.SelectedItems.Count > 0)
                UpdateViewPanelInfo();

            // Update du texte de la Status Bar
            SetDefaultStatusText();
        }

        private void dateTimePickerDateObservation_ValueChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification de la Date d'observation : {dateTimePickerDateObservation.Value.ToString("dd/MM/yyyy")}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppInputs().Inputs.DateHeureObservation = new DateTime(dateTimePickerDateObservation.Value.Year,
                                                                                dateTimePickerDateObservation.Value.Month,
                                                                                dateTimePickerDateObservation.Value.Day,
                                                                                factory.GetAppInputs().Inputs.DateHeureObservation.Hour,
                                                                                factory.GetAppInputs().Inputs.DateHeureObservation.Minute,
                                                                                0);
            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();

            // Actualisation du texte de la Status Bar
            SetDefaultStatusText();
        }
        
        private void dateTimePickerHeureObservation_ValueChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification de l'Heure d'observation : {dateTimePickerHeureObservation.Value.ToString("HH:mm")}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppInputs().Inputs.DateHeureObservation = new DateTime(factory.GetAppInputs().Inputs.DateHeureObservation.Year,
                                                                                factory.GetAppInputs().Inputs.DateHeureObservation.Month,
                                                                                factory.GetAppInputs().Inputs.DateHeureObservation.Day,
                                                                                dateTimePickerHeureObservation.Value.Hour,
                                                                                dateTimePickerHeureObservation.Value.Minute,
                                                                                0);
            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();

            // Actualisation du texte de la Status Bar
            SetDefaultStatusText();
        }

        private void comboBoxMinuteIntervalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification des minutes par intervalle : {comboBoxMinuteIntervalle.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppInputs().Inputs.MinuteIntervalSlice = Convert.ToInt32(comboBoxMinuteIntervalle.Text);

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void comboBoxTotalTimeSlice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification de la durée d'observation : {comboBoxTotalTimeSlice.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppInputs().Inputs.TotalTimeSlice = Convert.ToInt32(comboBoxTotalTimeSlice.Text);

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void comboBoxFiltreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification du Filtre Type : {comboBoxFiltreType.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppTarget().FiltreType = comboBoxFiltreType.Text;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void comboBoxFiltreRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification du Filtre Rank : {comboBoxFiltreRank.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppTarget().FiltreRank = comboBoxFiltreRank.Text;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void comboBoxFiltreMagnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification du Filtre Magnitude : {comboBoxFiltreMagnitude.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppTarget().FiltreMagnitude = comboBoxFiltreMagnitude.Text;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void textBoxFiltreNomDescription_TextChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification du Filtre Nom/Description : {textBoxFiltreNomDescription.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppTarget().FiltreNomDescription = textBoxFiltreNomDescription.Text;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void listViewTarget_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            TriListTargets(e.Column);
        }

        private void MainFenetre_ClientSizeChanged(object sender, EventArgs e)
        {
            // Sauvegarde du WindowState dans les Settings
            WindowMaximized = WindowState == FormWindowState.Maximized;
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAPropos();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenParametres();
        }

        private void btModifierParametre_Click(object sender, EventArgs e)
        {
            OpenParametres();
        }

        private void mettreÀJourLaListeDesobjetsCélestesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTargetFile();
        }

        private void mettreÀJourLaListeDescapteursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCapteurFile();
        }
    }
}
