using System;
using System.Windows.Threading;

namespace RatinFX.VP.Threading
{
    /// <summary>
    /// Provides Debounce() and Throttle() methods.
    /// Use these methods to ensure that events aren't handled too frequently.
    /// 
    /// Throttle() ensures that events are throttled by the interval specified.
    /// Only the last event in the interval sequence of events fires.
    /// 
    /// Debounce() fires an event only after the specified interval has passed
    /// in which no other pending event has fired. Only the last event in the
    /// sequence is fired.
    /// 
    /// Original source:
    /// https://weblog.west-wind.com/posts/2017/Jul/02/Debouncing-and-Throttling-Dispatcher-Events
    /// </summary>
    public class DebounceDispatcher
    {
        private static DispatcherTimer timer;
        private static DateTime TimerStarted { get; set; } = DateTime.UtcNow.AddYears(-1);
        private static DateTime CurrentTime;

        /// <summary>
        /// This method throttles events by allowing only 1 event to fire for the given
        /// timeout period. Only the last event fired is handled - all others are ignored.
        /// Throttle will fire events every timeout ms even if additional events are pending.
        /// 
        /// Use Throttle where you need to ensure that events fire at given intervals.
        /// </summary>
        /// <param name="interval">Timeout in Milliseconds</param>
        /// <param name="action">Action<object> to fire when debounced event fires</object></param>
        /// <param name="param">optional parameter</param>
        /// <param name="priority">optional priorty for the dispatcher</param>
        /// <param name="disp">optional dispatcher. If not passed or null CurrentDispatcher is used.</param>
        public static void Throttle(Action<object> action, int interval = 250,
            object param = null,
            DispatcherPriority priority = DispatcherPriority.ApplicationIdle,
            Dispatcher disp = null)
        {
            Debounce(action, interval, param, priority, disp, throttle: true);

            TimerStarted = CurrentTime;
        }

        /// <summary>
        /// Debounce an event by resetting the event timeout every time the event is 
        /// fired. The behavior is that the Action passed is fired only after events
        /// stop firing for the given timeout period.
        /// 
        /// Use Debounce when you want events to fire only after events stop firing
        /// after the given interval timeout period.
        /// 
        /// Wrap the logic you would normally use in your event code into
        /// the  Action you pass to this method to debounce the event.
        /// Example: https://gist.github.com/RickStrahl/0519b678f3294e27891f4d4f0608519a
        /// </summary>
        /// <param name="interval">Timeout in Milliseconds</param>
        /// <param name="action">Action<object> to fire when debounced event fires</object></param>
        /// <param name="param">optional parameter</param>
        /// <param name="priority">optional priorty for the dispatcher</param>
        /// <param name="disp">optional dispatcher. If not passed or null CurrentDispatcher is used.</param>
        public static void Debounce(Action<object> action, int interval = 250,
            object param = null,
            DispatcherPriority priority = DispatcherPriority.ApplicationIdle,
            Dispatcher disp = null,
            bool throttle = false)
        {
            // Kill pending timer and pending ticks
            timer?.Stop();
            timer = null;

            if (disp == null)
                disp = Dispatcher.CurrentDispatcher;

            if (throttle)
            {
                CurrentTime = DateTime.UtcNow;
                // If timeout is not up yet - adjust timeout to fire 
                // with potentially new Action parameters           
                if (CurrentTime.Subtract(TimerStarted).TotalMilliseconds < interval)
                    interval -= (int)CurrentTime.Subtract(TimerStarted).TotalMilliseconds;
            }

            // Timer is recreated for each event and effectively
            // resets the timeout. Action only fires after timeout has fully
            // elapsed without other events firing in between
            timer = new DispatcherTimer(TimeSpan.FromMilliseconds(interval), priority, (s, e) =>
            {
                if (timer == null)
                    return;

                timer?.Stop();
                timer = null;
                action.Invoke(param);
            }, disp);

            timer.Start();
        }
    }
}
