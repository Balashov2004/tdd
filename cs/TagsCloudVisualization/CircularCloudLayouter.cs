using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization;

public class CircularCloudLayouter
{
    private readonly Point center;
    private readonly List<Rectangle> placedRectangles = new();
    private readonly SpiralPointGenerator spiral;
    private const int Padding = 2;
    
    public CircularCloudLayouter(Point center)
    {
        this.center = center;
        spiral = new SpiralPointGenerator(center, 0.1); 
    }

    public Rectangle GetNextRectangle(Size rectangleSize)
    {
        if (placedRectangles.Count == 0)
        {
            var topLeft = CalculateTopLeft(center, rectangleSize);
            var centerRect = new Rectangle(topLeft.X, topLeft.Y, rectangleSize.Width, rectangleSize.Height);
            placedRectangles.Add(centerRect);
            return centerRect;
        }

        foreach (var point in spiral.GeneratePoints())
        {
            var topLeft = CalculateTopLeft(point, rectangleSize);
            var potentialRect = new Rectangle(topLeft.X, topLeft.Y, rectangleSize.Width, rectangleSize.Height);

            if (!IsIntersection(potentialRect))
            {
                var compactedRect = CompactRectangle(potentialRect);
                placedRectangles.Add(compactedRect);
                return compactedRect;
            }
        }
        
        throw new InvalidOperationException("Не удалось найти место для прямоугольника.");
    }

    private bool IsIntersection(Rectangle newRectangle)
    {
        var rectWithIndentation = AddIndentation(newRectangle, Padding);
        return placedRectangles.Any(r => 
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
        var stepX = currentRect.X + currentRect.Width / 2 < center.X ? 1 : -1;
        var stepY = currentRect.Y + currentRect.Height / 2 < center.Y ? 1 : -1;

        while ((currentRect.X + currentRect.Width / 2).CompareTo(center.X) != stepX)
        {
            var nextRect = new Rectangle(currentRect.X + stepX, currentRect.Y, currentRect.Width, currentRect.Height);
            if (IsIntersection(nextRect)) 
                break;
                
            currentRect = nextRect;
        }
        
        while ((currentRect.Y + currentRect.Height / 2).CompareTo(center.Y) != stepY)
        {
            var nextRect = new Rectangle(currentRect.X, currentRect.Y + stepY, currentRect.Width, currentRect.Height);
            if (IsIntersection(nextRect))
                break;
                
            currentRect = nextRect;
        }
        
        return currentRect;
    }
}