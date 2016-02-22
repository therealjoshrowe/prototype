using System.Collections.Generic;
using System.Xml.Serialization;

namespace Shared_Code
{
    public class CharactersBlock
    {
        [XmlElement("Sequences")]
        public List<Sequence> sequences = new List<Sequence>();
        [XmlElement("Taxa")]
        public List<string> taxa = new List<string>();
        public int ncharValue;
        public char missingChar;
        public char gapChar;
        public int dataSelection=0;
        public string SequenceChars;
        public string MorphChars;
        public enum InputDataType {
            Sequence=1,
            Morphological=2,
            Protein=3,

        };
    }
}