using System.Linq;
using System.Windows.Forms;

namespace Box9.Leds.Manager.Extensions
{
    public static class FormExtensions
    {
        public static void ToggleButtonAvailabilities(this Form form, bool enabled, params Button[] exceptions)
        {
            foreach (var control in form.Controls)
            {
                if (control is Button)
                {
                    var button = ((Button)control);

                    if (exceptions.AsEnumerable().Contains(control))
                    {
                        button.Enabled = !enabled;
                    }
                    else
                    {
                        button.Enabled = enabled;
                    }
                }
            }
        }
    }
}
