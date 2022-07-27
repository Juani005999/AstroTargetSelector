using ApplicationTools;
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
        public string Nom { get; internal set; }

        /// <summary>
        /// Largeur en pixel
        /// </summary>
        public decimal Largeur { get; internal set; }

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

        #region Méthode


        /// <summary>
        /// Valide une saisie au format string
        /// <para>L'allocation mémoire pour le paramètre ref <paramref name="capteur"/> doit être réalisé par l'appelant</para>
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="largeur"></param>
        /// <param name="capteur"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static bool TryParse(string nom, string largeur,
                                    out ObjCapteur capteur,
                                    AppObjFactory factory)
        {
            // On alloue de la mémoire pour l'objet capteur
            capteur = new ObjCapteur(factory);

            // On vérifie Nom
            if (string.IsNullOrEmpty(nom))
            {
                factory.GetLog().Log($"Nom du capteur non renseigné", "ObjCapteur", null, AppLog.TypeLog.Warning);
                return false;
            }

            // On TryParse largeur
            decimal largeurDec;
            if (string.IsNullOrEmpty(largeur) || !decimal.TryParse(largeur, out largeurDec))
            {
                factory.GetLog().Log($"TryParse pour la largeur de l'objet Capteur à renvoyer False", "ObjCapteur", null, AppLog.TypeLog.Warning);
                return false;
            }

            // Update de l'objet en entrée et retour
            capteur = factory.GetAppCapteur().GetCapteur(nom, largeurDec);
            return true;
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
