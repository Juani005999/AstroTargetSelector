using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;
using ApplicationTools;
using AstroTargetSelectorTelescope;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant la gestion du téléscope ASCOM
    /// </summary>
    internal class AppASCOMTelescope : IAppASCOMTelescope
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Dictionary<string, string> ListeDriverASCOM
        {
            get
            {
                if (listeDriverASCOM == null)
                {
                    ChargeListeDriverASCOM();
                }
                return listeDriverASCOM;
            }
        }

        /// <summary>
        /// Nom du dernier téléscope ASCOM utilisé
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        private string lastASCOMTelescopeId
        {
            get
            {
                return Properties.Settings.Default.LastASCOMTelescopeName;
            }
            set
            {
                Properties.Settings.Default.LastASCOMTelescopeName = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string LastASCOMTelescopeId
        {
            get
            {
                if (ListeDriverASCOM.Count == 0)
                    return string.Empty;

                string lastDriverId = lastASCOMTelescopeId;
                if (!string.IsNullOrEmpty(lastDriverId)
                    && ListeDriverASCOM.Where(da => da.Key == lastDriverId).FirstOrDefault().Key != null)
                {
                    return lastASCOMTelescopeId;
                }

                lastASCOMTelescopeId = ListeDriverASCOM.FirstOrDefault().Key;
                return lastASCOMTelescopeId;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().IsConnected;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsSlewing
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().IsSlewing;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsTracking
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().IsTracking;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? RightAscension
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().RightAscension;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Declination
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().Declination;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Altitude
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().Altitude;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Azimuth
        {
            get
            {
                return telescopeFactory.GetIASCOMTelescope().Azimuth;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppASCOMTelescope(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;

            // Initialisation de la factory technique pour Telescope ASCOM
            telescopeFactory = new AppTelescopeFactory(appToolFactory);
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Chargement des drivers ASCOM depuis la Registry
        /// </summary>
        private void ChargeListeDriverASCOM()
        {
            listeDriverASCOM = new Dictionary<string, string>();

            try
            {
                RegistryKey parentNode = appToolFactory.GetAppASCOMPlateform().NodeProgramKey;
                if (parentNode != null)
                {
                    RegistryKey telescopeKey = parentNode.OpenSubKey("Telescope Drivers");
                    if (telescopeKey != null)
                    {
                        foreach (string idTelescopeEnCours in telescopeKey.GetSubKeyNames())
                        {
                            RegistryKey driverKey = telescopeKey.OpenSubKey(idTelescopeEnCours);
                            if (driverKey != null)
                            {
                                string nomDriver = driverKey.GetValue("") as string;
                                listeDriverASCOM.Add(idTelescopeEnCours, nomDriver);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                // Trace
                appToolFactory.GetLog().LogException(err, GetType().Name);
                // Flush de la liste
                if (listeDriverASCOM != null)
                    listeDriverASCOM.Clear();
                listeDriverASCOM = new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public bool IsASCOMReady()
        {
            // On vérifie la présence de la dll dans le GAC
            try
            {
                if (isASCOMReady == null || !isASCOMReady.HasValue)
                {
                    // Trace
                    appToolFactory.GetLog().Log($"Vérification si la plateforme ASCOM 6.6 est installée et les dll chargées dans le GAC", GetType().Name);
                    
                    isASCOMReady = appToolFactory.GetAppASCOMPlateform().IsInstalled;
                    if (isASCOMReady.HasValue && isASCOMReady.Value)
                    {
                        isASCOMReady &= Assembly.ReflectionOnlyLoad("ASCOM.DeviceInterfaces, Version=6.0.0.0, Culture=neutral, PublicKeyToken=565de7938946fba7").GlobalAssemblyCache;
                        isASCOMReady &= Assembly.ReflectionOnlyLoad("ASCOM.DriverAccess, Version=6.0.0.0, Culture=neutral, PublicKeyToken=565de7938946fba7").GlobalAssemblyCache;
                        isASCOMReady &= Assembly.ReflectionOnlyLoad("ASCOM.Exceptions, Version=6.0.0.0, Culture=neutral, PublicKeyToken=565de7938946fba7").GlobalAssemblyCache;
                        isASCOMReady &= Assembly.ReflectionOnlyLoad("ASCOM.NewtonSoft.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=565de7938946fba7").GlobalAssemblyCache;
                        isASCOMReady &= Assembly.ReflectionOnlyLoad("ASCOM.Utilities, Version=6.0.0.0, Culture=neutral, PublicKeyToken=565de7938946fba7").GlobalAssemblyCache;
                    }

                    // Trace
                    appToolFactory.GetLog().Log($"Plateforme ASCOM 6.6 installée {isASCOMReady}", GetType().Name);
                }
            }
            catch (Exception err)
            {
                // Trace
                appToolFactory.GetLog().LogException(err, GetType().Name);
                isASCOMReady = false;
            }
            // Retour
            return isASCOMReady.HasValue ? isASCOMReady.Value : false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="idAscom"></param>
        public void Connect(string idAscom)
        {
            // On récupère le nom du Driver ASCOM
            string nomDriver = idAscom;
            if (ListeDriverASCOM.Where(da => da.Key == idAscom).FirstOrDefault().Key != null)
                nomDriver = ListeDriverASCOM.Where(da => da.Key == idAscom).FirstOrDefault().Value;

            // MAJ du dernier driver utilisé
            lastASCOMTelescopeId = idAscom;
            telescopeFactory.GetIASCOMTelescope().Connect(idAscom, nomDriver);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DisConnect()
        {
            telescopeFactory.GetIASCOMTelescope().DisConnect();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SlewTo(double ra, double dec)
        {
            telescopeFactory.GetIASCOMTelescope().SlewTo(ra, dec);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void StopSlew()
        {
            telescopeFactory.GetIASCOMTelescope().StopSlew();
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Instance de la fabrique d'objet technique des Télescopes ASCOM
        /// </summary>
        private readonly IAppTelescopeFactory telescopeFactory = null;

        /// <summary>
        /// Liste des drivers de télescope ASCOM
        /// </summary>
        private Dictionary<string, string> listeDriverASCOM = null;

        /// <summary>
        /// Membre interne permettant de créer un singleton
        /// </summary>
        private bool? isASCOMReady = null;
        #endregion
    }
}
