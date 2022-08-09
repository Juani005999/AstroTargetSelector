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
        /// Retourne l'objet <see cref="AppStellarium"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        public AppStellarium GetAppStellarium()
        {
            // Création et initialisation de l'objet AppStellarium s'il n'existe pas
            if (appStellarium == null)
            {
                appStellarium = new AppStellarium(this);
            }
            return appStellarium;
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

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinate"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Coordinate GetCoordinate(decimal value, CoordinatesType type)
        {
            // Création d'un nouvel objet de type Coordinates
            return new Coordinate(value, type);
        }

        /// <summary>
        /// Permet de savoir si un programme est installé sur le poste local
        /// </summary>
        /// <param name="programDisplayName">Champ DisplayName dans la registry</param>
        /// <param name="stellariumFileName"></param>
        /// <returns></returns>
        public bool IsProgramInstalled(string programDisplayName, out string stellariumFileName)
        {
            // Création d'un nouvel objet de type Coordinates
            return RegistryUtils.IsProgramInstalled(programDisplayName, out stellariumFileName, this);
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

        /// <summary>
        /// Objet <see cref="AppStellarium"/>
        /// </summary>
        private AppStellarium appStellarium = null;

        #endregion
    }
}
