using System.Xml.Serialization;

namespace Shared_Code
{
    public class Sequence
    {
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("Characters")]
        public string characters;

        public Sequence(string n, string c)
        {
            name = n;
            characters = c;
        }
    }
}