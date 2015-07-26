using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
    class ImageLoader
    {
        public ImageLoader()
        {
            
        }

        public Bitmap LoadPicture(string path)
        {
            Bitmap bits;
            try
            {
                bits = new Bitmap(path);
            }
            catch (Exception)
            {
                return null;
            }
            return bits;
        }

        public string Save(Bitmap bits, string pathOut)
        {
            try
            {
                bits.Save(pathOut);
            }
            catch (ArgumentException)
            {
                return "Неверный путь к итоговому файлу!";
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                return "Придумайте новое имя итоговому файлу!";
            }
            return "";
        }
    }
}
