using System;
using OpenCvSharp;

namespace CameraCalibration
{
    class Program
    {
        public void Run()
        {
            var imgPath= @"D:\\picture\\goprochessbord\\c300421";
            var img = new Mat(); 
            img = Cv2.ImRead(imgPath);
            var criteria = new TermCriteria(CriteriaTypes.Eps, 30, 0.001);
            var gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
            var corners= new Mat();
            var corner = new Point2f();
            var flags = new ChessboardFlags();
            Cv2.FindChessboardCorners(gray, new Size(6, 6), corners,flags);
            if(flags)
            {

            }
        }
    }
}