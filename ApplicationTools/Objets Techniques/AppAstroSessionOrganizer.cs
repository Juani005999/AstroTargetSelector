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
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "AstroSessionOrganizer";
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
                return "AstroSessionOrganizer.exe";
            }
        }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return "AstroSessionOrganizer";
            }
        }

        /// <summary>
        /// Serveur ASO
        /// <para>par défaut localhost</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Host
        {
            get
            {
                //if (string.IsNullOrEmpty(Settings.Default.HostStellarium))
                //{
                //    Settings.Default.HostStellarium = "localhost";
                //    Settings.Default.Save();
                //}
                //return Properties.Settings.Default.HostStellarium;
                return string.Empty;
            }
            set
            {
                //Settings.Default.HostStellarium = value;
                //Settings.Default.Save();
            }
        }

        /// <summary>
        /// Port du Serveur ASO
        /// <para>par défaut 7142</para>
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public override string Port
        {
            get
            {
                //if (string.IsNullOrEmpty(Settings.Default.PortStellarium))
                //{
                //    Settings.Default.PortStellarium = "8090";
                //    Settings.Default.Save();
                //}
                //return Settings.Default.PortStellarium;
                return string.Empty;
            }
            set
            {
                //Settings.Default.PortStellarium = value;
                //Settings.Default.Save();
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
                    installLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "AstrAuDobson", DisplayName);
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

        /// <summary>
        /// Méthode permettant le positionnement de la sélection dans ATS
        /// <para>Cette méthode remonte une Exception si une erreur survient lors du traitement de la commande ATS</para>
        /// </summary>
        /// <param name="nomTarget"></param>
        /// <param name="dateObservation"></param>
        /// <exception cref="Exception">Exception survenue lors du traitement</exception>
        public override void FocusTo(string nomTarget, DateTime dateObservation)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ra"></param>
        /// <param name="dec"></param>
        /// <param name="dateObservation"></param>
        /// <param name="fov"></param>
        /// <exception cref="Exception"></exception>
        public override void FocusTo(Coordinate ra, Coordinate dec, DateTime dateObservation, double fov = 1)
        {
            if (ra.Coordonnee == 0 && dec.Coordonnee == 0)
                return;

            throw new NotImplementedException();
        }

        #endregion

        #region Champs

        /// <summary>
        /// Path d'installation du Logiciel sur le poste
        /// </summary>
        private string installLocation = string.Empty;

        #endregion
    }
}
