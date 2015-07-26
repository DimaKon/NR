using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
    class Binarisator
    {
        private Bitmap bits;
        private double lev;

        public Binarisator(Bitmap bits, double lev)
        {
            this.bits = bits;
            this.lev = lev;
        }

        private static Bitmap MakeColor(int[,] minMax, double[,] br, int n, int m, double lev)
        {
            Bitmap returnBits = new Bitmap(n, m);
            int startPos, endPos;
            double med;
            int shift = 0;

            for (int i = 0; i < m; i++)
            {
                startPos = 0;
                for (int j = 1; j < n; j++)
                {
                    if (minMax[i, j] != 0)
                    {
                        endPos = j;
                        med = (br[i, startPos] + br[i, endPos]) * 0.5;
                        for (int k = startPos + shift; k <= endPos; k++)
                        {
                            if ((br[i, k] > med && br[i, k] > lev))
                            {
                                returnBits.SetPixel(k, i, Color.Black);
                                if (k == endPos)
                                    shift = 1;
                                //shift = 0;
                            }
                            else
                            {
                                returnBits.SetPixel(k, i, Color.White);
                                if (k == endPos)
                                    shift = 0;
                            }
                        }
                        startPos = endPos;
                    }
                }
            }

            return returnBits;
        }


        private static int[,] FindExtr(double[,] br, int n, int m)
        {
            int[,] minMax = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                if (br[i, 0] > br[i, 1])
                    minMax[i, 0] = 1;
                else
                    minMax[i, 0] = -1;

                if (br[i, n - 1] > br[i, n - 2])
                    minMax[i, n - 1] = 1;
                else
                    minMax[i, n - 1] = -1;
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 1; j < n - 1; j++)
                {
                    if ((br[i, j] > br[i, j - 1] && br[i, j] > br[i, j + 1]) ||
                        (br[i, j] >= br[i, j - 1] && br[i, j] > br[i, j + 1]) ||
                        (br[i, j] > br[i, j - 1] && br[i, j] >= br[i, j + 1]))
                        minMax[i, j] = 1;

                    else if ((br[i, j] < br[i, j - 1] && br[i, j] < br[i, j + 1]) ||
                             (br[i, j] <= br[i, j - 1] && br[i, j] < br[i, j + 1]) ||
                             (br[i, j] < br[i, j - 1] && br[i, j] <= br[i, j + 1]))
                        minMax[i, j] = -1;

                    else
                        minMax[i, j] = 0;
                }
            }

            return minMax;
        }

        public Bitmap Binarisation()
        {
            int width = bits.Width;
            int height = bits.Height;
            Bitmap returnBits = new Bitmap(width, height);

            double[,] brightness = new double[height, width];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    brightness[j, i] = 0.3 * bits.GetPixel(i, j).R + 0.59 * bits.GetPixel(i, j).G + 0.11 * bits.GetPixel(i, j).B;

            int[,] minMax = FindExtr(brightness, width, height);
            returnBits = MakeColor(minMax, brightness, width, height, lev);

            return returnBits;
        }

        private void RotateImage()
        {
            Bitmap newBitmap = new Bitmap(bits.Height, bits.Width);
            for (int i = 0; i < newBitmap.Height; i++)
                for(int j = 0; j < newBitmap.Width; j++)
                    newBitmap.SetPixel(j, i, bits.GetPixel(i, j));

            bits = newBitmap;
        }

        private Bitmap UnitedImage(Bitmap first, Bitmap second)
        {
            Bitmap returnBitmap = new Bitmap(first.Width, first.Height);
             for (int i = 0; i < first.Width; i++)
                 for (int j = 0; j < first.Height; j++)
                 {
                     if (first.GetPixel(i, j).R + first.GetPixel(i, j).G + first.GetPixel(i, j).B == 0 ||
                         second.GetPixel(j, i).R + second.GetPixel(j, i).G + second.GetPixel(j, i).B == 0)
                         returnBitmap.SetPixel(i,j,Color.Black);
                     else
                         returnBitmap.SetPixel(i, j, Color.White);
                 }
            return returnBitmap;
        }

        public Bitmap DoubleBinarisation()
        {
            Bitmap first = Binarisation();
            RotateImage();
            Bitmap second = Binarisation();
            return UnitedImage(first, second);
        }

        public Bitmap Sobel()
        {
            int[,] gx = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            Bitmap returnBits = new Bitmap(bits.Width, bits.Height);
            int width = bits.Width;
            int height = bits.Height;

            int[,] R = new int[width, height];
            int[,] G = new int[width, height];
            int[,] B = new int[width, height];

            int limit = (int)lev * (int)lev;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    R[i, j] = bits.GetPixel(i, j).R;
                    G[i, j] = bits.GetPixel(i, j).G;
                    B[i, j] = bits.GetPixel(i, j).B;
                }
            }

            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < bits.Width - 1; i++)
            {
                for (int j = 1; j < bits.Height - 1; j++)
                {

                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = R[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = G[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = B[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || 
                        new_gx * new_gx + new_gy * new_gy > limit || 
                        new_bx * new_bx + new_by * new_by > limit)
                        returnBits.SetPixel(i, j, Color.Black);

                    else
                        returnBits.SetPixel(i, j, Color.White);
                }
            }
            return returnBits;            
        }

        public Bitmap Laplas()
        {
            Bitmap returnBits = new Bitmap(bits.Width, bits.Height);
            int width = bits.Width;
            int height = bits.Height;

            int[,] L = {{-1, -1, -1}, {-1, 8, -1}, {-1, -1, -1}};

            int[,] R = new int[width, height];
            int[,] G = new int[width, height];
            int[,] B = new int[width, height];

            int limit = (int)lev;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    R[i, j] = bits.GetPixel(i, j).R;
                    G[i, j] = bits.GetPixel(i, j).G;
                    B[i, j] = bits.GetPixel(i, j).B;
                }
            }




            int new_r = 0, new_ry = 0;
            int new_g = 0, new_gy = 0;
            int new_b = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < bits.Width - 1; i++)
            {
                for (int j = 1; j < bits.Height - 1; j++)
                {

                    new_r = 0;
                    //new_ry = 0;
                    new_g = 0;
                    //new_gy = 0;
                    new_b = 0;
                    //new_by = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = R[i + hw, j + wi];
                            new_r += L[wi + 1, hw + 1] * rc;
                            //new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = G[i + hw, j + wi];
                            new_g += L[wi + 1, hw + 1] * gc;
                            //new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = B[i + hw, j + wi];
                            new_b += L[wi + 1, hw + 1] * bc;
                            //new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_r * new_r + new_ry * new_ry > limit ||
                        new_g * new_g + new_gy * new_gy > limit ||
                        new_b * new_b + new_by * new_by > limit)
                        returnBits.SetPixel(i, j, Color.Black);

                    else
                        returnBits.SetPixel(i, j, Color.White);
                }
            }


            return returnBits;
        }
    }
}
