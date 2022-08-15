using OpenCvSharp;

namespace Task5Stage2.Utils;

public static class CompareImagesUtil
{
    public static bool CompareImages(string imgPath1, string imgPath2)
    {
        using var src1 = new Mat(Environment.CurrentDirectory + imgPath1);
        using var src2 = new Mat(Environment.CurrentDirectory + imgPath2);

        Resize(src1, src2, out var resizeSrc1,out var resizeSrc2);
        
        var comparedSrc1 = new Mat();
        resizeSrc1.ConvertTo(comparedSrc1, MatType.CV_32F);
        var comparedSrc2 = new Mat();
        resizeSrc2.ConvertTo(comparedSrc2, MatType.CV_32F);

        var compareResult = Cv2.CompareHist(comparedSrc1, comparedSrc2, HistCompMethods.Correl);

        return compareResult >= -1.002;
    }

    private static void Resize(Mat image1, Mat image2, out Mat resizeImg1, out Mat resizeImg2)
    {
        resizeImg1 = image1;
        resizeImg2 = image2;

        if (image1.Size().Height == image2.Size().Height)
        {
            return;
        }
        
        if (image1.Size().Height < image2.Size().Height)
        {
            resizeImg2 = image2.Resize(image1.Size());
        }
        else
        {
            resizeImg1 = image1.Resize(image2.Size());
        }
    }
}