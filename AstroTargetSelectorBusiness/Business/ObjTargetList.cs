using System.Collections.Generic;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant une liste d'objet <see cref="ObjTarget"/>
    /// </summary>
    class ObjTargetList
    {
        #region Propriétés

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        private List<ObjTarget> ListeObjTarget
        {
            get
            {
                if (listeObjTarget == null)
                {
                    listeObjTarget = new List<ObjTarget>();
                    ForceUpdateListe = true;
                }
                ChargementListe();
                return listeObjTarget;
            }
        }

        /// <summary>
        /// Force le rechargement de la liste depuis le fichier de configuration
        /// <para>Le rechargement s'effectue lors du prochain accès à la propriété <see cref="ListeObjTarget"/></para>
        /// </summary>
        bool ForceUpdateListe { get; set; }

        /// <summary>
        /// Renvoi le nom complet (Path + Nom de fichier) du fichier de configuration
        /// </summary>
        string ConfigurationFile
        {
            get
            {
                return factory.GetAppContext().UserAppDataPath + "\\" + configurationFileName;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTargetList(AppObjFactory factory)
        {
            this.factory = factory;
            ForceUpdateListe = false;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet le chargement de la liste des objets céleste depuis le fichier de configuration
        /// </summary>
        private void ChargementListe()
        {
            if (ForceUpdateListe)
            {
                // Clear de la liste actuelle
                listeObjTarget.Clear();

                // Lecture du fichier de configuration
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory factory = null;

        /// <summary>
        /// Liste d'objets <see cref="ObjTarget"/>
        /// </summary>
        private List<ObjTarget> listeObjTarget = null;

        /// <summary>
        /// Nom du fichier de configuration contenant la liste des objets céleste
        /// </summary>
        private const string configurationFileName = "TargetListe.csv";

        #endregion
    }
}
