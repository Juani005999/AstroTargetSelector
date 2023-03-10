using System;
using System.IO;

namespace ApplicationTools
{
    /// <summary>
    /// Objet de communication avec le logiciel ASO
    /// </summary>
    public class AppAstroSessionOrganizer : AppProgramme
    {
        #region Constantes

        /// <summary>
        /// Temps d'attente supplémentaire (en ms) pour le démarrage du Remote Plugin plugin
        /// </summary>
        private const int ASOSleepForRemmoteControlStart = 7000;

        #endregion

        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "AstroSessionOrganizer";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Manufacturer
        {
            get
            {
                return "AstrAuDobson";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int StartTimeout
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string FileName
        {
            get
            {
                return "AstroSessionOrganizer.exe";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return "AstroSessionOrganizer";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string InstallLocation
        {
            get
            {
                // On récupère InstallLocation depuis la registry
                if (string.IsNullOrEmpty(installLocation))
                {
                    installLocation = RegistryUtils.GetInstallLocation(Manufacturer, DisplayName, appLog);
                }
                // Si InstallLocation n'est pas présent dans la Registry, on prend une valeur par défaut
                if (string.IsNullOrEmpty(installLocation))
                    installLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), Manufacturer, DisplayName);
                return installLocation;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppAstroSessionOrganizer(IAppLog appLog)
            : base(appLog)
        {
        }

        #endregion

        #region Méthodes
        #endregion

        #region Champs

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        private string installLocation = string.Empty;

        #endregion
    }
}
