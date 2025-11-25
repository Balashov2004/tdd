using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization;

public class CircularCloudLayouter
{
    private readonly Point Center;
    private readonly List<Rectangle> PlacedRectangles = new();
    private readonly SpiralPointGenerator SpiralGenerator;
    private const int Padding = 2;
    
    public CircularCloudLayouter(Point Center)
    {
        this.Center = Center;
        SpiralGenerator = new SpiralPointGenerator(Center, 0.1); 
    }

    public Rectangle GetNextRectangle(Size rectangleSize)
    {
        if (PlacedRectangles.Count == 0)
        {
            var topLeft = CalculateTopLeft(Center, rectangleSize);
            var CenterRect = new Rectangle(topLeft.X, topLeft.Y, rectangleSize.Width, rectangleSize.Height);
            PlacedRectangles.Add(CenterRect);
            return CenterRect;
        }

        foreach (var point in SpiralGenerator.GeneratePoints())
        {
            var topLeft = CalculateTopLeft(point, rectangleSize);
            var potentialRect = new Rectangle(topLeft.X, topLeft.Y, rectangleSize.Width, rectangleSize.Height);

            if (!IsIntersection(potentialRect))
            {
                var compactedRect = CompactRectangle(potentialRect);
                PlacedRectangles.Add(compactedRect);
                return compactedRect;
            }
        }
        
        throw new InvalidOperationException("Не удалось найти место для прямоугольника.");
    }

    private bool IsIntersection(Rectangle newRectangle)
    {
        var rectWithIndentation = AddIndentation(newRectangle, Padding);
        return PlacedRectangles.Any(r => 
            rectWithIndentation.IntersectsWith(AddIndentation(r, Padding)));
    }
    
    private Rectangle AddIndentation(Rectangle rect, int padding)
    {
        return new Rectangle(
            rect.X - padding,
            rect.Y - padding,
            rect.Width + 2 * padding,
            rect.Height + 2 * padding
        );
    }

    private Point CalculateTopLeft(Point targetCenter, Size size)
    {

        var x = targetCenter.X - size.Width / 2;
        var y = targetCenter.Y - size.Height / 2;
        return new Point(x, y);
    }
    
    private Rectangle CompactRectangle(Rectangle rect)
    {
        var currentRect = rect;
        var stepX = currentRect.X + currentRect.Width / 2 < Center.X ? 1 : -1;
        var stepY = currentRect.Y + currentRect.Height / 2 < Center.Y ? 1 : -1;

        while ((currentRect.X + currentRect.Width / 2).CompareTo(Center.X) != stepX)
        {
            var nextRect = new Rectangle(currentRect.X + stepX, currentRect.Y, currentRect.Width, currentRect.Height);
            if (IsIntersection(nextRect)) 
                break;
                
            currentRect = nextRect;
        }
        
        while ((currentRect.Y + currentRect.Height / 2).CompareTo(Center.Y) != stepY)
        {
            var nextRect = new Rectangle(currentRect.X, currentRect.Y + stepY, currentRect.Width, currentRect.Height);
            if (IsIntersection(nextRect))
                break;
                
            currentRect = nextRect;
        }
        
        return currentRect;
    }
}