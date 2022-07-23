using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace ApplicationTools
{
    /// <summary>
    /// Objet Collection d'éléments <see cref="ComboBoxItem"/>
    /// </summary>
    /// <example>
    ///     ComboBoxItems comboBoxItems = new ComboboxItems();
    ///     ComboBoxItem itemTous = comboBoxItems.NewItem("Tous", "-1");
    ///     comboBoxItems.Rows.Add(itemTous);
    ///     ListeTypeProjet.DisplayMember = "Text";
    ///     ListeTypeProjet.ValueMember = "Value";
    ///     ListeTypeProjet.DataSource = comboBoxItems;
    /// </example>
    public class ComboBoxItems : DataTable
    {
        #region Constructeur

        /// <summary>
        /// Constructeur de base
        /// </summary>
        public ComboBoxItems()
            : base()
        {
            this.Columns.Add("Text");
            this.Columns.Add("Value");
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode permettant la création d'un nouvel élément à la collection
        /// </summary>
        /// <returns></returns>
        public ComboBoxItem NewItem()
        {
            return NewRow() as ComboBoxItem;
        }

        /// <summary>
        /// Méthode permettant la création d'un nouvel élément à la collection
        /// </summary>
        /// <param name="text">Texte</param>
        /// <param name="value">Valeur</param>
        /// <returns></returns>
        public ComboBoxItem NewItem(string text, string value)
        {
            ComboBoxItem item = NewRow() as ComboBoxItem;
            if (item != null)
            {
                item.Text = text;
                item.Value = value;
            }
            return item;
        }

        #endregion

        #region Méthode protected

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Type GetRowType()
        {
            return typeof(ComboBoxItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ComboBoxItem(builder);
        }

        #endregion
    }

    /// <summary>
    /// Objet Elément de liste de ComboBox <see cref="ComboBoxItems"/>
    /// </summary>
    public class ComboBoxItem : DataRow
    {
        #region Constructeur

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="builder"></param>
        protected internal ComboBoxItem(DataRowBuilder builder)
            : base(builder)
        {
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Texte de l'élément de liste
        /// </summary>
        public string Text
        {
            get
            {
                return this["Text"].ToString();
            }
            set
            {
                this["Text"] = value;
            }
        }
        
        /// <summary>
        /// Valeur de l'élément de liste
        /// </summary>
        public string Value
        {
            get
            {
                return this["Value"].ToString();
            }
            set
            {
                this["Value"] = value;
            }
        }

        #endregion

        #region Méthode Override

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }

        #endregion
    }
}
