using System;
using OpenCvSharp;

namespace CameraCalibration
{
    class Program
    {
        public static void Main()
        {
            Program program = new();
            bool isExistImg = true;
            int num = 1;
            var patternSize = new Size(7, 7);
            var corners = new List<List<Point2f>>();
            
            var objPoints=new List<List<Point3f>>();
            var rectangleSize = new Size(21f, 21f);
            var objPoint = new List<Point3f>();
            var imgSize = new Size();
            bool isExistImgSize = false;
            for(int i=0;i<patternSize.Height;i++)
            {
                for(int j=0;j<patternSize.Width;j++)
                {
                    var p = new Point3f(j * rectangleSize.Width, i * rectangleSize.Height, 0f);
                    objPoint.Add(p);
                    //Console.Write(p);//debug
                }
                //Console.WriteLine();//debug
            }
            while (isExistImg)
            {
                var imgPath = program.MakeImagePath(num);
                var img = Cv2.ImRead(imgPath);
                //Cv2.ImShow("sumple", img);
                Console.WriteLine(imgPath);
                if (img.Empty()) break;
                        
                var gray = new Mat();
                Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

                

                var flags = new ChessboardFlags();
                //キャリブレーションボードに合わせてちゃんとサイズ(交差点？の数)を合わせよう！！ピッタリじゃないと検出できない
                var isfind = Cv2.FindChessboardCorners(gray, patternSize, out var corner, flags);
                if (isfind)
                {
                    //Cv2.DrawChessboardCorners(gray, new Size(7, 10), corner, isfind);
                    corners.Add( corner.ToList());
                    //Cv2.ImShow("output", gray);
                    objPoints.Add(objPoint);
                    if(isExistImgSize==false)
                    {
                        imgSize = img.Size();
                        isExistImgSize = true;
                    }
                    Console.WriteLine("succeed to find corners!!!!!\n");
                }
                else
                {
                    Console.WriteLine("failed to find corners\n");
                }
                num++;
            }

            var cameraMatrix = new double[3, 3];
            var distCoeffs = new double[8];
            Vec3d[] rvecs, tvecs;

            Cv2.CalibrateCamera(objPoints, corners, imgSize, cameraMatrix, distCoeffs, out rvecs, out tvecs);

            var optimalCameraMatrix = new double[3, 3];
            double alpha = 1;


            optimalCameraMatrix = Cv2.GetOptimalNewCameraMatrix(cameraMatrix, distCoeffs, imgSize, alpha, imgSize, out var validPixROI);
        }

        public string MakeImagePath(int num)
        {
            var a = num / 100;
            var b = (num % 100) / 10;
            var c = num % 10;
            var imgname = "cb2nd" + a + b + c+".jpg";
            var imgpath = @"D:\picture\cbresize2\" + imgname;
            return imgpath;
        }

        public Mat findcorner(Size size, Mat img)
        {
            var corner = new Mat();
            var flags = new ChessboardFlags();
            
            var isfind = Cv2.FindChessboardCorners(img, size, corner, flags);
            if (isfind)
            {
                Cv2.DrawChessboardCorners(img, size, corner, isfind);
                Cv2.ImShow("output", img);
            }
            else
            {
                Console.WriteLine("failed to find corners");
            }
            Cv2.WaitKey(0);
            return corner;

        }
    }

}