using System;
using System.IO;

namespace ApplicationTools
{
    /// <summary>
    /// Objet de communication avec le logiciel ASTAP (Astrometry)
    /// </summary>
    public class AppAstap : AppProgramme
    {
        #region Constantes

        /// <summary>
        /// Temps d'attente supplémentaire (en ms) pour le démarrage du Remote Plugin plugin
        /// </summary>
        private const int AstapSleepForRemmoteControlStart = 7000;

        #endregion

        #region Propriétés

        /// <summary>
        /// DisplayName du Logiciel dans la Registry
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "ASTAP";
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
                return "astap.exe";
            }
        }

        /// <summary>
        /// Nom du Processus d'exécution
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return "astap";
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
                return string.Empty;
            }
            set
            {
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
                return string.Empty;
            }
            set
            {
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppAstap(IAppLog appLog)
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
        #endregion
    }
}
