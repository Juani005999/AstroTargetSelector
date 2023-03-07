using System.Drawing;
using ApplicationTools;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un rectangle dans la Mosaïque
    /// </summary>
    internal class ObjMosaicRect : IObjMosaicRect
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate RA { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate DEC { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Point TopLeft { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Size Dimensions { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Color TextColor { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjMosaicRect()
        {
        }

        #endregion

        #region Champs
        #endregion
    }
}
