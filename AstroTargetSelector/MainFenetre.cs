using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Net;
using System.IO;
using System.Reflection;
using ApplicationTools;
using AstroTargetSelectorBusiness;
using AstroTargetSelectorResources;

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
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.SortColumn))
                {
                    Properties.Settings.Default.SortColumn = "0";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"SortColumn non présent dans les Settings. Positionnement de 0 par défaut", GetType().Name);
                }
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
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.SortOrder))
                {
                    Properties.Settings.Default.SortOrder = "Descending";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"SortOrder non présent dans les Settings. Positionnement de Descending par défaut", GetType().Name);
                }
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

        /// <summary>
        /// Determine si la série Hauteur est visible
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public bool SerieHauteurVisible
        {
            get
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.SerieHauteurVisible))
                    return true;
                return Properties.Settings.Default.SerieHauteurVisible == "1";
            }
            set
            {
                Properties.Settings.Default.SerieHauteurVisible = value ? "1" : "0";
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Determine si la série Lune est visible
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public bool SerieLuneVisible
        {
            get
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.SerieLuneVisible))
                {
                    Properties.Settings.Default.SerieLuneVisible = "1";
                    Properties.Settings.Default.Save();
                }
                return Properties.Settings.Default.SerieLuneVisible == "1";
            }
            set
            {
                Properties.Settings.Default.SerieLuneVisible = value ? "1" : "0";
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Url complète du fichier remote des versions de l'application
        /// </summary>
        public string ftpNewVersionFullPathFile
        {
            get
            {
                return $"ftp://{dlgUpdate.ftpHost}/{dlgUpdate.ftpDirectory}/{newVersionFileName}";
            }
        }

        /// <summary>
        /// Nomdu fichier remote des versions de l'application
        /// </summary>
        public string newVersionFileName
        {
            get
            {
                return $"Version.csv";
            }
        }

        /// <summary>
        /// Path et nom du fichier de destination du téléchargement du fichier des versions
        /// </summary>
        private string newVersionFullPathFile
        {
            get
            {
                return $"{factory.GetAppContext().UserAppDataPath}/{newVersionFileName}";
            }
        }
        
        /// <summary>
        /// Positionne en asynchrone le texte de la Status Date d'Observation
        /// </summary>
        private string StatusLabelDateObs
        {
            set
            {
                BeginInvoke(new Action(() => toolStripStatusLabelDateObs.Text = value), null);
            }
        }

        /// <summary>
        /// Positionne en asynchrone le texte de la Status Target
        /// </summary>
        private string StatusLabelNomTarget
        {
            set
            {
                BeginInvoke(new Action(() =>
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        toolStripStatusLabelNomTarget.Visible = false;
                        toolStripStatusLabelNomTarget.Text = string.Empty;
                    }
                    else
                    {
                        toolStripStatusLabelNomTarget.Visible = true;
                        toolStripStatusLabelNomTarget.Text = value;
                    }
                }), null);
            }
        }

        /// <summary>
        /// Positionne en asynchrone le texte de la Status Action
        /// </summary>
        private string StatusLabelAction
        {
            set
            {
                BeginInvoke(new Action(() => {
                    if (string.IsNullOrEmpty(value))
                    {
                        toolStripStatusActionEnCours.Visible = false;
                        toolStripStatusActionEnCours.Text = string.Empty;
                    }
                    else
                    {
                        toolStripStatusActionEnCours.Visible = true;
                        toolStripStatusActionEnCours.Text = value;
                    }
                }), null);
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MainFenetre()
        {
            // Positionnement du flag d'initialisation
            initialisationFormEnCours = true;

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
                factory.GetLog().Log($"Windows Version : {factory.GetAppContext().OSVersion}", GetType().Name);
                factory.GetLog().Log($"Code Langue - Code Pays : {factory.GetAppContext().CodeLangue}-{factory.GetAppContext().CodePays}", GetType().Name);

                // Force le scaling DPI mode
                AutoScaleDimensions = new SizeF(6F, 13F);
                AutoScaleMode = AutoScaleMode.Font;

                //// Création d'une instance du ListView column sorter et assignation à la liste
                lvwColumnSorter = new ListViewColumnSorter();
                listViewTarget.ListViewItemSorter = lvwColumnSorter as System.Collections.IComparer;

                // Chargements des libellés statiques
                LoadLibelles();

                // Repositionne l'état de la fenêtre principale sur la valeur positionnée en settings
                factory.GetLog().Log($"Valeur du Settings WindowMaximized : {WindowMaximized}", GetType().Name);
                if (WindowMaximized)
                {
                    factory.GetLog().Log($"Maximisation de la fenêtre.", GetType().Name); 
                    WindowState = FormWindowState.Maximized;
                }

                // Chargement des Inputs
                LoadInputs();

                // Etat par défaut du menu ModeNuit
                modeNuitToolStripMenuItem.Checked = factory.GetAppInputs().Inputs.ModeNuit;
                // Création des objets nécessaires à l'Affichage Mode Jour/Nuit
                toolstripRendererMenu = new ATSToolStripRenderer() {
                    ModeNuit = factory.GetAppInputs().Inputs.ModeNuit,
                    BackColor = factory.GetAppInputs().BackColor,
                    BackColorLight = factory.GetAppInputs().BackColorLight,
                    ForeColor = factory.GetAppInputs().ForeColor
                };
                menuStripGlobal.Renderer = toolstripRendererMenu;
                toolstripRendererStatus = new ATSToolStripRenderer()
                {
                    ModeNuit = factory.GetAppInputs().Inputs.ModeNuit,
                    BackColor = factory.GetAppInputs().BackColor,
                    BackColorLight = factory.GetAppInputs().BackColorLight,
                    ForeColor = factory.GetAppInputs().ForeColor
                };
                statusStripGlobal.Renderer = toolstripRendererStatus;
                // Positionnement Affichage Mode Jour/Nuit
                SetAffichage();

                // Bouton Stellarium et Cartes du Ciel masqués si Logiciel non installé
                buttonStellarium.Enabled = factory.GetAppStellarium().IsInstalled;
                buttonCartesDuCiel.Enabled = factory.GetAppCartesDuCiel().IsInstalled;
                astroSessionOrganizerToolStripMenuItem.Enabled = factory.GetAppAstroSessionOrganizer().IsInstalled;
                buttonAstroSessionOrganizer.Enabled = factory.GetAppAstroSessionOrganizer().IsInstalled;

                // Text par défaut de la Status Bar et positionnement ToolTip fixe
                SetDefaultStatusText();
                toolTipInfoActualisation.SetToolTip(buttonStartCalcul, Resources.ActualisationDeLaListeDesObjetsCelesteEtRecalculDesScoringRankEtPositions + " (F5)");
                toolTipInfoTarget.SetToolTip(pictureBoxInfoTarget, Resources.LesIntervallesQuiApparaissentEnGrisSignifientQueLObjetEstDntreDansUneZoneExclueDeVotreCielOuQueSaHauteurEstEnDessousDeLaHauteurMinimum);
                toolTipStellarium.SetToolTip(buttonStellarium, Resources.AfficherLObjetCelesteDansStellarium);
                toolTipCartesDuCiel.SetToolTip(buttonCartesDuCiel, Resources.AfficherLObjetCelesteDansCartesDuCiel);
                toolTipASO.SetToolTip(buttonAstroSessionOrganizer, Resources.DemarrerLApplicationAstroSessionOrganizer);
                toolTipMosaicCalculator.SetToolTip(buttonMosaicCalculator, Resources.AfficherLObjetCelesteDansMosaicCalculator
                                                                            + Environment.NewLine
                                                                            + Resources.LaDateEtLHeureUtiliseesPourLaCreationDeLaMosaiqueCorrespondentACellesUtiliseesPourLObservationHoraire);

                // Initialisation de la ListeView
                InitialisationListeTarget();

                // Initialisation des Combos et CheckBox
                InitialisationComboModeVisualisation();
                InitialisationComboMinuteIntervalle();
                InitialisationComboTotalTimeSlice();
                InitialisationComboFiltreRank();
                InitialisationComboFiltreMagnitude();
                checkBoxHauteur.Checked = SerieHauteurVisible;
                checkBoxLune.Checked = SerieLuneVisible;

                // On Précharge la liste des objets célestes depuis le fichier de configuration en rechargeant la combo des filtres
                RechargeListeFiltreType();

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
        /// Permet l'initialisation de la Combo comboBoxVisualisation
        /// </summary>
        private void InitialisationComboModeVisualisation()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxVisualisation", GetType().Name);

            // CLear de la liste
            comboBoxVisualisation.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Rechargement depuis la liste chargée
            foreach (KeyValuePair<ModeVisualisation, string> modeEnCours in factory.GetListeModeVisualisation())
            {
                ComboBoxItem item = comboBoxItems.NewItem(modeEnCours.Value, modeEnCours.Key.ToString());
                comboBoxItems.Rows.Add(item);
            }

            ModeVisualisation modeVisu = factory.GetAppInputs().Inputs.Visualisation;
            comboBoxVisualisation.DisplayMember = "Text";
            comboBoxVisualisation.ValueMember = "Value";
            comboBoxVisualisation.DataSource = comboBoxItems;
            factory.GetAppInputs().Inputs.Visualisation = modeVisu;

            // Positionnement du Settings
            comboBoxVisualisation.SelectedValue = factory.GetAppInputs().Inputs.Visualisation.ToString();

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxVisualisation.Items.Count} Mode de Visualisation et sélection de l'élément : {comboBoxVisualisation.SelectedItem}", GetType().Name);
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
            listViewTarget.Columns.Add(Resources.Constellation, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.RA, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.DEC, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Azimut, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Hauteur, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.Magnitude, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.GrandeurL, -2, HorizontalAlignment.Left);
            listViewTarget.Columns.Add(Resources.GrandeurH, -2, HorizontalAlignment.Left);

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

            // AutoFit des colonnes
            listViewTarget.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

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
            toolTipInfoParametre.ToolTipTitle = Resources.ParametresDeLObservation;
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

                // Positionnement du Curseur et Status Action text
                Cursor = Cursors.WaitCursor;
                SetActionStatusText(Resources.ActualisationDeLaListeDesObjetsCelesteEtRecalculDesScoringRankEtPositions);
                actualisationEnCours = true;

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewTarget.BeginUpdate();

                // On récupère la target sélectionnée pour la resélectionnée après actialisation de la liste
                string selectedTarget = string.Empty;
                if (listViewTarget.SelectedItems.Count > 0)
                    selectedTarget = listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text;
                if (!string.IsNullOrEmpty(forceSelectedNom))
                {
                    selectedTarget = forceSelectedNom;
                    forceSelectedNom = string.Empty;
                }

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewTarget.Items.Clear();
                ListViewItem itemSelected = null;

                foreach (ObjTarget target in factory.GetAppTarget().ListeObjTarget)
                {
                    ListViewItem item = listViewTarget.Items.Add(new ListViewItem(new[] {
                                                target.Scoring.ToString(),
                                                target.Nom,
                                                target.Type,
                                                target.Description,
                                                target.Constellation,
                                                target.RA.FormatedString,
                                                target.DEC.FormatedString,
                                                target.AzimutPrecise.FormatedString,
                                                target.HauteurPrecise.FormatedString,
                                                target.Magnitude.ToString("0.00"),
                                                target.GrandeurWidth.FormatedString,
                                                target.GrandeurHeight.FormatedString}));

                    // Image en fonction du Statut
                    item.ImageKey = target.Rank.ToString();
                    if (!string.IsNullOrEmpty(selectedTarget) && selectedTarget == target.Nom)
                    {
                        item.Selected = true;
                        item.Focused = true;
                        itemSelected = item;
                    }
                    else
                    {
                        item.Selected = false;
                        item.Focused = false;
                    }
                }

                // AutoFit des colonnes et affichage de l'élément sélectionné
                listViewTarget.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewTarget.FullRowSelect = true;
                if (itemSelected != null)
                    itemSelected.EnsureVisible();

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
                actualisationEnCours = false;
                // Texte de la Status
                SetDefaultStatusText();
                // Positionnement du Curseur
                Cursor = Cursors.Default;
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
                    IObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                    if (target != null && !string.IsNullOrEmpty(target.Nom))
                    {
                        // Infos sur l'objet
                        textBoxInfoPanelNom.Text = target.Nom;
                        textBoxInfoPanelType.Text = target.Type;
                        textBoxInfoPanelDescription.Text = target.Description;
                        textBoxInfoPanelGrandeurL.Text = target.GrandeurWidth.FormatedString;
                        textBoxInfoPanelGrandeurH.Text = target.GrandeurHeight.FormatedString;
                        textBoxInfoPanelMagnitude.Text = target.Magnitude.ToString("0.00");
                        textBoxInfoPanelRA.Text = target.RA.FormatedString;
                        textBoxInfoPanelDEC.Text = target.DEC.FormatedString;
                        textBoxInfoPanelAzimut.Text = target.AzimutPrecise.FormatedString;
                        textBoxInfoPanelHauteur.Text = target.HauteurPrecise.FormatedString;

                        // PictureBox Rank
                        pictureBoxRank1.Visible = target.Rank == 1 || target.Rank == 0;
                        pictureBoxRank2.Visible = target.Rank == 2;
                        pictureBoxRank3.Visible = target.Rank == 3;
                        pictureBoxRank4.Visible = target.Rank == 4;
                        pictureBoxRank5.Visible = target.Rank == 5;

                        // Graphique
                        chartSliceListe.Series.Clear();

                        // On récupère les couleurs a appliquer
                        Color backColor = factory.GetAppInputs().Inputs.ModeNuit ? factory.GetAppInputs().BackColor : SystemColors.ButtonFace;
                        Color foreColor = factory.GetAppInputs().Inputs.ModeNuit ? factory.GetAppInputs().ForeColor : SystemColors.MenuText;
                        chartSliceListe.BackColor = backColor;

                        // On récupère la série de Slices en cours en fonction du mode de visualisation
                        List<IChartSlice> listeSerie = target.GetCurrentChartSlice(factory.GetAppInputs().Inputs.Visualisation);

                        // Série Slices - TempsPoseCalcule
                        chartSliceListe.Series.Add(target.Nom);
                        chartSliceListe.Series[target.Nom].ChartType = SeriesChartType.Column;
                        chartSliceListe.Series[target.Nom].XValueType = ChartValueType.DateTime;
                        chartSliceListe.Series[target.Nom].CustomProperties = "LabelStyle=Top, DrawingStyle=LightToDark";
                        chartSliceListe.Series[target.Nom].IsVisibleInLegend = false;
                        foreach (IChartSlice slice in listeSerie)
                        {
                            // On positionne une police spécifique pour les Labels des Points
                            Font font = new Font("Tahoma", 8, FontStyle.Bold);
                            // On ajoute le point à la série
                            chartSliceListe.Series[target.Nom].Points.Add(new DataPoint()
                            {
                                XValue = slice.DateHeure.ToOADate(),
                                YValues = new double[] { slice.TempsPoseCalcule },
                                Color = slice.EstExclu ? Color.DarkGray : slice.CouleurPointGraphique,
                                BorderColor = slice.EstExclu ? Color.Red : slice.CouleurPointGraphique,
                                IsValueShownAsLabel = true,
                                LabelFormat = "0s",
                                Font = font,
                                LabelForeColor = foreColor,
                                ToolTip = slice.ToolTip,
                                LabelToolTip = slice.ToolTip
                            });
                        }

                        // Série Slices - Hauteur
                        if (SerieHauteurVisible)
                        {
                            Series serieHauteur = chartSliceListe.Series.Add(target.Nom + "H");
                            serieHauteur.ChartType = SeriesChartType.Spline;
                            serieHauteur.XValueType = ChartValueType.DateTime;
                            serieHauteur.BorderWidth = 2;
                            //serieHauteur.CustomProperties = "LabelStyle=Top, DrawingStyle=LightToDark";
                            serieHauteur.IsVisibleInLegend = false;
                            foreach (IChartSlice slice in listeSerie)
                            {
                                // On positionne une police spécifique pour les Labels des Points
                                Font font = new Font("Tahoma", 8, FontStyle.Italic);
                                double valSlice = slice.Hauteur.Coordonnee > 0 ? slice.Hauteur.Coordonnee : 0;
                                string labelSlice = valSlice > 0 ? $"{Math.Floor(valSlice)}°" : "";
                                // On ajoute le point à la série
                                serieHauteur.Points.Add(new DataPoint()
                                {
                                    XValue = slice.DateHeure.ToOADate(),
                                    YValues = new double[] { valSlice },
                                    Color = slice.CouleurHauteur,
                                    IsValueShownAsLabel = true,
                                    Label = labelSlice,
                                    Font = font,
                                    LabelForeColor = string.IsNullOrEmpty(labelSlice) ? Color.Transparent : foreColor,
                                    ToolTip = slice.ToolTip,
                                    LabelToolTip = slice.ToolTip
                                });
                            }
                            serieHauteur.YAxisType = AxisType.Secondary;
                        }

                        // Série Slices - Direction
                        if (SerieHauteurVisible)
                        {
                            // Série
                            Series serieDirection = chartSliceListe.Series.Add(target.Nom + "D");
                            serieDirection.ChartType = SeriesChartType.Column;
                            serieDirection.XValueType = ChartValueType.DateTime;
                            serieDirection.BorderWidth = 0;
                            serieDirection.CustomProperties = "LabelStyle=Top";
                            serieDirection.IsVisibleInLegend = false;
                            foreach (IChartSlice slice in listeSerie)
                            {
                                // On positionne une police spécifique pour les Labels des Points
                                Font font = new Font("Tahoma", 7, FontStyle.Bold);
                                // On ajoute le point à la série
                                serieDirection.Points.Add(new DataPoint()
                                {
                                    XValue = slice.DateHeure.ToOADate(),
                                    YValues = new double[] { 0 },
                                    //Color = slice.CouleurHauteur,
                                    IsValueShownAsLabel = false,
                                    //Label = slice.DirectionCharacterCode.ToString(),
                                    Label = Coordinate.GetDirectionString(slice.Direction),
                                    //LabelFormat = "0°",
                                    MarkerBorderWidth = 0,
                                    Font = font,
                                    LabelForeColor = foreColor,
                                    ToolTip = slice.ToolTip,
                                    LabelToolTip = slice.ToolTip
                                });
                            }
                            serieDirection.YAxisType = AxisType.Primary;
                            serieDirection.ChartArea = "AreaDirection";
                        }

                        // Série Slices - Lune
                        if (SerieLuneVisible &&
                            (factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire
                            || factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Nuits
                            || factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Mensuel))
                        {
                            Series serieHauteurLune = chartSliceListe.Series.Add(target.Nom + "LuneH");
                            serieHauteurLune.ChartType = SeriesChartType.SplineArea;
                            serieHauteurLune.XValueType = ChartValueType.DateTime;
                            serieHauteurLune.BorderWidth = 4;
                            serieHauteurLune.IsVisibleInLegend = false;
                            serieHauteurLune.Color = Color.FromArgb(225, Color.Yellow);
                            serieHauteurLune.BackGradientStyle = GradientStyle.TopBottom;
                            serieHauteurLune.BackSecondaryColor = Color.Transparent;
                            foreach (IChartSlice slice in listeSerie)
                            {
                                // On positionne une police spécifique pour les Labels des Points
                                Font font = new Font("Tahoma", 8, FontStyle.Italic | FontStyle.Bold);
                                string marker = File.Exists(slice.MoonPhaseImage)
                                                && slice.MoonAlt.HasValue && slice.MoonAlt.Value > 0 ? slice.MoonPhaseImage : "";
                                // On ajoute le point à la série
                                serieHauteurLune.Points.Add(new DataPoint()
                                {
                                    XValue = slice.DateHeure.ToOADate(),
                                    YValues = new double[] { slice.MoonAlt.HasValue ? slice.MoonAlt.Value > 0 ? slice.MoonAlt.Value : 0 : 0 },
                                    Color = Color.FromArgb(225, Color.Yellow),
                                    IsValueShownAsLabel = false,
                                    MarkerImage = marker,
                                    ToolTip = slice.ToolTip,
                                    LabelToolTip = slice.ToolTip
                                });
                            }
                            serieHauteurLune.YAxisType = AxisType.Secondary;
                        }

                        // Série Slices - Soleil
                        if (SerieLuneVisible &&
                            (factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire
                            || factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Nuits))
                        {
                            Series serieHauteurSoleil = chartSliceListe.Series.Add(target.Nom + "SoleilH");
                            serieHauteurSoleil.ChartType = SeriesChartType.SplineArea;
                            serieHauteurSoleil.XValueType = ChartValueType.DateTime;
                            serieHauteurSoleil.BorderWidth = 4;
                            serieHauteurSoleil.IsVisibleInLegend = false;
                            serieHauteurSoleil.Color = Color.FromArgb(225, Color.Yellow);
                            serieHauteurSoleil.BackGradientStyle = GradientStyle.TopBottom;
                            serieHauteurSoleil.BackSecondaryColor = Color.Transparent;
                            foreach (IChartSlice slice in listeSerie)
                            {
                                // On positionne une police spécifique pour les Labels des Points
                                Font font = new Font("Tahoma", 8, FontStyle.Italic | FontStyle.Bold);
                                string marker = File.Exists(Path.Combine(factory.GetAppContext().UserAppDataPath, "sun.png"))
                                                && slice.SunAlt.HasValue && slice.SunAlt.Value > 0 ? 
                                                    Path.Combine(factory.GetAppContext().UserAppDataPath, "sun.png")
                                                    : "";
                                // On ajoute le point à la série
                                serieHauteurSoleil.Points.Add(new DataPoint()
                                {
                                    XValue = slice.DateHeure.ToOADate(),
                                    YValues = new double[] { slice.SunAlt.HasValue ? slice.SunAlt.Value > 0 ? slice.SunAlt.Value : 0 : 0 },
                                    Color = Color.FromArgb(225, Color.Yellow),
                                    IsValueShownAsLabel = false,
                                    MarkerImage = marker,
                                    ToolTip = slice.ToolTip,
                                    LabelToolTip = slice.ToolTip
                                });
                            }
                            serieHauteurSoleil.YAxisType = AxisType.Secondary;
                        }

                        // ChartArea
                        switch (factory.GetAppInputs().Inputs.Visualisation)
                        {
                            case ModeVisualisation.Annuel:
                                chartSliceListe.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
                                chartSliceListe.ChartAreas[0].AxisX.Interval = 2;
                                chartSliceListe.ChartAreas[0].AxisX.LabelStyle.Format = CultureInfo.CurrentUICulture.DateTimeFormat.YearMonthPattern;
                                break;

                            case ModeVisualisation.Mensuel:
                                chartSliceListe.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                                chartSliceListe.ChartAreas[0].AxisX.Interval = 5;
                                chartSliceListe.ChartAreas[0].AxisX.LabelStyle.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                                //chartSliceListe.ChartAreas[0].AxisX.LabelAutoFitStyle = 0;
                                break;

                            case ModeVisualisation.Nuits:
                                chartSliceListe.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                                chartSliceListe.ChartAreas[0].AxisX.Interval = 60;
                                chartSliceListe.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
                                break;

                            case ModeVisualisation.Horaire:
                            default:
                                chartSliceListe.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                                chartSliceListe.ChartAreas[0].AxisX.Interval = 30;
                                chartSliceListe.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
                                break;
                        }
                        chartSliceListe.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                        chartSliceListe.ChartAreas[0].AxisY.Title = Resources.TempsDePose;
                        chartSliceListe.ChartAreas[0].AxisX.TitleForeColor = foreColor;
                        chartSliceListe.ChartAreas[0].AxisX.LabelStyle.ForeColor = foreColor;
                        chartSliceListe.ChartAreas[0].BackColor = backColor;
                        chartSliceListe.ChartAreas[0].AxisY.TitleForeColor = foreColor;
                        chartSliceListe.ChartAreas[0].AxisY.LabelStyle.ForeColor = foreColor;
                        chartSliceListe.ChartAreas[0].RecalculateAxesScale();

                        if (SerieHauteurVisible)
                        {
                            chartSliceListe.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                            chartSliceListe.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
                            chartSliceListe.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
                            chartSliceListe.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                            chartSliceListe.ChartAreas[0].AxisY2.IsStartedFromZero = chartSliceListe.ChartAreas[0].AxisY.IsStartedFromZero;
                            chartSliceListe.ChartAreas[0].AxisY2.Title = Resources.Hauteur;
                            chartSliceListe.ChartAreas[0].AxisY2.TitleForeColor = foreColor;
                            chartSliceListe.ChartAreas[0].AxisY2.LabelStyle.ForeColor = foreColor;
                            chartSliceListe.ChartAreas[0].Position.Height = 80;
                            chartSliceListe.ChartAreas[1].Visible = true;
                            chartSliceListe.ChartAreas[1].BackColor = backColor;
                            chartSliceListe.ChartAreas[1].RecalculateAxesScale();
                        }
                        else
                        {
                            chartSliceListe.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                            chartSliceListe.ChartAreas[0].Position.Height = 90;
                            chartSliceListe.ChartAreas[1].Visible = false;
                        }

                        // Titre graphique
                        chartSliceListe.Titles.Clear();
                        chartSliceListe.Titles.Add($"{Resources.TempsDePoseSecondesSansRotationDeChamps} ({Resources.BougeMax} {factory.GetAppInputs().BougeMaxString})");
                        chartSliceListe.Titles[0].TextStyle = TextStyle.Shadow;
                        chartSliceListe.Titles[0].ShadowColor = Color.Gray;
                        chartSliceListe.Titles[0].ForeColor = foreColor;

                        // On masque les controles d'intervalles si nécessaire
                        checkBoxLune.Enabled = factory.GetAppInputs().Inputs.Visualisation != ModeVisualisation.Annuel;
                        comboBoxMinuteIntervalle.Visible = factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire;
                        labelMinuteIntervalle.Visible = factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire;
                        labelUniteMinuteIntervalle.Visible = factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire;
                        comboBoxTotalTimeSlice.Visible = factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire;
                        labelTotalTimeSlice.Visible = factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire;
                        labelUniteTotalTimeSlice.Visible = factory.GetAppInputs().Inputs.Visualisation == ModeVisualisation.Horaire;

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
            // On ne lance pas l'actualisation lors de l'initialisation du Formulaire ou pendant le déroulement de celle-ci
            if (!initialisationFormEnCours && !actualisationEnCours)
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
        /// <param name="forceNoActionStellarium">Force le flush de la zone Action sur fin de tâche Stellarium</param>
        /// <param name="forceNoActionCartesDuCiel">Force le flush de la zone Action sur fin de tâche Cartes Du Ciel</param>
        private void SetDefaultStatusText(bool forceNoActionStellarium = false, bool forceNoActionCartesDuCiel = false)
        {
            // Status Text 1 : Date et Heure de l'observation
            StatusLabelDateObs = $"{Resources.DateDeLObservation} : {factory.GetAppInputs().Inputs.DateHeureObservation.ToString("d")} - {factory.GetAppInputs().Inputs.DateHeureObservation.ToString("t")}";
            
            // Status Text 2 : Objet céleste sélectionné
            StatusLabelNomTarget = string.Empty;
            BeginInvoke(new Action(() =>
            {
                if (listViewTarget.SelectedItems != null && listViewTarget.SelectedItems.Count == 1)
                {
                    // Affichage des informations de l'objet céleste
                    IObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                    if (target != null && !string.IsNullOrEmpty(target.Nom))
                        StatusLabelNomTarget = $"{target.Nom} - {target.Description}";
                    //StatusLabelNomTarget = $"{Resources.ObjetSelectionne} : {target.Nom} - {target.Description}";
                }
            }), null);

            // Status Text 3 : Action en cours
            if (backgroundWorkerStellarium.IsBusy && !forceNoActionStellarium)
                StatusLabelAction = $"{ApplicationTools.Properties.Resources.Stellarium} : {Resources.EnvoiDeLaCommande}";
            else if (backgroundWorkerCartesDuCiel.IsBusy && !forceNoActionCartesDuCiel)
                StatusLabelAction = $"{ApplicationTools.Properties.Resources.CartesDuCiel} : {Resources.EnvoiDeLaCommande}";
            else
                StatusLabelAction = string.Empty;

            // Trace
            factory.GetLog().Log($"Status Set Default Text", GetType().Name);
        }

        /// <summary>
        /// Positionne le texte de la Status Bar pour la zone "Action en cours"
        /// </summary>
        /// <param name="action"></param>
        private void SetActionStatusText(string action)
        {
            // Trace
            factory.GetLog().Log($"Status Set Action Text : {action}", GetType().Name);

            // On positionne le texte de la Status
            StatusLabelAction = action;

            // On force l'affichage du texte avant le lancement du traitement à suivre
            Application.DoEvents();
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
                    RechargeListeFiltreType();
                    //UpdateListeAndPanel();
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
                    RechargeListeFiltreType();
                    //UpdateListeAndPanel();

                    // On actualise les paramètres d'observation
                    lblLieuObservation.Text = factory.GetAppInputs().LieuObservation;
                    toolTipInfoParametre.SetToolTip(pictureBoxIconInfoToolTip, factory.GetAppInputs().ToolTipInfosTexte);

                    // En mode nuit, on actualise l'affichage
                    if (factory.GetAppInputs().Inputs.ModeNuit)
                        SetAffichage();
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
        /// Ouvre la boîte de dialogue des Options
        /// </summary>
        public void OpenOptions()
        {
            try
            {
                // Ouverture de la boîte de dialogue Paramètres
                dlgOptions dialogOptions = new dlgOptions(factory);
                if (dialogOptions.ShowDialog() == DialogResult.OK)
                {
                    // En mode nuit, on actualise l'affichage
                    if (factory.GetAppInputs().Inputs.ModeNuit)
                        SetAffichage();
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
        /// Vérifie si une nouvelle version de l'application est disponible
        /// </summary>
        /// <param name="version"></param>
        /// <param name="nom"></param>
        /// <param name="description"></param>
        /// <param name="url"></param>
        /// <returns>True si une nouvelle version est dispo</returns>
        public bool CheckIfNouvelleVersion(ref string version, ref string nom, ref string description, ref string url)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la vérification de la présence d'une nouvelle version de l'application", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;
                SetActionStatusText($"Vérification de la présence d'une nouvelle version de l'application");

                //Sleep(1000);

                // Lancement du téléchargement
                using (WebClient request = new WebClient())
                {
                    // Trace
                    factory.GetLog().Log($"Téléchargement du fichier {newVersionFileName}", GetType().Name);

                    // Téléchargement du fichier
                    request.Credentials = new NetworkCredential(dlgUpdate.ftpCredentialLogin, dlgUpdate.ftpCredentialPwd);
                    byte[] fileData = request.DownloadData(ftpNewVersionFullPathFile);
                    using (FileStream file = File.Create(newVersionFullPathFile))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }

                    // Trace
                    factory.GetLog().Log($"Téléchargement du fichier effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

                    // On vérifie la présence du fichier téléchargé
                    if (File.Exists(newVersionFullPathFile))
                    {
                        using (var reader = new StreamReader(newVersionFullPathFile))
                        {
                            // On passe la ligne d'en-tête
                            var lineTitre = reader.ReadLine();
                            // On lit la ligne Version en cours
                            var line = reader.ReadLine();
                            var values = line.Split('\t');
                            version = values[0];
                            nom = values[1];
                            description = values[2];
                            url = values[3];
                            // On vérifie la version par rapport à la courante
                            Version nouvelleVersionDispo = new Version(version);
                            if (nouvelleVersionDispo > Assembly.GetExecutingAssembly().GetName().Version)
                            {
                                // Une version plus récente est dispo, on affiche la boîte de dialogue
                                factory.GetLog().Log($"Nouvelle version disponible : {nouvelleVersionDispo}", GetType().Name);
                                return true;
                            }
                            else
                            {
                                factory.GetLog().Log($"Pas de nouvelle version disponible : {Assembly.GetExecutingAssembly().GetName().Version} / {nouvelleVersionDispo}", GetType().Name);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Warning);
            }
            finally
            {
                // Texte de la Status
                SetDefaultStatusText();
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
            return false;
        }

        /// <summary>
        /// Lance la commande de sélection dans Stellarium pour l'objet sélectionné
        /// </summary>
        public void StellariumFocusTo()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                buttonStellarium.Enabled = false;

                // Si un objet est sélectionné dans la liste, on lance la tâche de fond
                if (listViewTarget.SelectedItems == null || listViewTarget.SelectedItems.Count == 0)
                    throw new WarningException($"Aucun objet sélectionné dans la liste");

                // On récupère l'objet sélectionné et on lance la commande pour Stellarium
                IObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                if (target == null)
                    throw new WarningException($"Objet sélectionné non trouvé dans la collection");

                // Lancement de la tâche de fond si aucune autre action en cours de traitement
                if (!backgroundWorkerStellarium.IsBusy)
                    backgroundWorkerStellarium.RunWorkerAsync(target.Nom);
                else
                    factory.GetLog().Log($"backgroundWorkerStellarium BUSY", GetType().Name, null, TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Retour au process principal après {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch(WarningException err)
            {
                // Sur WarningException, le background n'a pas été lancé, donc on remet le bouton Enable
                BeginInvoke(new Action(() => buttonStellarium.Enabled = true), null);
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
        /// Traitement asynchrone de la commande de sélection dans Stellarium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerStellarium_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Status Text
                SetActionStatusText($"{ApplicationTools.Properties.Resources.Stellarium} : {Resources.EnvoiDeLaCommande}");

                // Récup du nom de l'objet passé en paramètre du Thread
                string nomTarget = (string)e.Argument;
                if (!string.IsNullOrEmpty(nomTarget))
                {
                    // On lance la commande
                    factory.GetAppStellarium().FocusTo(nomTarget, DateTime.Now);

                    // Trace
                    factory.GetLog().Log($"Exécution de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium} pour l'objet {nomTarget} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
                else
                {
                    // Trace
                    factory.GetLog().Log($"Aucun objet sélectionné", GetType().Name, null, TypeLog.Warning);
                }

                // On flush le texte de la Status Text
                SetDefaultStatusText(true);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                // On positionne le message d'erreur retour dans la Status Text
                SetActionStatusText($"{ApplicationTools.Properties.Resources.Stellarium} : {err.Message}");
            }
            finally
            {
                // Dans tous les cas, on remet le bouton Stellarium Enable
                BeginInvoke(new Action(() => buttonStellarium.Enabled = true), null);
            }
        }

        /// <summary>
        /// Lance la commande de sélection dans Cartes du Ciel pour l'objet sélectionné
        /// </summary>
        public void CartesDuCielFocusTo()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                buttonCartesDuCiel.Enabled = false;

                // Si un objet est sélectionné dans la liste, on lance la tâche de fond
                if (listViewTarget.SelectedItems == null || listViewTarget.SelectedItems.Count == 0)
                    throw new WarningException($"Aucun objet sélectionné dans la liste");

                // On récupère l'objet sélectionné et on lance la commande pour Stellarium
                IObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                if (target == null)
                    throw new WarningException($"Objet sélectionné non trouvé dans la collection");

                // Lancement de la tâche de fond si aucune autre action en cours de traitement
                if (!backgroundWorkerCartesDuCiel.IsBusy)
                    backgroundWorkerCartesDuCiel.RunWorkerAsync(target.Nom);
                else
                    factory.GetLog().Log($"backgroundWorkerCartesDuCiel BUSY", GetType().Name, null, TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Retour au process principal après {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (WarningException err)
            {
                // Sur WarningException, le background n'a pas été lancé, donc on remet le bouton Enable
                BeginInvoke(new Action(() => buttonCartesDuCiel.Enabled = true), null);
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
        /// Traitement asynchrone de la commande de sélection dans Cartes du Ciel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerCartesDuCiel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Status Text
                SetActionStatusText($"{ApplicationTools.Properties.Resources.CartesDuCiel} : {Resources.EnvoiDeLaCommande}");

                // Récup du nom de l'objet passé en paramètre du Thread
                string nomTarget = (string)e.Argument;
                if (!string.IsNullOrEmpty(nomTarget))
                {
                    // On lance la commande
                    factory.GetAppCartesDuCiel().FocusTo(nomTarget, factory.GetAppInputs().Inputs.DateHeureObservation);

                    // Trace
                    factory.GetLog().Log($"Exécution de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel} pour l'objet {nomTarget} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
                else
                {
                    // Trace
                    factory.GetLog().Log($"Aucun objet sélectionné", GetType().Name, null, TypeLog.Warning);
                }

                // On flush le texte de la Status Text
                SetDefaultStatusText(false, true);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                // On positionne le message d'erreur retour dans la Status Text
                SetActionStatusText($"{ApplicationTools.Properties.Resources.CartesDuCiel} : {err.Message}");
            }
            finally
            {
                // Dans tous les cas, on remet le bouton Enable
                BeginInvoke(new Action(() => buttonCartesDuCiel.Enabled = true), null);
            }
        }

        /// <summary>
        /// Traitement asynchrone du serveur UDP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerTCPServer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande Start du serveur TCP", GetType().Name);

                // Démarrage du serveur TCP
                factory.GetAppTCPServer().Start(new Action<IObjTarget> ((objParam) => FocusToObject(objParam)));
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Lance la commande de sélection d'un objet céleste (Réception de commande depuis TCP)
        /// </summary>
        public void FocusToObject(IObjTarget objetCeleste)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo depuis TCP", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                if (objetCeleste == null)
                    throw new WarningException($"Objet sélectionné non trouvé dans la collection");

                BeginInvoke(new Action(() =>
                {
                    // Flush des filtres en cours
                    actualisationEnCours = true;
                    textBoxFiltreNomDescription.Text = objetCeleste.Nom;
                    comboBoxFiltreType.SelectedIndex = 0;
                    comboBoxFiltreRank.SelectedIndex = 0;
                    comboBoxFiltreMagnitude.SelectedIndex = 0;
                    comboBoxVisualisation.SelectedValue = ModeVisualisation.Annuel.ToString();
                    actualisationEnCours = false;
                    // Sélection objet et actualisation de la liste
                    forceSelectedNom = objetCeleste.Nom;
                    UpdateListeAndPanel();
                }), null);

                // Trace
                factory.GetLog().Log($"Commande FocusTo depuis TCP exécutée en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (WarningException err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
        /// Charge des libellés statiques
        /// </summary>
        private void LoadLibelles()
        {
            // Menu Fichier
            fichierToolStripMenuItem.Text = Resources.menuFichier;
            mettreÀJourLaListeDesobjetsCélestesToolStripMenuItem.Text = Resources.menuMettreAJourLeCatalogueDesObjetsCelestes;
            mettreÀJourLaListeDescapteursToolStripMenuItem.Text = Resources.menuMettreJourLeCatalogueDesCapteurs;
            quitterToolStripMenuItem.Text = Resources.menuQuitter;
            // Menu Affichage
            affichageToolStripMenuItem.Text = Resources.menuAffichage;
            modeNuitToolStripMenuItem.Text = Resources.menuModeNuit;
            // Menu Outils
            outilsToolStripMenuItem.Text = Resources.menuOutils;
            actualiserLaListeDesObjetsToolStripMenuItem.Text = Resources.menuActualiserLaListeDesObjetsCelestes;
            parametresToolStripMenuItem.Text = Resources.menuParametresDeLObservation;
            // Menu A Propos
            toolStripMenuItemAbout.Text = "&?";
            aProposToolStripMenuItem.Text = Resources.menuAPropos;

            // Panneau Inputs
            labelVisualisation.Text = Resources.Visualisation;
            labelDateHeureDeObservation.Text = Resources.DateEtHeureDeLObservation;
            labelInputsDate.Text = Resources.Date;
            labelInputsHeure.Text = Resources.Heure;
            labelParametresObservation.Text = Resources.ParametresDeLObservation;
            labelLieuObservation.Text = Resources.LieuDeLObservationLatLong;
            btModifierParametre.Text = Resources.Modifier;

            // Panneau Filtre
            groupBoxFiltres.Text = Resources.FiltrerLesResultats;
            labelRechercher.Text = Resources.Rechercher;
            labelFiltreType.Text = Resources.Type;
            labelFiltreRank.Text = Resources.RankMin;
            labelFiltreMagnitude.Text = Resources.MagnitudeMax;

            // Panneau Info
            labelObjetCeleste.Text = Resources.ObjetCeleste;
            labelInfoNom.Text = Resources.Nom;
            labelInfoType.Text = Resources.Type;
            labelInfoDescription.Text = Resources.Description;
            labelInfoRA.Text = Resources.RA;
            labelInfoDEC.Text = Resources.DEC;
            labelInfoAzimut.Text = Resources.Azimut;
            labelInfoHauteur.Text = Resources.Hauteur;
            labelInfoMagnitude.Text = Resources.Magnitude;
            labelInfoWidth.Text = Resources.GrandeurL;
            labelInfoHeight.Text = Resources.GrandeurH;
            checkBoxHauteur.Text = Resources.HauteurEtDirection;
            checkBoxLune.Text = Resources.SoleilEtLune;
            labelMinuteIntervalle.Text = Resources.DureeDUnIntervalle;
            labelUniteMinuteIntervalle.Text = Resources.Minutes;
            labelTotalTimeSlice.Text = Resources.DureeTotale;
            labelUniteTotalTimeSlice.Text = Resources.Heures;
        }

        /// <summary>
        /// Positionne l'affichage en mode Jour / Nuit
        /// </summary>
        private void SetAffichage()
        {
            bool nuit = factory.GetAppInputs().Inputs.ModeNuit;
            // Fenêtre
            BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // Menu et Status
            toolstripRendererMenu.ModeNuit = nuit;
            toolstripRendererMenu.BackColor = factory.GetAppInputs().BackColor;
            toolstripRendererMenu.BackColorLight = factory.GetAppInputs().BackColorLight;
            toolstripRendererMenu.ForeColor = factory.GetAppInputs().ForeColor;
            toolstripRendererStatus.ModeNuit = nuit;
            toolstripRendererStatus.BackColor = factory.GetAppInputs().BackColor;
            toolstripRendererStatus.BackColorLight = factory.GetAppInputs().BackColorLight;
            toolstripRendererStatus.ForeColor = factory.GetAppInputs().ForeColor;

            // Panneau saisie
            comboBoxVisualisation.DrawMode = nuit ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
            comboBoxVisualisation.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            comboBoxVisualisation.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            dateTimePickerDateObservation.CalendarMonthBackground = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            dateTimePickerDateObservation.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            dateTimePickerDateObservation.CalendarTitleBackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            dateTimePickerHeureObservation.CalendarMonthBackground = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            dateTimePickerHeureObservation.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            dateTimePickerHeureObservation.CalendarTitleBackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            buttonStartCalcul.BackColor = nuit ? factory.GetAppInputs().BackColor : default(Color);
            btModifierParametre.BackColor = nuit ? factory.GetAppInputs().BackColor : default(Color);
            if (!nuit)
            {
                buttonStartCalcul.UseVisualStyleBackColor = true;
                btModifierParametre.UseVisualStyleBackColor = true;
            }

            // ToolTips toolTipInfoParametre
            toolTipInfoParametre.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfoParametre.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfoParametre.OwnerDraw = nuit;

            // ToolTips toolTipInfoActualisation
            toolTipInfoActualisation.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfoActualisation.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfoActualisation.OwnerDraw = nuit;

            // ToolTips toolTipInfoTarget
            toolTipInfoTarget.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipInfoTarget.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipInfoTarget.OwnerDraw = nuit;

            // ToolTips toolTipStellarium
            toolTipStellarium.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipStellarium.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipStellarium.OwnerDraw = nuit;

            // ToolTips toolTipCartesDuCiel
            toolTipCartesDuCiel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipCartesDuCiel.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipCartesDuCiel.OwnerDraw = nuit;

            // ToolTips toolTipASO
            toolTipASO.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipASO.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipASO.OwnerDraw = nuit;

            // ToolTips toolTipMosaicCalculator
            toolTipMosaicCalculator.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Info;
            toolTipMosaicCalculator.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.InfoText;
            toolTipMosaicCalculator.OwnerDraw = nuit;

            // Groupe Filtre
            groupBoxFiltres.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxFiltreNomDescription.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            textBoxFiltreNomDescription.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            comboBoxFiltreType.DrawMode = nuit ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
            comboBoxFiltreType.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            comboBoxFiltreType.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            comboBoxFiltreRank.DrawMode = nuit ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
            comboBoxFiltreRank.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            comboBoxFiltreRank.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            comboBoxFiltreMagnitude.DrawMode = nuit ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
            comboBoxFiltreMagnitude.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            comboBoxFiltreMagnitude.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;

            // ListView & Graphique
            listViewTarget.BackColor = nuit ? factory.GetAppInputs().BackColorLight : SystemColors.Window;
            listViewTarget.ForeColor = nuit ? factory.GetAppInputs().ForeColorLight : SystemColors.ControlText;

            // Panneau d'info
            textBoxInfoPanelNom.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelNom.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelType.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelType.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelDescription.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelDescription.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelRA.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelRA.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelDEC.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelDEC.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelAzimut.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelAzimut.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelHauteur.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelHauteur.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelMagnitude.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelMagnitude.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelGrandeurL.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelGrandeurL.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            textBoxInfoPanelGrandeurH.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            textBoxInfoPanelGrandeurH.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            comboBoxMinuteIntervalle.DrawMode = nuit ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
            comboBoxMinuteIntervalle.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            comboBoxMinuteIntervalle.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            comboBoxTotalTimeSlice.DrawMode = nuit ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
            comboBoxTotalTimeSlice.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Window;
            comboBoxTotalTimeSlice.ForeColor = nuit ? factory.GetAppInputs().ForeColor : SystemColors.ControlText;
            UpdateViewPanelInfo();
        }

        /// <summary>
        /// Lance l'application AstroSessionOrganizer
        /// </summary>
        private void StartASO()
        {
            try
            {
                factory.GetAppAstroSessionOrganizer().Start();
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
        /// Ouvre la boîte de dialogue Mosaic Calculator
        /// </summary>
        public void OpenMosaicCalculator()
        {
            try
            {
                // Si un objet est sélectionné dans la liste, on lance la tâche de fond
                if (listViewTarget.SelectedItems == null || listViewTarget.SelectedItems.Count == 0)
                    throw new WarningException($"Aucun objet sélectionné dans la liste");

                // On récupère l'objet sélectionné et on lance la commande pour Stellarium
                IObjTarget target = factory.GetAppTarget().GetTarget(listViewTarget.SelectedItems[0].SubItems[IndexColonneNom].Text);
                if (target == null)
                    throw new WarningException($"Objet sélectionné non trouvé dans la collection");
                
                // Ouverture de la boîte de dialogue Paramètres
                dlgMosaicCalculator dialogMosaicCalculator = new dlgMosaicCalculator(factory, target);
                dialogMosaicCalculator.ShowDialog();
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
        private IAppObjFactory factory = null;

        /// <summary>
        /// Flag permettant de savoir si le formulaire est en cours d'initialisation afin de stopper la transmission des events de modification des contrôles
        /// </summary>
        private bool initialisationFormEnCours = false;

        /// <summary>
        /// Flag permettant de savoir si l'actualisation de la Liste est en cours afin de stopper la transmission des events de modification des contrôles
        /// </summary>
        private bool actualisationEnCours = false;

        /// <summary>
        /// IComparer permettant le tri de la listview
        /// </summary>
        private ListViewColumnSorter lvwColumnSorter = null;

        /// <summary>
        /// Composant de rendu jour/Nuit pour le Menu
        /// </summary>
        private ATSToolStripRenderer toolstripRendererMenu = null;

        /// <summary>
        /// Composant de rendu jour/Nuit pour la Status Bar
        /// </summary>
        private ATSToolStripRenderer toolstripRendererStatus = null;

        /// <summary>
        /// Sélection forcée d'un élément dans la liste
        /// </summary>
        private string forceSelectedNom = string.Empty;

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

            // Update du Panel Info
            UpdateViewPanelInfo();
        }

        private void MainFenetre_Shown(object sender, EventArgs e)
        {
            // Vérification de la présence d'une nouvelle version
            string version = string.Empty;
            string nom = string.Empty;
            string description = string.Empty;
            string url = string.Empty;
            if (CheckIfNouvelleVersion(ref version, ref nom, ref description, ref url) && !string.IsNullOrEmpty(description))
            {
                // On affiche la boîte de dialogue informant d'une nouvelle version disponible
                dlgNewVersion dialogNewVersion = new dlgNewVersion(factory, version, nom, description, url);
                dialogNewVersion.ShowDialog();
            }

            // Rechargement de la ListeView et de la liste des filtres sur Type
            UpdateListeAndPanel();

            // Lancement de la tâche de fond du serveur UDP
            if (!backgroundWorkerTCPServer.IsBusy)
                backgroundWorkerTCPServer.RunWorkerAsync();
        }

        #endregion

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listViewTarget_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Update du panneau d'information de l'objet sélectionné
            if (listViewTarget.SelectedItems != null && listViewTarget.SelectedItems.Count > 0
                && !actualisationEnCours)
            {
                UpdateViewPanelInfo();

                // Update du texte de la Status Bar
                SetDefaultStatusText();
            }
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
        }

        private void comboBoxMinuteIntervalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification des minutes par intervalle : {comboBoxMinuteIntervalle.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppInputs().Inputs.MinuteIntervalSlice = Convert.ToInt32(comboBoxMinuteIntervalle.Text);

            // On Force le rechargement des Slices au prochain appel
            factory.GetAppTarget().ForceUpdateSlices = true;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void comboBoxTotalTimeSlice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification de la durée d'observation : {comboBoxTotalTimeSlice.Text}", GetType().Name);

            // Actualisation de l'input
            factory.GetAppInputs().Inputs.TotalTimeSlice = Convert.ToInt32(comboBoxTotalTimeSlice.Text);

            // On Force le rechargement des Slices au prochain appel
            factory.GetAppTarget().ForceUpdateSlices = true;

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
            if (!initialisationFormEnCours)
                WindowMaximized = WindowState == FormWindowState.Maximized;
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAPropos();
        }

        private void parametresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenParametres();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenOptions();
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

        private void buttonStartCalcul_Click(object sender, EventArgs e)
        {
            // On Force le rechargement des Slices au prochain appel
            factory.GetAppTarget().ForceUpdateSlices = true;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void checkBoxHauteur_CheckedChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification de la visibilité de la série Hauteur : {checkBoxHauteur.Checked}", GetType().Name);

            // Actualisation de l'input
            SerieHauteurVisible = checkBoxHauteur.Checked;

            //// On Force le rechargement des Slices au prochain appel
            //factory.GetAppTarget().ForceUpdateSlices = true;

            // Actualisation de la liste et du panneau d'information
            //UpdateListeAndPanel();
            UpdateViewPanelInfo();
        }

        private void buttonStellarium_Click(object sender, EventArgs e)
        {
            // Lancement de la commande de sélection dans Stellarium
            StellariumFocusTo();
        }

        private void actualiserLaListeDesObjetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // On Force le rechargement des Slices au prochain appel
            factory.GetAppTarget().ForceUpdateSlices = true;

            // Actualisation de la liste et du panneau d'information
            UpdateListeAndPanel();
        }

        private void buttonCartesDuCiel_Click(object sender, EventArgs e)
        {
            // Lancement de la commande de sélection dans Cartes du Ciel
            CartesDuCielFocusTo();
        }

        private void comboBoxVisualisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update de l'état par défaut de la dateTimePickerHeureObservation
            dateTimePickerHeureObservation.Enabled = true;
            ModeVisualisation mode;
            if (Enum.TryParse(comboBoxVisualisation.SelectedValue.ToString(), out mode))
            {
                // On met à jour le Settings et l'état de la dateTimePickerHeureObservation
                factory.GetAppInputs().Inputs.Visualisation = mode;
                dateTimePickerHeureObservation.Enabled = mode == ModeVisualisation.Horaire;
            }
        }

        private void modeNuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Actualisation de l'input
            factory.GetAppInputs().Inputs.ModeNuit = modeNuitToolStripMenuItem.Checked;

            // Actualisation de l'affichage
            SetAffichage();
        }

        private void toolTipInfoParametre_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Background et Border
            e.DrawBackground();
            e.DrawBorder();
            // Icon
            Rectangle rectangleIcon = new Rectangle(4, 4, 16, 16);
            e.Graphics.DrawIcon(SystemIcons.Information, rectangleIcon);
            // Titre
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleTitre = new Rectangle(20, 0, e.Bounds.Width, 16);
                using (Font fontTitre = new Font(e.Font, FontStyle.Bold))
                {
                    e.Graphics.DrawString(toolTipInfoParametre.ToolTipTitle, fontTitre, brush, rectangleTitre);
                }
                // Text
                Rectangle rectangleText = new Rectangle(18, 14, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }

        private void comboBoxVisualisation_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            Brush brushText = new SolidBrush(factory.GetAppInputs().ForeColor);
            // Background
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(factory.GetAppInputs().ForeColor), e.Bounds);
                brushText = Brushes.Black;
            }
            else
            {
                e.DrawBackground();
            }
            e.DrawFocusRectangle();
            // Texte
            //if (e.Index != -1)
            //    e.Graphics.DrawString(comboBoxVisualisation.Items[e.Index].ToString(), e.Font, brushText, e.Bounds);
            if (combo != null && e.Index != -1)
            {
                ComboBoxItems comboItems = combo.DataSource as ComboBoxItems;
                if (comboItems != null)
                {
                    ComboBoxItem comboItem = comboItems.Rows[e.Index] as ComboBoxItem;
                    if (comboItem != null)
                        e.Graphics.DrawString(comboItem.Text, e.Font, brushText, e.Bounds);
                }
            }
        }

        private void toolTipInfoActualisation_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void toolTipInfoTarget_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void toolTipStellarium_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void toolTipCartesDuCiel_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void toolTipASO_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void comboBoxFiltreType_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brushText = new SolidBrush(factory.GetAppInputs().ForeColor);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(factory.GetAppInputs().ForeColor), e.Bounds);
                brushText = Brushes.Black;
            }
            else
            {
                e.DrawBackground();
            }
            e.DrawFocusRectangle();
            if (e.Index != -1)
                e.Graphics.DrawString(comboBoxFiltreType.Items[e.Index].ToString(), e.Font, brushText, e.Bounds);
        }

        private void comboBoxFiltreRank_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brushText = new SolidBrush(factory.GetAppInputs().ForeColor);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(factory.GetAppInputs().ForeColor), e.Bounds);
                brushText = Brushes.Black;
            }
            else
            {
                e.DrawBackground();
            }
            e.DrawFocusRectangle();
            if (e.Index != -1)
                e.Graphics.DrawString(comboBoxFiltreRank.Items[e.Index].ToString(), e.Font, brushText, e.Bounds);
        }

        private void comboBoxFiltreMagnitude_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brushText = new SolidBrush(factory.GetAppInputs().ForeColor);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(factory.GetAppInputs().ForeColor), e.Bounds);
                brushText = Brushes.Black;
            }
            else
            {
                e.DrawBackground();
            }
            e.DrawFocusRectangle();
            if (e.Index != -1)
                e.Graphics.DrawString(comboBoxFiltreMagnitude.Items[e.Index].ToString(), e.Font, brushText, e.Bounds);
        }

        private void comboBoxMinuteIntervalle_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brushText = new SolidBrush(factory.GetAppInputs().ForeColor);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(factory.GetAppInputs().ForeColor), e.Bounds);
                brushText = Brushes.Black;
            }
            else
            {
                e.DrawBackground();
            }
            e.DrawFocusRectangle();
            if (e.Index != -1)
                e.Graphics.DrawString(comboBoxMinuteIntervalle.Items[e.Index].ToString(), e.Font, brushText, e.Bounds);
        }

        private void comboBoxTotalTimeSlice_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brushText = new SolidBrush(factory.GetAppInputs().ForeColor);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(factory.GetAppInputs().ForeColor), e.Bounds);
                brushText = Brushes.Black;
            }
            else
            {
                e.DrawBackground();
            }
            e.DrawFocusRectangle();
            if (e.Index != -1)
                e.Graphics.DrawString(comboBoxTotalTimeSlice.Items[e.Index].ToString(), e.Font, brushText, e.Bounds);
        }

        private void astroSessionOrganizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartASO();
        }

        private void buttonAstroSessionOrganizer_Click(object sender, EventArgs e)
        {
            StartASO();
        }

        private void checkBoxLune_CheckedChanged(object sender, EventArgs e)
        {
            // Trace
            factory.GetLog().Log($"Modification de la visibilité de la série Lune : {checkBoxLune.Checked}", GetType().Name);

            // Actualisation de l'input
            SerieLuneVisible = checkBoxLune.Checked;

            //// On Force le rechargement des Slices au prochain appel
            //factory.GetAppTarget().ForceUpdateSlices = true;

            // Actualisation de la liste et du panneau d'information
            //UpdateListeAndPanel();
            UpdateViewPanelInfo();
        }

        private void buttonMosaicCalculator_Click(object sender, EventArgs e)
        {
            OpenMosaicCalculator();
        }

        private void toolTipMosaicCalculator_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            //e.DrawText();
            // Text
            using (Brush brush = new SolidBrush(factory.GetAppInputs().ForeColor))
            {
                Rectangle rectangleText = new Rectangle(0, 0, e.Bounds.Width + 10, e.Bounds.Height + 10);
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, rectangleText);
            }
        }
    }
}
