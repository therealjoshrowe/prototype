using System.Collections.Generic;
using System.Xml.Serialization;

namespace Shared_Code
{
    public class CharactersBlock
    {
        [XmlElement("Sequences")]
        public List<Sequence> sequences = new List<Sequence>();
    }
}