using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using ApplicationTools;
using AstroTargetSelectorBusiness.Properties;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant toutes les données nécessaires à l'application des règles applicatives
    /// </summary>
    public class ObjInputs : IObjInputs
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime DateHeureObservation { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinates LieuObservation
        {
            get
            {
                // On charge depuis les settings. Si non présent, on positionne sur Paris 0 / 0
                // TODO : Si non présent en settings, on récupère la position GPS du poste en cours ?
                if (string.IsNullOrEmpty(Settings.Default.LatitudeObs) || string.IsNullOrEmpty(Settings.Default.LongitudeObs))
                {
                    Settings.Default.LatitudeObs = "48.858611";
                    Settings.Default.LongitudeObs = "2.294166";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"Localisation non présente dans les Settings. Positionnement de Paris par défaut", GetType().Name);
                }

                // Si le membre privé n'existe pas, on le créer
                if (lieuObservation == null)
                {
                    lieuObservation = appToolFactory.GetCoordinates(Convert.ToDouble(Settings.Default.LatitudeObs, CultureInfo.InvariantCulture),
                                                            Convert.ToDouble(Settings.Default.LongitudeObs, CultureInfo.InvariantCulture));
                    appToolFactory.GetLog().Log($"Lieu d'Observation : {LieuObservation.LocalisationComplete}", GetType().Name);
                }
                // S'il existe déjà, on l'actualise
                else
                {
                    lieuObservation.UpdateCoordonnees(Convert.ToDouble(Settings.Default.LatitudeObs, CultureInfo.InvariantCulture),
                                                            Convert.ToDouble(Settings.Default.LongitudeObs, CultureInfo.InvariantCulture));
                }
                return lieuObservation;
            }
            set
            {
                Settings.Default.LongitudeObs = value.LongitudeValue.ToString(CultureInfo.InvariantCulture);
                Settings.Default.LatitudeObs = value.LatitudeValue.ToString(CultureInfo.InvariantCulture);
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjCapteur Capteur
        {
            get
            {
                // Capteur : On charge depuis les settings. Si non présent, on positionne le premier capteur de la liste des capteurs
                if (string.IsNullOrEmpty(Settings.Default.NomCapteur) || string.IsNullOrEmpty(Settings.Default.LargeurCapteur))
                {
                    if (appCapteur.Capteurs.ListeObjCapteur.Count > 0)
                    {
                        Settings.Default.NomCapteur = appCapteur.Capteurs.ListeObjCapteur.FirstOrDefault().Nom;
                        Settings.Default.LargeurCapteur = appCapteur.Capteurs.ListeObjCapteur.FirstOrDefault().Largeur.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Settings.Default.NomCapteur = "IMX290";
                        Settings.Default.LargeurCapteur = "1936";
                    }
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"Capteur non présent dans les Settings. Positionnement de IMX533 par défaut", GetType().Name);
                }

                // On récupère le capteur correspondant aux Settings, un nouveau capteur s'il n'existe pas déjà dans la liste des capteurs
                capteur = appCapteur.GetCapteur(Settings.Default.NomCapteur,
                                                            Convert.ToDouble(Settings.Default.LargeurCapteur, CultureInfo.InvariantCulture));

                // Retour
                return capteur;
            }
            set
            {
                Settings.Default.NomCapteur = value.Nom;
                Settings.Default.LargeurCapteur = value.Largeur.ToString(CultureInfo.InvariantCulture);
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double BougeMax
        {
            get
            {
                //return 1;
                if (string.IsNullOrEmpty(Settings.Default.BougeMax))
                {
                    Settings.Default.BougeMax = "1";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"BougeMax non présent dans les Settings. Positionnement de 1 par défaut", GetType().Name);
                }
                //return Convert.ToInt32(Settings.Default.BougeMax);
                return Convert.ToDouble(Settings.Default.BougeMax, CultureInfo.InvariantCulture);
            }
            set
            {
                Settings.Default.BougeMax = value.ToString(CultureInfo.InvariantCulture);
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double HauteurMin
        {
            get
            {
                //return 1;
                if (string.IsNullOrEmpty(Settings.Default.HauteurMin))
                {
                    Settings.Default.HauteurMin = "25";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"HauteurMin non présent dans les Settings. Positionnement de 25 par défaut", GetType().Name);
                }
                return Convert.ToDouble(Settings.Default.HauteurMin, CultureInfo.InvariantCulture);
            }
            set
            {
                Settings.Default.HauteurMin = value.ToString(CultureInfo.InvariantCulture);
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int NombreSlice
        {
            get
            {
                // Nombre d'intervalle = Durée totale (minutes) / Durée d'un intervalle
                if (MinuteIntervalSlice != 0)
                    return Convert.ToInt32((TotalTimeSlice * 60) / MinuteIntervalSlice);
                // Retour par défaut
                return 1;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int MinuteIntervalSlice { 
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.MinuteIntervalSlice))
                {
                    Settings.Default.MinuteIntervalSlice = "10";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"MinuteIntervalSlice non présente dans les Settings. Positionnement de 5 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Settings.Default.MinuteIntervalSlice);
            }
            set
            {
                Settings.Default.MinuteIntervalSlice = value.ToString();
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int TotalTimeSlice
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.TotalTimeSlice))
                {
                    Settings.Default.TotalTimeSlice = "2";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"TotalTimeSlice non présente dans les Settings. Positionnement de 2 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Settings.Default.TotalTimeSlice);
            }
            set
            {
                Settings.Default.TotalTimeSlice = value.ToString();
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<CoordinatesDirection> ZonesExclues
        {
            get
            {
                // Si le membre privé n'existe pas, on le créer
                if (zonesExclues == null)
                {
                    zonesExclues = new List<CoordinatesDirection>();
                    appToolFactory.GetLog().Log($"Création liste ZonesExclues", GetType().Name);
                }

                // On vide la liste locale avant rechargement
                zonesExclues.Clear();

                // On charge depuis les settings
                if (!string.IsNullOrEmpty(Settings.Default.ZonesExclues))
                {
                    // La Settings est sous la forme d'un tableau de chaîne avec '|' pour séparateur
                    string [] tabZonesExclues = Settings.Default.ZonesExclues.Split('|');
                    foreach(string zoneEnCours in tabZonesExclues)
                    {
                        // Après vérification de la validité de la zone, on l'ajoute au tableau des zones exclues
                        CoordinatesDirection directionZone;
                        if (Enum.TryParse(zoneEnCours, out directionZone))
                                zonesExclues.Add(directionZone);
                    }
                }

                // Retour
                return zonesExclues;
            }
            set
            {
                string settingValue = string.Empty;
                // On parcours la liste pour formatage de la chaîne
                foreach(CoordinatesDirection direction in value)
                {
                    settingValue += direction.ToString() + "|";
                }
                Settings.Default.ZonesExclues = settingValue;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ModeVisualisation Visualisation
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.Visualisation))
                {
                    Settings.Default.Visualisation = ModeVisualisation.Horaire.ToString();
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"Visualisation non présent dans les Settings. Positionnement de ModeVisualisation.Horaire par défaut", GetType().Name);
                }
                ModeVisualisation mode;
                if (Enum.TryParse(Settings.Default.Visualisation, out mode))
                    return mode;
                return ModeVisualisation.Horaire;
            }
            set
            {
                Settings.Default.Visualisation = value.ToString();
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool ModeNuit
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.ModeNuit))
                {
                    Settings.Default.ModeNuit = "0";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"ModeNuit non présent dans les Settings. Positionnement de 0 par défaut", GetType().Name);
                }
                return Settings.Default.ModeNuit == "1";
            }
            set
            {
                Settings.Default.ModeNuit = value ? "1" : "0";
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color BackColor
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.BackColor))
                {
                    Settings.Default.BackColor = "FF303030";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"BackColor non présent dans les Settings. Positionnement de Color.Black par défaut", GetType().Name);
                }
                if (!string.IsNullOrEmpty(Settings.Default.BackColor) && 
                    Settings.Default.BackColor.ToLower().StartsWith("ff") && 
                    Settings.Default.BackColor.Length == 8)
                {
                    int red = int.Parse(Settings.Default.BackColor.Substring(2, 2), NumberStyles.HexNumber);
                    int green = int.Parse(Settings.Default.BackColor.Substring(4, 2), NumberStyles.HexNumber);
                    int blue = int.Parse(Settings.Default.BackColor.Substring(6, 2), NumberStyles.HexNumber);
                    return Color.FromArgb(red, green, blue);
                }
                return Color.FromName(Settings.Default.BackColor);
            }
            set
            {
                Settings.Default.BackColor = value != null ? value.Name : Color.Black.Name;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color BackColorLight
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.BackColorLight))
                {
                    //Settings.Default.BackColorLight = Color.DarkSlateGray.Name;
                    Settings.Default.BackColorLight = "FF707070";
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"BackColorLight non présent dans les Settings. Positionnement de Color.DarkSlateGray par défaut", GetType().Name);
                }
                if (!string.IsNullOrEmpty(Settings.Default.BackColorLight) && 
                    Settings.Default.BackColorLight.ToLower().StartsWith("ff") && 
                    Settings.Default.BackColorLight.Length == 8)
                {
                    int red = int.Parse(Settings.Default.BackColorLight.Substring(2, 2), NumberStyles.HexNumber);
                    int green = int.Parse(Settings.Default.BackColorLight.Substring(4, 2), NumberStyles.HexNumber);
                    int blue = int.Parse(Settings.Default.BackColorLight.Substring(6, 2), NumberStyles.HexNumber);
                    return Color.FromArgb(red, green, blue);
                }
                return Color.FromName(Settings.Default.BackColorLight);
            }
            set
            {
                Settings.Default.BackColorLight = value != null ? value.Name : Color.DarkSlateGray.Name;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color ForeColor
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.ForeColor))
                {
                    Settings.Default.ForeColor = Color.OrangeRed.Name;
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"ForeColor non présent dans les Settings. Positionnement de Color.OrangeRed par défaut", GetType().Name);
                }
                if (!string.IsNullOrEmpty(Settings.Default.ForeColor) &&
                    Settings.Default.ForeColor.ToLower().StartsWith("ff") &&
                    Settings.Default.ForeColor.Length == 8)
                {
                    int red = int.Parse(Settings.Default.ForeColor.Substring(2, 2), NumberStyles.HexNumber);
                    int green = int.Parse(Settings.Default.ForeColor.Substring(4, 2), NumberStyles.HexNumber);
                    int blue = int.Parse(Settings.Default.ForeColor.Substring(6, 2), NumberStyles.HexNumber);
                    return Color.FromArgb(red, green, blue);
                }
                return Color.FromName(Settings.Default.ForeColor);
            }
            set
            {
                Settings.Default.ForeColor = value != null ? value.Name : Color.OrangeRed.Name;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Color ForeColorLight
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.ForeColorLight))
                {
                    Settings.Default.ForeColorLight = Color.LightYellow.Name;
                    Settings.Default.Save();
                    appToolFactory.GetLog().Log($"ForeColorLight non présent dans les Settings. Positionnement de Color.LightYellow par défaut", GetType().Name);
                }
                if (!string.IsNullOrEmpty(Settings.Default.ForeColorLight) &&
                    Settings.Default.ForeColorLight.ToLower().StartsWith("ff") &&
                    Settings.Default.ForeColorLight.Length == 8)
                {
                    int red = int.Parse(Settings.Default.ForeColorLight.Substring(2, 2), NumberStyles.HexNumber);
                    int green = int.Parse(Settings.Default.ForeColorLight.Substring(4, 2), NumberStyles.HexNumber);
                    int blue = int.Parse(Settings.Default.ForeColorLight.Substring(6, 2), NumberStyles.HexNumber);
                    return Color.FromArgb(red, green, blue);
                }
                return Color.FromName(Settings.Default.ForeColorLight);
            }
            set
            {
                Settings.Default.ForeColorLight = value != null ? value.Name : Color.LightYellow.Name;
                Settings.Default.Save();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjInputs(IAppToolFactory appToolFactory, IAppCapteur appCapteur)
        {
            this.appToolFactory = appToolFactory;
            this.appCapteur = appCapteur;

            // Positionnement des valeurs par défaut
            SetDefaultValue();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Positionne les valeurs par défaut
        /// </summary>
        private void SetDefaultValue()
        {
            // Trace
            appToolFactory.GetLog().Log($"Positionnement des paramètres Inputs par défaut", GetType().Name);

            // Date et heure de l'obs. : date/heure du jour et quart d'heure suivant
            DateHeureObservation = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                DateTime.Now.Hour, DateTime.Now.Minute - (DateTime.Now.Minute % 15), 0);
            DateHeureObservation = DateHeureObservation.AddMinutes(15);
            appToolFactory.GetLog().Log($"Date d'Observation : {DateHeureObservation}", GetType().Name);
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Instance de l'objet applicatif appCapteur
        /// </summary>
        private readonly IAppCapteur appCapteur = null;

        /// <summary>
        /// Coordonnées (Longitude et Latitude) du lieu d'observation
        /// </summary>
        private Coordinates lieuObservation = null;

        /// <summary>
        /// Capteur
        /// </summary>
        private IObjCapteur capteur = null;

        /// <summary>
        /// Liste des zones exclues
        /// </summary>
        private List<CoordinatesDirection> zonesExclues = null;

        #endregion
    }
}
