using ApplicationTools;
using AstroTargetSelectorBusiness.Properties;
using System;
using System.Globalization;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet applicatif permettant la gestion du serveur UDP
    /// </summary>
    public interface IAppTCPServer
    {
        /// <summary>
        /// Port du server TCP
        /// <para>par défaut : 7142</para>
        /// <para>Les accesseurs get set positionne le paramètre applicatif</para>
        /// </summary>
        int TCPServerPort { get; set; }

        /// <summary>
        /// IP du serveur TCP
        /// <para>par défaut : 127.0.0.1</para>
        /// <para>Les accesseurs get set positionne le paramètre applicatif</para>
        /// </summary>
        string TCPServerIP { get; set; }

        /// <summary>
        /// Le serveur est-il en cours d'exécution
        /// </summary>
        bool IsRunning { get; set; }

        /// <summary>
        /// Démarrage du serveur TCP
        /// </summary>
        void Start(Action<IObjTarget> onSucces);

        /// <summary>
        /// Arrêt du serveur TCP
        /// </summary>
        void Abort();
    }
}