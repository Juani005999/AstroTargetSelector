using System;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un capteur
    /// </summary>
    public class ObjCapteur
    {
        #region Propriétés

        /// <summary>
        /// Nom du Capteur
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Largeur en pixel
        /// </summary>
        public decimal Largeur { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjCapteur(AppObjFactory factory)
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
