using System;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un objet céleste
    /// </summary>
    public class ObjTarget
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
        /// Description de l'objet céleste
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// RA : Acsension droite de l'objet céleste
        /// <para>Valeur exprimée en "degrés horaires" décimal</para>
        /// </summary>
        public decimal RA { get; set; }

        /// <summary>
        /// DEC : Déclinaison de l'objet céleste
        /// <para>Valeur exprimée en "degrés" décimal</para>
        /// </summary>
        public decimal DEC { get; set; }

        /// <summary>
        /// Magnitude de l'objet céleste
        /// </summary>
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Grandeur : Largeur de l'objet céleste
        /// </summary>
        public decimal GrandeurWidth { get; set; }

        /// <summary>
        /// Grandeur : Hauteur de l'objet céleste
        /// </summary>
        public decimal GrandeurHeight { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTarget(AppObjFactory factory)
        {
            this.factory = factory;
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
