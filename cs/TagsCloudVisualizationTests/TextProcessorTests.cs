using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class TextProcessorTests
{
    private AppSettings settings;
    private const string testFilePath = "C:\\Users\\HP\\Desktop\\911\\tdd\\cs\\TagsCloudVisualizationTests\\resurses\\test_words.txt";
    [SetUp]
    public void SetUp()
    {
        settings = new AppSettings(
            minFontSize: 10, 
            maxFontSize: 20,
            wordsFilePath: testFilePath
        );
    }
    [Test]
    public void Process_CalculatesCorrectFontSize_BasedOnFrequency()
    {
        var processor = new TextProcessor(settings);
        
        processor.Process();
        var catData = processor.ProcessWords.Single(w => w.Word.Equals("cat"));
        var dogData = processor.ProcessWords.Single(w => w.Word.Equals("dog"));
        var appleData = processor.ProcessWords.Single(w => w.Word.Equals("apple"));
        
        Assert.That(appleData.WordFont.Size, Is.EqualTo(10));
        Assert.That(dogData.WordFont.Size, Is.EqualTo(15));
        Assert.That(catData.WordFont.Size, Is.EqualTo(20));
    }
    
}