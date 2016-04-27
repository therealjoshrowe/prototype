using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shared_Code
{
    class NexusXMLWriter
    {
        NexusFile n;
        /// <summary>
        /// Constructor: accepts NexusFIle obj
        /// </summary>
        /// <param name="f"></param>
        public NexusXMLWriter(NexusFile f)
        {
            n = f;
        }
        /// <summary>
        /// Serializes NexusFile obj to xml
        /// </summary>
        public void writeXML()
        {
            XmlSerializer ser = new XmlSerializer(typeof(NexusFile));
            string path = "Output.xml";
           // ser.Serialize(writer, n);
        }
    }
}
