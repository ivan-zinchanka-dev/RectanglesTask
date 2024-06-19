using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Example
    {
        [JsonProperty] private List<Rectangle> _secondaryRectangles = new List<Rectangle>();
        public Rectangle PrimaryRectangle { get; private set; }

        public List<Rectangle> SecondaryRectangles => new List<Rectangle>(_secondaryRectangles);
        

    }
}