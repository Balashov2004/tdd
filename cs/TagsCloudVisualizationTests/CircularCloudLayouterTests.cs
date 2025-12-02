using System.Drawing;
using TagsCloudVisualization;
using System.Drawing.Imaging;
using FakeItEasy;
using TagsCloudVisualization.Interface;


namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    private AppSettings defaultSettings;
    private List<Rectangle> placesRectangles;
    private Point center;
    
    [SetUp]
    public void SetUp()
    {
        center = new Point(500, 500); 
        defaultSettings = new AppSettings(padding: 0, imageSize: new Size(1000, 1000));
        placesRectangles = new List<Rectangle>();
    }
    
    

    [Test]
    public void FirstRectangleInCenter_Test()
    {
        var customCenter = new Point(100, 60);
        var rectangleSize = new Size(20, 10);
        var fakeGenerator = A.Fake<IPointGenerator>();
        A.CallTo(() => fakeGenerator.Center).Returns(customCenter);
        var layouter = new CircularCloudLayouter(defaultSettings, fakeGenerator);
        
        var rectangle = layouter.PutNextRectangle(rectangleSize);
        var expectedX = customCenter.X - rectangleSize.Width / 2;
        var expectedY = customCenter.Y - rectangleSize.Height / 2;
        placesRectangles.Add(rectangle);
        
        Assert.That(rectangle.X, Is.EqualTo(expectedX));
        Assert.That(rectangle.Y, Is.EqualTo(expectedY));
        Assert.That(rectangle.Width, Is.EqualTo(rectangleSize.Width));
        Assert.That(rectangle.Height, Is.EqualTo(rectangleSize.Height));
    }
    

    [Test]
    public void PutNextRectangle_NoIntersection_Test()
    {
        var settingsWithPadding = new AppSettings(padding: 5);
        var generator = new SpiralPointGenerator(center, settingsWithPadding.SpiralDensity, 1);
        var fakeGenerator = A.Fake<IPointGenerator>();
        A.CallTo(() => fakeGenerator.GeneratePoints()).Returns(generator.GeneratePoints());
        A.CallTo(() => fakeGenerator.Center).Returns(center);
        
        var layouter = new CircularCloudLayouter(settingsWithPadding, fakeGenerator); 
        
        var random = new Random();
        var rectangles = new List<Rectangle>();
        var count = 50;
        
        for (int i = 0; i < count; i++)
        {
            var size = new Size(random.Next(10, 50), random.Next(10, 50));
            var rect = layouter.PutNextRectangle(size);
            rectangles.Add(rect);
            placesRectangles.Add(rect);
        }
        
        for (int i = 0; i < rectangles.Count; i++)
        {
            for (int j = i + 1; j < rectangles.Count; j++)
            {
                var rect1 = rectangles[i];
                var rect2 = rectangles[j];
                
                Assert.That(rect1.IntersectsWith(rect2), Is.False, 
                    $"Прямоугольники пересеклись.");
            }
        }
    }
    
    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            SaveFailedLayout();
        }
    }
    

    private void SaveFailedLayout()
    {
        if (placesRectangles.Count == 0) return;
        var testName = TestContext.CurrentContext.Test.Name;
        var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var projectRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
            "..", "..", "..");
        var outputPath = Path.Combine(projectRootPath, "FailedTestsImages");
        Directory.CreateDirectory(outputPath);
        var fullPath = Path.Combine(outputPath, fileName);
        var dummyWordData = placesRectangles
            .Select(r => new WordData($"Word{r.Width}", new Font("Arial", 12), r.Size)) 
            .ToList();
        var cloudVisualizer = new CreateCloud(placesRectangles, defaultSettings, dummyWordData);
        cloudVisualizer.SaveImage(fullPath, ImageFormat.Png);
        
        TestContext.WriteLine($"Tag cloud visualization saved to file {fullPath}");
    }
}
