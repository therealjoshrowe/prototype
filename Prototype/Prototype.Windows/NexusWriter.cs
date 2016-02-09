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

            if (! (file == null))
            {
                // Create a file to write to

                List<String> info = new List<String>();
                info.Add("#NEXUS");
                info.Add("BEGIN TAXA;");
                info.Add("Dimensions NTax=\"" + nexusOb.T.taxa.Count + "\"");
                info.Add("TaxLabels ");

                foreach (string taxon in nexusOb.T.taxa)
                {
                    info.Add(taxon + " ");
                }
                info.Add("\n");

                info.Add("END;");

                info.Add("BEGIN CHARACTERS;");
                info.Add(@"DIMENSIONS NChar=""");
                info.Add("MATRIX");
                foreach (Sequence s in App.f.C.sequences)
                {
                    info.Add(s.name + " " + s.characters);
                }
                info.Add(";");
                info.Add("END;");
                await FileIO.WriteLinesAsync(file, info);                   
                
            }
            


            
        }
    }
}
