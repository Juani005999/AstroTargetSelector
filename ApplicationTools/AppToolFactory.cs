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
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
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
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
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
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
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
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
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
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IAppProgramme GetAppAstroTargetSelector()
        {
            // Création et initialisation de l'objet AppAstroTargetSelector s'il n'existe pas
            if (appATS == null)
            {
                appATS = new AppAstroTargetSelector(GetLog());
            }
            return appATS;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IAppProgramme GetAppAstroSessionOrganizer()
        {
            // Création et initialisation de l'objet AppAstroSessionOrganizer s'il n'existe pas
            if (appASO == null)
            {
                appASO = new AppAstroSessionOrganizer(GetLog());
            }
            return appASO;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IAppProgramme GetAppAstap()
        {
            // Création et initialisation de l'objet AppAstap s'il n'existe pas
            if (appAstap == null)
            {
                appAstap = new AppAstap(GetLog());
            }
            return appAstap;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IAppProgramme GetAppASCOMPlateform()
        {
            // Création et initialisation de l'objet AppAstap s'il n'existe pas
            if (appASCOMPlateform == null)
            {
                appASCOMPlateform = new AppASCOMPlateform(GetLog());
            }
            return appASCOMPlateform;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public Coordinates GetCoordinates(double latitude, double longitude)
        {
            // Création d'un nouvel objet de type Coordinates
            return new Coordinates(latitude, longitude);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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
        /// Objet <see cref="AppAstroTargetSelector"/>
        /// </summary>
        private IAppProgramme appATS = null;

        /// <summary>
        /// Objet <see cref="AppAstroSessionOrganizer"/>
        /// </summary>
        private IAppProgramme appASO = null;

        /// <summary>
        /// Objet <see cref="AppAstap"/>
        /// </summary>
        private IAppProgramme appAstap = null;

        /// <summary>
        /// Objet <see cref="AppASCOMPlateform"/>
        /// </summary>
        private IAppProgramme appASCOMPlateform = null;

        /// <summary>
        /// Objet permettant la gestion des messages de la Console
        /// </summary>
        private IConsoleQueue consoleQueue = null;

        #endregion
    }
}
