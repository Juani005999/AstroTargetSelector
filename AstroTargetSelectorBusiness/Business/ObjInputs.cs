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
        /// Capteur
        /// </summary>
        public ObjCapteur Capteur { get; set; }

        /// <summary>
        /// Bougé max.
        /// </summary>
        public decimal BougeMax { get; set; }

        /// <summary>
        /// Nombre d'intervalle de temps (1/4 d'heure) pour le calcul des temps de pose
        /// <para>par défaut 8</para>
        /// </summary>
        public int NombreSlice { get; set; }

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

            // Capteur : On charge depuis les settings. Si non présent, on positionne IMX533 / 4096px
            if (string.IsNullOrEmpty(Settings.Default.NomCapteur) || string.IsNullOrEmpty(Settings.Default.LargeurCapteur))
            {
                Settings.Default.NomCapteur = "IMX533";
                Settings.Default.LargeurCapteur = "5200";
                Settings.Default.Save();
                factory.GetLog().Log($"Capteur non présent dans les Settings. Positionnement de IMX533 par défaut", GetType().Name);
            }
            Capteur = factory.GetAppCapteur().GetCapteur(Settings.Default.NomCapteur,
                                                   Convert.ToDecimal("5200", CultureInfo.InvariantCulture));
            factory.GetLog().Log($"Capteur : {Capteur.Nom} / {Capteur.Largeur.ToString(CultureInfo.InvariantCulture)} px", GetType().Name);

            // Zones à exclure

            // Bougé max
            BougeMax = 1;

            // Nombre de SLices
            NombreSlice = 12;
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
