using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Example
    {
        public Rectangle PrimaryRectangle { get; private set; }
        [JsonProperty] private List<Rectangle> _secondaryRectangles = new List<Rectangle>();
        [JsonIgnore] public List<Rectangle> SecondaryRectangles => new List<Rectangle>(_secondaryRectangles);


        public Example(Rectangle primaryRectangle, List<Rectangle> secondaryRectangles)
        {
            PrimaryRectangle = primaryRectangle;
            _secondaryRectangles = secondaryRectangles;
        }
    }
}