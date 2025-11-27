using System.Drawing;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualization;

public class TextProcessor
{
    
    private readonly IWorkWithFile workWithFile;
    private readonly AppSettings appSettings;

    public List<WordData> ProcessWords { get; private set; } = new List<WordData>();

    public TextProcessor(IWorkWithFile workWithFile, AppSettings appSettings)
    {
        this.workWithFile = workWithFile;
        this.appSettings = appSettings;
    }

    public void Process()
    {
        var words = GetWords(appSettings.WordsFilePath);
        foreach (var word in words)
        {
            ProcessWords.Add(CreateWordData(word));
        }
    }

    private List<string> GetWords(string path)
    {
        var text = workWithFile.ReadAllText(path);
        var delimiters = new[] { ' ', '\n', ',', '/'};
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    
    private WordData CreateWordData(string word)
    {
        var random = new Random();
        var fontSize = random.Next(appSettings.MinFontSize, appSettings.MaxFontSize);
        using var wordFont = new Font(appSettings.DefaultFontName, fontSize);
        var size = MeasureWordSize(word, wordFont);
        var storedFont = (Font)wordFont.Clone(); 
        
        return new WordData(word, storedFont, size);
    }

    private Size MeasureWordSize(string word, Font font)
    {
        using var tempBitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(tempBitmap);
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        var sizeF = graphics.MeasureString(word, font);
        
        return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
}