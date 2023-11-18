using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RatinFX.VP.General
{
    public static class MessageBoxes
    {
        static DialogResult Base(
            string message,
            string title = "",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None
        )
        {
            return MessageBox.Show(
                message,
                title,
                buttons,
                icon
            );
        }

        public static DialogResult Warning(
            string message,
            string title = "Warning",
            MessageBoxIcon icon = MessageBoxIcon.Warning,
            [CallerMemberName] string callerName = ""
            )
        {

#if DEBUG
            Debug.WriteLine($"<!> Warning: \t{message}");
            Debug.WriteLine($"[!] Caller: \t{callerName}");
#endif

            return Base(
#if DEBUG
                "> " + callerName + "\n\n" +
#endif
                message,
                title,
                icon: icon
            );
        }

        public static DialogResult Error(
            string message,
            string title = "Something went wrong",
            MessageBoxIcon icon = MessageBoxIcon.Error,
            [CallerMemberName] string callerName = ""
            )
        {
#if DEBUG
            Debug.WriteLine($"[!] ERROR: \t{message}");
            Debug.WriteLine($"[!] Caller: \t{callerName}");
#endif

            return Base(
#if DEBUG
                "> " + callerName + "\n\n" +
#endif
                message,
                title,
                icon: icon
            );
        }

        public static DialogResult Error(
            Exception ex,
            string title = "Something went wrong",
            MessageBoxIcon icon = MessageBoxIcon.Error,
            [CallerMemberName] string callerName = ""
        )
        {
#if DEBUG
            Debug.WriteLine($"[!] ERROR: \t{ex.Message}");
            Debug.WriteLine($"[!] Caller: \t{callerName}");
            Debug.WriteLine($"[!] StackTrace: \t{ex.StackTrace}");
#endif

            return Base(
#if DEBUG
                "> " + callerName + "\n\n" + "> " + ex.StackTrace + "\n\n" +
#endif
                ex.Message,
                title,
                icon: icon
            );
        }

        public static DialogResult OK(string message, string title)
        {
            return Base(
                message,
                title
            );
        }

        public static DialogResult YesNo(
            string message,
            string title,
            MessageBoxButtons buttons = MessageBoxButtons.YesNo,
            MessageBoxIcon icon = MessageBoxIcon.Question
        )
        {
            return MessageBox.Show(
                message,
                title,
                buttons: buttons,
                icon: icon
            );
        }
    }
}
