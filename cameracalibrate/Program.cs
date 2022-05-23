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
            var corners = new List<Mat>();
            var patternSize = new Size(7, 7);
            var objPoints=new List<Mat>();
            var rectangleSize = new Size(21f, 21f);
            var objPoint = new List<Point3d>();
            for(int i=0;i<patternSize.Height;i++)
            {
                for(int j=0;j<patternSize.Width;j++)
                {
                    var p = new Point3d(j * rectangleSize.Width, i * rectangleSize.Height, 0f);
                    objPoint.Add(p);
                    Console.Write(p);
                }
                Console.WriteLine();
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
                var corner = new Mat();
                

                var flags = new ChessboardFlags();
                //キャリブレーションボードに合わせてちゃんとサイズ(交差点？の数)を合わせよう！！ピッタリじゃないと検出できない
                var isfind = Cv2.FindChessboardCorners(gray, patternSize, corner, flags);
                if (isfind)
                {
                    //Cv2.DrawChessboardCorners(gray, new Size(7, 10), corner, isfind);
                    corners.Add(corner);
                    //Cv2.ImShow("output", gray);
                    Console.WriteLine("succeed to find corners!!!!!\n");
                }
                else
                {
                    Console.WriteLine("failed to find corners\n");
                }
                num++;
            }
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