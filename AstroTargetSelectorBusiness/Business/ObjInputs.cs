using System;

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
        /// Latitude du lieu d'observation
        /// </summary>
        public decimal LatitudeLieu { get; set; }

        /// <summary>
        /// Latitude du lieu d'observation
        /// </summary>
        public decimal LongitudeLieu { get; set; }

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
        internal ObjInputs(AppObjFactory toolFactory)
        {
            this.toolFactory = toolFactory;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory toolFactory = null;

        #endregion
    }
}
