using System.Diagnostics;

namespace ApplicationTools
{
    /// <summary>
    /// Objet représentant un logiciel
    /// </summary>
    public abstract class AppProgramme
    {
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
        /// Singleton permettant de savoir si le programme est installé sur le poste
        /// </summary>
        public bool IsInstalled
        {
            get
            {
                if (!isInstalled.HasValue)
                {
                    // Trace et Chrono
                    factory.GetLog().Log($"Vérification de l'installation du logiciel {DisplayName} sur le poste", GetType().Name);
                    Stopwatch debutFonction = new Stopwatch();
                    debutFonction.Start();

                    // Lecture dans la Registry
                    isInstalled = RegistryUtils.IsProgramInstalled(DisplayName, factory);

                    // Trace
                    factory.GetLog().Log($"La vérification de l'installation a retourné {isInstalled} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name);
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
                    fileVersion = RegistryUtils.GetDisplayVersion(DisplayName, factory);
                }
                return fileVersion;
            }
        }

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        public string InstallLocation
        {
            get
            {
                if (string.IsNullOrEmpty(installLocation))
                {
                    installLocation = RegistryUtils.GetInstallLocation(DisplayName, factory);
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
                    executableFile = InstallLocation + "\\" + FileName;
                }
                return executableFile;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppProgramme(AppToolFactory factory)
        {
            this.factory = factory;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la ToolFactory en cours
        /// </summary>
        private readonly AppToolFactory factory = null;

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

        #endregion
    }
}
