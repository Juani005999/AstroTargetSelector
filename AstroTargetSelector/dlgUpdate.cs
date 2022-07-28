using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AstroTargetSelectorResources;
using AstroTargetSelectorBusiness;
using System.Diagnostics;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue "Téléchargement et Update d'un fichier de configuration"
    /// <para>Cette boîte de dialogue s'ouvre de 2 modes différents : <see cref="UpdateDialogMode"/></para>
    /// </summary>
    public partial class dlgUpdate : Form
    {
        #region Constantes

        /// <summary>
        /// Url complète du fichier remote de configuration des objets célestes
        /// </summary>
        private string urlFichierTarget = "C:\\Users\\miste\\Documents\\DepotGitHub\\Juani005999\\AstroTargetSelector\\TargetListe.csv";

        /// <summary>
        /// Url complète du fichier remote de configuration des capteurs
        /// </summary>
        private string urlFichierCapteur = "C:\\Users\\miste\\Documents\\DepotGitHub\\Juani005999\\AstroTargetSelector\\CapteurListe.csv";

        #endregion

        #region Enums

        /// <summary>
        /// Mode d'ouverture de la boîte de dialogue dlgUpdate
        /// </summary>
        public enum UpdateDialogMode
        {
            /// <summary>
            /// Liste des objets célestes
            /// </summary>
            Target,

            /// <summary>
            /// Liste des Capteurs
            /// </summary>
            Capteur
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public dlgUpdate(AppObjFactory factory, UpdateDialogMode dialogMode)
        {
            InitializeComponent();
            this.factory = factory;
            this.dialogMode = dialogMode;

            // TODO : Positionner en const les valeur des RemotURL
            urlFichierTarget = factory.GetAppContext().StartupPath + "\\" + factory.GetAppTarget().TargetListeFileName;
            urlFichierCapteur = factory.GetAppContext().StartupPath + "\\" + factory.GetAppCapteur().CapteurListeFileName;

            // Trace
            factory.GetLog().Log($"Ouverture de la boîte de dialogue en mode {dialogMode}", GetType().Name);
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Initialisation de la boîte de dialogue
        /// </summary>
        private void InitialisationDialog()
        {
            try
            {
                // Clear du formulaire
                ClearAll();

                // Chargement de la première étape du processus
                LoadFirstStep();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                LoadLastStepError(err.Message);
            }
        }

        #endregion

        /// <summary>
        /// Flush le formulaire
        /// </summary>
        private void ClearAll()
        {
            // Trace
            factory.GetLog().Log($"Clear du formulaire", GetType().Name);

            // Clear des contrôles
            mainLabel.Text = string.Empty;
            pictureBoxActionIconInfo.Visible = false;
            pictureBoxActionIconError.Visible = false;
            btOK.Visible = false;
            btCancel.Visible = false;
        }

        /// <summary>
        /// Charge le formulaire afin d'afficher les éléments de la première étape du processus
        /// </summary>
        private void LoadFirstStep()
        {
            // Trace
            factory.GetLog().Log($"LoadFirstStep en mode {dialogMode}", GetType().Name);

            // Texte principal
            string nomModeUpdate = dialogMode == UpdateDialogMode.Capteur ? Resources.Capteurs : Resources.ObjetsCelestes;
            mainLabel.Text = $"{Resources.VousAllerTeléchargerEtMettreAJourLeFichierDeConfigurationDes} {nomModeUpdate}.{Environment.NewLine}{Resources.CetteOperationPeutPrendreQuelquesInstants}";

            // Icones
            pictureBoxActionIconInfo.Visible = true;
            pictureBoxActionIconError.Visible = false;

            // Boutons
            btOK.Text = Resources.Suivant;
            btOK.Visible = true;
            btCancel.Text = Resources.Annuler;
            btCancel.Visible = true;
        }

        /// <summary>
        /// Charge le formulaire afin d'afficher les éléments de la dernière étape du processus sur succès du traitement
        /// </summary>
        private void LoadLastStepSuccess()
        {
            // Trace
            factory.GetLog().Log($"LoadLastStepSuccess en mode {dialogMode}", GetType().Name);

            // Texte principal
            string nomModeUpdate = dialogMode == UpdateDialogMode.Capteur ? "capteurs" : "objets célestes";
            mainLabel.Text = $"{Resources.MiseAJourDuFichierDeConfigurationDes} {nomModeUpdate} {Resources.EffectueAvecSucces}.";

            // Icones
            pictureBoxActionIconInfo.Visible = true;
            pictureBoxActionIconError.Visible = false;

            // Boutons
            btOK.Visible = false;
            btCancel.Text = Resources.Terminer;
            btCancel.Visible = true;
            btCancel.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Charge le formulaire afin d'afficher les éléments de la dernière étape du processus sur erreur du traitement
        /// </summary>
        /// <param name="message">Message d'erreur à afficher</param>
        private void LoadLastStepError(string message)
        {
            // Trace
            factory.GetLog().Log($"LoadLastStepError en mode {dialogMode}", GetType().Name);

            // Texte principal
            mainLabel.Text = $"{ApplicationTools.Properties.Resources.UneErreurEstSurvenue} :{Environment.NewLine}{message}";

            // Icones
            pictureBoxActionIconInfo.Visible = false;
            pictureBoxActionIconError.Visible = true;

            // Boutons
            btOK.Visible = false;
            btCancel.Text = Resources.Terminer;
            btCancel.Visible = true;
            btCancel.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Lance le téléchargement et l'update du fichier de configuration
        /// </summary>
        private void TelechargeEtUpdate()
        {
            try
            {
                // Positionnement du Curseur
                Cursor.Current = Cursors.WaitCursor;

                // Trace et Chrono
                factory.GetLog().Log($"Lancement du processus de mise à jour du fichier de confiruration en mode {dialogMode}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Lancement du téléchargement
                using (var client = new WebClient())
                {
                    // Fichier distant
                    string urlRemote = dialogMode == UpdateDialogMode.Capteur ? urlFichierCapteur : urlFichierTarget;

                    // Trace
                    factory.GetLog().Log($"Téléchargement du fichier {urlRemote}", GetType().Name);

                    string localDestFile = dialogMode == UpdateDialogMode.Capteur
                                                ? factory.GetAppCapteur().CapteurListeFullPathFile
                                                : factory.GetAppTarget().TargetListeFullPathFile;
                    string localTemporaryFile = factory.GetAppContext().UserAppDataPath + "\\" + "test.csv";
                    client.DownloadFile(urlRemote, localTemporaryFile);

                    // Si le fichier téléchargé n'existe pas, on throw une exception
                    if (!File.Exists(localTemporaryFile))
                        throw new Exception($"{Resources.UneErreurEstSurvenueLorsDuTelechargementFichierTelechargeNonPresent}");

                    // Trace
                    factory.GetLog().Log($"Remplacement du fichier {localDestFile} par {localTemporaryFile}", GetType().Name);

                    // On remplace le fichier de configuration par le fichier temporaire téléchargé
                    File.Copy(localTemporaryFile, localDestFile, true);

                    // On supprime le fichier temporaire
                    File.Delete(localTemporaryFile);
                }

                // Affichage de la boîte de dialogue en mode Succès
                LoadLastStepSuccess();

                // Trace
                factory.GetLog().Log($"Processus de mise à jour du fichier de confiruration effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                LoadLastStepError(err.Message);
            }
            finally
            {
                // Positionnement du Curseur
                Cursor.Current = Cursors.Default;
            }
        }

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Mode d'ouverture de la boîte de dialogue
        /// </summary>
        private readonly UpdateDialogMode dialogMode = UpdateDialogMode.Target;

        #endregion

        private void dlgUpdate_Load(object sender, EventArgs e)
        {
            InitialisationDialog();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            TelechargeEtUpdate();
        }
    }
}
