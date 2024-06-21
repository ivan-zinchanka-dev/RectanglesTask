using System.Collections.Generic;
using Models.Enums;

namespace Models
{
    public struct ResolveSettings
    {
        public bool ExcludeOuterPoints { get; set; }
        public List<ColorType> IncludedColors { get; set; }
    }
}