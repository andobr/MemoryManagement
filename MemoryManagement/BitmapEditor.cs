using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
            CheckIndex(x, y);

            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            unsafe
            {
                var index = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride) + x * bitmapData.Stride / bitmap.Width;
                index[0] = b;
                index[1] = g;
                index[2] = r;
            }
        }

        public Color GetPixel(int x, int y)
        {
            CheckIndex(x, y);

            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            unsafe
            {
                var index = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride) + x * bitmapData.Stride / bitmap.Width;
                return Color.FromArgb(index[2], index[1], index[0]);
            }
        }

        private void CheckIndex(int x, int y)
        {
            if(x >= bitmap.Width || y >= bitmap.Height) throw new ArgumentException();
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
