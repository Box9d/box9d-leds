using System.Collections.Generic;
using System.Windows.Forms;

namespace Box9.Leds.Manager.Extensions
{
    public static class ListBoxExtensions
    {
        public static void RemoveAllItems(this ListBox listBox)
        {
            var items = new List<object>();
            foreach (var item in listBox.Items)
            {
                items.Add(item);
            }

            foreach (var item in items)
            {
                listBox.Items.Remove(item);
            }
        }
    }
}
