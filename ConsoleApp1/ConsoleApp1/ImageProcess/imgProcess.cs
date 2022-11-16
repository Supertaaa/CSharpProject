using System;
using OpenCvSharp.Flann;
using System.Threading;
using PreProcessImage;
using OpenCvSharp;
using EdgeProperty;

namespace ImageProcess
{
    internal class imgProcess
    {
        public static double findPeakPoint(Mat Linesobel1, Mat Linesobel2, int threshold) {
            int n = Linesobel1.Cols;

            List<int> edge_Point = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (Linesobel1.At<double>(0, i) > threshold)
                {
                    edge_Point.Add(i);
                }
            }

            int index = 0;
            int index_crossing = -1;
            double finalIndex = -1;
            for (int j = 0; j < edge_Point.Count; j++)
            {
                if (index + 1 < edge_Point.Count)
                {
                    if ((index_crossing != -1) & (index_crossing + 1 == index))
                    {
                        
                    }
                    else
                    {
                        if (Linesobel2.At<double>(0, edge_Point[j]) * Linesobel2.At<double>(0, edge_Point[j] + 1) < 0)
                        {
                            index_crossing = index;
                            finalIndex = edge_Point[j];
                            return finalIndex;
                            

                        }
                    }
                }
                index = index + 1;
            }
            return finalIndex;
        }

        public static void DrawFitLine(Mat img, List<Point> listEdge, Scalar color, int thickNess)
        {
            Line2D line = Cv2.FitLine(listEdge, DistanceTypes.Huber, 0, 0.01, 0.01);
            Cv2.Line(img, (int)(line.X1 - line.Vx * img.Cols), (int)(line.Y1 - line.Vy * img.Cols), (int)(line.X1 + line.Vx * img.Cols), (int)(line.Y1 + line.Vy * img.Cols), color, thickNess);
        }
    }
}

