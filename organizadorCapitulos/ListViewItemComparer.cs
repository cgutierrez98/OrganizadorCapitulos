using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ListViewItemComparer : IComparer
{
    private readonly int col;
    private readonly SortOrder order;

    public ListViewItemComparer(int column, SortOrder sortOrder)
    {
        col = column;
        order = sortOrder;
    }

    public int Compare(object x, object y)
    {
        int returnVal = string.Compare(
            ((ListViewItem)x).SubItems[col].Text,
            ((ListViewItem)y).SubItems[col].Text);

        // Determina si el orden es ascendente o descendente
        return order == SortOrder.Ascending ? returnVal : -returnVal;
    }
}
