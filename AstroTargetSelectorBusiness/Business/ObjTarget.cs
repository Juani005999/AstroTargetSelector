using System;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un objet céleste
    /// </summary>
    class ObjTarget
    {
        #region Propriétés

        /// <summary>
        /// Nom de l'objet céleste
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Type de l'objet céleste (amas globulaire, nébuleuse planétaire, galaxie spirale, ...)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// RA : Acsension droite de l'objet céleste
        /// </summary>
        public DateTime RA { get; set; }

        /// <summary>
        /// DEC : Déclinaison de l'objet céleste
        /// </summary>
        public double DEC { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTarget(AppObjFactory toolFactory)
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
