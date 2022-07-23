using System;

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
                    inputs = new ObjInputs(toolFactory);
                return inputs;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppInputs(AppObjFactory toolFactory)
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
        /// Objets métier contenant les données en entrée
        /// </summary>
        private ObjInputs inputs = null;

        #endregion
    }
}
