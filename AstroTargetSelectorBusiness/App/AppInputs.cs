using ApplicationTools;
using System;
using System.Linq;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
    /// </summary>
    public class AppInputs
    {
        #region Propriétés

        /// <summary>
        /// Objet métier contenant les données nécessaires à l'application des règles applicatives
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé</para>
        /// </summary>
        public ObjInputs Inputs
        {
            get
            {
                if (inputs == null)
                    inputs = new ObjInputs(factory);
                return inputs;
            }
        }

        /// <summary>
        /// Renvoi le lieu sous la forme d'une chaîne de caractères formatée (Latitude - Longitude)
        /// <para>XX° XX' XX" N/S - XX° XX' XX E/O"</para>
        /// </summary>
        public string LieuObservation
        {
            get
            {
                return Inputs.LieuObservation.LocalisationComplete;
            }
        }

        /// <summary>
        /// Renvoi le capteur sous la forme d'une chaîne de caractères formatée (Nom - Largeur px)
        /// <para>XXXX px</para>
        /// </summary>
        public string CapteurFormatedString
        {
            get
            {
                return Inputs.Capteur.Nom + " / " + Inputs.Capteur.Largeur.ToString() + " px";
            }
        }

        /// <summary>
        /// Renvoi le tes=xte à afficher dans l'info-bulle d'infos complémentaires sur les champs Inputs
        /// <para>Lieu de l'observation</para>
        /// <para>Capteur</para>
        /// <para>Zones exclues</para>
        /// <para>Bougé max.</para>
        /// </summary>
        public string ToolTipInfosTexte
        {
            get
            {
                // Lieu
                string toolTipRetour = $"Lieu : {LieuObservation}";

                // Capteur
                toolTipRetour += Environment.NewLine;
                toolTipRetour += $"Capteur : {CapteurFormatedString}";

                // Zones Exclues
                toolTipRetour += Environment.NewLine;
                string zonesExclues = "Aucune";
                if (Inputs.ZonesExclues.Count > 0)
                {
                    zonesExclues = string.Empty;
                    CoordinatesDirection lastItem = Inputs.ZonesExclues.Last();
                    foreach (CoordinatesDirection direction in Inputs.ZonesExclues)
                    {
                        zonesExclues += direction.ToString();
                        // Si ce n'est pas le dernier élément de la liste, on ajoute la séparation
                        if (direction != lastItem)
                            zonesExclues += " - ";
                    }
                }
                toolTipRetour += $"Zones exclues : {zonesExclues}";

                // Capteur
                toolTipRetour += Environment.NewLine;
                toolTipRetour += $"Bougé max. : {Inputs.BougeMax} px";

                //Lieu: 48°21'35" N - 7°07'57" E
                //Capteur: IMX492 - Largeur : 2055px
                //Zones exclues du ciel: NO - N - NE
                //Décalé max. : 1px

                return toolTipRetour;
            }
        }

        /// <summary>
        /// Renvoi le bougé max. sous la forme d'une chaîne de caractères formatée (X px)
        /// <para>X px</para>
        /// </summary>
        public string BougeMaxString
        {
            get
            {
                return Inputs.BougeMax.ToString() + " px";
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppInputs(AppObjFactory factory)
        {
            this.factory = factory;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Objets métier contenant les données en entrée
        /// </summary>
        private ObjInputs inputs = null;

        #endregion
    }
}
