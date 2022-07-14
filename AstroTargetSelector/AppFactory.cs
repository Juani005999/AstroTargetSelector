using System.Runtime.CompilerServices;
using ApplicationTools;
using AstroTargetSelectorBusiness;

namespace AstroTargetSelector
{
    /// <summary>
    /// Fabrique d'objets globale de l'application
    /// </summary>
    internal class AppFactory : AppObjFactory
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AppFactory()
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
        public override void Log(string message,
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

        #endregion

        #region Champs
        #endregion
    }
}
