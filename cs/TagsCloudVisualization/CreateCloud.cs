using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization;

public class CreateCloud
{
    private readonly List<Rectangle> rectangles;
    private readonly Point center;
    private readonly Size imageSize;
    
    public CreateCloud(List<Rectangle> rectangles, Point center, Size imageSize)
    {
        this.rectangles = rectangles;
        this.center = center;
        this.imageSize = imageSize;
    }

    public Bitmap DrawCloud()
    {
        var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);

        foreach (var rect in rectangles)
        {
            using var brush = new SolidBrush(Color.Black);
            graphics.FillRectangle(brush, rect);
            graphics.DrawRectangle(Pens.Green, rect);
        }
        graphics.FillEllipse(Brushes.Red, center.X - 3, center.Y - 3, 6, 6);
        
        return bitmap;
    }
    
    public void SaveImage(string path, ImageFormat format)
    {
        using var bitmap = DrawCloud();
        bitmap.Save(path, format);
    }
}