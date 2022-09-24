using ApplicationTools;
using AstroTargetSelectorResources;
using System;
using System.Linq;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant la gestion des données nécessaires à l'application des règles applicatives
    /// </summary>
    public class AppInputs : IAppInputs
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IObjInputs Inputs
        {
            get
            {
                if (inputs == null)
                    inputs = new ObjInputs(appToolFactory, appCapteur);
                return inputs;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string LieuObservation
        {
            get
            {
                return Inputs.LieuObservation.LocalisationComplete;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string CapteurFormatedString
        {
            get
            {
                return Inputs.Capteur.Nom + " / " + Inputs.Capteur.Largeur.ToString() + " px";
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string ToolTipInfosTexte
        {
            get
            {
                // Lieu
                string toolTipRetour = $"{Resources.Lieu} : {LieuObservation}";

                // Capteur
                toolTipRetour += Environment.NewLine;
                toolTipRetour += $"{Resources.Capteur} : {CapteurFormatedString}";

                // Zones Exclues
                toolTipRetour += Environment.NewLine;
                string zonesExclues = Resources.Aucune;
                if (Inputs.ZonesExclues.Count > 0)
                {
                    zonesExclues = string.Empty;
                    CoordinatesDirection lastItem = Inputs.ZonesExclues.Last();
                    foreach (CoordinatesDirection direction in Inputs.ZonesExclues)
                    {
                        zonesExclues += Coordinate.GetDirectionString(direction);
                        // Si ce n'est pas le dernier élément de la liste, on ajoute la séparation
                        if (direction != lastItem)
                            zonesExclues += " - ";
                    }
                }
                toolTipRetour += $"{Resources.ZonesExclues} : {zonesExclues}";

                // Hauteur min
                toolTipRetour += Environment.NewLine;
                toolTipRetour += $"{Resources.HauteurMin} : {Inputs.HauteurMin} °";

                // Bougé Max
                toolTipRetour += Environment.NewLine;
                toolTipRetour += $"{Resources.BougeMax} : {Inputs.BougeMax} px";

                return toolTipRetour;
            }
        }

        /// <summary>
        /// <inheritdoc />
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
        internal AppInputs(IAppToolFactory appToolFactory, IAppCapteur appCapteur)
        {
            this.appToolFactory = appToolFactory;
            this.appCapteur = appCapteur;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Instance de l'objet applicatif appCapteur
        /// </summary>
        private readonly IAppCapteur appCapteur = null;

        /// <summary>
        /// Objets métier contenant les données en entrée
        /// </summary>
        private IObjInputs inputs = null;

        #endregion
    }
}
