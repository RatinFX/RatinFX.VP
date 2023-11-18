using System.Windows.Forms;

namespace RatinFX.VP.General
{
    public static class ControlHelper
    {
        public static bool IsControlActive => (Control.ModifierKeys & Keys.Control) != 0;
        public static bool IsShiftActive => (Control.ModifierKeys & Keys.Shift) != 0;
    }
}
