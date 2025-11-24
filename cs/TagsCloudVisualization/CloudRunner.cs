using System.Drawing;
using System.Drawing.Imaging;


namespace TagsCloudVisualization;

public class CloudRunner
{
    private readonly Size imageSize;
    private readonly Point center;
    private readonly int rectanglesCount;
    private readonly string outputPath;
    
    private readonly CircularCloudLayouter layouter;
    private readonly List<Rectangle> placedRectangles;
    
    public CloudRunner(Size imageSize, int rectanglesCount, string outputPath)
    {
        this.imageSize = imageSize;
        this.rectanglesCount = rectanglesCount;
        this.outputPath = outputPath;
        
        center = new Point(imageSize.Width / 2, imageSize.Height / 2);
        layouter = new CircularCloudLayouter(this.center);
        placedRectangles = new List<Rectangle>();
    }

    public void Run()
    {
        
        
        for (int i = 0; i < rectanglesCount; i++)
        {
            var width = 20;
            var height = 10;
            
            placedRectangles.Add(layouter.GetNextRectangle(new Size(width, height)));
        }
        var visualizer = new CreateCloud(placedRectangles, center, imageSize); 
        
        Console.WriteLine($"Отрисовка и сохранение в {outputPath}...");
        visualizer.SaveImage(outputPath, ImageFormat.Png);
        
        Console.WriteLine($"Изображение сохранено в файл: {Path.GetFullPath(outputPath)}");
    }
}