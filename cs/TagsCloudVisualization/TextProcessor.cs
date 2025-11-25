using System.Drawing;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualization;

public class TextProcessor
{
    
    private readonly IWorkWithFile workWithFile;
    private readonly string FilePath;
    private const string DefaultFont = "Times New Roman";
    private const int MinSize = 10;
    private const int MaxSize = 48;

    public List<WordData> ProcessWords { get; private set; } = new List<WordData>();

    public TextProcessor(IWorkWithFile workWithFile, string FilePath)
    {
        this.workWithFile = workWithFile;
        this.FilePath =  FilePath;
    }

    public void Process()
    {
        var words = GetWords(FilePath);
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
        var fontSize = random.Next(MinSize, MaxSize);
        using var wordFont = new Font(DefaultFont, fontSize);
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