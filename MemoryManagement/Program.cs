using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace MemoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();

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


            var bitmap = (Bitmap)Image.FromFile(@"C:\Users\Anton\Documents\Visual Studio 2015\Projects\Photoshop\cat.jpg");

            using (var bitmapEditor = new BitmapEditor(bitmap))
            {
                bitmapEditor.SetPixel(0, 100, 16, 240, 120);            
            }

            using (var bitmapEditor = new BitmapEditor(bitmap))
            {
                Console.WriteLine(bitmapEditor.GetPixel(0, 100));
            }

            Console.Read();
        }
    }
}
