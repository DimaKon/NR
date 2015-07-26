using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CircuitFunctionMaker
{
    class Circuit
    {
        private Bitmap bits;
        private List<Pixel> list;
        private double d1, d2, p1, p2;
        private string pathOut;
        private List<List<Pixel>> clustersList = new List<List<Pixel>>();
        private List<int> usefulClusters = new List<int>();
        private Dictionary<int, int> pointerDictionary = new Dictionary<int, int>(); 

        public Bitmap bitsCF;

        private int S, P;

        public Circuit(Bitmap bits, double d1, double d2, double p1, double p2, string pathOut)
        {
            this.bits = bits;
            this.d1 = d1;
            this.d2 = d2;
            this.p1 = p1;
            this.p2 = p2;
            this.pathOut = pathOut;

            bitsCF = bits.Clone() as Bitmap;
        }

        private int recountingClasters(int[,] circuitMatrix, int height, int width, int newClaster, int oldClaster)
        {
            int maxClusterNumber = 0;
            int temp = Math.Max(newClaster, oldClaster);
            newClaster = Math.Min(newClaster, oldClaster);
            oldClaster = temp;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (circuitMatrix[i, j] == oldClaster)
                        circuitMatrix[i, j] = newClaster;
                    if (circuitMatrix[i, j] > maxClusterNumber)
                        maxClusterNumber = circuitMatrix[i, j];
                }
            return maxClusterNumber;
        }

        private List<FunctionPont> FindBasicFunctioin(List<Pixel> list, Pixel centre)
        {
            List<FunctionPont> cuircuitFunction = new List<FunctionPont>();
            List<Pixel>.Enumerator k = list.GetEnumerator();
            double phi;
            while (k.MoveNext())
            {
                phi = 0;
                if (k.Current.Y <= centre.Y && k.Current.X >= centre.X)
                {
                    if (k.Current.Y - centre.Y != 0)
                        phi = Math.Atan((float)Math.Abs(k.Current.X - centre.X) / Math.Abs(k.Current.Y - centre.Y)) * 57.3;
                    else
                        phi = 90;
                }
                else if (k.Current.Y >= centre.Y && k.Current.X >= centre.X)
                {
                    if (k.Current.X - centre.X != 0)
                        phi = Math.Atan((float)Math.Abs(k.Current.Y - centre.Y) / Math.Abs(k.Current.X - centre.X)) * 57.3;
                    else
                        phi = 90;
                    phi += 90;
                }
                else if (k.Current.Y >= centre.Y && k.Current.X <= centre.X)
                {
                    if (k.Current.Y - centre.Y != 0)
                        phi = Math.Atan((float)Math.Abs(k.Current.X - centre.X) / Math.Abs(k.Current.Y - centre.Y)) * 57.3;
                    else
                        phi = 90;
                    phi += 180;
                }
                else if (k.Current.Y <= centre.Y && k.Current.X <= centre.X)
                {
                    if (k.Current.X - centre.X != 0)
                        phi = Math.Atan((float)Math.Abs(k.Current.Y - centre.Y) / Math.Abs(k.Current.X - centre.X)) * 57.3;
                    else
                        phi = 90;
                    phi += 270;
                }
                FunctionPont point = cuircuitFunction.Find(p => p.corner == Math.Round(phi));
                if (point == null)
                    cuircuitFunction.Add(new FunctionPont(Math.Round(phi), new Radius(Math.Abs(k.Current.X - centre.X), Math.Abs(k.Current.Y - centre.Y))));
                else if (point.radius.X * point.radius.X + point.radius.Y * point.radius.Y <
                            (k.Current.X - centre.X)*(k.Current.X - centre.X) + (k.Current.Y - centre.Y) *(k.Current.Y - centre.Y))
                {
                    cuircuitFunction.Remove(point);
                    cuircuitFunction.Add(new FunctionPont(Math.Round(phi), new Radius(Math.Abs(k.Current.X - centre.X), Math.Abs(k.Current.Y - centre.Y))));
                }
            }
            return cuircuitFunction;
        }

        private List<FunctionPont> FindFunctionWithStep(List<FunctionPont> basicFunctions)
        {
            basicFunctions.Add(new FunctionPont(basicFunctions[0].corner + 360, new Radius(basicFunctions[0].radius.X, basicFunctions[0].radius.Y)));
            List<FunctionPont> cuircuitFunction = new List<FunctionPont>();

            cuircuitFunction.Add(new FunctionPont(basicFunctions[0].corner, new Radius(basicFunctions[0].radius.X, basicFunctions[0].radius.Y)));

            for (int i = 1; i < basicFunctions.Count; i++)
            {
                FunctionPont first = basicFunctions[i - 1];
                FunctionPont second = basicFunctions[i];

                double deltaX = second.radius.X - first.radius.X;
                double deltaY = second.radius.Y - first.radius.Y;
                double deltaPhi = (double)(second.corner - first.corner);
                double stepX = deltaX / deltaPhi;
                double stepY = deltaY / deltaPhi;

                while (first.corner != second.corner)
                {
                    first.corner++;
                    if (first.corner >= 360)
                        break;
                    first.radius.X += stepX;
                    first.radius.Y += stepY;
                    cuircuitFunction.Add(new FunctionPont(first.corner, new Radius(first.radius.X, first.radius.Y)));
                }
            }

            return cuircuitFunction;
        }

        private void GetClasters(int[,,] circuitMatrix, Bitmap bits, int clusterNumber)
        {
            int widht = circuitMatrix.GetLength(0);
            int height = circuitMatrix.GetLength(1);
            
            DirectoryInfo dirInfo = new DirectoryInfo(pathOut + "\\Контуры");
            dirInfo.Create();

            foreach (FileInfo file in dirInfo.GetFiles())
                file.Delete();

            DirectoryInfo dirInfoStr = new DirectoryInfo(pathOut + "\\Строки");
            dirInfoStr.Create();

            foreach (FileInfo file in dirInfoStr.GetFiles())
                file.Delete();


            int costil = 0;

            //for (int clasterCount = 2; clasterCount < clusterNumber; clasterCount++)
            for (int clasterCount = 0; clasterCount < usefulClusters.Count; clasterCount++)
            {
                /*list = new List<Pixel>();
                for (int i = 0; i < widht; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (circuitMatrix[i, j] == clasterCount)
                            list.Add(new Pixel(i, j, 0));
                    }
                }*/
                list = clustersList[usefulClusters[clasterCount]-2];

                if (list.Count == 0)
                    continue;

                int minX = list[0].X;
                int maxX = list[0].X;
                foreach (var pix in list)
                {
                    if (pix.X < minX)
                        minX = pix.X;

                    if (pix.X > maxX)
                        maxX = pix.X;
                }

                int minY = list[0].Y;
                int maxY = list[0].Y;
                foreach (var pix in list)
                {
                    if (pix.Y < minY)
                        minY = pix.Y;

                    if (pix.Y > maxY)
                        maxY = pix.Y;
                }

                P = Math.Abs((maxX - minX) + (maxY - minY))*2;

                if (Math.Abs(maxX - minX) > d2 || Math.Abs(maxX - minX) < d1 || Math.Abs(maxY - minY) > d2 || Math.Abs(maxY - minY) < d1 )
                {
                    costil++;
                    continue;
                }

                if ( P > p1 || P < p2)
                    continue;
                S = Math.Abs((maxX - minX) * (maxY - minY));
                
                List<Pixel>.Enumerator k = list.GetEnumerator();
                Pixel centre = new Pixel(0, 0, 0);
                while (k.MoveNext())
                {
                    centre.X += k.Current.X;
                    centre.Y += k.Current.Y;
                }
                centre.X /= list.Count;
                centre.Y /= list.Count;

                //bits.SetPixel(centre.X, centre.Y, Color.Red);

                int num = 0;
                foreach (var pix in list)
                {
                    num++;
                    bits.SetPixel(pix.X, pix.Y, Color.DarkOrange);
                    if (num%4 == 0)
                        bitsCF.SetPixel(pix.X, pix.Y, Color.DarkOrange);
                    else
                        bitsCF.SetPixel(pix.X, pix.Y, Color.White);
                }
                bits.SetPixel(centre.X, centre.Y, Color.Red);

                List<FunctionPont> basicFunctions = FindBasicFunctioin(list, centre);
                basicFunctions.Sort();

                List<FunctionPont> ciurcuitFunction = FindFunctionWithStep(basicFunctions);
                string file = dirInfo.FullName + "\\Контурная функция" + (clasterCount - costil).ToString() + ".txt";

                if (ciurcuitFunction.Count != 360)
                    continue;
                using (StreamWriter record = new StreamWriter(file))
                {
                    for (int n = 0; n < 360; n++)
                        record.Write(
                            "угол: " + ciurcuitFunction[n].corner.ToString() + "; радиус: " +
                            (Math.Sqrt(Math.Pow(ciurcuitFunction[n].radius.X, 2) +
                                       Math.Pow(ciurcuitFunction[n].radius.Y, 2)))
                                .ToString() + "\r\n");
                }

                using (StreamWriter record = new StreamWriter(dirInfoStr.FullName + "\\Строка" + (clasterCount - costil).ToString() + ".txt"))
                {
                    record.Write("КФ: ");
                    for (int n = 0; n < 360; n++)
                        record.Write(
                            (Math.Sqrt(Math.Pow(ciurcuitFunction[n].radius.X, 2) +
                                       Math.Pow(ciurcuitFunction[n].radius.Y, 2)))
                                .ToString() + " ");
                    record.Write("\r\nЦМ: " + centre.X + " " + centre.Y);
                    record.Write("\r\nS: " + S);
                    record.Write("\r\nP: " + P);
                    record.Write("\r\nМестность: " + "<не задано>");
                }
            }
        }
        
        private void Merge(int firstCluster, int secondClaster)
        {
            int max = Math.Max(firstCluster, secondClaster);
            int min = Math.Min(firstCluster, secondClaster);
            if (clustersList[secondClaster - 2] != clustersList[firstCluster - 2])// проверка на то, что списки не указывают на один и тот же
            {
                firstCluster = min;
                secondClaster = max;

                clustersList[firstCluster - 2].AddRange(clustersList[secondClaster - 2]);
                //clustersList[secondClaster - 2].Clear();
                //while (clustersList[secondClaster - 2].Count > 0)
                clustersList[secondClaster - 2].RemoveRange(0, clustersList[secondClaster - 2].Count);
                clustersList[secondClaster - 2] = clustersList[firstCluster - 2];

                usefulClusters.Remove(secondClaster);
            }
        }

        public Bitmap GetFunction()
        {
            int clusterNumber = 2;
            int[,,] circuitMatrix = new int[bits.Width, bits.Height,2];
            for (int i = 0; i < bits.Width; i++)
                for (int j = 0; j < bits.Height; j++)
                {
                    Color pixelColor = bits.GetPixel(i, j);
                    circuitMatrix[i, j, 0] = (pixelColor.R + pixelColor.G + pixelColor.B == 0) ? 1 : 0;
                }

            for (int i = 0; i < bits.Width; i++)
            {
                for (int j = 0; j < bits.Height; j++)
                {
                    //int shift_i = 0, shift_j = 0;
                    if (circuitMatrix[i, j, 0] == 1)
                    {
                        int nearClaster = 0;
                        for (int _i = -1; _i <= 0; _i++)
                            for (int _j = -1; _j <= 1; _j++)
                            {
                                if (_i == 0 && (_j == 0 || _j == 1))
                                    continue;

                                //shift_i = _i;
                                //shift_j = _j;
                                nearClaster = (i + _i < 0 || i + _i >= bits.Width || j + _j < 0 || j + _j >= bits.Height)
                                    ? 0
                                    : Math.Abs(circuitMatrix[i + _i, j + _j, 0]);
                                if (nearClaster > 1)
                                {
                                    if (circuitMatrix[i, j, 0] == 1)
                                    {
                                        circuitMatrix[i, j, 0] = nearClaster;
                                        //добавить в кластер пиксель
                                        clustersList[nearClaster - 2].Add(new Pixel(i, j, 0));
                                    }
                                    else if (Math.Abs(circuitMatrix[i, j, 0]) != nearClaster)
                                    {
                                        int clasterId = Math.Abs(circuitMatrix[i, j, 0]);
                                        int clasterIdSecond = nearClaster;

                                        circuitMatrix[i, j, 0] = -1 * Math.Min(clasterId, clasterIdSecond);
                                        circuitMatrix[i, j, 1] = Math.Max(clasterId, clasterIdSecond);

                                        //слить вместе 2 кластера 
                                        //clustersList[nearClaster - 2].Add(new Pixel(i, j, 0));
                                        Merge(clasterId, nearClaster);
                                        //usefulClusters.Remove(circuitMatrix[i, j, 1]);
                                        //

                                        //clusterNumber++;
                                    }
                                    //clusterNumber = recountingClasters(circuitMatrix, bits.Height, bits.Width, circuitMatrix[i + shift_i, j + shift_j], circuitMatrix[i, j]) + 1;

                                }
                                else //if (circuitMatrix[i, j, 0] == 1)
                                {
                                    continue;  
                                }
                            }
                        if (circuitMatrix[i, j, 0] == 1)
                        {
                            circuitMatrix[i, j, 0] = clusterNumber;
                            //создать новый кластер
                            clustersList.Add(new List<Pixel>());
                            clustersList[clusterNumber - 2].Add(new Pixel(i, j, 0));
                            pointerDictionary.Add(clusterNumber, clusterNumber);
                            usefulClusters.Add(clusterNumber);
                            //
                            clusterNumber++;
                        }
                    }
                }
            }

            GetClasters(circuitMatrix, bits, clusterNumber);


            string test = pathOut + "\\Карта кластеров.txt";
            using (StreamWriter record = new StreamWriter(test))
            {
                for (int i = 0; i < bits.Height; i++)
                {
                    for (int j = 0; j < bits.Width; j++)
                    {
                        if (circuitMatrix[j, i, 0] < 0)
                            record.Write("(" + circuitMatrix[j, i, 0].ToString() +"  "+ circuitMatrix[j, i, 1].ToString()+")");
                        else 
                            record.Write("(  " + circuitMatrix[j, i, 0].ToString() + "  )");
                    }
                    record.Write("\n");
                }
            }


            return bits;
        }
    }
}
