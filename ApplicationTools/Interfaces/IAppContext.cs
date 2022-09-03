
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

        #endregion
    }
}
