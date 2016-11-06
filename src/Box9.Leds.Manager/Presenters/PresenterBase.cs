using System;
using Box9.Leds.Core.EventsArguments;

namespace Box9.Leds.Manager.Presenters
{
    public abstract class PresenterBase<T>
    {
        public event EventHandler<EventArgs> IsDirty;
        public event EventHandler<EventArgs> ProgressUpdate;
        public event EventHandler<EventArgs> CancelledPresenting;
        public event EventHandler<T> FinishedPresenting;
        public event EventHandler<ExceptionArgs> ExceptionRaised;

        protected PresenterBase()
        {
            IsDirty += (args, sender) =>
            {
            };

            ExceptionRaised += (s, args) =>
            {
            };

            ProgressUpdate += (s, args) =>
            {
            };

            CancelledPresenting += (s, args) =>
            {
            };

            FinishedPresenting += (s, value) =>
            {
            };
        }

        protected virtual void CancelPresenting()
        {
            CancelledPresenting(null, EventArgs.Empty);
        }

        protected virtual void FinishPresenting(T result)
        {
            FinishedPresenting(null, result);
        }

        protected virtual void MarkAsDirty()
        {
            IsDirty(null, EventArgs.Empty);
        }

        protected virtual void ProgressChanged()
        {
            ProgressUpdate(null, EventArgs.Empty);
        }

        protected void NotifyError(Exception ex)
        {
            ExceptionRaised(null, new ExceptionArgs(ex));
        }
    }

}
