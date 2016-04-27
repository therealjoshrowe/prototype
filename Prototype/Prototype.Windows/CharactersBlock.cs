using System.Collections.Generic;
using System.Xml.Serialization;

namespace Shared_Code
{
    public class CharactersBlock
    {
        //Represents a list of Sequence class, which can hold the Taxa and the Data matrix paired together
        [XmlElement("Sequences")]
        public List<Sequence> sequences = new List<Sequence>();
        //List of all taxa
        [XmlElement("Taxa")]
        public List<string> taxa = new List<string>();
        //Number of characters to input into data matrix
        public int ncharValue;
        //The missing character
        public char missingChar;
        //The Gap character
        public char gapChar;
        //the integer representation of the InputDataType Enum
        public int dataSelection;
        //Flag to indicate if symbols are being used for the Nexus file
        public bool useSymbol;
        //List of symbols being used
        public List<string> symbols;
        //Types of data that can be entered
        public enum InputDataType {
            DNA=1,
            RNA = 2,
            Protein=3,
            Morphological = 4

        };
      
    }
}