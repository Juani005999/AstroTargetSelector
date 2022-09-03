using System;
using System.Runtime.CompilerServices;

namespace ApplicationTools
{
    #region Enum

    /// <summary>
    /// Type de trace
    /// </summary>
    public enum TypeLog
    {
        /// <summary>
        /// Trace de type Infos
        /// </summary>
        Infos,

        /// <summary>
        /// Trace de type Warning
        /// </summary>
        Warning,

        /// <summary>
        /// Trace de type Error
        /// </summary>
        Error,

        /// <summary>
        /// Trace de type Fatal
        /// </summary>
        Fatal
    }

    #endregion

    /// <summary>
    /// Interface de l'Objet Log
    /// </summary>
    public interface IAppLog
    {
        #region Propriétés

        /// <summary>
        /// Path complet et Nom du fichier de log
        /// </summary>
        string FullPathName { get; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Trace en console et dans le fichier de log
        /// </summary>
        /// <param name="message">Trace</param>
        /// <param name="callerClassName">Classe appelante</param>
        /// <param name="millisecond">Durée en ms</param>
        /// <param name="typeLog">Type de trace <see cref="TypeLog"/></param>
        /// <param name="callerModuleName">Module appelant : par défaut System.Reflection.Assembly.GetCallingAssembly().GetName().Name</param>
        /// <param name="callerMemberName">Fonction appelante</param>
        /// <param name="callerFilePath">Fichier contenant la fonction appelante</param>
        /// <param name="callerLineNumber">Ligne dans le fichier de l'appelant</param>
        void Log(string message,
                        string callerClassName = "",
                        double? millisecond = null,
                        TypeLog typeLog = TypeLog.Infos,
                        string callerModuleName = "",
                        [CallerMemberName] string callerMemberName = "",
                        [CallerFilePath] string callerFilePath = "",
                        [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        /// Trace une Exception en console et dans le fichier de log
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="callerClassName">Classe appelante</param>
        /// <param name="typeLog">Type de trace <see cref="TypeLog"/></param>
        /// <param name="callerMemberName">Fonction appelante</param>
        /// <param name="callerFilePath">Fichier contenant la fonction appelante</param>
        /// <param name="callerLineNumber">Ligne dans le fichier de l'appelant</param>
        void LogException(Exception ex,
                        string callerClassName = "",
                        TypeLog typeLog = TypeLog.Fatal,
                        [CallerMemberName] string callerMemberName = "",
                        [CallerFilePath] string callerFilePath = "",
                        [CallerLineNumber] int callerLineNumber = 0);

        #endregion
    }
}
