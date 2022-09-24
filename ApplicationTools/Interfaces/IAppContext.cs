
using System.Drawing;

namespace ApplicationTools
{
    /// <summary>
    /// Interface de l'Objet permettant d'accéder au contexte d'éxecution de l'application en cours
    /// </summary>
    public interface IAppContext
    {
        #region Propriétés

        /// <summary>
        /// ProductName de l'application en cours
        /// </summary>
        string ProductName { get; }

        /// <summary>
        /// ProductVersion de l'application en cours
        /// </summary>
        string ProductVersion { get; }

        /// <summary>
        /// ExecutablePath (Path + Nom du fichier executable) de l'application en cours
        /// </summary>
        string ExecutablePath { get; }

        /// <summary>
        /// StartupPath de l'application en cours
        /// </summary>
        string StartupPath { get; }

        /// <summary>
        /// UserAppDataPath de l'application en cours
        /// </summary>
        string UserAppDataPath { get; }

        /// <summary>
        /// Nom du fichier de log de l'application en cours
        /// </summary>
        string LogFileName { get; }

        /// <summary>
        /// Code langue d'exécution
        /// </summary>
        string CodeLangue { get; }

        /// <summary>
        /// Code pays d'exécution
        /// </summary>
        string CodePays { get; }

        /// <summary>
        /// Version du système d'exploitation (Windows)
        /// </summary>
        string OSVersion { get; }

        /// <summary>
        /// Back Color du mode Nuit
        /// </summary>
        Color BackColor { get; }

        /// <summary>
        /// Back Color Light du mode Nuit
        /// </summary>
        Color BackColorLight { get; }

        /// <summary>
        /// Fore Color du mode Nuit
        /// </summary>
        Color ForeColor { get; }

        /// <summary>
        /// Fore Color Light du mode Nuit
        /// </summary>
        Color ForeColorLight { get; }

        #endregion
    }
}
