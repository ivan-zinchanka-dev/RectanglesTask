using System.Collections.Generic;

namespace Core.Models
{
    public struct ResolveSettings
    {
        public bool ExcludeOuterPoints { get; set; }
        public List<ColorType> IncludedColors { get; set; }
    }
}