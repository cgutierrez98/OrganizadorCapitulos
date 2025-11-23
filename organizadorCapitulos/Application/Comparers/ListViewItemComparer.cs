using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace organizadorCapitulos.Application.Comparers
{
    public class ListViewItemComparer(int column, SortOrder sortOrder) : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null) return 0;
            int returnVal = string.Compare(
                ((ListViewItem)x).SubItems[column].Text,
                ((ListViewItem)y).SubItems[column].Text);

            return sortOrder == SortOrder.Ascending ? returnVal : -returnVal;
        }
    }
}
