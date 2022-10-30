using ApplicationTools.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        /// Port du Serveur ATS
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
