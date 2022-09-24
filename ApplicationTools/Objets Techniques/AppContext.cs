using System;
using System.Drawing;
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
        /// <inheritdoc />
        /// </summary>
        public string ProductName
        {
            get
            {
                return Application.ProductName;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string ProductVersion
        {
            get
            {
                return Application.ProductVersion;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string ExecutablePath
        {
            get
            {
                return Application.ExecutablePath;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string StartupPath
        {
            get
            {
                return Application.StartupPath;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string UserAppDataPath
        {
            get
            {
                return Application.UserAppDataPath;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string LogFileName
        {
            get
            {
                return "Log_" + ProductName.ToLower().Replace(" ", "") + ".csv";
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string CodeLangue
        {
            get
            {
                return CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string CodePays
        {
            get
            {
                return RegionInfo.CurrentRegion.TwoLetterISORegionName.ToUpper();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string OSVersion
        {
            get
            {
                return Environment.OSVersion.VersionString;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color BackColor
        {
            get
            {
                //return SystemColors.ControlDarkDark;
                return Color.FromArgb(30, 30, 30);
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color BackColorLight
        {
            get
            {
                return Color.FromArgb(70, 70, 70);
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return Color.OrangeRed;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color ForeColorLight
        {
            get
            {
                return Color.LightYellow;
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
