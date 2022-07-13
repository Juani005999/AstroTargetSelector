using System;
using System.Collections.Generic;
using System.Linq;
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
