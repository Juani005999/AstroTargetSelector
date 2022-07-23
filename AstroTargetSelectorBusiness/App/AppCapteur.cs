using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AstroTargetSelectorBusiness.Properties;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant d'accéder à la collection des Capteurs
    /// </summary>
    public class AppCapteur
    {
        #region Propriétés

        /// <summary>
        /// Liste des Capteurs
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé et la liste est chargée depuis le fichier des paramètres</para>
        /// </summary>
        public ObjCapteurList Capteurs
        {
            get
            {
                if (capteurs == null)
                    capteurs = new ObjCapteurList(factory);
                return capteurs;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppCapteur(AppObjFactory factory)
        {
            this.factory = factory;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi le capteur <see cref="ObjCapteur"/> correspondant au nom passé en paramètre
        /// <para>Si le capteur n'est pas présent dans la liste, il est crée et ajouté dans la liste des capteurs</para>
        /// </summary>
        /// <param name="nomCapteur">Nom du Capteur recherché</param>
        /// <param name="largeurCapteur">Largeur en pixel du Capteur</param>
        /// <returns><see cref="ObjCapteur"/>. Null si nomCapteur est vide ou si largeurCapteur est égal à 0.</returns>
        public ObjCapteur GetCapteur(string nomCapteur, decimal largeurCapteur)
        {
            if (string.IsNullOrEmpty(nomCapteur) || largeurCapteur == 0)
                return null;
            ObjCapteur objEnCours = Capteurs.ListeObjCapteur.Where(t => t.Nom == nomCapteur).FirstOrDefault();
            if (objEnCours == null)
            {
                objEnCours = new ObjCapteur(factory)
                {
                    Nom = nomCapteur,
                    Largeur = largeurCapteur
                };
                Capteurs.ListeObjCapteur.Add(objEnCours);
            }
            return objEnCours;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Liste des Capteurs
        /// </summary>
        private ObjCapteurList capteurs = null;

        #endregion
    }
}
