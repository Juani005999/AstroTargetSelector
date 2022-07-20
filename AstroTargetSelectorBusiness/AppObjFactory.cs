using ApplicationTools;
using System.Runtime.CompilerServices;

namespace AstroTargetSelectorBusiness
{
    /// <summary>
    /// Fabrique d'objets Business (métier) et Logic (applicatif)
    /// </summary>
    public class AppObjFactory : AppToolFactory
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AppObjFactory()
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Renvoi l'objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        public AppTarget GetAppTarget()
        {
            if (appTarget == null)
                appTarget = new AppTarget(this);
            return appTarget;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Objet applicatif permettant d'accéder à la collection des Targets avec application des règles applicatives
        /// </summary>
        private AppTarget appTarget = null;

        #endregion
    }
}
