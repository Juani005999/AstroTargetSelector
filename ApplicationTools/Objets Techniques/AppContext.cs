using System.Globalization;
using System.Windows.Forms;

namespace ApplicationTools
{
    /// <summary>
    /// Objet permettant d'accéder au contexte d'éxecution de l'application en cours
    /// </summary>
    public class AppContext : IAppContext
    {
        #region Propriétés

        /// <summary>
        /// ProductName de l'application en cours
        /// </summary>
        public string ProductName
        {
            get
            {
                return Application.ProductName;
            }
        }

        /// <summary>
        /// ProductVersion de l'application en cours
        /// </summary>
        public string ProductVersion
        {
            get
            {
                return Application.ProductVersion;
            }
        }

        /// <summary>
        /// ExecutablePath (Path + Nom du fichier executable) de l'application en cours
        /// </summary>
        public string ExecutablePath
        {
            get
            {
                return Application.ExecutablePath;
            }
        }

        /// <summary>
        /// StartupPath de l'application en cours
        /// </summary>
        public string StartupPath
        {
            get
            {
                return Application.StartupPath;
            }
        }

        /// <summary>
        /// UserAppDataPath de l'application en cours
        /// </summary>
        public string UserAppDataPath
        {
            get
            {
                return Application.UserAppDataPath;
            }
        }

        /// <summary>
        /// Nom du fichier de log de l'application en cours
        /// </summary>
        public string LogFileName
        {
            get
            {
                return "Log_" + ProductName.ToLower().Replace(" ", "") + ".csv";
            }
        }

        /// <summary>
        /// Code langue d'exécution
        /// </summary>
        public string CodeLangue
        {
            get
            {
                return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            }
        }

        /// <summary>
        /// Code pays d'exécution
        /// </summary>
        public string CodePays
        {
            get
            {
                return RegionInfo.CurrentRegion.TwoLetterISORegionName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppContext()
        {
        }

        #endregion

        #region Champs
        #endregion
    }
}
