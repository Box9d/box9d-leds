using System.Windows.Forms;

namespace Box9.Leds.Manager.Extensions
{
    public static class ComboBoxExtensions
    {
        public static void AsPercentageOptions(this ComboBox comboBox, int startAt = 0, int finishAt = 100)
        {
            comboBox.Items.Clear();

            for (int i = startAt; i <= finishAt; i++)
            {
                comboBox.Items.Add(i);
            }
        }
    }
}
