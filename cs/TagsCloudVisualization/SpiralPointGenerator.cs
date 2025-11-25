using System.Drawing;
using System.Collections.Generic;
using System;

namespace TagsCloudVisualization;

public class SpiralPointGenerator
{
    private readonly Point Center;
    private readonly double Density;
    private double Angle = 0;

    public SpiralPointGenerator(Point center, double density)
    {
        this.Center = center;
        this.Density = density;
    }
    
    public IEnumerable<Point> GeneratePoints()
    {
        while (true)
        {
            var radius = Density * Angle;
            var x = (int)Math.Round(Center.X + radius * Math.Cos(Angle));
            var y = (int)Math.Round(Center.Y + radius * Math.Sin(Angle));
            
            Angle += 0.1; 

            yield return new Point(x, y);
        }
    }
}