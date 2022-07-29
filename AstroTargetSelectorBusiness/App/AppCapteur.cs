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
        internal ObjCapteurList Capteurs
        {
            get
            {
                if (capteurs == null)
                    capteurs = new ObjCapteurList(factory);
                return capteurs;
            }
        }

        /// <summary>
        /// Liste d'objets <see cref="ObjCapteur"/>
        /// </summary>
        public List<ObjCapteur> ListeObjCapteur
        {
            get
            {
                Capteurs.ForceUpdateListe = ForceUpdateListe;

                // Flush du flag permettant de forcer le rechargement de la liste depuis le fichier de configuration et renvoi de la liste
                ForceUpdateListe = false;
                return Capteurs.ListeObjCapteur;
            }
        }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration des capteurs
        /// </summary>
        public string CapteurListeFullPathFile
        {
            get
            {
                return Capteurs.CapteurListeFullPathFile;
            }
        }

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des capteurs
        /// </summary>
        public string CapteurListeFileName
        {
            get
            {
                return Capteurs.CapteurListeFileName;
            }
        }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjCapteur"/></para>
        /// </summary>
        public bool ForceUpdateListe { get; set; }

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
            // Si le capteur n'existe pas, on le créer et on l'ajoute à la liste
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
            else
            {
                // Si il existe déjà dans la liste, on met à jour sa largeur
                objEnCours.Largeur = largeurCapteur;
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
