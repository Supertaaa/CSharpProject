using System;
using OpenCvSharp;
using PreProcessImage;
using listEnum;
using System.Collections.Generic;
using ImageProcess;
using EdgeProperty;

namespace test
{
    class Program
    {
        public static void Main(string[] args)
        {
            Mat img = new Mat("C:\\Users\\Admin\\Downloads\\FindEdge\\FindEdge\\Bracket\\Bracket 2.png");
            Mat imgDst1 = new Mat();
            Mat imgDst2 = new Mat();
            Mat ImgBlur = new Mat();
            List<Point> listEdge = new List<Point>();
            int gap = 20;

            ImgBlur = PreProcessImg.getBlurImage(img);
            imgDst1 = PreProcessImg.getSobelImage(ImgBlur, Direct.LEFTTORIGHT);
            imgDst2 = PreProcessImg.getSobelImage(imgDst1, Direct.LEFTTORIGHT);
            
            for(int i = 0; i < imgDst1.Rows; i = i + gap)
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
            imgProcess.DrawFitLine(img, listEdge, new Scalar(0, 0, 255), 1);
            Cv2.ImShow("Test", img);
            Cv2.WaitKey(0);                      
       
        }
        










    }
}




