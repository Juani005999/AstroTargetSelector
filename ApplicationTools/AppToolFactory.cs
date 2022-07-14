using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApplicationTools
{
    /// <summary>
    /// Fabrique d'objets
    /// </summary>
    public class AppToolFactory
    {
        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AppToolFactory()
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Appel à la méthode de log (<see cref="AppToolLog"/>) pour simplication d'écriture des traces
        /// </summary>
        /// <param name="message">Message à ajouter à la trace</param>
        /// <param name="className">Nom de la classe appelante</param>
        /// <param name="millisecond">Durée de la trace</param>
        /// <param name="typeLog">Type de trace <see cref="AppToolLog.TypeLog"/></param>
        /// <param name="callerMemberName">Nom de la fonction appelante</param>
        /// <param name="callerFilePath">Fichier contenant la fonction appelante</param>
        /// <param name="callerLineNumber">Numéro de ligne de l'appelant</param>
        public virtual void Log(string message,
                                string className,
                                double? millisecond = null,
                                AppToolLog.TypeLog typeLog = AppToolLog.TypeLog.Infos,
                                [CallerMemberName] string callerMemberName = "",
                                [CallerFilePath] string callerFilePath = "",
                                [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog().Log(typeLog,
                message,
                millisecond,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                className,
                callerMemberName,
                callerFilePath,
                callerLineNumber);
        }

        /// <summary>
        /// Retourne l'objet <see cref="AppToolLog"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet Log</returns>
        public AppToolLog GetLog()
        {
            // Création et initialisation de l'objet de log s'il n'existe pas
            if (log == null)
            {
                log = new AppToolLog(this);
            }
            return log;
        }

        /// <summary>
        /// Retourne l'objet <see cref="AppContext"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        public AppContext GetAppContext()
        {
            // Création et initialisation de l'objet AppContext s'il n'existe pas
            if (appContext == null)
            {
                appContext = new AppContext(this);
            }
            return appContext;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Objet <see cref="AppToolLog"/>
        /// </summary>
        private AppToolLog log = null;

        /// <summary>
        /// Objet <see cref="AppContext"/>
        /// </summary>
        private AppContext appContext = null;

        #endregion
    }
}
