using System;
using OpenCvSharp;

namespace CameraCalibration
{
    class Program
    {
        public static void Main()
        {
            var imgPath= @"D:\picture\chessboardresize\c101681.jpg";
            var img = Cv2.ImRead(imgPath);
            Cv2.ImShow("sumple", img);
            
            var criteria = new TermCriteria(CriteriaTypes.Eps, 30, 0.001);
            var gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
            var corners= new Mat();
            
            var flags = new ChessboardFlags();
            //キャリブレーションボードに合わせてちゃんとサイズ(交差点？の数)を合わせよう！！ピッタリじゃないと検出できない
            var isfind = Cv2.FindChessboardCorners(gray, new Size(7, 10), corners,flags);
            if(isfind)
            {
                Cv2.DrawChessboardCorners(gray, new Size(6, 6), corners, isfind);
                Cv2.ImShow("output", gray);
            }
            else
            {
                Console.WriteLine("failed to find corners");
            }
            Cv2.WaitKey(0);
        }
    }
}