using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ApplicationTools.Properties;

namespace ApplicationTools
{
    /// <summary>
    /// Objet de communication avec le logiciel Cartes du Ciel
    /// </summary>
    public class AppCartesDuCiel : AppProgramme
    {
        #region Constantes

        /// <summary>
        /// Chemin de la clé Cartes du Ciel pour Current User dans la Registry
        /// </summary>
        private const string HKCUPath = @"Software\Astro_PC\Ciel\Status";

        /// <summary>
        /// Clé Cartes du Ciel pour Current User dans la Registry
        /// </summary>
        private const string HKCUKey = @"TcpPort";

        /// <summary>
        /// Niveau de Fov positionné après la sélection
        /// </summary>
        private const int CartesDuCielFovLevelAfterFocus = 3;

        #endregion
        
        #region Propriétés

        /// <summary>
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "Cartes du Ciel";
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
                return "skychart.exe";
            }
        }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return "skychart";
            }
        }

        /// <summary>
        /// Serveur Cartes du Ciel
        /// <para>par défaut 127.0.0.1</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Host
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.HostCartesDuCiel))
                {
                    Settings.Default.HostCartesDuCiel = "127.0.0.1";
                    Settings.Default.Save();
                }
                return Properties.Settings.Default.HostCartesDuCiel;
            }
            set
            {
                Settings.Default.HostCartesDuCiel = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Port du Serveur
        /// </summary>
        public override string Port { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppCartesDuCiel(IAppLog appLog)
            : base(appLog)
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode permettant le positionnement de la sélection dans Cartes du Ciel
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande Cartes du Ciel</para>
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
                // On vérifie s'il n'y a pas eu un souci à la lecture du fichier exécutable Stellarium
                if (string.IsNullOrEmpty(ExecutableFile))
                    throw new Exception(Resources.LeLogicielNAPasEteTrouve);
                appLog.Log($"{DisplayName} est bien installé sur le poste : {ExecutableFile}", GetType().Name);

                // On Try/Catch ce traitement afin de remonter une Exception avec un mesage utilisateur formaté
                try
                {
                    // On vérifie si Stellarium est bien en cours d'exécution
                    if (!IsRunning)
                    {
                        // Trace
                        appLog.Log($"{DisplayName} n'est pas cours d'exécution. On démarre l'application {ExecutableFile}", GetType().Name);

                        // On démarre le process
                        Process processCartesDuCiel = Process.Start(ExecutableFile);

                        // On attend que Stellarium soit démarré
                        int timeOut = 0;
                        while (string.IsNullOrEmpty(processCartesDuCiel.MainWindowTitle) && timeOut++ < StartTimeout)
                        {
                            appLog.Log($"IsCartesDuCielRunning false", GetType().Name);
                            Thread.Sleep(1000);
                            processCartesDuCiel.Refresh();
                        }
                        appLog.Log($"IsCartesDuCielRunning true : Timeout = {timeOut}", GetType().Name);
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

                // On récupère la valeur du port dans la Registry
                string portCdC = string.Empty;
                try
                {
                    portCdC = RegistryUtils.GetCurrentUserValue(HKCUPath, HKCUKey, appLog);
                }
                catch (Exception err)
                {
                    // On trace et on remonte l'Exception formatée
                    appLog.LogException(err, GetType().Name);
                    throw new Exception(Resources.VeuillezDemarrerLeLogicielAuMoinsUneFoisAfinDeTerminerSaConfiguration, err);
                }
                appLog.Log($"La valeur du port pour Cartes du Ciel est : {portCdC}");

                // Si le port vaut 0 : Soit CdC n'est pas démarré, soit le serveur n'est pas activé
                int port = 0;
                if (string.IsNullOrEmpty(portCdC) || !int.TryParse(portCdC, out port) || port == 0)
                    throw new Exception(Resources.LeServeurNEstPasActive);

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
                            appLog.Log($"Lancement requête : newchart {nomTarget.Replace(" ", "")}", GetType().Name);
                            Byte[] data = Encoding.ASCII.GetBytes($"newchart {nomTarget.Replace(" ", "")}\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            string responseData = String.Empty;
                            int bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            if (string.IsNullOrEmpty(responseData) || !responseData.Contains("OK!"))
                                throw new WarningException(Resources.ImpossibleDeCreerDeNouvelleCarteNombreMaxAtteint);
                            appLog.Log($"Réponse newchart : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // Sélection carte.
                            debutRequete.Restart();
                            stream.Flush();
                            //data = Encoding.ASCII.GetBytes($"selectchart Marcel\r\n");
                            appLog.Log($"Lancement requête : selectchart {nomTarget.Replace(" ", "")}", GetType().Name);
                            data = Encoding.ASCII.GetBytes($"selectchart {nomTarget.Replace(" ", "")}\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            responseData = String.Empty;
                            bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            if (string.IsNullOrEmpty(responseData) || !responseData.Contains("OK!"))
                                appLog.Log($"Une erreur est survenue lors de la sélection de la nouvelle carte.", GetType().Name, null, TypeLog.Warning);
                            appLog.Log($"Réponse selectchart : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // Positionnement Date Observation.
                            debutRequete.Restart();
                            stream.Flush();
                            string dateObs = dateObservation.ToString("yyyy-MM-ddTHH:mm:ss");
                            appLog.Log($"Lancement requête : setdate {dateObs}", GetType().Name);
                            data = Encoding.ASCII.GetBytes($"setdate {dateObs}\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            responseData = String.Empty;
                            bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            appLog.Log($"Réponse setdate : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // Redraw.
                            debutRequete.Restart();
                            stream.Flush();
                            appLog.Log($"Lancement requête : redraw", GetType().Name);
                            data = Encoding.ASCII.GetBytes($"redraw\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            responseData = String.Empty;
                            bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            if (string.IsNullOrEmpty(responseData) || !responseData.Contains("OK!"))
                                appLog.Log($"Une erreur est survenue lors de la sélection de la nouvelle carte.", GetType().Name, null, TypeLog.Warning);
                            appLog.Log($"Réponse redraw : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // Recherche objet.
                            debutRequete.Restart();
                            stream.Flush();
                            appLog.Log($"Lancement requête : search {nomTarget.Replace(" ", "")}", GetType().Name);
                            data = Encoding.ASCII.GetBytes($"search {nomTarget.Replace(" ", "")}\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            responseData = String.Empty;
                            bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            if (string.IsNullOrEmpty(responseData) || !responseData.Contains("OK!"))
                                throw new WarningException(Resources.ObjetCelesteNonTrouve);
                            appLog.Log($"Réponse search : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // FOV.
                            debutRequete.Restart();
                            stream.Flush();
                            appLog.Log($"Lancement requête : setfov {CartesDuCielFovLevelAfterFocus}", GetType().Name);
                            data = Encoding.ASCII.GetBytes($"setfov 3\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            responseData = String.Empty;
                            bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            appLog.Log($"Réponse setfov : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

                            // Redraw.
                            debutRequete.Restart();
                            stream.Flush();
                            appLog.Log($"Lancement requête : redraw", GetType().Name);
                            data = Encoding.ASCII.GetBytes($"redraw\r\n");
                            stream.Write(data, 0, data.Length);
                            // Lecture réponse
                            data = new Byte[2048];
                            responseData = String.Empty;
                            bytes = stream.Read(data, 0, data.Length);
                            responseData = Encoding.ASCII.GetString(data, 0, bytes);
                            appLog.Log($"Réponse redraw : {responseData.Replace("\r", "").Replace("\n", "")} en {debutRequete.ElapsedMilliseconds} ms", GetType().Name);

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
