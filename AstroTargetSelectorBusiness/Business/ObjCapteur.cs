using ApplicationTools;
using System;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant un capteur
    /// </summary>
    public class ObjCapteur : IObjCapteur
    {
        #region Propriétés

        /// <summary>
        /// Nom du Capteur
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Largeur en pixel
        /// </summary>
        public double Largeur { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjCapteur()
        {
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
        /// <param name="appLog"></param>
        /// <param name="appCapteur"></param>
        /// <returns></returns>
        public static bool TryParse(string nom, string largeur,
                                    out IObjCapteur capteur,
                                    IAppLog appLog,
                                    IAppCapteur appCapteur)
        {
            // On alloue de la mémoire pour l'objet capteur
            capteur = new ObjCapteur();

            // On vérifie Nom
            if (string.IsNullOrEmpty(nom))
            {
                appLog.Log($"Nom du capteur non renseigné", "ObjCapteur", null, TypeLog.Warning);
                return false;
            }

            // On TryParse largeur
            double largeurDec;
            if (string.IsNullOrEmpty(largeur) || !double.TryParse(largeur, out largeurDec))
            {
                appLog.Log($"TryParse pour la largeur de l'objet Capteur à renvoyer False", "ObjCapteur", null, TypeLog.Warning);
                return false;
            }

            // Update de l'objet en entrée et retour
            capteur = appCapteur.GetCapteur(nom, largeurDec);
            return true;
        }
        
        #endregion

        #region Champs
        #endregion
    }
}
