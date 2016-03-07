using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Foundation;
using Shared_Code;
using System.Collections.Generic;
using System;

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
            FileSavePicker fp = new FileSavePicker();
            fp.DefaultFileExtension = ".nex";
            fp.SuggestedStartLocation = PickerLocationId.Desktop;
            fp.FileTypeChoices.Add("NEXUS File", new List<string>() { ".nex" });
            // Default file name if the user does not type one in or select a file to replace
            fp.SuggestedFileName = "Output";

            StorageFile file = await fp.PickSaveFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file 
                List<string> info = new List<string>();
                info.Add("#NEXUS");
                info.Add("BEGIN TAXA;");
                info.Add("Dimensions NTax=\"" + nexusOb.C.taxa.Count + "\"");
                info.Add("TaxLabels ");

                foreach (string taxon in nexusOb.C.taxa)
                {
                    info.Add(taxon + " ");
                }
                info.Add("\n");

                info.Add("END;");

                info.Add("BEGIN CHARACTERS;");
                info.Add("DIMENSIONS NChar=" + nexusOb.C.ncharValue);
                info.Add("FORMAT missing=" + nexusOb.C.missingChar + " gap =" + nexusOb.C.gapChar);
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





















