using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            CheckIndex(x, y);
            var color = new byte[] { b, g, r };
            var index = bitmapData.Scan0 + y * bitmapData.Stride + x * bitmapData.Stride / bitmapData.Width;
            Marshal.Copy(color, 0, index, color.Length);
        }

        public Color GetPixel(int x, int y)
        {
            CheckIndex(x, y);
            var color = new byte[3];
            var index = bitmapData.Scan0 + y * bitmapData.Stride + x * bitmapData.Stride / bitmapData.Width;
            Marshal.Copy(index, color, 0, color.Length);
            return Color.FromArgb(color[2], color[1], color[0]);
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
