using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement
{
    public class Timer : IDisposable
    {
        private Stopwatch watch;

        public long ElapsedMilliseconds => watch.ElapsedMilliseconds;

        public Timer Start()
        {
            watch = Stopwatch.StartNew();
            watch.Start();
            return this;
        }

        public Timer Continue()
        {
            watch.Start();
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
                    watch.Stop();
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
