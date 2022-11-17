using System;
using OpenCvSharp.Flann;
using PreProcessImage;
using OpenCvSharp;
using listEnum;

namespace ImageProcess
{
    internal class imgProcess
    {
        // Find Peak Point by Horizontal Direction.
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

        // Find Peak Point by Vertical Direction.
        public static double findPeakPointByY(Mat Linesobel1, Mat Linesobel2, int threshold)
        {
            int n = Linesobel1.Rows;

            List<int> edge_Point = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (Linesobel1.At<double>(i, 0) > threshold)
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
                        if (Linesobel2.At<double>(edge_Point[j], 0) * Linesobel2.At<double>(edge_Point[j] + 1, 0) < 0)
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

        // Simple Draw Line using FitLine.
        public static void DrawFitLine(Mat img, List<Point> listEdge, Scalar color, int thickNess)
        {
            Line2D line = Cv2.FitLine(listEdge, DistanceTypes.Huber, 0, 0.01, 0.01);
            Cv2.Line(img, (int)(line.X1 - line.Vx * img.Cols), (int)(line.Y1 - line.Vy * img.Cols), (int)(line.X1 + line.Vx * img.Cols), (int)(line.Y1 + line.Vy * img.Cols), color, thickNess);
        }


        //Up level of Find Peak Point, Return a list of Peak Point by Direction.
        public static List<Point> getListPoint(Mat img, Direct d, int gap)
        {
            // Matrix using in Method.
            Mat imgDst1 = new Mat();
            Mat imgDst2 = new Mat();
            Mat ImgBlur = new Mat();
            List<Point> listEdge = new List<Point>();

            int width = img.Cols;
            int height = img.Rows;

            ImgBlur = PreProcessImg.getBlurImage(img); // Blur Image using BilateralFilter.
            imgDst1 = PreProcessImg.getSobelImage(ImgBlur, d); // First Oder of Sobel Image.
            imgDst2 = PreProcessImg.getSobelImage(imgDst1, d); // Second Oder of Sobel Image.

            if (d == Direct.LEFTTORIGHT)
            {
                for (int i = 0; i < imgDst1.Rows; i = i + gap)
                {
                    Point EachEdge = new Point();
                    if (imgProcess.findPeakPoint(imgDst1.Row(i), imgDst2.Row(i), 100) != -1)
                    {
                        EachEdge.X = (int)imgProcess.findPeakPoint(imgDst1.Row(i), imgDst2.Row(i), 100);
                        EachEdge.Y = i;
                        Cv2.Circle(img, (int)EachEdge.X, (int)EachEdge.Y, 2, new Scalar(0, 255, 0), Cv2.FILLED);
                        listEdge.Add(EachEdge);
                    }
                }
            }
            else if (d == Direct.RIGHTTOLEFT){

                for (int i = 0; i < imgDst1.Rows; i = i + gap)
                {
                    Point EachEdge = new Point();
                    Cv2.Flip(imgDst1.Row(i), imgDst1.Row(i), FlipMode.Y);
                    Cv2.Flip(imgDst2.Row(i), imgDst2.Row(i), FlipMode.Y);
                    if (imgProcess.findPeakPoint(imgDst1.Row(i), imgDst2.Row(i), 100) != -1)
                    {
                        EachEdge.X = width - (int)imgProcess.findPeakPoint(imgDst1.Row(i), imgDst2.Row(i), 100);
                        EachEdge.Y = i;
                        Cv2.Circle(img, (int)EachEdge.X, (int)EachEdge.Y, 2, new Scalar(0, 255, 0), Cv2.FILLED);
                        listEdge.Add(EachEdge);
                    }
                }
            }

            else if (d == Direct.TOPTOBOTTOM)
            {

                for (int i = 0; i < imgDst1.Cols; i = i + gap)
                {
                    Point EachEdge = new Point();

                    if (imgProcess.findPeakPointByY(imgDst1.Col(i), imgDst2.Col(i), 100) != -1)
                    {
                        EachEdge.Y = (int)imgProcess.findPeakPointByY(imgDst1.Col(i), imgDst2.Col(i), 100);
                        EachEdge.X = i;
                        Cv2.Circle(img, (int)EachEdge.X, (int)EachEdge.Y, 2, new Scalar(0, 255, 0), Cv2.FILLED);
                        listEdge.Add(EachEdge);
                    }
                }
            }

            else if (d == Direct.BOTTOMTOTOP)
            {

                for (int i = 0; i < imgDst1.Cols; i = i + gap)
                {
                    Point EachEdge = new Point();
                    Cv2.Flip(imgDst1.Col(i), imgDst1.Col(i), FlipMode.X);
                    Cv2.Flip(imgDst2.Col(i), imgDst2.Col(i), FlipMode.X);
                    if (imgProcess.findPeakPointByY(imgDst1.Col(i), imgDst2.Col(i), 100) != -1)
                    {
                        EachEdge.Y = height - (int)imgProcess.findPeakPointByY(imgDst1.Col(i), imgDst2.Col(i), 100);
                        EachEdge.X = i;
                        Cv2.Circle(img, (int)EachEdge.X, (int)EachEdge.Y, 2, new Scalar(0, 255, 0), Cv2.FILLED);
                        listEdge.Add(EachEdge);
                    }
                }
            }
            return listEdge;




        }
    }
}

