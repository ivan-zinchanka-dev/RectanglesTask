namespace Core.Models
{
    public struct Rectangle
    {
        public Point TopLeft { get; private set; }
        public Point BottomRight { get; private set; }

        public Rectangle(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}