using System.Collections.Generic;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet représentant un intervalle regroupant des intervalles <see cref="ObjSliceTarget"/>
    /// </summary>
    public interface IObjSliceSpan
    {
        /// <summary>
        /// Rotation angulaire correspondant au Slice
        /// </summary>
        double RotationAngulaireGlobale { get; }

        /// <summary>
        /// Liste des objets <see cref="ObjSliceTarget"/> représentant la liste des intervalles de temps de la Target
        /// </summary>
        List<IChartSlice> Slices { get; }

        /// <summary>
        /// Rotation angulaire correspondant au Slice sous la forme d'une chaîne formatée
        /// </summary>
        string RotationAngulaireGlobaleFormated { get; }
    }
}