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
    public class AppToolFactory : IAppToolFactory
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
        /// Retourne l'interface <see cref="IAppLog"/> de l'objet <see cref="AppLog"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet Log</returns>
        public IAppLog GetLog()
        {
            // Création et initialisation de l'objet de log s'il n'existe pas
            if (log == null)
            {
                log = new AppLog(GetAppContext());
            }
            return log;
        }

        /// <summary>
        /// Retourne l'interface <see cref="IAppContext"/> de l'objet <see cref="AppContext"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        public IAppContext GetAppContext()
        {
            // Création et initialisation de l'objet AppContext s'il n'existe pas
            if (appContext == null)
            {
                appContext = new AppContext();
            }
            return appContext;
        }

        /// <summary>
        /// Retourne l'objet <see cref="AppStellarium"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        public IAppProgramme GetAppStellarium()
        {
            // Création et initialisation de l'objet AppStellarium s'il n'existe pas
            if (appStellarium == null)
            {
                appStellarium = new AppStellarium(GetLog());
            }
            return appStellarium;
        }

        /// <summary>
        /// Retourne l'objet <see cref="AppCartesDuCiel"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        public IAppProgramme GetAppCartesDuCiel()
        {
            // Création et initialisation de l'objet AppStellarium s'il n'existe pas
            if (appCartesDuCiel == null)
            {
                appCartesDuCiel = new AppCartesDuCiel(GetLog());
            }
            return appCartesDuCiel;
        }

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinates"/>
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public Coordinates GetCoordinates(double latitude, double longitude)
        {
            // Création d'un nouvel objet de type Coordinates
            return new Coordinates(latitude, longitude);
        }

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinate"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Coordinate GetCoordinate(double value, CoordinatesType type)
        {
            // Création d'un nouvel objet de type Coordinates
            return new Coordinate(value, type);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IConsoleQueue GetConsoleQueue()
        {
            if (consoleQueue == null)
                consoleQueue = new ConsoleQueue();
            return consoleQueue;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Interface de l'Objet <see cref="AppLog"/>
        /// </summary>
        private IAppLog log = null;

        /// <summary>
        /// Interface de l'Objet <see cref="AppContext"/>
        /// </summary>
        private IAppContext appContext = null;

        /// <summary>
        /// Objet <see cref="AppStellarium"/>
        /// </summary>
        private IAppProgramme appStellarium = null;

        /// <summary>
        /// Objet <see cref="AppCartesDuCiel"/>
        /// </summary>
        private IAppProgramme appCartesDuCiel = null;

        /// <summary>
        /// Objet permettant la gestion des messages de la Console
        /// </summary>
        private IConsoleQueue consoleQueue = null;

        #endregion
    }
}
