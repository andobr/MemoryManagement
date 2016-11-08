using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MemoryManagement
{
    class BitmapEditor : IDisposable
    {
        private readonly Bitmap bitmap;

        private readonly BitmapData bitmapData;

        public BitmapEditor(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                ImageLockMode.WriteOnly, bitmap.PixelFormat);          
        }

        public unsafe void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            CheckIndex(x, y);
            var index = (byte*)bitmapData.Scan0 + y * bitmapData.Stride + x * bitmapData.Stride / bitmapData.Width;
            index[0] = b;
            index[1] = g;
            index[2] = r;
        }

        public unsafe Color GetPixel(int x, int y)
        {
            CheckIndex(x, y);           
            var index = (byte*) bitmapData.Scan0 + y * bitmapData.Stride + x * bitmapData.Stride / bitmapData.Width;
            return Color.FromArgb(index[2], index[1], index[0]);
        }

        private void CheckIndex(int x, int y)
        {
            if(x > bitmap.Width || y > bitmap.Height)
                throw new ArgumentException();
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
