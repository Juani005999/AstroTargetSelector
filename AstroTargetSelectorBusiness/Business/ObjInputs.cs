using System;
using System.Globalization;
using System.Linq;
using ApplicationTools;
using AstroTargetSelectorBusiness.Properties;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant toutes les données nécessaires à l'application des règles applicatives
    /// </summary>
    public class ObjInputs
    {
        #region Propriétés

        /// <summary>
        /// Date et Heure de l'observation
        /// </summary>
        public DateTime DateHeureObservation { get; set; }

        /// <summary>
        /// Coordonnées (Longitude et Latitude) du lieu d'observation
        /// </summary>
        public Coordinates LieuObservation
        {
            get
            {
                // On charge depuis les settings. Si non présent, on positionne sur Paris 0 / 0
                // TODO : Si non présent en settings, on récupère la position GPS du poste en cours ?
                if (string.IsNullOrEmpty(Settings.Default.LatitudeObs) || string.IsNullOrEmpty(Settings.Default.LongitudeObs))
                {
                    Settings.Default.LatitudeObs = "48.2512";
                    Settings.Default.LongitudeObs = "7.7";
                    Settings.Default.Save();
                    factory.GetLog().Log($"Localisation non présente dans les Settings. Positionnement de Paris par défaut", GetType().Name);
                }

                // Si le membre privé n'existe pas, on le créer
                if (lieuObservation == null)
                {
                    lieuObservation = factory.GetCoordinates(Convert.ToDecimal(Settings.Default.LatitudeObs, CultureInfo.InvariantCulture),
                                                            Convert.ToDecimal(Settings.Default.LongitudeObs, CultureInfo.InvariantCulture));
                    factory.GetLog().Log($"Lieu d'Observation : {LieuObservation.LocalisationComplete}", GetType().Name);
                }
                //// S'il existe déjà, on l'actualise
                //else
                //{
                //    lieuObservation.UpdateCoordonnees(Convert.ToDecimal(Settings.Default.LatitudeObs, CultureInfo.InvariantCulture),
                //                                            Convert.ToDecimal(Settings.Default.LongitudeObs, CultureInfo.InvariantCulture));
                //}
                return lieuObservation;
            }
        }

        /// <summary>
        /// Capteur
        /// </summary>
        public ObjCapteur Capteur
        {
            get
            {
                // Capteur : On charge depuis les settings. Si non présent, on positionne le premier capteur de la liste des capteurs
                if (string.IsNullOrEmpty(Settings.Default.NomCapteur) || string.IsNullOrEmpty(Settings.Default.LargeurCapteur))
                {
                    if (factory.GetAppCapteur().Capteurs.ListeObjCapteur.Count > 0)
                    {
                        Settings.Default.NomCapteur = factory.GetAppCapteur().Capteurs.ListeObjCapteur.FirstOrDefault().Nom;
                        Settings.Default.LargeurCapteur = factory.GetAppCapteur().Capteurs.ListeObjCapteur.FirstOrDefault().Largeur.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Settings.Default.NomCapteur = "IMX585";
                        Settings.Default.LargeurCapteur = "3096";
                    }
                    Settings.Default.Save();
                    factory.GetLog().Log($"Capteur non présent dans les Settings. Positionnement de IMX533 par défaut", GetType().Name);
                }

                // Si le membre privé n'existe pas, on le créer
                if (capteur == null)
                {
                    capteur = factory.GetAppCapteur().GetCapteur(Settings.Default.NomCapteur,
                                                           Convert.ToDecimal(Settings.Default.LargeurCapteur, CultureInfo.InvariantCulture));
                    factory.GetLog().Log($"Capteur : {Capteur.Nom} / {Capteur.Largeur} px", GetType().Name);
                }
                return capteur;
            }
        }

        /// <summary>
        /// Bougé max.
        /// </summary>
        public decimal BougeMax { get; set; }

        /// <summary>
        /// Nombre d'intervalle de temps : (<see cref="TotalTimeSlice"/> * 60) / <see cref="MinuteIntervalSlice"/>
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
        /// Nombre de minutes d'un Intervalle pour le calcul des temps de pose
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 10</para>
        /// </summary>
        public int MinuteIntervalSlice { 
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.MinuteIntervalSlice))
                {
                    Settings.Default.MinuteIntervalSlice = "10";
                    Settings.Default.Save();
                    factory.GetLog().Log($"MinuteIntervalSlice non présente dans les Settings. Positionnement de 5 par défaut", GetType().Name);
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
        /// Durée totale de l'observation pour définir le nombre de slice en fonction de l'intervalle
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est 2</para>
        /// </summary>
        public int TotalTimeSlice
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.TotalTimeSlice))
                {
                    Settings.Default.TotalTimeSlice = "2";
                    Settings.Default.Save();
                    factory.GetLog().Log($"TotalTimeSlice non présente dans les Settings. Positionnement de 2 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Settings.Default.TotalTimeSlice);
            }
            set
            {
                Settings.Default.TotalTimeSlice = value.ToString();
                Settings.Default.Save();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjInputs(AppObjFactory factory)
        {
            this.factory = factory;

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
            factory.GetLog().Log($"Positionnement des paramètres Inputs par défaut", GetType().Name);

            // Date et heure de l'obs. : date/heure du jour et précédent quart d'heure
            DateHeureObservation = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                DateTime.Now.Hour, DateTime.Now.Minute - (DateTime.Now.Minute % 15), 0);
            factory.GetLog().Log($"Date d'Observation : {DateHeureObservation}", GetType().Name);

            // Zones à exclure

            // Bougé max
            BougeMax = 1;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Coordonnées (Longitude et Latitude) du lieu d'observation
        /// </summary>
        private Coordinates lieuObservation = null;

        /// <summary>
        /// Capteur
        /// </summary>
        public ObjCapteur capteur = null;

        #endregion
    }
}
