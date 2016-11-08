using System;
using System.Threading;
using System.Drawing;

namespace MemoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer();

            using (timer.Start())
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            using (timer.Continue())
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            using (timer.Start())
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            Console.WriteLine(timer.ElapsedTicks);

            var bitmap = (Bitmap)Image.FromFile(@"C:\Users\Anton\Documents\Visual Studio 2015\Projects\Photoshop\cat.jpg");

            using (timer.Start())
            {
                using (var bitmapEditor = new BitmapEditor(bitmap))
                { 
                    for (int i = 0; i < bitmap.Width; i++)
                        for (int j = 0; j < bitmap.Height; j++)
                            bitmapEditor.SetPixel(i, j, 0, 0, 0);
                }
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            using (timer.Start())
            {
                for (int i = 0; i < bitmap.Width; i++)
                    for (int j = 0; j < bitmap.Height; j++)
                        bitmap.SetPixel(i, j, Color.White);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            Console.Read();
        }
    }
}
