using TagsCloudVisualization;
using System.Drawing;


public class Program
{
    public static void Main(string[] args)
    {
        
        var runner = new CloudRunner(
            new AppSettings(outputPath: "./results/result9.png",
                wordsFilePath: "./resurses/words1.txt")
        );
        runner.Run();
    }
}