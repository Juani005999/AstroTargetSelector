using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApplicationTools
{
    /// <summary>
    /// List view IComparer
    /// <para>IComparer<see cref="ListViewItem"/></para>
    /// </summary>
    public class ListViewItemComparer : IComparer<ListViewItem>
    {
        #region Champs

        private int ColumnToSort;
        private SortOrder OrderOfSort;

        #endregion

        #region Constructeur

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mCols"></param>
        public ListViewItemComparer(int[] mCols)
        {
            this.ColumnToSort = mCols[0];
            this.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mCols"></param>
        /// <param name="order"></param>
        public ListViewItemComparer(int[] mCols, System.Windows.Forms.SortOrder order)
        {
            this.ColumnToSort = mCols[0];
            this.OrderOfSort = order;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listviewX"></param>
        /// <param name="listviewY"></param>
        /// <returns></returns>
        public int Compare(ListViewItem listviewX, ListViewItem listviewY)
        {
            // Initialize the CaseInsensitiveComparer object
            int compareResult;
            Comparer ObjectCompare = new Comparer(CultureInfo.CurrentUICulture);

            // Compare the two items
            int intX = 0;
            int intY = 0;
            decimal decimalX = 0;
            decimal decimalY = 0;
            DateTime dateX = DateTime.Now;
            DateTime dateY = DateTime.Now;
            Coordinate nouveauLieuX = new Coordinate(0, CoordinatesType.Degree);
            Coordinate nouveauLieuY = new Coordinate(0, CoordinatesType.Degree);
            // Si c'est du int
            if (int.TryParse(listviewX.SubItems[ColumnToSort].Text, out intX)
                && int.TryParse(listviewY.SubItems[ColumnToSort].Text, out intY))
            {
                compareResult = ObjectCompare.Compare(intX, intY);
            }
            // Si c'est du decimal
            else if (decimal.TryParse(listviewX.SubItems[ColumnToSort].Text, out decimalX)
                && decimal.TryParse(listviewY.SubItems[ColumnToSort].Text, out decimalY))
            {
                compareResult = ObjectCompare.Compare(decimalX, decimalY);
            }
            // Si c'est du DateTime
            else if (DateTime.TryParse(listviewX.SubItems[ColumnToSort].Text, out dateX)
                && DateTime.TryParse(listviewY.SubItems[ColumnToSort].Text, out dateY))
            {
                compareResult = ObjectCompare.Compare(dateX, dateY);
            }
            // Si c'est du Coordinate
            else if (Coordinate.TryParseFromFormatedString(listviewX.SubItems[ColumnToSort].Text, ref nouveauLieuX)
                && Coordinate.TryParseFromFormatedString(listviewY.SubItems[ColumnToSort].Text, ref nouveauLieuY))
            {
                compareResult = ObjectCompare.Compare(nouveauLieuX.Coordonnee, nouveauLieuY.Coordonnee);
            }
            // Sinon, du texte
            else
            {
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
                //string stringA = listviewX.SubItems[ColumnToSort].Text as string;
                //string stringB = listviewY.SubItems[ColumnToSort].Text as string;
                //if (!string.IsNullOrEmpty(stringA) && !string.IsNullOrEmpty(stringB))
                //    compareResult = ObjectCompare.Compare(stringA.Replace(" ", ""), stringB.Replace(" ", ""));
                //else
                //    compareResult = ObjectCompare.Compare(stringA, stringB);
            }

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        #endregion
    }
}
