using System;
using System.Diagnostics;

namespace MemoryManagement
{
    public class Timer : Stopwatch, IDisposable
    {
        public new Timer Start()
        {
            Restart();
            return this;
        }

        public Timer Continue()
        {
            base.Start();
            return this;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                }
                disposedValue = true;
            }
        }

        ~Timer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);           
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
