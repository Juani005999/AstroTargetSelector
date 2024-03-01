using System.Collections.Generic;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet applicatif permettant la gestion du téléscope ASCOM
    /// </summary>
    public interface IAppASCOMTelescope
    {
        /// <summary>
        /// Nom du dernier téléscope ASCOM utilisé
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        string LastASCOMTelescopeId { get; }

        /// <summary>
        /// Liste des drivers de télescope ASCOM
        /// </summary>
        Dictionary<string, string> ListeDriverASCOM { get; }

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
        /// Tracking en cours sur le Télescope ASCOM connecté
        /// </summary>
        bool IsTracking { get; }

        /// <summary>
        /// Connexion au télescope ASCOM
        /// </summary>
        /// <param name="idAscom"></param>
        void Connect(string idAscom);

        /// <summary>
        /// Déconnexion du télescope ASCOM
        /// </summary>
        void DisConnect();

        /// <summary>
        /// Définit si les pré-requis ASCOM sont remplis
        /// <para>Singleton</para>
        /// <para>Plateforme 6.6 installée sur le poste</para>
        /// <para>Driver ASCOM.Interfaces chargée dans le GAC</para>
        /// </summary>
        /// <returns></returns>
        bool IsASCOMReady();

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