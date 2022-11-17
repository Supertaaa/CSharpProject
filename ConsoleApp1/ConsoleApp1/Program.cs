using System;
using OpenCvSharp;
using PreProcessImage;
using listEnum;
using ImageProcess;


namespace test
{
    class Program
    {
        public static void Main(string[] args)
        {
            Mat img = new Mat("C:\\Users\\Admin\\Downloads\\FindEdge\\FindEdge\\Bracket\\Bracket 2.png");
            List<Point> listEdge = imgProcess.getListPoint(img, Direct.BOTTOMTOTOP, 20);
            imgProcess.DrawFitLine(img, listEdge, new Scalar(0, 0, 255), 1);
            Cv2.ImShow("Test", img);
            Cv2.WaitKey(0);                      
       
        }
        










    }
}




