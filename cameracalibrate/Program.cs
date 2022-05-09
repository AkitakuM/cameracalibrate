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


        }
    }
}