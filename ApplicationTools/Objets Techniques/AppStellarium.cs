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
    public class AppStellarium
    {
        #region Constantes

        /// <summary>
        /// DisplayName de Stellarium dans la Registry
        /// </summary>
        private const string StellariumDisplayName = "Stellarium";

        /// <summary>
        /// TimeOut (en s) pour le démarage de l'application Stellarium
        /// </summary>
        private const int StellariumStartTimeout = 20;

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
        /// Singleton permettant de savoir si le programme Stellarium est installé sur le poste
        /// </summary>
        public bool IsStellariumInstalled
        {
            get
            {
                if (!isStellariumInstalled.HasValue)
                {
                    // Trace et Chrono
                    factory.GetLog().Log($"Vérification de l'installation du logiciel Stellarium sur le poste", GetType().Name);
                    Stopwatch debutFonction = new Stopwatch();
                    debutFonction.Start();

                    // Lecture dans la Registry
                    isStellariumInstalled = factory.IsProgramInstalled(StellariumDisplayName, out stellariumFileName);

                    // Trace
                    factory.GetLog().Log($"La vérification de l'installation a retourné {isStellariumInstalled} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name);
                }
                return isStellariumInstalled.Value;
            }
        }

        /// <summary>
        /// Permet de savoir si le programme Stellarium est en cours d'exécution sur le poste
        /// </summary>
        public bool IsStellariumRunning
        {
            get
            {
                return Process.GetProcessesByName(StellariumDisplayName).Length > 0;
            }
        }

        /// <summary>
        /// Serveur Stellarium
        /// <para>par défaut localhost</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string Host
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
        public string Port
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
        internal AppStellarium(AppToolFactory factory)
        {
            this.factory = factory;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode permettant le positionnement de la sélection dans Stellarium
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande Stellarium</para>
        /// </summary>
        /// <param name="nomTarget"></param>
        /// <exception cref="Exception">Exception survenue lors du traitement</exception>
        public void FocusTo(string nomTarget)
        {
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
                factory.GetLog().Log($"Lancement de la commande Stellarium FocusTo", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On vérifie si Stellarium est bien installé sur le poste
                if (!IsStellariumInstalled)
                    throw new Exception(Resources.StellariumNEstPasInstalleSurCePoste);
                // On vérifie s'il n'y a pas eu un souci à la lecture du fichier exécutable Stellarium
                if (string.IsNullOrEmpty(stellariumFileName))
                    throw new Exception(Resources.LeLogicielStellariumNAPasEteTrouve);
                factory.GetLog().Log($"Stellarium est bien installé sur le poste : {stellariumFileName}", GetType().Name);

                // On Try/Catch ce traitement afin de remonter une Exception avec un mesage utilisateur formaté
                try
                {
                    // On vérifie si Stellarium est bien en cours d'exécution
                    if (!IsStellariumRunning)
                    {
                        // Trace
                        factory.GetLog().Log($"Stellarium n'est pas cours d'exécution. On démarre l'application {stellariumFileName}", GetType().Name);

                        // On démarre le process
                        Process processStellarium = Process.Start(stellariumFileName);

                        // On attend que Stellarium soit démarré
                        int timeOut = 0;
                        while (string.IsNullOrEmpty(processStellarium.MainWindowTitle) && timeOut++ < StellariumStartTimeout)
                        {
                            factory.GetLog().Log($"IsStellariumRunning false", GetType().Name);
                            Thread.Sleep(1000);
                            processStellarium.Refresh();
                        }
                        factory.GetLog().Log($"IsStellariumRunning true : Timeout = {timeOut}", GetType().Name);

                        // On attend 7s de plus le temps que le Remote Control démarre
                        Thread.Sleep(StellariumSleepForRemmoteControlStart);
                        factory.GetLog().Log($"Démarrage de Stellarium effectué", GetType().Name);
                    }
                    else
                        factory.GetLog().Log($"Stellarium est déjà en cours d'exécution", GetType().Name);
                }
                catch (Exception err)
                {
                    // On trace et on remonte l'Exception formatée
                    factory.GetLog().LogException(err, GetType().Name);
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
                        factory.GetLog().Log($"Retour http place le Focus : {responseFocus}");
                        //if (string.IsNullOrEmpty(responseFocus) || responseFocus != "true")
                        //    // On remonte l'Exception formatée
                        //    throw new Exception(Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommandeAuPluginDeCommandeADistance);

                        // On place le fov
                        string urlFov = $"http://{Host}:{Port}/api/main/fov";
                        string paramFov = $"fov={StellariumFovLevelAfterFocus}";
                        string responseFov = Encoding.ASCII.GetString(request.UploadData(new Uri(urlFov), "POST", Encoding.UTF8.GetBytes(paramFov)));
                        factory.GetLog().Log($"Retour http place le Fov : {responseFov}");
                        //if (string.IsNullOrEmpty(responseFov) || responseFov != "ok")
                        //    // On remonte l'Exception formatée
                        //    throw new Exception(Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommandeAuPluginDeCommandeADistance);
                    }
                }
                catch (Exception err)
                {
                    // On trace et on remonte l'Exception formatée
                    factory.GetLog().LogException(err, GetType().Name);
                    throw new Exception(Resources.UneErreurEstSurvenueLorsDeLEnvoiDeLaCommandeAuPluginDeCommandeADistance, err);
                }

                // Repositionnement du flag d'action en cours
                isRunning = false;

                // Trace
                factory.GetLog().Log($"Commande Stellarium FocusTo exécutée avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name);
            }
            catch (Exception ex)
            {
                // Repositionnement du flag d'action en cours
                isRunning = false;
                // On trace et on remonte l'Exception
                factory.GetLog().LogException(ex, GetType().Name);
                throw ex;
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la ToolFactory en cours
        /// </summary>
        private readonly AppToolFactory factory = null;

        /// <summary>
        /// Singleton permettant de savoir si le programme Stellarium est installé sur le poste
        /// </summary>
        private bool? isStellariumInstalled = null;

        /// <summary>
        /// Nom de fichier de Stellarium sur le poste
        /// </summary>
        private string stellariumFileName = string.Empty;

        /// <summary>
        /// Flag permettant de savoir si une action est en cours
        /// </summary>
        private bool isRunning = false;

        #endregion
    }
}
