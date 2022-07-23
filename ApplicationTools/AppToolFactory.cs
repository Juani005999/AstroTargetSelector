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
        /// Retourne l'objet <see cref="AppLog"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet Log</returns>
        public AppLog GetLog()
        {
            // Création et initialisation de l'objet de log s'il n'existe pas
            if (log == null)
            {
                log = new AppLog(this);
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

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinate"/>
        /// </summary>
        /// <returns>Objet Coordinate</returns>
        public Coordinate GetCoordinate(decimal coordonnee)
        {
            // Création d'un nouvel objet de type Coordinate
            return new Coordinate(coordonnee);
        }

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinates"/>
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public Coordinates GetCoordinates(decimal latitude, decimal longitude)
        {
            // Création d'un nouvel objet de type Coordinates
            return new Coordinates(latitude, longitude);
        }

        #endregion

        #region Champs

        /// <summary>
        /// Objet <see cref="AppLog"/>
        /// </summary>
        private AppLog log = null;

        /// <summary>
        /// Objet <see cref="AppContext"/>
        /// </summary>
        private AppContext appContext = null;

        #endregion
    }
}
