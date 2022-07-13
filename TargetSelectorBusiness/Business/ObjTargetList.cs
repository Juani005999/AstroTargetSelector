using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSelectorBusiness
{
    /// <summary>
    /// Objet représentant une liste d'objet <see cref="ObjTarget"/>
    /// </summary>
    class ObjTargetList
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTargetList(AppObjFactory toolFactory)
        {
            this.toolFactory = toolFactory;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly AppObjFactory toolFactory = null;

        #endregion
    }
}
