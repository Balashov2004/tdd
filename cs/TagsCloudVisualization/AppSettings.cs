using System.Drawing;

namespace TagsCloudVisualization;

public class AppSettings
{
    private readonly Size defaultImageSize = new Size(1000, 1000);
    private readonly string defaultFontNameValue = "Times New Roman";
    private readonly int defaultMinFontSize = 10;
    private readonly int defaultMaxFontSize = 48;
    private readonly int defaultPadding = 2;
    private readonly double defaultSpiralDensity = 0.1;
    private readonly string defaultImageFormat = "jpg";
    private readonly string defaultOutputPath = "./results/result5.png";
    private readonly string defaultWordsFilePath = "./resurses/Words.txt";
    private readonly Color defaultBackgroundColor = Color.White;
    private readonly Color defaultWordColor = Color.BurlyWood;
    private readonly Color defaultContourColor = Color.Black;
    
    
    public AppSettings(
        Size? imageSize = null,
        string? defaultFontName = null,
        int? minFontSize = null,
        int? maxFontSize = null,
        int? maxWeight = null,
        int? padding = null,
        double? spiralDensity = null,
        string? outputPath = null,
        string? wordsFilePath = null,
        Color? backgroundColor = null,
        Color? wordColors = null,
        Color? contourColor = null)
    {
        ImageSize = imageSize ?? defaultImageSize;
        DefaultFontName = defaultFontName ?? defaultFontNameValue;
        MinFontSize = minFontSize ?? defaultMinFontSize;
        MaxFontSize = maxFontSize ?? defaultMaxFontSize;
        Padding = padding ?? defaultPadding;
        SpiralDensity = spiralDensity ?? defaultSpiralDensity;
        OutputPath = outputPath ?? defaultOutputPath;
        WordsFilePath = wordsFilePath ?? defaultWordsFilePath;
        BackgroundColor = backgroundColor ?? defaultBackgroundColor;
        WordColor = wordColors ?? defaultWordColor;
        ContourColor = contourColor ?? defaultContourColor;
    }
    
    public Size ImageSize { get; }
    public string DefaultFontName { get; }
    public int MinFontSize { get; }
    public int MaxFontSize { get; }
    public int Padding { get; }
    public double SpiralDensity { get; }
    public string OutputPath { get; }
    public string WordsFilePath { get; }
    public Color BackgroundColor { get; }
    public Color WordColor { get; }
    public Color ContourColor { get; }
}