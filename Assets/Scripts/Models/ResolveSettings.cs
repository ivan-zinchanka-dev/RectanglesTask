using System.Collections.Generic;

namespace Models
{
    public struct ResolveSettings
    {
        public bool ExcludeOuterPoints { get; set; }
        public List<ColorType> IncludedColors { get; set; }
    }
}