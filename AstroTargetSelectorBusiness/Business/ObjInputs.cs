using System;
using System.Globalization;
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
        public Coordinates LieuObservation { get; set; }

        /// <summary>
        /// Largeur du capteur
        /// </summary>
        public decimal LargeurCapteur { get; set; }

        /// <summary>
        /// Bougé max.
        /// </summary>
        public decimal BougeMax { get; set; }

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

            // Lieu de l'obs. : On charge depuis les settings. Si non présent, on positionne sur Paris 0 / 0
            // TODO : Si non présent en settings, on récupère la position GPS du poste en cours ?
            if (string.IsNullOrEmpty(Settings.Default.LatitudeObs) || string.IsNullOrEmpty(Settings.Default.LongitudeObs))
            {
                Settings.Default.LatitudeObs = "48.2512";
                Settings.Default.LongitudeObs = "7.7";
                Settings.Default.Save();
                factory.GetLog().Log($"Localisation non présente dans les Settings. Positionnement de Paris par défaut", GetType().Name);
            }
            LieuObservation = factory.GetCoordinates(Convert.ToDecimal(Settings.Default.LatitudeObs, CultureInfo.InvariantCulture),
                                                    Convert.ToDecimal(Settings.Default.LongitudeObs, CultureInfo.InvariantCulture));
            factory.GetLog().Log($"Lieu d'Observation : {LieuObservation.LocalisationComplete}", GetType().Name);
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        #endregion
    }
}
