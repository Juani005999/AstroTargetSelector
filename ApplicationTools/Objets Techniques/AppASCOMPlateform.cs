using ApplicationTools.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTools
{
    /// <summary>
    /// Objet de communication avec la plateforme ASCOM 6
    /// </summary>
    public class AppASCOMPlateform : AppProgramme
    {
        #region Constantes
        #endregion

        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "ASCOM Platform 6.6";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Manufacturer
        {
            get
            {
                return "ASCOM";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int StartTimeout
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string FileName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ProcessName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override RegistryKey NodeProgramKey
        {
            get
            {
                if (nodeProgramKey == null)
                {
                    nodeProgramKey = RegistryUtils.GetNodeProgramKey(Manufacturer, ProcessName, appLog);
                }
                return nodeProgramKey;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal AppASCOMPlateform(IAppLog appLog)
            : base(appLog)
        {
        }

        #endregion

        #region Méthodes
        #endregion

        #region Champs
        #endregion
    }
}
