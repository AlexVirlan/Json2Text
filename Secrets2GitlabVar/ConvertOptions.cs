using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets2GitlabVar
{
    public class ConvertOptions
    {
        public EqualitySymbol EqualitySymbol { get; set; }
        public ChildBehaviour ChildBehaviour { get; set; }
        public ChildSeparator ChildSeparator { get; set; }
        public ArrayBehaviour ArrayBehaviour { get; set; }
        public ArrayBrackets ArrayBrackets { get; set; }

        public bool SpacesInEqualitySymbol { get; set; }
        public bool TrimProperties { get; set; }
        public bool TrimValues { get; set; }
    }

    public enum EqualitySymbol
    {
        Equal, Colon
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
