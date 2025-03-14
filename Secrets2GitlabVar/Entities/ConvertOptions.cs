using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets2GitlabVar.Entities
{
    public class ConvertOptions
    {
        public EqualitySymbol EqualitySymbol { get; set; } = EqualitySymbol.Equal;
        public ChildBehaviour ChildBehaviour { get; set; } = ChildBehaviour.Include;
        public ChildSeparator ChildSeparator { get; set; } = ChildSeparator.Dot;
        public ArrayBehaviour ArrayBehaviour { get; set; } = ArrayBehaviour.Include;
        public ArrayBrackets ArrayBrackets { get; set; } = ArrayBrackets.Square;

        public bool SpacesInEqualitySymbol { get; set; } = false;
        public bool TrimProperties { get; set; } = true;
        public bool TrimValues { get; set; } = true;
        public bool IgnorePropertiesWithEmptyValues { get; set; } = true;
    }

    public enum EqualitySymbol
    {
        Equal, Colon, GreaterThan, Arrow
    }

    public enum ChildBehaviour
    {
        Include, Ignore
    }

    public enum ChildSeparator
    {
        Dot, Underscore, Dash, None
    }

    public enum ArrayBehaviour
    {
        Include, Ignore
    }

    public enum ArrayBrackets
    {
        Round, Square, Curly, None
    }
}
