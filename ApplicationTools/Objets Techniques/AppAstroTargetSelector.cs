using ApplicationTools.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ApplicationTools
{
    /// <summary>
    /// Objet de communication avec le logiciel ATS
    /// </summary>
    public class AppAstroTargetSelector : AppProgramme
    {
        #region Constantes

        /// <summary>
        /// Temps d'attente supplémentaire (en ms) pour le démarrage du Remote Plugin plugin
        /// </summary>
        private const int ATSSleepForRemmoteControlStart = 7000;

        #endregion

        #region Propriétés

        /// <summary>
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "AstroTargetSelector";
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
                return "AstroTargetSelector.exe";
            }
        }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return "AstroTargetSelector";
            }
        }

        /// <summary>
        /// Serveur ATS
        /// <para>par défaut localhost</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Host
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.HostATS))
                {
                    Settings.Default.HostATS = "127.0.0.1";
                    Settings.Default.Save();
                }
                return Settings.Default.HostATS;
            }
            set
            {
                Settings.Default.HostATS = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Port du Serveur ATS
        /// <para>par défaut 7142</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Port
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.PortATS))
                {
                    Settings.Default.PortATS = "7142";
                    Settings.Default.Save();
                }
                return Settings.Default.PortATS;
            }
            set
            {
                Settings.Default.PortATS = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        public override string InstallLocation
        {
            get
            {
                // On récupère InstallLocation depuis la registry
                if (string.IsNullOrEmpty(installLocation))
                {
                    installLocation = RegistryUtils.GetInstallLocation("AstrAuDobson", DisplayName, appLog);
                }
                // Si InstallLocation n'est pas présent dans la Registry, on prend une valeur par défaut
                if (string.IsNullOrEmpty(installLocation))
                    installLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "AstrAuDobson", "AstroTargetSelector");
                return installLocation;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppAstroTargetSelector(IAppLog appLog)
            : base(appLog)
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode permettant le positionnement de la sélection dans ATS
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande ATS</para>
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

                // On vérifie si Cartes du Ciel est bien installé sur le poste
                if (!IsInstalled)
                    throw new Exception(Resources.LeLogicielNEstPasInstalleSurCePoste);
                // On vérifie s'il n'y a pas eu un souci à la lecture du fichier exécutable
                if (string.IsNullOrEmpty(ExecutableFile))
                    throw new Exception(Resources.LeLogicielNAPasEteTrouve);
                appLog.Log($"{DisplayName} est bien installé sur le poste : {ExecutableFile}", GetType().Name);

                // Fonctionnalité disable pour les versions < 1.4
                Version versionMinimum = new Version("1.4.0.0");
                Version versionActuelle = new Version(FileVersion);
                if (string.IsNullOrEmpty(FileVersion) || versionActuelle < versionMinimum)
                    throw new Exception($"{Resources.FonctionnaliteNonDisponible} ({Resources.VersionMinimaleRequise} : {versionMinimum} / {Resources.VersionActuelle} : {versionActuelle}).");
                appLog.Log($"{DisplayName} est installé en version : {FileVersion}", GetType().Name);
                
                // On Try/Catch ce traitement afin de remonter une Exception avec un mesage utilisateur formaté
                try
                {
                    // On vérifie si ATS est bien en cours d'exécution
                    if (!IsRunning)
                    {
                        // Trace
                        appLog.Log($"{DisplayName} n'est pas cours d'exécution. On démarre l'application {ExecutableFile}", GetType().Name);

                        // On démarre le process
                        Process processATS = Process.Start(ExecutableFile);

                        // On attend que Stellarium soit démarré
                        int timeOut = 0;
                        while (string.IsNullOrEmpty(processATS.MainWindowTitle) && timeOut++ < StartTimeout)
                        {
                            appLog.Log($"IsATSRunning false", GetType().Name);
                            Thread.Sleep(1000);
                            processATS.Refresh();
                        }
                        appLog.Log($"IsATSRunning true : Timeout = {timeOut}", GetType().Name);

                        // On attend 7s de plus le temps que le Remote Control démarre
                        //Thread.Sleep(ATSSleepForRemmoteControlStart);
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

                // Si le port vaut 0 : Soit CdC n'est pas démarré, soit le serveur n'est pas activé
                int port = 0;
                if (!int.TryParse(Port, out port))
                    throw new Exception(Resources.UneErreurEstSurvenue);

                // On Try/Catch ce traitement afin de remonter une Exception avec un mesage utilisateur formaté
                try
                {
                    using (TcpClient client = new TcpClient(Host, port))
                    {
                        // Recup Stream du Socket
                        using (NetworkStream stream = client.GetStream())
                        {
                            // Chrono spécifique pour requête
                            Stopwatch debutRequete = new Stopwatch();
                            debutRequete.Start();

                            stream.ReadTimeout = 1000;
                            // Création carte.
                            appLog.Log($"Lancement requête : {nomTarget}", GetType().Name);
                            byte[] data = Encoding.UTF8.GetBytes(nomTarget);
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new byte[2048];
                            string responseData = string.Empty;
                            int bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.UTF8.GetString(data, 0, bytes);
                            appLog.Log($"Réponse [{responseData}] en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // Fermeture Stream et Socket.
                            stream.Close();
                            client.Close();
                        }
                    }
                }
                catch (WarningException err)
                {
                    // On trace et on remonte l'Exception
                    appLog.LogException(err, GetType().Name);
                    throw err;
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

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        private string installLocation = string.Empty;

        #endregion
    }
}
