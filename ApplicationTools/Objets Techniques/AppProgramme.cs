using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ApplicationTools
{
    /// <summary>
    /// Objet représentant un logiciel
    /// </summary>
    public abstract class AppProgramme : IAppProgramme
    {
        #region Imports

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion

        #region Propriétés

        /// <summary>
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// TimeOut (en s) pour le démarage de l'application
        /// </summary>
        public abstract int StartTimeout { get; }

        /// <summary>
        /// Nom de fichier du Logiciel sur le poste
        /// </summary>
        public abstract string FileName { get; }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        public abstract string ProcessName { get; }

        /// <summary>
        /// Manufacturer
        /// </summary>
        public virtual string Manufacturer { get; set; }

        /// <summary>
        /// Serveur
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public virtual string Host { get; set; }

        /// <summary>
        /// Port du Serveur Stellarium
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public virtual string Port { get; set; }

        /// <summary>
        /// Singleton permettant de savoir si le programme est installé sur le poste
        /// </summary>
        public bool IsInstalled
        {
            get
            {
                if (!isInstalled.HasValue)
                {
                    // Trace et Chrono
                    appLog.Log($"Vérification de l'installation du logiciel {DisplayName} sur le poste", GetType().Name);
                    Stopwatch debutFonction = new Stopwatch();
                    debutFonction.Start();

                    // Lecture dans la Registry
                    isInstalled = RegistryUtils.IsProgramInstalled(DisplayName, appLog);

                    // Trace
                    appLog.Log($"La vérification de l'installation a retourné {isInstalled} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name);
                }
                return isInstalled.Value;
            }
        }

        /// <summary>
        /// Permet de savoir si le programme est en cours d'exécution sur le poste
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return Process.GetProcessesByName(ProcessName).Length > 0;
            }
        }

        /// <summary>
        /// Version du Logiciel sur le poste
        /// </summary>
        public string FileVersion
        {
            get
            {
                if (string.IsNullOrEmpty(fileVersion))
                {
                    fileVersion = RegistryUtils.GetDisplayVersion(DisplayName, appLog);
                }
                return fileVersion;
            }
        }

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        public virtual string InstallLocation
        {
            get
            {
                if (string.IsNullOrEmpty(installLocation))
                {
                    installLocation = RegistryUtils.GetInstallLocation(DisplayName, appLog);
                }
                return installLocation;
            }
        }

        /// <summary>
        /// Fichier exécutable du Logiciel sur le poste
        /// </summary>
        public string ExecutableFile
        {
            get
            {
                if (string.IsNullOrEmpty(executableFile))
                {
                    executableFile = Path.Combine(InstallLocation, FileName);
                }
                return executableFile;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual RegistryKey NodeProgramKey
        {
            get
            {
                if (nodeProgramKey == null)
                {
                    nodeProgramKey = RegistryUtils.GetNodeProgramKey(Manufacturer, DisplayName, appLog);
                }
                return nodeProgramKey;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppProgramme(IAppLog appLog)
        {
            this.appLog = appLog;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="nomTarget"></param>
        /// <param name="dateObservation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void FocusTo(string nomTarget, DateTime dateObservation)
        {
            if (string.IsNullOrEmpty(nomTarget))
                return;

            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ra"></param>
        /// <param name="dec"></param>
        /// <param name="dateObservation"></param>
        /// <param name="fov"></param>
        public virtual void FocusTo(Coordinate ra, Coordinate dec, DateTime dateObservation, double fov = 1)
        {
            if (ra.Coordonnee == 0 && dec.Coordonnee == 0)
                return;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Démarre l'application
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande</para>
        /// </summary>
        /// <exception cref="Exception">Exception survenue lors du traitement</exception>
        public virtual void Start()
        {
            if (IsInstalled && !string.IsNullOrEmpty(ExecutableFile))
            {
                if (IsRunning)
                {
                    appLog.Log($"Activation du programme en cours d'exécution : {ExecutableFile}");
                    Process processEnCours = Process.GetProcessesByName(ProcessName).FirstOrDefault();
                    if (processEnCours != null)
                        SetForegroundWindow(processEnCours.MainWindowHandle);
                }
                else
                {
                    appLog.Log($"Lancement exécution du programme : {ExecutableFile}");
                    Process.Start(ExecutableFile);
                }
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de l'Objet de log en cours
        /// </summary>
        protected readonly IAppLog appLog = null;

        /// <summary>
        /// Singleton permettant de savoir si le programme est installé sur le poste
        /// </summary>
        private bool? isInstalled = null;

        /// <summary>
        /// Version du Logiciel sur le poste
        /// </summary>
        private string fileVersion = string.Empty;

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        private string installLocation = string.Empty;

        /// <summary>
        /// Fichier exécutable du Logiciel sur le poste
        /// </summary>
        private string executableFile = string.Empty;

        /// <summary>
        /// Clé principale du programme dans la registry
        /// </summary>
        protected RegistryKey nodeProgramKey = null;

        #endregion
    }
}
