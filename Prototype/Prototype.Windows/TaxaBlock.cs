using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Shared_Code
{
    public class TaxaBlock
    {
       [XmlElement("Taxa")]
       public List<String> taxa = new List<String>();
    }
}