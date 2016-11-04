using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement
{
    class BitmapEditor : IDisposable
    {
        private readonly Bitmap bitmap;

        private BitmapData bitmapData;

        public BitmapEditor(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            bitmapData = bitmap.LockBits(new Rectangle(x, y, 1, 1), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var index = (byte*) bitmapData.Scan0;
                index[0] = b;
                index[1] = g;
                index[2] = r;
            }
        }

        public Color GetPixel(int x, int y)
        {
            bitmapData = bitmap.LockBits(new Rectangle(x, y, 1, 1), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var index = (byte*) bitmapData.Scan0;
                return Color.FromArgb(index[2], index[1], index[0]);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    bitmap.UnlockBits(bitmapData);
                }
                disposedValue = true;
            }
        }

        ~BitmapEditor()
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
