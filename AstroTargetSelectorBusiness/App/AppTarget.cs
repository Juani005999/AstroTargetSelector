using System;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
    /// </summary>
    public class AppTarget
    {
        #region Propriétés

        /// <summary>
        /// Liste des Targets
        /// <para>Objet renvoyé sous la forme d'un singleton. S'il n'existe pas, il est créé et la liste est chargée depuis le fichier des paramètres</para>
        /// </summary>
        public ObjTargetList Targets
        {
            get
            {
                if (targets == null)
                    targets = new ObjTargetList(toolFactory);
                return targets;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppTarget(AppObjFactory toolFactory)
        {
            this.toolFactory = toolFactory;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory toolFactory = null;

        /// <summary>
        /// Liste des Targets
        /// </summary>
        private ObjTargetList targets = null;

        #endregion
    }
}
