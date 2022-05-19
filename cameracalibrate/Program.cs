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
            int i = 1;
            var corners = new List<Mat>();
            while (isExistImg)
            {
                var imgPath = program.MakeImagePath(i);
                var img = Cv2.ImRead(imgPath);
                //Cv2.ImShow("sumple", img);
                Console.WriteLine(imgPath);
                if (img.Empty()) break;
                        
                var gray = new Mat();
                Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
                var corner = new Mat();
                

                var flags = new ChessboardFlags();
                //キャリブレーションボードに合わせてちゃんとサイズ(交差点？の数)を合わせよう！！ピッタリじゃないと検出できない
                var isfind = Cv2.FindChessboardCorners(gray, new Size(6, 8), corner, flags);
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
                i++;
            }
        }

        public string MakeImagePath(int num)
        {
            var a = num / 100;
            var b = num / 10;
            var c = num % 10;
            var imgname = "cb" + a + b + c+".jpg";
            var imgpath = @"D:\picture\chessboardresize\" + imgname;
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