using System;
using System.Drawing;
using System.Windows.Forms;
using Box9.Leds.Business.EventArgs;

namespace Box9.Leds.Manager.Views
{
    public interface IConfigureLedMappingView
    {
        event MouseEventHandler ReadyToDraw;

        event MouseEventHandler StopDrawing;

        event MouseEventHandler MousePositionChanged;

        event EventHandler Clear;

        event EventHandler Undo;

        event EventHandler Confirm;

        event EventHandler<LedMappingEventArgs> FinishedMapping;

        Bitmap Image { get; set; }
    }
}
