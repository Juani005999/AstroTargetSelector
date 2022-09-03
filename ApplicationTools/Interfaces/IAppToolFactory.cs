namespace ApplicationTools
{
    /// <summary>
    /// Interface de la Fabrique d'objets technique
    /// </summary>
    public interface IAppToolFactory
    {
        #region Méthodes

        /// <summary>
        /// Retourne l'interface <see cref="IAppLog"/> de l'objet <see cref="AppLog"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet Log</returns>
        IAppLog GetLog();

        /// <summary>
        /// Retourne l'interface <see cref="IAppContext"/> de l'objet <see cref="AppContext"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        IAppContext GetAppContext();

        /// <summary>
        /// Retourne l'objet <see cref="AppStellarium"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        IAppProgramme GetAppStellarium();

        /// <summary>
        /// Retourne l'objet <see cref="AppCartesDuCiel"/>
        /// </summary>
        /// <remarks>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</remarks>
        /// <returns>Objet AppContext</returns>
        IAppProgramme GetAppCartesDuCiel();

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinates"/>
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        Coordinates GetCoordinates(double latitude, double longitude);

        /// <summary>
        /// Retourne un nouvel objet de type <see cref="Coordinate"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Coordinate GetCoordinate(double value, CoordinatesType type);

        #endregion
    }
}
