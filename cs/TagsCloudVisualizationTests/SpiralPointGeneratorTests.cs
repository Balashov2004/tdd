using NUnit.Framework;
using System.Drawing;
using System.Linq;
using System;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class SpiralPointGeneratorTests
{
    private SpiralPointGenerator spiral;
    private readonly Point center = new Point(500, 500);
    
    [SetUp]
    public void SetUp()
    {
        spiral = new SpiralPointGenerator(center, 0.1);
    }
    
    [Test]
    public void FirstPointInCenter_Test()
    {
        var firstPoint = spiral.GeneratePoints().First();
        var distance = Math.Sqrt(
            Math.Pow(firstPoint.X - center.X, 2) + Math.Pow(firstPoint.Y - center.Y, 2)
        );
        Assert.That(distance, Is.LessThan(1.0));
    }
    
    [Test]
    public void AveragePointsAreNearWithCenter_Test()
    {
        var points = spiral.GeneratePoints().Take(500).ToList();
        
        var avgX = points.Average(p => p.X);
        var avgY = points.Average(p => p.Y);
        
        Assert.That(avgX, Is.InRange(center.X - 5, center.X + 5));
        Assert.That(avgY, Is.InRange(center.Y - 5, center.Y + 5));
    }
}