using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AstroTargetSelectorResources;
using AstroTargetSelectorBusiness;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace AstroTargetSelector
{
    /// <summary>
    /// Boîte de Dialogue "Téléchargement et Update d'un fichier de configuration"
    /// <para>Cette boîte de dialogue s'ouvre de 2 modes différents : <see cref="UpdateDialogMode"/></para>
    /// </summary>
    public partial class dlgUpdate : Form
    {
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

        #region Constantes

        /// <summary>
        /// Nom du Serveur FTP de téléchargement des fichiers de configuration
        /// </summary>
        public const string ftpHost = "ftp.byethost9.com";

        /// <summary>
        /// Identifiant de connexion au serveur FTP
        /// </summary>
        public const string ftpCredentialLogin = "b9_36081231";

        /// <summary>
        /// Mot de passe de connexion au serveur FTP
        /// </summary>
        public const string ftpCredentialPwd = "AstrAuDobTeam";

        /// <summary>
        /// Répertoire sur le Serveur FTP de téléchargement contenant les fichiers de configuration
        /// </summary>
        public const string ftpDirectory = "htdocs/AstroTargetSelector_config";

        #endregion

        #region Propriétés

        /// <summary>
        /// Url complète du fichier remote de configuration des objets célestes / Capteurs
        /// </summary>
        private string ftpFullPathFile
        {
            get
            {
                return $"ftp://{ftpHost}/{ftpDirectory}/{localFileName}";
            }
        }

        /// <summary>
        /// Nom du fichier local pour l'update
        /// </summary>
        private string localFileName
        {
            get
            {
                return dialogMode == UpdateDialogMode.Capteur
                                                ? factory.GetAppCapteur().CapteurListeFileName
                                                : factory.GetAppTarget().TargetListeFileName;
            }
        }

        /// <summary>
        /// Nom du fichier temporaire servant au téléchargement et à l'update
        /// </summary>
        private string temporaryFileName = "downloaded.csv";

        /// <summary>
        /// Path et nom du fichier temporaire de téléchargement
        /// </summary>
        private string temporaryFullPathFile
        {
            get
            {
                return $"{factory.GetAppContext().UserAppDataPath}/{temporaryFileName}";
            }
        }

        /// <summary>
        /// Path et nom du fichier de destination du téléchargement
        /// </summary>
        private string destFullPathFile
        {
            get
            {
                return dialogMode == UpdateDialogMode.Capteur
                                            ? factory.GetAppCapteur().CapteurListeFullPathFile
                                            : factory.GetAppTarget().TargetListeFullPathFile;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public dlgUpdate(IAppObjFactory factory, UpdateDialogMode dialogMode)
        {
            InitializeComponent();

            // Valorisation des membres internes
            this.factory = factory;
            this.dialogMode = dialogMode;

            // Positionne les libellés et le mode Jour/Nuit
            LoadLibelles();
            SetAffichage();

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
            pictureBoxActionIconSuccess.Visible = false;
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
            pictureBoxActionIconSuccess.Visible = false;

            // Boutons
            btOK.Text = Resources.Suivant;
            btOK.Visible = true;
            btCancel.Text = ApplicationTools.Properties.Resources.Annuler;
            btCancel.Visible = true;
            btCancel.Enabled = true;
        }

        /// <summary>
        /// Charge le formulaire afin d'afficher les éléments de la dernière étape du processus sur succès du traitement
        /// </summary>
        private void LoadLastStepSuccess()
        {
            // Trace
            factory.GetLog().Log($"LoadLastStepSuccess en mode {dialogMode}", GetType().Name);

            // Texte principal
            string nomModeUpdate = dialogMode == UpdateDialogMode.Capteur ? Resources.Capteurs : Resources.ObjetsCelestes;
            mainLabel.Text = $"{Resources.MiseAJourDuFichierDeConfigurationDes} {nomModeUpdate} {Resources.EffectueAvecSucces}.";

            // Icones
            pictureBoxActionIconInfo.Visible = false;
            pictureBoxActionIconError.Visible = false;
            pictureBoxActionIconSuccess.Visible = true;

            // Boutons
            btOK.Visible = false;
            btCancel.Text = Resources.Terminer;
            btCancel.Visible = true;
            btCancel.Enabled = true;
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
            pictureBoxActionIconSuccess.Visible = false;

            // Boutons
            btOK.Visible = false;
            btCancel.Text = Resources.Terminer;
            btCancel.Visible = true;
            btCancel.Enabled = true;
            btCancel.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Lance le téléchargement et l'update du fichier de configuration
        /// </summary>
        private async Task TelechargeEtUpdate()
        {
            try
            {
                // Positionnement du Curseur
                UseWaitCursor = true;
                //Cursor.Current = Cursors.WaitCursor;

                // Boutons
                btOK.Visible = false;
                btCancel.Enabled = false;

                // Trace et Chrono
                factory.GetLog().Log($"Lancement du processus de mise à jour du fichier de confiruration en mode {dialogMode}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Lancement du téléchargement
                using (WebClient request = new WebClient())
                {
                    // Trace
                    factory.GetLog().Log($"Téléchargement du fichier {localFileName}", GetType().Name);

                    // Téléchargement du fichier
                    request.Credentials = new NetworkCredential(ftpCredentialLogin, ftpCredentialPwd);
                    byte[] fileData = await request.DownloadDataTaskAsync(ftpFullPathFile);
                    using (FileStream file = File.Create(temporaryFullPathFile))
                    {
                        await file.WriteAsync(fileData, 0, fileData.Length);
                        file.Close();
                    }

                    // Si le fichier téléchargé n'existe pas, on throw une exception
                    if (!File.Exists(temporaryFullPathFile))
                        throw new Exception($"{Resources.UneErreurEstSurvenueLorsDuTelechargementFichierTelechargeNonPresent}");

                    // Trace
                    factory.GetLog().Log($"Remplacement du fichier {destFullPathFile} par {temporaryFullPathFile}", GetType().Name);

                    // On remplace le fichier de configuration par le fichier temporaire téléchargé
                    File.Copy(temporaryFullPathFile, destFullPathFile, true);

                    // Trace
                    factory.GetLog().Log($"Suppression du fichier temporaire {temporaryFullPathFile}", GetType().Name);

                    // On supprime le fichier temporaire
                    File.Delete(temporaryFullPathFile);
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
                //Cursor.Current = Cursors.Default;
                UseWaitCursor = false;
            }
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

            // Boutons et Contrôles
            btOK.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            btCancel.BackColor = nuit ? factory.GetAppInputs().BackColor : SystemColors.Control;
            if (!nuit)
            {
                btOK.UseVisualStyleBackColor = true;
                btCancel.UseVisualStyleBackColor = true;
            }
        }

        /// <summary>
        /// Charge des libellés statiques
        /// </summary>
        private void LoadLibelles()
        {
            // Titre
            this.Text = Resources.MiseAJourDesFichiersDeConfiguration;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Mode d'ouverture de la boîte de dialogue
        /// </summary>
        private readonly UpdateDialogMode dialogMode = UpdateDialogMode.Target;

        #endregion

        private void dlgUpdate_Load(object sender, EventArgs e)
        {
            InitialisationDialog();
        }

        private async void btOK_Click(object sender, EventArgs e)
        {
            //TelechargeEtUpdate().ConfigureAwait(false).GetAwaiter().GetResult();
            await TelechargeEtUpdate();
        }
    }
}
