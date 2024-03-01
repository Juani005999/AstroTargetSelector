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
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet Log</returns>
        IAppLog GetLog();

        /// <summary>
        /// Retourne l'interface <see cref="IAppContext"/> de l'objet <see cref="AppContext"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet AppContext</returns>
        IAppContext GetAppContext();

        /// <summary>
        /// Retourne l'objet <see cref="AppStellarium"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet AppContext</returns>
        IAppProgramme GetAppStellarium();

        /// <summary>
        /// Retourne l'objet <see cref="AppCartesDuCiel"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
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

        /// <summary>
        /// Retourne l'objet <see cref="IConsoleQueue"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet IConsoleQueue</returns>
        IConsoleQueue GetConsoleQueue();

        /// <summary>
        /// Retourne l'objet <see cref="AppAstroTargetSelector"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet IAppProgramme</returns>
        IAppProgramme GetAppAstroTargetSelector();

        /// <summary>
        /// Retourne l'objet <see cref="AppAstroSessionOrganizer"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet IAppProgramme</returns>
        IAppProgramme GetAppAstroSessionOrganizer();

        /// <summary>
        /// Retourne l'objet <see cref="AppAstap"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet IAppProgramme</returns>
        IAppProgramme GetAppAstap();

        /// <summary>
        /// Retourne l'objet <see cref="AppASCOMPlateform"/>
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas, il est crée</para>
        /// </summary>
        /// <returns>Objet IAppProgramme</returns>
        IAppProgramme GetAppASCOMPlateform();

        /// <summary>
        /// Retourne l'interface <see cref="IWebP"/> de l'objet <see cref="WebP"/>
        /// </summary>
        /// <returns></returns>
        IWebP GetWebpWrapper();

        #endregion
    }
}
