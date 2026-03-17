using System;
using System.Collections;
using System.Windows.Forms;

namespace organizadorCapitulos.Application.Comparers
{
    public class ListViewItemComparer : IComparer
    {
        private readonly int _column;
        private readonly SortOrder _sortOrder;

        public ListViewItemComparer(int column, SortOrder sortOrder)
        {
            _column = column;
            _sortOrder = sortOrder;
        }

        public int Compare(object? x, object? y)
        {
            if (x == null || y == null) return 0;
            int returnVal = string.Compare(
                ((ListViewItem)x).SubItems[_column].Text,
                ((ListViewItem)y).SubItems[_column].Text);

            return _sortOrder == SortOrder.Ascending ? returnVal : -returnVal;
        }
    }
}
