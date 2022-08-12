using System;
using System.Xml;

namespace ApplicationTools
{
    /// <summary>
    /// Exception de type Warning
    /// <para>Permet d'afficher un message de type Warning à l'utilisateur</para>
    /// </summary>
    public class WarningException : Exception
    {
        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="message">Message utlisateur</param>
        public WarningException(string message)
            : base(message)
        {
        }
    }
}
