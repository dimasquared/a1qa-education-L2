using System.Drawing;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Visualization;

namespace Task5Stage2.Utils;

public static class CompareImagesUtil
{
    public static bool CompareImages(string imgPath, string imgUrl)
    {
        using var client = new HttpClient();
        using var response = client.GetAsync(imgUrl).Result;
        using var inputStream = response.Content.ReadAsStream();
        
        var compareResult = AqualityServices.Get<IImageComparator>()
            .PercentageDifference(Image.FromFile(imgPath), Image.FromStream(inputStream));
        return compareResult == 0;
    }
}