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
            if (log == null)
            {
                log = new AppToolLog(this);
                log.Initialise(System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.StartupPath);
            }
            return log;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Objet <see cref="AppToolLog"/>
        /// </summary>
        private AppToolLog log = null;

        #endregion
    }
}
