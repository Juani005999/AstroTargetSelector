using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet représentant un capteur
    /// </summary>
    public interface IObjCapteur
    {
        #region Propriétés

        /// <summary>
        /// Nom du Capteur
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Largeur en pixel
        /// </summary>
        double Largeur { get; set; }

        #endregion
    }
}
