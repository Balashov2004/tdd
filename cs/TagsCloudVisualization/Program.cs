using TagsCloudVisualization;
using System.Drawing;


public class Program
{
    public static void Main(string[] args)
    {
        
        var runner = new CloudRunner(
            new AppSettings(outputPath: "./results/result7.png",
                wordsFilePath: "./resurses/words2.txt")
        );
        runner.Run();
    }
}