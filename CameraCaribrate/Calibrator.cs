using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CameraCalibrate
{
    internal class Calibrator
    {
        private double[,] cameraMatrix { get; set; }
        private double[] distCoeffs { get; set; }
        public Calibrator(string CamMatPath, string distCoeffPath)
        {
            cameraMatrix = new double[3, 3];
            distCoeffs = new double[5];

            cameraMatrix = ReadCameraMatrix(CamMatPath);
            distCoeffs = ReadDistCoeffs(distCoeffPath);
        }

        public Mat CalibrateImage(Mat src)
        {
            var dst=new Mat();

            Cv2.Undistort(src, dst, InputArray.Create(cameraMatrix), InputArray.Create(distCoeffs));

            return dst;
        }

        private double[,] ReadCameraMatrix(string matrixPath)
        {
            var cameraMatrix = new double[3, 3];
            using (StreamReader sr = new StreamReader(matrixPath, System.Text.Encoding.UTF32))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        cameraMatrix[i, j] = double.Parse(sr.ReadLine());
                    }
                }
            }
            return cameraMatrix;
        }
        private double[] ReadDistCoeffs(string matrixPath)
        {
            var distDoeffs = new double[5];
            using (StreamReader sr = new StreamReader(matrixPath, System.Text.Encoding.UTF32))
            {
                for (int i = 0; i < 5; i++)
                {
                    distDoeffs[i] = double.Parse(sr.ReadLine());
                }
            }
            return distDoeffs;
        }
    }
}
