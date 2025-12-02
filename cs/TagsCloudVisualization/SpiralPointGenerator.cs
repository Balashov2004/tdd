using System.Drawing;
using System.Collections.Generic;
using System;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualization;

public class SpiralPointGenerator : IPointGenerator
{
    private readonly Point center;
    private readonly double density;
    private double angle;
    
    public Point Center => center;

    public SpiralPointGenerator(Point center, double density)
    {
        this.center = center;
        this.density = density;
    }
    
    public IEnumerable<Point> GeneratePoints()
    {
        while (true)
        {
            var radius = density * angle;
            var x = (int)Math.Round(center.X + radius * Math.Cos(angle));
            var y = (int)Math.Round(center.Y + radius * Math.Sin(angle));
            
            angle += 0.1; 

            yield return new Point(x, y);
        }
    }
}