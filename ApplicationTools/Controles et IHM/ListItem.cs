using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTools
{
    /// <summary>
    /// Elément de ListBox
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// Champ Text
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Champ Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Surcharge pour affichage dans les listes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
