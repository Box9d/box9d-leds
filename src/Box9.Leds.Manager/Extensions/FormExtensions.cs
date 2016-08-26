using System.Linq;
using System.Windows.Forms;

namespace Box9.Leds.Manager.Extensions
{
    public static class FormExtensions
    {
        public static void ToggleControlAvailabilites(this Form form, bool enabled, params Control[] exceptions)
        {
            foreach (var obj in form.Controls)
            {
                if (obj is Control)
                {
                    var control = ((Control)obj);

                    if (exceptions.AsEnumerable().Contains(control))
                    {
                        control.Enabled = !enabled;
                    }
                    else
                    {
                        control.Enabled = enabled;
                    }
                }
            }
        }
    }
}
