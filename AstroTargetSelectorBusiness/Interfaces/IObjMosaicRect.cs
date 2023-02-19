using ApplicationTools;
using System.Drawing;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet représentant un rectangle dans la Mosaïque
    /// </summary>
    public interface IObjMosaicRect
    {
        /// <summary>
        /// Index du rectangle dans le panneau
        /// </summary>
        string Index { get; set; }

        /// <summary>
        /// Texte du rectangle
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// RA du FOV correspondant
        /// </summary>
        Coordinate RA { get; set; }

        /// <summary>
        /// DEC du FOV correspondant
        /// </summary>
        Coordinate DEC { get; set; }

        /// <summary>
        /// Coordonnées du point Top Left dans le panel
        /// </summary>
        Point TopLeft { get; set; }

        /// <summary>
        /// Dimensions du rectangle dans le panel
        /// </summary>
        Size Dimensions { get; set; }

        /// <summary>
        /// Couleur du contour
        /// </summary>
        Color BorderColor { get; set; }

        /// <summary>
        /// Couleur du texte
        /// </summary>
        Color TextColor { get; set; }
    }
}