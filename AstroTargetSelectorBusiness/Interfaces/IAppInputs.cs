using System.Drawing;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Interface de l'Objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
    /// </summary>
    public interface IAppInputs
    {
        #region Propriétés

        /// <summary>
        /// Objet métier contenant les données nécessaires à l'application des règles applicatives
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé</para>
        /// </summary>
        IObjInputs Inputs { get; }

        /// <summary>
        /// Renvoi le lieu sous la forme d'une chaîne de caractères formatée (Latitude - Longitude)
        /// <para>XX° XX' XX" N/S - XX° XX' XX E/O"</para>
        /// </summary>
        string LieuObservation { get; }

        /// <summary>
        /// Renvoi le capteur sous la forme d'une chaîne de caractères formatée (Nom - Largeur px)
        /// <para>XXXX px</para>
        /// </summary>
        string CapteurFormatedString { get; }

        /// <summary>
        /// Renvoi le tes=xte à afficher dans l'info-bulle d'infos complémentaires sur les champs Inputs
        /// <para>Lieu de l'observation</para>
        /// <para>Capteur</para>
        /// <para>Zones exclues</para>
        /// <para>Bougé max.</para>
        /// </summary>
        string ToolTipInfosTexte { get; }

        /// <summary>
        /// Renvoi le bougé max. sous la forme d'une chaîne de caractères formatée (X px)
        /// <para>X px</para>
        /// </summary>
        string BougeMaxString { get; }

        /// <summary>
        /// Couleur de fond des fenêttre en mode nuit
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est Color.Black</para>
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// Couleur de fond des fenêttre en mode nuit (partie claire du fond)
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est Color.DarkSlateGray</para>
        /// </summary>
        Color BackColorLight { get; set; }

        /// <summary>
        /// Couleur des polices des fenêttre en mode nuit
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est Color.OrangeRed</para>
        /// </summary>
        Color ForeColor { get; set; }

        /// <summary>
        /// Couleur des polices des fenêttre en mode nuit (composant clairs)
        /// <para>Le paramètre est stocké dans les settings automatiquement sur set</para>
        /// <para>Si le paramètre n'existe pas dans les settings, la valeur par défaut positionnée et stockée dans les settings est Color.LightYellow</para>
        /// </summary>
        Color ForeColorLight { get; set; }

        #endregion
    }
}
