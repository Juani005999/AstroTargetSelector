using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ApplicationTools;
using AstroTargetSelectorBusiness.Properties;
using AstroTargetSelectorResources;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant la gestion du serveur UDP
    /// </summary>
    public class AppTCPServer : IAppTCPServer
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int TCPServerPort
        {
            get
            {
                //return 1;
                if (string.IsNullOrEmpty(Settings.Default.TCPServerPort))
                {
                    Settings.Default.TCPServerPort = "7142";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"TCPServerPort non présent dans les Settings. Positionnement de 7142 par défaut", GetType().Name);
                }
                //return Convert.ToInt32(Settings.Default.BougeMax);
                return Convert.ToInt32(Settings.Default.TCPServerPort, CultureInfo.InvariantCulture);
            }
            set
            {
                Settings.Default.TCPServerPort = value.ToString(CultureInfo.InvariantCulture);
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string TCPServerIP
        {
            get
            {
                //return 1;
                if (string.IsNullOrEmpty(Settings.Default.TCPServerIP))
                {
                    Settings.Default.TCPServerIP = "127.0.0.1";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"TCPServerIP non présent dans les Settings. Positionnement de 127.0.0.1 par défaut", GetType().Name);
                }
                //return Convert.ToInt32(Settings.Default.BougeMax);
                return Settings.Default.TCPServerIP;
            }
            set
            {
                Settings.Default.TCPServerIP = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsRunning { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppTCPServer(IAppToolFactory appToolFactory, IAppTarget appTarget, IAppInputs appInputs)
        {
            this.appToolFactory = appToolFactory;
            this.appTarget = appTarget;
            this.appInputs = appInputs;
            IsRunning = false;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Start(Action<IObjTarget> onSucces)
        {
            IPAddress localIp = IPAddress.Parse(TCPServerIP);
            TcpListener server = null;
            try
            {
                // Trace et Chrono
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();
                appToolFactory.GetLog().Log($"Start du serveur TCP", GetType().Name);

                // Démarrage du serveur
                server = new TcpListener(localIp, TCPServerPort);
                server.Start();
                IsRunning = true;
                while (true)
                {
                    // Mise en écoute bloquante
                    appToolFactory.GetLog().Log($"[TCP] Attente de commande", GetType().Name);
                    using (TcpClient client = server.AcceptTcpClient())
                    {
                        // Lecture du stream TCP
                        NetworkStream stream = client.GetStream();
                        stream.ReadTimeout = 1000;
                        byte[] data = new byte[2048];
                        int bytes = stream.Read(data, 0, data.Length);
                        string tcpCommande = Encoding.UTF8.GetString(data, 0, bytes);
                        appToolFactory.GetLog().Log($"[TCP] Reception message : {tcpCommande}", GetType().Name);

                        // Interpretation commande
                        IObjTarget objetCeleste;
                        string messageRetour = InterpreteCommande(tcpCommande, out objetCeleste);

                        // Affichage Objet céleste
                        if (objetCeleste != null)
                        {
                            onSucces(objetCeleste);
                        }

                        // Envoi réponse
                        byte[] dataReponse = Encoding.UTF8.GetBytes(messageRetour);
                        stream.Write(dataReponse, 0, dataReponse.Length);
                        appToolFactory.GetLog().Log($"[TCP] Envoi Acknowledge : {messageRetour}", GetType().Name);
                    }
                }


                // Trace
            }
            catch (SocketException ex)
            {
                // Sur Exception, on trace l'erreur
                appToolFactory.GetLog().LogException(ex, GetType().Name);
            }
            finally
            {
                server.Stop();
                IsRunning = false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Abort()
        {
            try
            {
                IsRunning = false;
            }
            catch (Exception ex)
            {
                // Sur Exception, on trace l'erreur et on la throw à l'appelant
                appToolFactory.GetLog().LogException(ex, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Interpretation de la commande envoyée sur TCP
        /// </summary>
        /// <param name="tcpCommande"></param>
        /// <param name="objet"></param>
        /// <returns>messageRetour</returns>
        private string InterpreteCommande(string tcpCommande, out IObjTarget objet)
        {
            string messageRetour = string.Empty;
            try
            {
                appToolFactory.GetLog().Log($"Interpretation commande TCP : {tcpCommande}");
                string[] tabArgs = tcpCommande.Split(';');
                if (tabArgs == null || tabArgs.Length == 0)
                {
                    throw new Exception(Resources.NombreDeParametresIncorrect);
                }
                // Commande
                string commande = tabArgs[0];
                string nom = string.Empty;
                string type = string.Empty;
                string constellation = string.Empty;
                string denominations = string.Empty;
                string ra = string.Empty;
                string dec = string.Empty;
                string magnitude = string.Empty;
                string width = string.Empty;
                string height = string.Empty;

                // FocusTo
                if (!string.IsNullOrEmpty(commande) && commande.ToLower() == "focusto")
                {
                    // On vérifie le nombre de paramètre de la commande
                    if (tabArgs.Length < 9)
                        throw new Exception(Resources.NombreDeParametresIncorrect);
                    nom = string.IsNullOrEmpty(tabArgs[1]) ? "" : tabArgs[1].ToUpper();
                    type = tabArgs[2];
                    constellation = tabArgs[3];
                    ra = tabArgs[4];
                    dec = tabArgs[5];
                    magnitude = tabArgs[6];
                    width = tabArgs[7];
                    height = tabArgs[8];
                    if (tabArgs.Length > 9)
                        denominations = tabArgs[9];
                }

                // Recherche Objet
                objet = appTarget.GetTarget(nom);
                // Si l'objet n'est pas déjà présent dans la liste, on l'ajoute
                if (objet == null)
                {
                    appTarget.Targets.ListeObjTarget.Add(objet = new ObjTarget(appToolFactory, appInputs)
                    {
                        Nom = nom,
                        Type = type,
                        Description = denominations,
                        Constellation = constellation,
                        RA = appToolFactory.GetCoordinate(Convert.ToDouble(ra.Replace(',', '.'), CultureInfo.InvariantCulture), CoordinatesType.RA),
                        DEC = appToolFactory.GetCoordinate(Convert.ToDouble(dec.Replace(',', '.'), CultureInfo.InvariantCulture), CoordinatesType.DEC),
                        Magnitude = Convert.ToDouble(magnitude.Replace(',', '.'), CultureInfo.InvariantCulture),
                        GrandeurWidth = appToolFactory.GetCoordinate(Convert.ToDouble(width.Replace(',', '.'), CultureInfo.InvariantCulture) / 60, CoordinatesType.Degree),
                        GrandeurHeight = appToolFactory.GetCoordinate(Convert.ToDouble(height.Replace(',', '.'), CultureInfo.InvariantCulture) / 60, CoordinatesType.Degree)
                    });
                    appToolFactory.GetLog().Log($"Ajout nouvel objet à la liste : {objet.Nom}");
                }
                messageRetour = $"FocusTo OK [{nom}]";
            }
            catch (Exception ex)
            {
                // Sur Exception, on trace l'erreur
                appToolFactory.GetLog().LogException(ex, GetType().Name);
                messageRetour = $"FocusTo ERROR : {ex.Message}";
                objet = null;
            }
            return messageRetour;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Objet applicatif permettant d'accéder à la collection des Targets
        /// </summary>
        private IAppTarget appTarget = null;

        /// <summary>
        /// Objet applicatif permettant d'accéder aux Inputs
        /// </summary>
        private IAppInputs appInputs = null;

        #endregion
    }
}
