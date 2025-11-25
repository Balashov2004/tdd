using System.Drawing;
using System.Drawing.Imaging;


namespace TagsCloudVisualization;

public class CloudRunner
{
    private readonly Size ImageSize;
    private readonly Point Center;
    private readonly string OutputPath;
    
    private readonly CircularCloudLayouter Layouter;
    private readonly List<Rectangle> PlaceRectangles;
    private readonly TextProcessor TextProcessor;
    private readonly List<WordData> WordDataList;
    
    public CloudRunner(Size imageSize, string outputPath, string wordsFilePath)
    {
        this.ImageSize = imageSize;
        this.OutputPath = outputPath;
        
        Center = new Point(imageSize.Width / 2, imageSize.Height / 2);
        Layouter = new CircularCloudLayouter(Center);
        
        TextProcessor = new TextProcessor(new FileReader(), wordsFilePath);
        WordDataList = new List<WordData>();
        PlaceRectangles = new List<Rectangle>();
    }

    public void Run()
    {
        TextProcessor.Process();
        var processWords = TextProcessor.ProcessWords;
        
        foreach (var word in processWords)
        {
            var rect = Layouter.GetNextRectangle(word.Size);
            WordDataList.Add(new WordData(word.Word, word.WordFont, word.Size));
            PlaceRectangles.Add(rect);
        }
        var visualizer = new CreateCloud(PlaceRectangles, ImageSize, processWords); 
        
        Console.WriteLine($"Отрисовка и сохранение в {OutputPath}...");
        visualizer.SaveImage(OutputPath, ImageFormat.Png);
        
        Console.WriteLine($"Изображение сохранено в файл: {Path.GetFullPath(OutputPath)}");
    }
}