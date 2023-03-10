using System;
using System.Threading;
using System.Windows.Forms;
using ApplicationTools;
using AstroTargetSelectorTelescope.Properties;
using ASCOM.DriverAccess;

namespace AstroTargetSelectorTelescope
{
    /// <summary>
    /// Objet technique permettant la gestion du téléscope ASCOM
    /// </summary>
    internal class ASCOMTelescope : IASCOMTelescope
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsConnected
        {
            get
            {
                try
                {
                    using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                    {
                        if (telescope != null)
                            return telescope.Connected;
                    }
                    return false;
                }
                catch(Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name, TypeLog.Warning);
                    DisConnect();
                    return false;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsSlewing
        {
            get
            {
                try
                {
                    using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                    {
                        if (telescope != null)
                            return telescope.Slewing;
                    }
                    return false;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    DisConnect();
                    return false;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? RightAscension
        {
            get
            {
                try
                {
                    using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                    {
                        if (telescope != null)
                            return Math.Abs(telescope.RightAscension);
                    }
                    return null;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    DisConnect();
                    return null;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Declination
        {
            get
            {
                try
                {
                    using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                    {
                        if (telescope != null)
                            return telescope.Declination;
                    }
                    return null;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    DisConnect();
                    return null;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Altitude
        {
            get
            {
                try
                {
                    using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                    {
                        if (telescope != null)
                            return telescope.Altitude;
                    }
                    return null;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    DisConnect();
                    return null;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Azimuth
        {
            get
            {
                try
                {
                    using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                    {
                        if (telescope != null)
                            return telescope.Azimuth;
                    }
                    return null;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    DisConnect();
                    return null;
                }
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ASCOMTelescope(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="idAscom"></param>
        /// <param name="nomDriver"></param>
        /// <exception cref="WarningException"></exception>
        public void Connect(string idAscom, string nomDriver)
        {
            // On vérifie que le télescope n'est pas déjà connecté
            if (connected)
                DisConnect();

            // MAJ des membres internes
            this.idAscom = idAscom;
            this.nomDriver = nomDriver;

            // Vérification Id
            if (string.IsNullOrEmpty(idAscom))
                throw new WarningException(Resource.VeuillezSelectionnerUnDriverASCOM);

            // Connexion au Driver
            try
            {
                using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                {
                    // Trace
                    appToolFactory.GetLog().Log($"Connexion au télescope : {nomDriver} / {idAscom}", GetType().Name);

                    // Connection
                    telescope = new Telescope(idAscom);
                    telescope.Connected = true;
                    connected = true;

                    // Trace
                    appToolFactory.GetLog().Log($"Connexion au télescope : {nomDriver} / {idAscom} effectuée avec succès", GetType().Name);

                    // Notification
                    NotifyIcon notifyIconConnexion = new NotifyIcon();
                    notifyIconConnexion.Visible = true;
                    notifyIconConnexion.Icon = Properties.Resource.Telescope;
                    notifyIconConnexion.BalloonTipTitle = Resource.Connection;
                    notifyIconConnexion.BalloonTipText = $"{nomDriver}";
                    notifyIconConnexion.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIconConnexion.ShowBalloonTip(2000);
                    notifyIconConnexion.BalloonTipClosed += (sender, e) => notifyIconConnexion.Dispose();
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                if (telescope != null)
                    telescope.Dispose();
                telescope = null;
                this.idAscom = string.Empty;
                this.nomDriver = string.Empty;
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DisConnect()
        {
            try
            {
                using (new LockTelescope(lockTelescope, new TimeSpan (0, 0, 1)))
                {
                    if (telescope != null)
                    {
                        // Trace
                        appToolFactory.GetLog().Log($"Déconnexion du télescope ASCOM [{idAscom}]", GetType().Name);

                        // Déconnexion et Dispose
                        //telescope.Connected = false;
                        telescope.Dispose();

                        // Notification
                        NotifyIcon notifyIconConnexion = new NotifyIcon();
                        notifyIconConnexion.Visible = true;
                        notifyIconConnexion.Icon = Resource.Telescope;
                        notifyIconConnexion.BalloonTipTitle = Resource.Deconnection;
                        notifyIconConnexion.BalloonTipText = $"{nomDriver}";
                        notifyIconConnexion.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIconConnexion.ShowBalloonTip(2000);
                        notifyIconConnexion.BalloonTipClosed += (sender, e) => notifyIconConnexion.Dispose();
                    }
                    telescope = null;
                    connected = false;
                    idAscom = string.Empty;
                    nomDriver = string.Empty;
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                connected = false;
                idAscom = string.Empty;
                nomDriver = string.Empty;
                if (telescope != null)
                    telescope.Dispose();
                telescope = null;
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ra"></param>
        /// <param name="dec"></param>
        public void SlewTo(double ra, double dec)
        {
            try
            {
                using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                {
                    if (telescope != null)
                    {
                        // Si Slew en cours, On abort
                        if (telescope.Slewing)
                            StopSlew();
                        // Trace
                        appToolFactory.GetLog().Log($"Lancement de la commande SlewTo", GetType().Name);
                        // Lancement de la commande
                        telescope.Tracking = true;
                        telescope.SlewToCoordinatesAsync(ra, dec);
                    }
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                DisConnect();
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ra"></param>
        /// <param name="dec"></param>
        public void StopSlew()
        {
            try
            {
                using (new LockTelescope(lockTelescope, new TimeSpan(0, 0, 1)))
                {
                    if (telescope != null)
                    {
                        // Trace
                        appToolFactory.GetLog().Log($"Lancement de la commande AbortSlew", GetType().Name);
                        // On abort l'eventuel Slew En Cours
                        telescope.AbortSlew();
                    }
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                DisConnect();
                throw;
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Identifiant ASCOM du télescope
        /// </summary>
        private string idAscom = string.Empty;

        /// <summary>
        /// Nom du driver ASCOM
        /// </summary>
        private string nomDriver = string.Empty;

        /// <summary>
        /// Objet <see cref="Telescope"/> permettant la communication avec le driver ASCOM
        /// </summary>
        private Telescope telescope = null;

        /// <summary>
        /// Lock sur l'objet Telescope pour l'accès à l'objet en multiThread
        /// </summary>
        private static object lockTelescope = new object();

        /// <summary>
        /// Singleton interne pour sauvegarde de l'état Connecté
        /// </summary>
        private bool connected = false;

        #endregion
    }

    /// <summary>
    /// Lock sur le Télescope ASCOM
    /// </summary>
    internal class LockTelescope : IDisposable
    {
        /// <summary>
        /// Objet servant au lock
        /// </summary>
        private object lockTelescope;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="lockTelescope"></param>
        /// <param name="timeout"></param>
        /// <exception cref="TimeoutException"></exception>
        public LockTelescope(object lockTelescope, TimeSpan timeout)
        {
            this.lockTelescope = lockTelescope;
            if (!Monitor.TryEnter(this.lockTelescope, timeout))
                throw new TimeoutException();
        }

        /// <summary>
        /// Dispose du lock
        /// </summary>
        public void Dispose()
        {
            Monitor.Exit(lockTelescope);
        }
    }
}
