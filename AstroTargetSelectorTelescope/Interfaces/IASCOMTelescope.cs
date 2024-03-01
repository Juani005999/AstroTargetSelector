namespace AstroTargetSelectorTelescope
{
    /// <summary>
    /// Interface de l'Objet technique permettant la gestion du téléscope ASCOM
    /// </summary>
    public interface IASCOMTelescope
    {
        /// <summary>
        /// Télescope ASCOM connecté
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Slew en cours sur le Télescope ASCOM connecté
        /// </summary>
        bool IsSlewing { get; }

        /// <summary>
        /// RightAscension en cours sur le Télescope ASCOM connecté
        /// </summary>
        double? RightAscension { get; }

        /// <summary>
        /// Déclinaison en cours sur le Télescope ASCOM connecté
        /// </summary>
        double? Declination { get; }

        /// <summary>
        /// Altitude en cours sur le Télescope ASCOM connecté
        /// </summary>
        double? Altitude { get; }

        /// <summary>
        /// Azimuth en cours sur le Télescope ASCOM connecté
        /// </summary>
        double? Azimuth { get; }

        /// <summary>
        /// Latitude du Site du Télescope ASCOM connecté
        /// </summary>
        double? SiteLatitude { get; }

        /// <summary>
        /// Longitude du Site du Télescope ASCOM connecté
        /// </summary>
        double? SiteLongitude { get; }

        /// <summary>
        /// Tracking en cours sur le Télescope ASCOM connecté
        /// </summary>
        bool IsTracking { get; }

        /// <summary>
        /// Connexion au télescope ASCOM
        /// </summary>
        /// <param name="idAscom"></param>
        /// <param name="nomDriver"></param>
        void Connect(string idAscom, string nomDriver);

        /// <summary>
        /// Déconnexion du télescope ASCOM
        /// </summary>
        void DisConnect();

        /// <summary>
        /// Envoi la commande Slew au télescope
        /// </summary>
        /// <param name="ra"></param>
        /// <param name="dec"></param>
        void SlewTo(double ra, double dec);

        /// <summary>
        /// Lance la commande de AbortSlew
        /// </summary>
        void StopSlew();
    }
}