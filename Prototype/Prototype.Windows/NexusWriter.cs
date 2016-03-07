using Shared_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using Windows.Foundation;

namespace Prototype
{
    class NexusWriter
    {
        private NexusFile nexusOb;
        public NexusWriter(NexusFile n)
        {
            nexusOb = n;
        }

        public async Task WriteToFile()
        {

            StorageFile file = await DownloadsFolder.CreateFileAsync("output.nex");

            if (!(file == null))
            {
                // Create a file to write to

                List<string> info = PreprareNexusString();
                await FileIO.WriteLinesAsync(file, info);

            }

        }

        public List<string> PreprareNexusString()
        {
            List<String> info = new List<String>();
            info.Add("#NEXUS");
            info.Add("\n");
            info.Add("BEGIN TAXA;");
            info.Add("\n");
            info.Add("Dimensions NTax=\"" + nexusOb.C.taxa.Count + "\"");
            info.Add("\n");
            info.Add("TaxLabels ");
            info.Add("\n");

            foreach (string taxon in nexusOb.C.taxa)
            {
                info.Add(taxon + " ");
                info.Add("\n");
            }
            info.Add("\n");

            info.Add("END;");
            info.Add("\n");

            info.Add("BEGIN CHARACTERS;");
            info.Add("\n");
            info.Add("DIMENSIONS NChar=" + nexusOb.C.ncharValue);
            info.Add("\n");
            info.Add("FORMAT missing=" + nexusOb.C.missingChar + " gap =" + nexusOb.C.gapChar);
            info.Add("\n");
            info.Add("MATRIX");
            info.Add("\n");
            foreach (Sequence s in App.f.C.sequences)
            {
                info.Add(s.name + " " + s.characters);
                info.Add("\n");
            }
            info.Add(";");
            info.Add("\n");
            info.Add("END;");
            return info;
        }
    }
}
