using listEnum;
using NumSharp;
using OpenCvSharp;

namespace PreProcessImage
{
    internal class PreProcessImg
    {
        //Sobel Matric for Left to Right
        public static double[,] dataX = new double[,]{{ -1, 0, 1 }, { -2, 0, 2 },{ -1, 0, 1 }};
        public static InputArray sobelMatrixX = new Mat(3, 3, MatType.CV_64F, dataX);

        //Sobel Matrix for Right to Left
        public static double[,] dataXR = new double[,] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
        public static InputArray sobelMatrixXReverse = new Mat(3, 3, MatType.CV_64F, dataXR);

        //Sobel Matrix for Top to Bottom
        public static double[,] dataY = new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        public static InputArray sobelMatrixY = new Mat(3, 3, MatType.CV_64F, dataY);

        //Sobel Matrix for Bottom to Top
        public static double[,] dataYR = new double[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
        public static InputArray sobelMatrixYReverse = new Mat(3, 3, MatType.CV_64F, dataYR);

        public static Mat getBlurImage(Mat img) // Get blur gray image.
        {
            Mat imgBlur = new Mat();
            Mat imgGray = new Mat();
            Cv2.CvtColor(img, imgGray, ColorConversionCodes.RGB2GRAY);
            Cv2.BilateralFilter(imgGray, imgBlur, 3, 55, 55);
            return imgBlur;
        }



        public static Mat getSobelImage(Mat img, Direct direct) // Get sobel image by direction.
        {
            Mat imgSobel = new Mat();
            if (direct == Direct.LEFTTORIGHT)
            {
                Cv2.Filter2D(img, imgSobel, MatType.CV_64F, sobelMatrixX);
                //Cv2.Filter2D(imgSobel[0], imgSobel[1], MatType.CV_64F, sobelMatrixX);
            }

            else if (direct == Direct.RIGHTTOLEFT)
            {
                Cv2.Filter2D(img, imgSobel, MatType.CV_64F, sobelMatrixXReverse);
                //Cv2.Filter2D(imgSobel[0], imgSobel[1], MatType.CV_64F, sobelMatrixXReverse);
            }
            else if (direct == Direct.TOPTOBOTTOM)
            {
                Cv2.Filter2D(img, imgSobel, MatType.CV_64F, sobelMatrixY);
                //Cv2.Filter2D(imgSobel[0], imgSobel[1], MatType.CV_64F, sobelMatrixY);
            }

            else if (direct == Direct.BOTTOMTOTOP)
            {
                Cv2.Filter2D(img, imgSobel, MatType.CV_64F, sobelMatrixYReverse);
                //Cv2.Filter2D(imgSobel[0], imgSobel[1], MatType.CV_64F, sobelMatrixYReverse);
            }

            return imgSobel;

        }
    }
}
