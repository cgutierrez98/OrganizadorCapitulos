using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Application.Comparers
{
    public class ListViewItemComparer : IComparer
    {
        private readonly int _column;
        private readonly SortOrder _order;

        public ListViewItemComparer(int column, SortOrder sortOrder)
        {
            _column = column;
            _order = sortOrder;
        }

        public int Compare(object x, object y)
        {
            int returnVal = string.Compare(
                ((ListViewItem)x).SubItems[_column].Text,
                ((ListViewItem)y).SubItems[_column].Text);

            return _order == SortOrder.Ascending ? returnVal : -returnVal;
        }
    }
}
