using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization;

public class CreateCloud
{
    private readonly List<Rectangle> Rectangles;
    private readonly Size ImageSize;
    private readonly List<WordData> WordDataList;
    
    public CreateCloud(List<Rectangle> rectangles, Size imageSize, List<WordData> wordDataList)
    {
        this.Rectangles = rectangles;
        this.ImageSize = imageSize;
        this.WordDataList = wordDataList;
    }

    public Bitmap DrawCloud()
    {
        var bitmap = new Bitmap(ImageSize.Width, ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);

        for (int i = 0; i < Rectangles.Count; i++)
        {
            var rect = Rectangles[i];
            var data = WordDataList[i];
            using var brush = new SolidBrush(Color.BurlyWood);
            
            graphics.DrawString(data.Word, data.WordFont, brush, rect.Location);
            graphics.DrawRectangle(Pens.Black, rect);
        }
        
        return bitmap;
    }
    
    public void SaveImage(string path, ImageFormat format)
    {
        using var bitmap = DrawCloud();
        bitmap.Save(path, format);
    }
}