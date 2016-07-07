using System.Windows.Forms;

namespace Box9.Leds.Manager.Extensions
{
    public static class ComboBoxExtensions
    {
        public static void AsPercentageOptions(this ComboBox comboBox, int startAt = 0, int finishAt = 100)
        {
            var selectedValue = (int?)comboBox.SelectedItem;

            for (int i = 0; i <= 100; i++)
            {
                if (selectedValue != i && comboBox.Items.Contains(i))
                {
                    comboBox.Items.Remove(i);
                }
            }

            if (selectedValue > finishAt)
            {
                comboBox.Items.Remove(selectedValue);
                comboBox.SelectedIndex = -1;
                comboBox.Text = string.Empty;
            }

            for (int i = startAt; i <= finishAt; i++)
            {
                if (i != selectedValue)
                {
                    comboBox.Items.Insert(i, i);
                }
            }
        }
    }
}
