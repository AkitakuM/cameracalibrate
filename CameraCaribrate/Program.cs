using System;
using OpenCvSharp;
using System.IO;

namespace CameraCalibrate

{
    class Program
    {
       
        public static void Main()
        {
           
    
            var cameraMatrix = new double[3, 3];
            var distCoeffs = new double[5];

            string CamMatPath = @"D:\cameracalibrate\cameraMatrix.txt";
            string DistCoeffPath = @"D:\cameracalibrate\distCoeffs.txt";
            var caliblator = new Calibrator(CamMatPath, DistCoeffPath);


            var src = new Mat();
            var dst = new Mat();

            src = Cv2.ImRead(@"D:\picture\cbresize2\cb2nd001.jpg");

            dst = caliblator.CalibrateImage(src);
            
            Cv2.ImShow("calibrated", dst);
            Cv2.WaitKey(0);
        }

        
    }

}
