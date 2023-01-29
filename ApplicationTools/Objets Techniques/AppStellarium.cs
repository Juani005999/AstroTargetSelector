using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using ApplicationTools.Properties;

namespace ApplicationTools
{
    /// <summary>
    /// Objet de communication avec le logiciel Stellarium
    /// </summary>
    public class AppStellarium : AppProgramme
    {
        #region Constantes

        /// <summary>
        /// Temps d'attente supplémentaire (en ms) pour le démarrage du Remote Plugin plugin
        /// </summary>
        private const int StellariumSleepForRemmoteControlStart = 7000;

        /// <summary>
        /// Niveau de Fov positionné après la sélection
        /// </summary>
        private const int StellariumFovLevelAfterFocus = 1;

        #endregion

        #region Propriétés

        /// <summary>
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "Stellarium";
            }
        }

        /// <summary>
        /// TimeOut (en s) pour le démarage de l'application
        /// </summary>
        public override int StartTimeout
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// Nom de fichier du Logiciel sur le poste
        /// </summary>
        public override string FileName
        {
            get
            {
                return "stellarium.exe";
            }
        }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return "stellarium";
            }
        }

        /// <summary>
        /// Serveur Stellarium
        /// <para>par défaut localhost</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Host
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.HostStellarium))
                {
                    Settings.Default.HostStellarium = "localhost";
                    Settings.Default.Save();
                }
                return Properties.Settings.Default.HostStellarium;
            }
            set
            {
                Settings.Default.HostStellarium = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Port du Serveur Stellarium
        /// <para>par défaut 8090</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Port
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.PortStellarium))
                {
                    Settings.Default.PortStellarium = "8090";
                    Settings.Default.Save();
                }
                return Settings.Default.PortStellarium;
            }
            set
            {
                Settings.Default.PortStellarium = value;
                Settings.Default.Save();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppStellarium(IAppLog appLog)
            : base(appLog)
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode permettant le positionnement de la sélection dans Stellarium
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande Stellarium</para>
        /// </summary>
        /// <param name="nomTarget"></param>
        /// <param name="dateObservation"></param>
        /// <exception cref="Exception">Exception survenue lors du traitement</exception>
        public override void FocusTo(string nomTarget, DateTime dateObservation)
        {
            if (string.IsNullOrEmpty(nomTarget))
                return;

            // VERROU DE SURETE : pas d'action si une autre action est en cours
            // On est pas censé arriver dans ce cas car géré par le thread appelant
            if (isRunning)
                throw new Exception(Resources.UneAutreActionEstActuellementEnCours);

            // On Try/Cacth uniquement afin d'être sûr du bon repositionnement du flag d'action en cours. L'exception survenue sera throw
            try
            {
                // Positionnement du flag d'action en cours
                isRunning = true;

                // Trace et Chrono
                appLog.Log($"Lancement de la commande {DisplayName} FocusTo", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On vérifie si Stellarium est bien installé sur le poste
                if (!IsInstalled)
                    throw new Exception(Resources.LeLogicielNEstPasInstalleSurCePoste);
                // On vérifie s'il n'y a pas eu un souci à la lecture du fichier exécutable Stellarium
                if (string.IsNullOrEmpty(ExecutableFile))
                    throw new Exception(Resources.LeLogicielNAPasEteTrouve);
                appLog.Log($"{DisplayName} est bien installé sur le poste : {ExecutableFile}", GetType().Name);

                // Fonctionnalité disable sur la version 0.22.X de Stellarium
                if (string.IsNullOrEmpty(FileVersion) || FileVersion.StartsWith("0.22"))
                    throw new Exception(Resources.FonctionnaliteNonDisponiblePourLaVersion022XDeStellarium);
                appLog.Log($"{DisplayName} est installé en version : {FileVersion}", GetType().Name);

                // On Try/Catch ce traitement afin de remonter une Exception avec un mesage utilisateur formaté
                try
                {
                    // On vérifie si Stellarium est bien en cours d'exécution
                    if (!IsRunning)
                    {
                        // Trace
                        appLog.Log($"{DisplayName} n'est pas cours d'exécution. On démarre l'application {ExecutableFile}", GetType().Name);

                        // On démarre le process
                        Process processStellarium = Process.Start(ExecutableFile, "--full-screen=no");

                        // On attend que Stellarium soit démarré
                        int timeOut = 0;
                        while (string.IsNullOrEmpty(processStellarium.MainWindowTitle) && timeOut++ < StartTimeout)
                        {
                            appLog.Log($"IsStellariumRunning false", GetType().Name);
                            Thread.Sleep(1000);
                            processStellarium.Refresh();
                        }
                        appLog.Log($"IsStellariumRunning true : Timeout = {timeOut}", GetType().Name);

                        // On attend 7s de plus le temps que le Remote Control démarre
                        Thread.Sleep(StellariumSleepForRemmoteControlStart);
                        appLog.Log($"Démarrage de {DisplayName} effectué", GetType().Name);
                    }
                    else
                        appLog.Log($"{DisplayName} est déjà en cours d'exécution", GetType().Name);
                }
                catch (Exception err)
                {
                    // On trace et on remonte l'Exception formatée
                    appLog.LogException(err, GetType().Name);
                    throw new Exception(Resources.UneErreurEstSurvenueLorsDeLOuvertureDuLogiciel, err);
                }

                // On Try/Catch ce traitement afin de remonter une Exception avec un mesage utilisateur formaté
                try
                {
                    // Lancement du téléchargement
                    using (WebClient request = new WebClient())
                    {
                        // On place le focus
                        string urlFocus = $"http://{Host}:{Port}/api/main/focus";
                        string paramFocus = $"target={nomTarget}&zoom=center";
                        string responseFocus = Encoding.ASCII.GetString(request.UploadData(new Uri(urlFocus), "POST", Encoding.UTF8.GetBytes(paramFocus)));
                        appLog.Log($"Retour http place le Focus : {responseFocus}");
                        // Si le retour est différent de "true", on trace un WARNING
                        if (string.IsNullOrEmpty(responseFocus) || responseFocus != "true")
                            appLog.Log(Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommande, GetType().Name, null, TypeLog.Warning);

                        // On place le fov
                        string urlFov = $"http://{Host}:{Port}/api/main/fov";
                        string paramFov = $"fov={StellariumFovLevelAfterFocus}";
                        string responseFov = Encoding.ASCII.GetString(request.UploadData(new Uri(urlFov), "POST", Encoding.UTF8.GetBytes(paramFov)));
                        appLog.Log($"Retour http place le Fov : {responseFov}");
                        // Si le retour est différent de "ok", on trace un WARNING
                        if (string.IsNullOrEmpty(responseFov) || responseFov != "ok")
                            appLog.Log(Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommande, GetType().Name, null, TypeLog.Warning);
                    }
                }
                catch (Exception err)
                {
                    // On trace et on remonte l'Exception formatée
                    appLog.LogException(err, GetType().Name);
                    throw new Exception(Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommande, err);
                }

                // Repositionnement du flag d'action en cours
                isRunning = false;
                // On passe le programme en avant plan
                Start();

                // Trace
                appLog.Log($"Commande {DisplayName} FocusTo exécutée avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name);
            }
            catch (Exception ex)
            {
                // Repositionnement du flag d'action en cours
                isRunning = false;
                // On trace et on remonte l'Exception
                appLog.LogException(ex, GetType().Name);
                throw ex;
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Flag permettant de savoir si une action est en cours
        /// </summary>
        private bool isRunning = false;

        #endregion
    }
}
