using OpenCvSharp;

namespace Task5Stage2.Utils;

public static class CompareImagesUtil
{
    public static bool CompareImages(string imgPath1, string imgPath2)
    {
        using var src1 = new Mat(imgPath1);
        using var src2 = new Mat(imgPath2);
        using var matchResult = new Mat();

        Resize(src1, src2, out var resizeSrc1,out var resizeSrc2);
        
        Cv2.MatchTemplate(resizeSrc2, resizeSrc1, matchResult, TemplateMatchModes.CCoeffNormed);
        Cv2.Threshold(matchResult, matchResult, 0.9, 1.0, ThresholdTypes.Tozero);
        Cv2.MinMaxLoc(matchResult, out var minval, out var maxval, out Point _, out Point _);
        var compareResult = minval;
        
        return compareResult >= 0.992;
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