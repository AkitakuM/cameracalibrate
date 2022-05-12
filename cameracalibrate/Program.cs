using System;
using OpenCvSharp;

namespace CameraCalibration
{
    class Program
    {
        public void Main()
        {
            var imgPath= @"D:\\picture\\goprochessbord\\c300421";
            var img = new Mat(); 
            img = Cv2.ImRead(imgPath);
            var criteria = new TermCriteria(CriteriaTypes.Eps, 30, 0.001);
            var gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
            var corners= new Mat();
            
            var flags = new ChessboardFlags();
            var isfind = Cv2.FindChessboardCorners(gray, new Size(6, 6), corners,flags);
            if(isfind)
            {
                Cv2.DrawChessboardCorners(gray, new Size(6, 6), corners, isfind);
                Cv2.ImShow("sumple", gray);
                Cv2.WaitKey(1);
            }
            else
            {
                Console.WriteLine("failed to find corners");
            }
        }
    }
}